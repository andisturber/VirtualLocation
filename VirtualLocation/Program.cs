using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualLocation
{
    static class Program
    {
        public static string User = "";
        public static double ver = 2.00;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
        Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Main());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"软件发生异常! {ex.Message}");
            }
        }
    }
}
