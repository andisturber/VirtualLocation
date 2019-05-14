using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationCleaned
{
    [ComVisible(true)]
    public partial class frmMap : Form
    {
        /// <summary>
        /// 经纬度坐标
        /// </summary>
        public Location Location { get; set; } = new Location();
        public frmMap()
        {
            InitializeComponent();
        }

        private void frmMap_Load(object sender, EventArgs e)
        {
            this.webBrowser1.ObjectForScripting = this;
            this.webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(@System.IO.Directory.GetCurrentDirectory()+"\\map.html");

        }
        public void position(string a_0, string a_1, string b_0)
        {
            this.label3.Text = (double.Parse( a_1) - 0.0125).ToString();
            this.label4.Text = (double.Parse(a_0) - 0.00736).ToString();
            this.label5.Text = b_0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Location.Longitude = double.Parse(label3.Text);
            Location.Latitude = double.Parse(label4.Text);
            Close();
        }
        public void Alert(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
