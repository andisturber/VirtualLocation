using iMobileDevice;
using iMobileDevice.iDevice;
using iMobileDevice.Lockdown;
using iMobileDevice.Plist;
using iMobileDevice.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualLocation
{
    public class LocationService
    {
        private bool d_r_0;
        List<DeviceModel> Devices = new List<DeviceModel>();
        IiDeviceApi iDevice = LibiMobileDevice.Instance.iDevice;
        ILockdownApi lockdown = LibiMobileDevice.Instance.Lockdown;
        IServiceApi service = LibiMobileDevice.Instance.Service;
        private static LocationService _instance;

        public Action<string> PrintMessageEvent = null;
        private LocationService() { }
        public static LocationService GetInstance() => _instance ?? (_instance = new LocationService());

        public void ListeningDevice()
        {
            var num = 0;
            var deviceError = iDevice.idevice_get_device_list(out var devices, ref num);
            if (deviceError != iDeviceError.Success)
            {
                PrintMessage("无法继续.可能本工具权限不足, 或者未正确安装iTunes工具.");
                return;
            }
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    deviceError = iDevice.idevice_get_device_list(out devices, ref num);
                    if (devices.Count > 0)
                    {
                        var lst = Devices.Select(s => s.UDID).ToList().Except(devices).ToList();

                        var dst = devices.Except(Devices.Select(s => s.UDID)).ToList();

                        foreach (string udid in dst)
                        {
                            iDeviceHandle iDeviceHandle;
                            iDevice.idevice_new(out iDeviceHandle, udid).ThrowOnError();
                            LockdownClientHandle lockdownClientHandle;

                            lockdown.lockdownd_client_new_with_handshake(iDeviceHandle, out lockdownClientHandle, "Quamotion").ThrowOnError("无法读取设备Quamotion");

                            lockdown.lockdownd_get_device_name(lockdownClientHandle, out var deviceName).ThrowOnError("获取设备名称失败.");

                            lockdown.lockdownd_client_new_with_handshake(iDeviceHandle, out lockdownClientHandle, "waua").ThrowOnError("无法读取设备waua");

                            lockdown.lockdownd_get_value(lockdownClientHandle, null, "ProductVersion", out var node).ThrowOnError("获取设备系统版本失败.");

                            LibiMobileDevice.Instance.Plist.plist_get_string_val(node, out var version);

                            iDeviceHandle.Dispose();
                            lockdownClientHandle.Dispose();
                            var device = new DeviceModel
                            {
                                UDID = udid,
                                Name = deviceName,
                                Version = version
                            };

                            PrintMessage($"发现设备: {deviceName}  {version}");
                            LoadDevelopmentTool(device);
                            Devices.Add(device);
                        }

                    }
                    else
                    {
                        Devices.ForEach(itm => PrintMessage($"设备 {itm.Name} {itm.Version} 已断开连接."));
                        Devices.Clear();
                    }
                    Thread.Sleep(1000);
                }
            });
        }
        public bool GetDevice()
        {
            Devices.Clear();
            var num = 0;
            iDeviceError iDeviceError = iDevice.idevice_get_device_list(out var readOnlyCollection, ref num);
            if (iDeviceError == iDeviceError.NoDevice)
            {
                return false;
            }
            iDeviceError.ThrowOnError();
            foreach (string udid in readOnlyCollection)
            {
                //iDeviceHandle iDeviceHandle;
                iDevice.idevice_new(out var iDeviceHandle, udid).ThrowOnError();
                //LockdownClientHandle lockdownClientHandle;
                lockdown.lockdownd_client_new_with_handshake(iDeviceHandle, out var lockdownClientHandle, "Quamotion").ThrowOnError();
                //string deviceName;
                lockdown.lockdownd_get_device_name(lockdownClientHandle, out var deviceName).ThrowOnError();
                string version = "";
                PlistHandle node;
                if (lockdown.lockdownd_client_new_with_handshake(iDeviceHandle, out lockdownClientHandle, "waua") == LockdownError.Success && lockdown.lockdownd_get_value(lockdownClientHandle, null, "ProductVersion", out node) == LockdownError.Success)
                {
                    LibiMobileDevice.Instance.Plist.plist_get_string_val(node, out version);
                }
                iDeviceHandle.Dispose();
                lockdownClientHandle.Dispose();
                var device = new DeviceModel
                {
                    UDID = udid,
                    Name = deviceName,
                    Version = version
                };

                PrintMessage($"发现设备: {deviceName}  {version}  {udid}");
                LoadDevelopmentTool(device);
                Devices.Add(device);
            }
            return true;
        }
        /// <summary>
        /// 加载开发者工具
        /// </summary>
        /// <param name="device"></param>
        public void LoadDevelopmentTool(DeviceModel device)
        {
            var shortVersion = string.Join(".", device.Version.Split('.').Take(2));
            PrintMessage($"为设备加载驱动版本 {shortVersion}。");

            var basePath = AppDomain.CurrentDomain.BaseDirectory + "/drivers/";

            if (!File.Exists($"{basePath}{shortVersion}/inject.dmg"))
            {
                PrintMessage($"未找到 {shortVersion} 驱动版本,请下载驱动后重新加载设备。");
                PrintMessage($"请前往：https://pan.baidu.com/s/1MPUTYJTdv7yXEtE8nMIRbQ 提取码：p9ep 按版本自行下载后放入drivers目录。");
                return;
            }
            Process.Start(new ProcessStartInfo
            {
                FileName = "injecttool",
                UseShellExecute = false,
                RedirectStandardOutput = false,
                CreateNoWindow = true,
                Arguments = ".\\drivers\\" + shortVersion + "\\inject.dmg"
            })
            .WaitForExit();
        }
        /// <summary>
        /// 修改定位
        /// </summary>
        /// <param name="location"></param>
        public void UpdateLocation(Location location)
        {
            if (Devices.Count == 0)
            {
                PrintMessage($"修改失败! 未发现任何设备。");
                return;
            }

            iDevice.idevice_set_debug_level(1);

            var Longitude = location.Longitude.ToString();
            var Latitude = location.Latitude.ToString();

            PrintMessage($"尝试修改位置。");
            PrintMessage($"经度:{location.Longitude}");
            PrintMessage($"纬度:{location.Latitude}");

            var size = BitConverter.GetBytes(0u);
            Array.Reverse(size);
            Devices.ForEach(itm =>
            {
                PrintMessage($"开始修改设备 {itm.Name} {itm.Version}");

                var num = 0u;
                iDevice.idevice_new(out var device, itm.UDID);
                lockdown.lockdownd_client_new_with_handshake(device, out var client, "com.alpha.jailout").ThrowOnError();//com.alpha.jailout
                lockdown.lockdownd_start_service(client, "com.apple.dt.simulatelocation", out var service2).ThrowOnError();//com.apple.dt.simulatelocation
                var se = service.service_client_new(device, service2, out var client2);
                // 先置空
                se = service.service_send(client2, size, 4u, ref num);

                num = 0u;
                var bytesLocation = Encoding.ASCII.GetBytes(Latitude);
                size = BitConverter.GetBytes((uint)Latitude.Length);
                Array.Reverse(size);
                se = service.service_send(client2, size, 4u, ref num);
                se = service.service_send(client2, bytesLocation, (uint)bytesLocation.Length, ref num);


                bytesLocation = Encoding.ASCII.GetBytes(Longitude);
                size = BitConverter.GetBytes((uint)Longitude.Length);
                Array.Reverse(size);
                se = service.service_send(client2, size, 4u, ref num);
                se = service.service_send(client2, bytesLocation, (uint)bytesLocation.Length, ref num);



                //device.Dispose();
                //client.Dispose();
                PrintMessage($"设备 {itm.Name} {itm.Version} 位置修改完成。");
            });
        }

        private string u_0;
        //public void ClearLocation()
        //{
        //    try
        //    {
        //        iDeviceHandle handle;
        //        bool flag1 = this.d_r_0;
        //        ILockdownApi lockdown = LibiMobileDevice.Instance.Lockdown;
        //        IServiceApi service = LibiMobileDevice.Instance.Service;
        //        IiDeviceApi iDevice = LibiMobileDevice.Instance.iDevice;
        //        iDevice.idevice_set_debug_level(1);
        //        if (iDevice.idevice_new(out handle, this.u_0) == iDeviceError.NoDevice)
        //        {
        //            PrintMessage($"修改失败! 未发现任何设备。");
        //        }
        //        else
        //        {
        //            LockdownClientHandle handle2;
        //            LockdownServiceDescriptorHandle handle3;
        //            ServiceClientHandle handle4;
        //            PrintMessage($"尝试还原位置。");

        //            lockdown.lockdownd_client_new_with_handshake(handle, out handle2, "com.alpha.jailout");
        //            lockdown.lockdownd_start_service(handle2, "com.apple.dt.simulatelocation", out handle3);
        //            service.service_client_new(handle, handle3, out handle4);
        //            uint sent = 0;
        //            byte[] bytes = BitConverter.GetBytes((uint)1);
        //            if (BitConverter.IsLittleEndian)
        //            {
        //                Array.Reverse(bytes);
        //            }
        //            if (service.service_send(handle4, bytes, (uint)bytes.Length, ref sent) != ServiceError.Success)
        //            {
        //                //this.w_1("WXMrfM3cWqW8QxBrG20bGg==");
        //                PrintMessage($"不知道是啥2。");
        //            }
        //            else
        //            {
        //                iDeviceHandle handle5;
        //                PrintMessage($"貌似还原成功了，请关闭位置服务并重新启用。");
        //                ILockdownApi api3 = LibiMobileDevice.Instance.Lockdown;
        //                IServiceApi api4 = LibiMobileDevice.Instance.Service;
        //                IiDeviceApi api5 = LibiMobileDevice.Instance.iDevice;
        //                api5.idevice_set_debug_level(1);
        //                if (api5.idevice_new(out handle5, null) != iDeviceError.NoDevice)
        //                {
        //                    LockdownClientHandle handle6;
        //                    LockdownServiceDescriptorHandle handle7;
        //                    ServiceClientHandle handle8;
        //                    api3.lockdownd_client_new_with_handshake(handle5, out handle6, "com.alpha.jailout");
        //                    api3.lockdownd_start_service(handle6, "com.apple.dt.simulatelocation", out handle7);
        //                    api4.service_client_new(handle5, handle7, out handle8);
        //                    uint num2 = 0;
        //                    byte[] array = BitConverter.GetBytes((uint)1);
        //                    if (BitConverter.IsLittleEndian)
        //                    {
        //                        Array.Reverse(array);
        //                    }
        //                    api4.service_send(handle8, array, (uint)array.Length, ref num2);
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        //this.w_1("jayIitA/0u6gZNa0qVZHPRcBFjEB\r\nLwOD6cQoFVX6rFcoVjGXFosWhA==");
        //        PrintMessage($"不知道是啥3。");
        //    }
        //    //if (Devices.Count == 0)
        //    //{
        //    //    PrintMessage($"修改失败! 未发现任何设备。");
        //    //    return;
        //    //}

        //    //iDevice.idevice_set_debug_level(1);

        //    //PrintMessage($"尝试还原位置。");

        //    //Devices.ForEach(itm =>
        //    //{
        //    //    PrintMessage($"开始还原设备 {itm.Name} {itm.Version}");
        //    //    var num = 0u;
        //    //    iDevice.idevice_new(out var device, itm.UDID);
        //    //    //lockdown.lockdownd_client_new_with_handshake(device, out LockdownClientHandle client, "com.alpha.jailout").ThrowOnError();//com.alpha.jailout
        //    //    var lockdowndError = lockdown.lockdownd_client_new_with_handshake(device, out LockdownClientHandle client, "com.alpha.jailout");//com.alpha.jailout
        //    //    lockdown.lockdownd_start_service(client, "com.apple.dt.simulatelocation", out var service2).ThrowOnError();//com.apple.dt.simulatelocation
        //    //    var se = service.service_client_new(device, service2, out var client2);

        //    //    se = service.service_send(client2, new byte[4] { 0, 0, 0, 0 }, 4u, ref num);
        //    //    se = service.service_send(client2, new byte[4] { 0, 0, 0, 1 }, 4u, ref num);

        //    //    device.Dispose();
        //    //    client.Dispose();
        //    //    PrintMessage($"设备 {itm.Name} {itm.Version} 位置还原成功。");
        //    //});
        //}
        /// <summary>
        /// 输出日志消息
        /// </summary>
        /// <param name="msg"></param>
        public void ClearLocation()
        {
            if (Devices.Count == 0)
            {
                PrintMessage($"修改失败! 未发现任何设备.");
                return;
            }

            iDevice.idevice_set_debug_level(1);

            PrintMessage($"发起还原位置.");

            Devices.ForEach(itm =>
                    {
                        PrintMessage($"开始还原设备 {itm.Name} {itm.Version}");
                        var num = 0u;
                        iDevice.idevice_new(out var device, itm.UDID);
                        var lockdowndError = lockdown.lockdownd_client_new_with_handshake(device, out LockdownClientHandle client, "com.alpha.jailout");//com.alpha.jailout
                        lockdowndError = lockdown.lockdownd_start_service(client, "com.apple.dt.simulatelocation", out var service2);//com.apple.dt.simulatelocation
                        var se = service.service_client_new(device, service2, out var client2);

                        se = service.service_send(client2, new byte[4] { 0, 0, 0, 0 }, 4, ref num);
                        se = service.service_send(client2, new byte[4] { 0, 0, 0, 1 }, 4, ref num);

                        device.Dispose();
                        client.Dispose();
                        PrintMessage($"设备 {itm.Name} {itm.Version} 还原成功.");
                    });
        }
        
        /// <summary>
        /// 输出日志消息
        /// </summary>
        /// <param name="msg"></param>

        public void PrintMessage(string msg)
        {
            PrintMessageEvent?.Invoke(msg);
        }
    }

    public class DeviceModel
    {
        public string UDID { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
    }

    public class Location
    {
        public Location()
        {

        }
        public Location(double lo, double la)
        {
            Longitude = lo; Latitude = la;
        }
        public Location(string location)
        {
            var arry = location.Split(',');
            Longitude = double.Parse(arry[0]);
            Latitude = double.Parse(arry[1]);
        }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
    }
}
