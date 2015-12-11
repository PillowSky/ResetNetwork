using System;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;

namespace ResetNetwork {
    public partial class MainForm : Form {
        private SplitContainer container;
        private TableLayoutPanel buttonPanel;
        private Button renewIPButton;
        private Button resetAdapterButton;
        private Button flushDNSButton;
        private Button resetProtocolButton;
        private Button resetWinsockButton;
        private TextBox shellTextBox;
        private Process shellProcess;
        private SynchronizationContext formSync;

        public MainForm() {
            InitializeComponent();
            InitializeShellWorker();
        }

        private void InitializeComponent() {
            this.container = new System.Windows.Forms.SplitContainer();
            this.buttonPanel = new System.Windows.Forms.TableLayoutPanel();
            this.renewIPButton = new System.Windows.Forms.Button();
            this.resetAdapterButton = new System.Windows.Forms.Button();
            this.flushDNSButton = new System.Windows.Forms.Button();
            this.resetProtocolButton = new System.Windows.Forms.Button();
            this.resetWinsockButton = new System.Windows.Forms.Button();
            this.shellTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.container)).BeginInit();
            this.container.Panel1.SuspendLayout();
            this.container.Panel2.SuspendLayout();
            this.container.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Name = "container";
            // 
            // container.Panel1
            // 
            this.container.Panel1.Controls.Add(this.buttonPanel);
            // 
            // container.Panel2
            // 
            this.container.Panel2.Controls.Add(this.shellTextBox);
            this.container.Size = new System.Drawing.Size(464, 281);
            this.container.SplitterDistance = 120;
            this.container.TabIndex = 0;
            // 
            // buttonPanel
            // 
            this.buttonPanel.ColumnCount = 1;
            this.buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonPanel.Controls.Add(this.renewIPButton, 0, 0);
            this.buttonPanel.Controls.Add(this.resetAdapterButton, 0, 1);
            this.buttonPanel.Controls.Add(this.flushDNSButton, 0, 2);
            this.buttonPanel.Controls.Add(this.resetProtocolButton, 0, 3);
            this.buttonPanel.Controls.Add(this.resetWinsockButton, 0, 4);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Location = new System.Drawing.Point(0, 0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.RowCount = 5;
            this.buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonPanel.Size = new System.Drawing.Size(120, 281);
            this.buttonPanel.TabIndex = 1;
            // 
            // renewIPButton
            // 
            this.renewIPButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renewIPButton.Location = new System.Drawing.Point(3, 3);
            this.renewIPButton.Name = "renewIPButton";
            this.renewIPButton.Size = new System.Drawing.Size(114, 50);
            this.renewIPButton.TabIndex = 0;
            this.renewIPButton.Text = "Renew IP";
            this.renewIPButton.UseVisualStyleBackColor = true;
            this.renewIPButton.Click += new System.EventHandler(this.renewIPButton_Click);
            // 
            // resetAdapterButton
            // 
            this.resetAdapterButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetAdapterButton.Location = new System.Drawing.Point(3, 59);
            this.resetAdapterButton.Name = "resetAdapterButton";
            this.resetAdapterButton.Size = new System.Drawing.Size(114, 50);
            this.resetAdapterButton.TabIndex = 1;
            this.resetAdapterButton.Text = "Reset Adapter";
            this.resetAdapterButton.UseVisualStyleBackColor = true;
            this.resetAdapterButton.Click += new System.EventHandler(this.resetAdapterButton_Click);
            // 
            // flushDNSButton
            // 
            this.flushDNSButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flushDNSButton.Location = new System.Drawing.Point(3, 115);
            this.flushDNSButton.Name = "flushDNSButton";
            this.flushDNSButton.Size = new System.Drawing.Size(114, 50);
            this.flushDNSButton.TabIndex = 2;
            this.flushDNSButton.Text = "Flush DNS";
            this.flushDNSButton.UseVisualStyleBackColor = true;
            this.flushDNSButton.Click += new System.EventHandler(this.flushDNSButton_Click);
            // 
            // resetProtocolButton
            // 
            this.resetProtocolButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetProtocolButton.Location = new System.Drawing.Point(3, 171);
            this.resetProtocolButton.Name = "resetProtocolButton";
            this.resetProtocolButton.Size = new System.Drawing.Size(114, 50);
            this.resetProtocolButton.TabIndex = 3;
            this.resetProtocolButton.Text = "Reset Protocol";
            this.resetProtocolButton.UseVisualStyleBackColor = true;
            this.resetProtocolButton.Click += new System.EventHandler(this.resetProtocolButton_Click);
            // 
            // resetWinsockButton
            // 
            this.resetWinsockButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetWinsockButton.Location = new System.Drawing.Point(3, 227);
            this.resetWinsockButton.Name = "resetWinsockButton";
            this.resetWinsockButton.Size = new System.Drawing.Size(114, 51);
            this.resetWinsockButton.TabIndex = 4;
            this.resetWinsockButton.Text = "Reset Winsock";
            this.resetWinsockButton.UseVisualStyleBackColor = true;
            this.resetWinsockButton.Click += new System.EventHandler(this.resetWinsockButton_Click);
            // 
            // shellTextBox
            // 
            this.shellTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellTextBox.Location = new System.Drawing.Point(0, 0);
            this.shellTextBox.Multiline = true;
            this.shellTextBox.Name = "shellTextBox";
            this.shellTextBox.ReadOnly = true;
            this.shellTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.shellTextBox.Size = new System.Drawing.Size(340, 281);
            this.shellTextBox.TabIndex = 0;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.container);
            this.Name = "MainForm";
            this.Text = "ResetNetwork";
            this.container.Panel1.ResumeLayout(false);
            this.container.Panel2.ResumeLayout(false);
            this.container.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.container)).EndInit();
            this.container.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void InitializeShellWorker() {
            formSync = WindowsFormsSynchronizationContext.Current;

            shellProcess = new Process();
            shellProcess.StartInfo.FileName = "cmd.exe";
            shellProcess.StartInfo.UseShellExecute = false;
            shellProcess.StartInfo.CreateNoWindow = true;

            shellProcess.StartInfo.RedirectStandardError = true;
            shellProcess.StartInfo.RedirectStandardInput = true;
            shellProcess.StartInfo.RedirectStandardOutput = true;

            shellProcess.OutputDataReceived += shellDataHandler;
            shellProcess.ErrorDataReceived += shellDataHandler;

            shellProcess.Start();
            shellProcess.BeginOutputReadLine();
            shellProcess.BeginErrorReadLine();
        }

        private void shellDataHandler(object sender, DataReceivedEventArgs e) {
            formSync.Post((o) => shellTextBox.Text += e.Data + Environment.NewLine, null);
        }

        private void renewIPButton_Click(object sender, EventArgs e) {         
            shellProcess.StandardInput.WriteLine("ipconfig -release");
            shellProcess.StandardInput.WriteLine("ipconfig -renew");
        }

        private void resetAdapterButton_Click(object sender, EventArgs e) {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_NetworkAdapter WHERE  Manufacturer != 'Microsoft' AND NOT PNPDeviceID LIKE 'ROOT\\%'");
            ManagementObjectCollection adapters = searcher.Get();

            foreach (ManagementObject adapter in adapters) {
                string name = (string)adapter.Properties["Name"].Value;
                shellTextBox.Text += name + ": Disable" + Environment.NewLine;
                adapter.InvokeMethod("Disable", null);
                shellTextBox.Text += name + ": Enable" + Environment.NewLine;
                adapter.InvokeMethod("Enable", null);
            }
        }

        private void flushDNSButton_Click(object sender, EventArgs e) {
            shellProcess.StandardInput.WriteLine("ipconfig -flushdns");
        }

        private void resetProtocolButton_Click(object sender, EventArgs e) {
            shellProcess.StandardInput.WriteLine("netsh int ip reset");
        }

        private void resetWinsockButton_Click(object sender, EventArgs e) {
            shellProcess.StandardInput.WriteLine("netsh winsock reset");
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            shellProcess.StandardInput.WriteLine("exit");
        }
    }
}
