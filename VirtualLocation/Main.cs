using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using iMobileDevice;
using iMobileDevice.iDevice;
using iMobileDevice.Lockdown;
using iMobileDevice.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VirtualLocation
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class Main : Form
    {
        Form frm;
        bool _Right = false;
        IniFile file;
        LocationService service;
        public Main()
        {
            InitializeComponent();
        }
        public class JsCallback
        {
            private Form ContainerForm { get; set; }


            public JsCallback(Form containerForm)
            {
                ContainerForm = containerForm;
            }
            public void minWin()
            {
                ContainerForm.WindowState = FormWindowState.Minimized;
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text= "VirtualLocation For iOS v"+Program.ver;
            file = new IniFile(@System.Windows.Forms.Application.StartupPath + "\\Config.ini");
            nudLon.Value = decimal.Parse(file.IniReadValue("Location", "Longitude"));
            nudLat.Value = decimal.Parse(file.IniReadValue("Location", "Latitude"));

            txtLocation.Text = "Hi," + Program.User + ".\r\n请选择坐标。";
            NativeLibraries.Load();
            service = LocationService.GetInstance();
            service.PrintMessageEvent = PrintMessage;
            service.ListeningDevice();
            this.webBrowser.ObjectForScripting = this;
            this.webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Navigate(@System.IO.Directory.GetCurrentDirectory() + "\\radar\\index.html");
        }
        public Location Location1 { get; set; } = new Location();
        public Location Location2 { get; set; } = new Location();
        public void position(string lat, string lng, string b_0)
        {
            if (_Right == false)
            {
                if (cbGet.Checked == false)
                {
                    Location1.Longitude = double.Parse(lng) + Convert.ToDouble(nudLon.Value);
                    Location1.Latitude = double.Parse(lat) + Convert.ToDouble(nudLat.Value);

                    txtLocation.Text = "经度：" + Location1.Longitude + "\r\n纬度：" + Location1.Latitude + "\r\n位置：" + b_0;
                    if (cbAuto.Checked)
                    {
                        service.UpdateLocation(Location1);
                    }
                }
                else//精准模式 需调用经纬转换API
                {
                    Location1.Longitude = double.Parse(lng);
                    Location1.Latitude = double.Parse(lat);
                    Debug.WriteLine("经度：" + Location1.Longitude + "纬度：" + Location1.Latitude);
                    Location2 = getLocation("http://api.gpsspg.com/convert/coord/?oid=[oid]&key=[key]&from=3&to=0&latlng=" + Location1.Latitude + "," + Location1.Longitude);
                    txtLocation.Text = "经度：" + Location2.Longitude + "\r\n纬度：" + Location2.Latitude + "\r\n位置：" + b_0;
                    if (cbAuto.Checked)
                    {
                        service.UpdateLocation(Location2);
                    }
                }
            }
            else//偏移校正 需调用经纬转换API
            {
                Location1.Longitude = double.Parse(lng);
                Location1.Latitude = double.Parse(lat);
                Location2 = getLocation("http://api.gpsspg.com/convert/coord/?oid=[oid]&key=[key]&from=3&to=0&latlng=" + Location1.Latitude + "," + Location1.Longitude);
                nudLon.Value = Convert.ToDecimal(Location2.Longitude - Location1.Longitude);
                nudLat.Value = Convert.ToDecimal(Location2.Latitude - Location1.Latitude);
                MessageBox.Show("位置偏移已校准。");
                _Right = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (cbGet.Checked == false)
            {
                service.UpdateLocation(Location1);
            }
            else//精准模式 需调用经纬转换API
            {
                Location2 = getLocation("http://api.gpsspg.com/convert/coord/?oid=[oid]&key=[key]&from=3&to=0&latlng=" + Location1.Latitude + "," + Location1.Longitude);
                Debug.WriteLine("纬度：" + Location2.Latitude + "精度：" + Location2.Longitude);
                service.UpdateLocation(Location2);
            }
        }
        public static Location getLocation(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            string URL = url;
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            myWebClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)");
            byte[] myDataBuffer = myWebClient.DownloadData(URL);
            string SourceCode = Encoding.GetEncoding("utf-8").GetString(myDataBuffer);
            Debug.WriteLine("返回值：" + SourceCode);
            Location Location = new Location();
            var rb = JObject.Parse(SourceCode);
            var result = JObject.Parse(rb["result"].ToString().Replace("[", "").Replace("]", ""));
            Location.Latitude = Convert.ToDouble(result["lat"].ToString());
            Location.Longitude = Convert.ToDouble(result["lng"].ToString());
            Debug.WriteLine("经度：" + result["lat"].ToString());

            return Location;
        }
        public void PrintMessage(string msg)
        {
            if (rtbxLog.InvokeRequired)
            {
                this.Invoke(new Action<string>(PrintMessage), msg);
            }
            else
            {
                rtbxLog.AppendText($"{DateTime.Now.ToString("HH:mm:ss")}：\r\n{msg}\r\n");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            service.ClearLocation();
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            //RoundFormPainter.Paint(sender, e);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void nudLon_ValueChanged(object sender, EventArgs e)
        {
            file.IniWriteValue("Location", "Longitude", nudLon.Value.ToString());
        }

        private void nudLat_ValueChanged(object sender, EventArgs e)
        {
            file.IniWriteValue("Location", "Latitude", nudLat.Value.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _Right = true;
            cbAuto.Checked = false;
            MessageBox.Show("请使用搜索功能或在地图上点击，选择一个离你当前实际位置较近的地点。");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Tag.ToString() == "0")
            {
                webBrowser.Navigate(@System.IO.Directory.GetCurrentDirectory() + "\\map.html");
                button4.Text = "捉妖雷达";
                button4.Tag = 1;
            }
            else
            {
                webBrowser.Navigate(@System.IO.Directory.GetCurrentDirectory() + "\\radar\\index.html");
                button4.Text = "普通地图";
                button4.Tag = 0;
            }
        }
    }
    public class Result
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string match { get; set; }
    }

    public class RootObject
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string count { get; set; }
        public List<Result> result { get; set; }
    }
}
