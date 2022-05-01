using Loatheb.winforms;
namespace Loatheb
{
    partial class LoathebForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus2 = new Loatheb.winforms.BindableToolStripStatusLabel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnTryResetUI = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.imgDebug = new System.Windows.Forms.PictureBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTakeSS = new System.Windows.Forms.Button();
            this.refreshLAWindowLocBtn = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgDebug)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.label1,
            this.lblStatus2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 689);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.Text = "Running:";
            // 
            // lblStatus2
            // 
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(165, 17);
            this.lblStatus2.Text = "bindableToolStripStatusLabel1";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Controls.Add(this.tabPage5);
            this.tabs.Controls.Add(this.tabPage3);
            this.tabs.Controls.Add(this.tabPage4);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(984, 689);
            this.tabs.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(976, 661);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Grinding";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(87, 6);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(976, 661);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fishing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(976, 661);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Collect";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(976, 661);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnTryResetUI);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.refreshLAWindowLocBtn);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(976, 661);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Debug";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnTryResetUI
            // 
            this.btnTryResetUI.Location = new System.Drawing.Point(3, 32);
            this.btnTryResetUI.Name = "btnTryResetUI";
            this.btnTryResetUI.Size = new System.Drawing.Size(144, 23);
            this.btnTryResetUI.TabIndex = 2;
            this.btnTryResetUI.Text = "Try resetting UI";
            this.btnTryResetUI.UseVisualStyleBackColor = true;
            this.btnTryResetUI.Click += new System.EventHandler(this.btnTryResetUI_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.imgDebug);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtY);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnTakeSS);
            this.groupBox1.Location = new System.Drawing.Point(183, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 499);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Take game window screenshot";
            // 
            // imgDebug
            // 
            this.imgDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgDebug.BackColor = System.Drawing.Color.CadetBlue;
            this.imgDebug.Location = new System.Drawing.Point(6, 51);
            this.imgDebug.Name = "imgDebug";
            this.imgDebug.Size = new System.Drawing.Size(778, 442);
            this.imgDebug.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgDebug.TabIndex = 10;
            this.imgDebug.TabStop = false;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(419, 21);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(37, 23);
            this.txtHeight.TabIndex = 9;
            this.txtHeight.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(371, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Height:";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(310, 22);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(37, 23);
            this.txtWidth.TabIndex = 7;
            this.txtWidth.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(262, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Width:";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(205, 22);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(37, 23);
            this.txtY.TabIndex = 5;
            this.txtY.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(182, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 3;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(129, 22);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(37, 23);
            this.txtX.TabIndex = 2;
            this.txtX.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "X:";
            // 
            // btnTakeSS
            // 
            this.btnTakeSS.Location = new System.Drawing.Point(6, 22);
            this.btnTakeSS.Name = "btnTakeSS";
            this.btnTakeSS.Size = new System.Drawing.Size(75, 23);
            this.btnTakeSS.TabIndex = 0;
            this.btnTakeSS.Text = "Screenshot";
            this.btnTakeSS.UseVisualStyleBackColor = true;
            this.btnTakeSS.Click += new System.EventHandler(this.btnTakeSS_Click);
            // 
            // refreshLAWindowLocBtn
            // 
            this.refreshLAWindowLocBtn.Location = new System.Drawing.Point(3, 3);
            this.refreshLAWindowLocBtn.Name = "refreshLAWindowLocBtn";
            this.refreshLAWindowLocBtn.Size = new System.Drawing.Size(144, 23);
            this.refreshLAWindowLocBtn.TabIndex = 0;
            this.refreshLAWindowLocBtn.Text = "Refresh screen location";
            this.refreshLAWindowLocBtn.UseVisualStyleBackColor = true;
            this.refreshLAWindowLocBtn.Click += new System.EventHandler(this.refreshLAWindowLocBtn_Click);
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logBox.Location = new System.Drawing.Point(0, 542);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.PlaceholderText = "Initializing...";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.ShortcutsEnabled = false;
            this.logBox.Size = new System.Drawing.Size(984, 147);
            this.logBox.TabIndex = 4;
            this.logBox.Text = "Hello\r\nThere\r\nGeneral\r\nKenobi!";
            // 
            // LoathebForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 711);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 640);
            this.Name = "LoathebForm";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgDebug)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel label1;
        private TabControl tabs;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TextBox logBox;
        private TabPage tabPage5;
        private Button refreshLAWindowLocBtn;
        private Button btnStop;
        private Button btnStart;
        private GroupBox groupBox1;
        private TextBox txtWidth;
        private Label label5;
        private TextBox txtY;
        private Label label4;
        private Label label3;
        private TextBox txtX;
        private Label label2;
        private Button btnTakeSS;
        private TextBox txtHeight;
        private Label label6;
        private PictureBox imgDebug;
        private Button btnTryResetUI;
        private BindableToolStripStatusLabel lblStatus2;
    }
}
