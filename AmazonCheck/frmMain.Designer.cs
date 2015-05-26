namespace AmazonCheck
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnStart = new System.Windows.Forms.Button();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.webBwmain = new System.Windows.Forms.WebBrowser();
            this.progressCtrl = new System.Windows.Forms.ProgressBar();
            this.resultTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSettingUrl = new System.Windows.Forms.Label();
            this.btnUserOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoKindCA = new System.Windows.Forms.RadioButton();
            this.rdoKindFR = new System.Windows.Forms.RadioButton();
            this.rdoKindUK = new System.Windows.Forms.RadioButton();
            this.rdoKindUS = new System.Windows.Forms.RadioButton();
            this.txtProxyServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenProxy = new System.Windows.Forms.Button();
            this.lblProxysUrl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(112, 173);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 36);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // textBox_id
            // 
            this.textBox_id.Enabled = false;
            this.textBox_id.Location = new System.Drawing.Point(77, 323);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(137, 20);
            this.textBox_id.TabIndex = 4;
            // 
            // textBox_password
            // 
            this.textBox_password.Enabled = false;
            this.textBox_password.Location = new System.Drawing.Point(280, 321);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(134, 20);
            this.textBox_password.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 327);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 327);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Password:";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(261, 173);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(77, 36);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // webBwmain
            // 
            this.webBwmain.Location = new System.Drawing.Point(460, 12);
            this.webBwmain.Name = "webBwmain";
            this.webBwmain.Size = new System.Drawing.Size(485, 612);
            this.webBwmain.TabIndex = 12;
            // 
            // progressCtrl
            // 
            this.progressCtrl.Location = new System.Drawing.Point(165, 229);
            this.progressCtrl.Name = "progressCtrl";
            this.progressCtrl.Size = new System.Drawing.Size(238, 20);
            this.progressCtrl.TabIndex = 13;
            // 
            // resultTextBox
            // 
            this.resultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resultTextBox.Enabled = false;
            this.resultTextBox.Location = new System.Drawing.Point(56, 367);
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.Size = new System.Drawing.Size(358, 330);
            this.resultTextBox.TabIndex = 15;
            this.resultTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Scraping Progress";
            // 
            // lblSettingUrl
            // 
            this.lblSettingUrl.AutoSize = true;
            this.lblSettingUrl.Location = new System.Drawing.Point(171, 22);
            this.lblSettingUrl.Name = "lblSettingUrl";
            this.lblSettingUrl.Size = new System.Drawing.Size(10, 13);
            this.lblSettingUrl.TabIndex = 18;
            this.lblSettingUrl.Text = "-";
            // 
            // btnUserOpen
            // 
            this.btnUserOpen.Location = new System.Drawing.Point(35, 14);
            this.btnUserOpen.Name = "btnUserOpen";
            this.btnUserOpen.Size = new System.Drawing.Size(120, 29);
            this.btnUserOpen.TabIndex = 19;
            this.btnUserOpen.Text = "Import Userinfo Txt..";
            this.btnUserOpen.UseVisualStyleBackColor = true;
            this.btnUserOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoKindCA);
            this.groupBox1.Controls.Add(this.rdoKindFR);
            this.groupBox1.Controls.Add(this.rdoKindUK);
            this.groupBox1.Controls.Add(this.rdoKindUS);
            this.groupBox1.Location = new System.Drawing.Point(35, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 49);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Country";
            // 
            // rdoKindCA
            // 
            this.rdoKindCA.AutoSize = true;
            this.rdoKindCA.Location = new System.Drawing.Point(279, 19);
            this.rdoKindCA.Name = "rdoKindCA";
            this.rdoKindCA.Size = new System.Drawing.Size(39, 17);
            this.rdoKindCA.TabIndex = 3;
            this.rdoKindCA.TabStop = true;
            this.rdoKindCA.Text = "CA";
            this.rdoKindCA.UseVisualStyleBackColor = true;
            // 
            // rdoKindFR
            // 
            this.rdoKindFR.AutoSize = true;
            this.rdoKindFR.Location = new System.Drawing.Point(195, 19);
            this.rdoKindFR.Name = "rdoKindFR";
            this.rdoKindFR.Size = new System.Drawing.Size(39, 17);
            this.rdoKindFR.TabIndex = 2;
            this.rdoKindFR.TabStop = true;
            this.rdoKindFR.Text = "FR";
            this.rdoKindFR.UseVisualStyleBackColor = true;
            // 
            // rdoKindUK
            // 
            this.rdoKindUK.AutoSize = true;
            this.rdoKindUK.Location = new System.Drawing.Point(117, 20);
            this.rdoKindUK.Name = "rdoKindUK";
            this.rdoKindUK.Size = new System.Drawing.Size(40, 17);
            this.rdoKindUK.TabIndex = 1;
            this.rdoKindUK.Text = "UK";
            this.rdoKindUK.UseVisualStyleBackColor = true;
            // 
            // rdoKindUS
            // 
            this.rdoKindUS.AutoSize = true;
            this.rdoKindUS.Checked = true;
            this.rdoKindUS.Location = new System.Drawing.Point(42, 20);
            this.rdoKindUS.Name = "rdoKindUS";
            this.rdoKindUS.Size = new System.Drawing.Size(40, 17);
            this.rdoKindUS.TabIndex = 0;
            this.rdoKindUS.TabStop = true;
            this.rdoKindUS.Text = "US";
            this.rdoKindUS.UseVisualStyleBackColor = true;
            // 
            // txtProxyServer
            // 
            this.txtProxyServer.Enabled = false;
            this.txtProxyServer.Location = new System.Drawing.Point(123, 277);
            this.txtProxyServer.Name = "txtProxyServer";
            this.txtProxyServer.Size = new System.Drawing.Size(153, 20);
            this.txtProxyServer.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Proxy Server";
            // 
            // btnOpenProxy
            // 
            this.btnOpenProxy.Location = new System.Drawing.Point(35, 58);
            this.btnOpenProxy.Name = "btnOpenProxy";
            this.btnOpenProxy.Size = new System.Drawing.Size(120, 27);
            this.btnOpenProxy.TabIndex = 25;
            this.btnOpenProxy.Text = "Import Proxy Txt";
            this.btnOpenProxy.UseVisualStyleBackColor = true;
            this.btnOpenProxy.Click += new System.EventHandler(this.btnOpenProxy_Click);
            // 
            // lblProxysUrl
            // 
            this.lblProxysUrl.AutoSize = true;
            this.lblProxysUrl.Location = new System.Drawing.Point(171, 63);
            this.lblProxysUrl.Name = "lblProxysUrl";
            this.lblProxysUrl.Size = new System.Drawing.Size(10, 13);
            this.lblProxysUrl.TabIndex = 26;
            this.lblProxysUrl.Text = "-";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 747);
            this.Controls.Add(this.lblProxysUrl);
            this.Controls.Add(this.btnOpenProxy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProxyServer);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnUserOpen);
            this.Controls.Add(this.lblSettingUrl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.progressCtrl);
            this.Controls.Add(this.webBwmain);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_id);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Simplicity Amazon Checker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.WebBrowser webBwmain;
        private System.Windows.Forms.ProgressBar progressCtrl;
        private System.Windows.Forms.RichTextBox resultTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSettingUrl;
        private System.Windows.Forms.Button btnUserOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoKindUK;
        private System.Windows.Forms.RadioButton rdoKindUS;
        private System.Windows.Forms.TextBox txtProxyServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpenProxy;
        private System.Windows.Forms.Label lblProxysUrl;
        private System.Windows.Forms.RadioButton rdoKindCA;
        private System.Windows.Forms.RadioButton rdoKindFR;

        public System.Windows.Forms.WebBrowserDocumentCompletedEventHandler webBwmain_DocumentCompleted { get; set; }
    }
}

