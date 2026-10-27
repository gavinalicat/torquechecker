using System.ComponentModel;
using System.Windows.Forms;

namespace SerialTFT
{
	partial class Test
	{
		private IContainer components = null;

		private Timer timer1;

		private Button button2;

		private Button button3;

		private NumericUpDown TimesUp;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem File;

		private StatusStrip statusStrip1;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panel1;

		private Button button1;

		private NumericUpDown TimesDown;

		private Panel panel4;

		private GroupBox groupBox1;

		private ProgressBar progressBar1;

		private Button button4;

		private GroupBox groupBox2;

		private Label label3;

		private Button button5;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private ToolStripStatusLabel toolStripStatusLabel2;

		private Label label1;

		private ToolStripMenuItem openToolStripMenuItem;

		private ToolStripMenuItem exitToolStripMenuItem;

		private Button button6;

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialTFT.Test));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.TimesUp = new System.Windows.Forms.NumericUpDown();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.File = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.TimesDown = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.button6 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)this.TimesUp).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.TimesDown).BeginInit();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.timer1.Tick += new System.EventHandler(timer1_Tick);
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(button2_Click);
			resources.ApplyResources(this.button3, "button3");
			this.button3.Name = "button3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(button3_Click);
			resources.ApplyResources(this.TimesUp, "TimesUp");
			this.TimesUp.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
			this.TimesUp.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
			this.TimesUp.Name = "TimesUp";
			this.TimesUp.Value = new decimal(new int[4] { 1, 0, 0, 0 });
			resources.ApplyResources(this.menuStrip1, "menuStrip1");
			this.menuStrip1.Name = "menuStrip1";
			resources.ApplyResources(this.File, "File");
			this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.openToolStripMenuItem, this.exitToolStripMenuItem });
			this.File.Name = "File";
			resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			resources.ApplyResources(this.statusStrip1, "statusStrip1");
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.toolStripStatusLabel1, this.toolStripStatusLabel2 });
			this.statusStrip1.Name = "statusStrip1";
			resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
			this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Chartreuse;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Name = "panel1";
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Controls.Add(this.button6);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.button5);
			this.groupBox2.Controls.Add(this.progressBar1);
			this.groupBox2.Controls.Add(this.TimesDown);
			this.groupBox2.Controls.Add(this.TimesUp);
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			resources.ApplyResources(this.button5, "button5");
			this.button5.Name = "button5";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(button5_Click);
			resources.ApplyResources(this.progressBar1, "progressBar1");
			this.progressBar1.Maximum = 10000;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Value = 5;
			resources.ApplyResources(this.TimesDown, "TimesDown");
			this.TimesDown.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
			this.TimesDown.Minimum = new decimal(new int[4] { 2, 0, 0, 0 });
			this.TimesDown.Name = "TimesDown";
			this.TimesDown.Value = new decimal(new int[4] { 10, 0, 0, 0 });
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Controls.Add(this.button4);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			resources.ApplyResources(this.button4, "button4");
			this.button4.Name = "button4";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(button4_Click);
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(button1_Click);
			resources.ApplyResources(this.panel4, "panel4");
			this.panel4.Name = "panel4";
			resources.ApplyResources(this.button6, "button6");
			this.button6.Name = "button6";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(button6_Click);
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Controls.Add(this.statusStrip1);
			base.Controls.Add(this.menuStrip1);
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "Test";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Test_FormClosing);
			base.Load += new System.EventHandler(Test_Load);
			((System.ComponentModel.ISupportInitialize)this.TimesUp).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.TimesDown).EndInit();
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
