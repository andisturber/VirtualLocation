using System.Windows.Forms;

namespace VirtualLocation
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.button2 = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.cbAuto = new System.Windows.Forms.CheckBox();
            this.rtbxLog = new System.Windows.Forms.TextBox();
            this.nudLon = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudLat = new System.Windows.Forms.NumericUpDown();
            this.cbGet = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudLon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLat)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(0, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 37);
            this.button2.TabIndex = 1;
            this.button2.Text = "修改定位";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReset.Enabled = false;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.Location = new System.Drawing.Point(134, 105);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(131, 37);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "还原定位";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLocation.Location = new System.Drawing.Point(0, 0);
            this.txtLocation.Multiline = true;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(265, 99);
            this.txtLocation.TabIndex = 6;
            this.txtLocation.Text = "请选择位置";
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(271, 40);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(712, 518);
            this.webBrowser.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            // 
            // cbAuto
            // 
            this.cbAuto.AutoSize = true;
            this.cbAuto.BackColor = System.Drawing.Color.Transparent;
            this.cbAuto.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbAuto.ForeColor = System.Drawing.Color.Black;
            this.cbAuto.Location = new System.Drawing.Point(696, 10);
            this.cbAuto.Name = "cbAuto";
            this.cbAuto.Size = new System.Drawing.Size(93, 25);
            this.cbAuto.TabIndex = 8;
            this.cbAuto.Text = "实时模式";
            this.cbAuto.UseVisualStyleBackColor = false;
            // 
            // rtbxLog
            // 
            this.rtbxLog.BackColor = System.Drawing.SystemColors.Control;
            this.rtbxLog.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.rtbxLog.Location = new System.Drawing.Point(0, 148);
            this.rtbxLog.Multiline = true;
            this.rtbxLog.Name = "rtbxLog";
            this.rtbxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rtbxLog.Size = new System.Drawing.Size(265, 412);
            this.rtbxLog.TabIndex = 9;
            // 
            // nudLon
            // 
            this.nudLon.DecimalPlaces = 10;
            this.nudLon.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudLon.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudLon.Location = new System.Drawing.Point(393, 9);
            this.nudLon.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudLon.Name = "nudLon";
            this.nudLon.ReadOnly = true;
            this.nudLon.Size = new System.Drawing.Size(119, 26);
            this.nudLon.TabIndex = 11;
            this.nudLon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudLon.ValueChanged += new System.EventHandler(this.nudLon_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(271, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "位置偏移：经度                            纬度";
            // 
            // nudLat
            // 
            this.nudLat.DecimalPlaces = 10;
            this.nudLat.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudLat.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudLat.Location = new System.Drawing.Point(571, 9);
            this.nudLat.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudLat.Name = "nudLat";
            this.nudLat.ReadOnly = true;
            this.nudLat.Size = new System.Drawing.Size(119, 26);
            this.nudLat.TabIndex = 11;
            this.nudLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudLat.ValueChanged += new System.EventHandler(this.nudLat_ValueChanged);
            // 
            // cbGet
            // 
            this.cbGet.AutoSize = true;
            this.cbGet.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbGet.Location = new System.Drawing.Point(172, 74);
            this.cbGet.Name = "cbGet";
            this.cbGet.Size = new System.Drawing.Size(93, 25);
            this.cbGet.TabIndex = 13;
            this.cbGet.Text = "精准模式";
            this.cbGet.UseVisualStyleBackColor = true;
            this.cbGet.Visible = false;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(792, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 27);
            this.button3.TabIndex = 14;
            this.button3.Text = "偏移校正";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button4.Location = new System.Drawing.Point(888, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 27);
            this.button4.TabIndex = 14;
            this.button4.Tag = "0";
            this.button4.Text = "普通地图";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cbGet);
            this.Controls.Add(this.nudLat);
            this.Controls.Add(this.nudLon);
            this.Controls.Add(this.rtbxLog);
            this.Controls.Add(this.cbAuto);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VirtualLocation For iOS";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.nudLon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbAuto;
        private System.Windows.Forms.TextBox rtbxLog;
        private NumericUpDown nudLon;
        private Label label1;
        private NumericUpDown nudLat;
        private CheckBox cbGet;
        private Button button3;
        private Button button4;
    }
}

