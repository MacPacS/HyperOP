using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace TurboFPS
{
    public partial class Form2 : Form
    {
        private bool dragging = false;     // Indicates whether the panel is being dragged
        private Point dragCursorPoint;     // Stores the position of the cursor
        private Point dragFormPoint;

        const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        const uint MEM_COMMIT = 0x00001000;
        private Button btnSelectDLL;
        private Button btnInject;
        private ComboBox comboBoxProcesses;
        private TextBox txtDllPath;
        const uint PAGE_READWRITE = 0x04;



        //Begin of the combox interface




        private void InitializeComboBox()
        {
            comboBoxProcesses.DrawMode = DrawMode.OwnerDrawFixed;
            comboBoxProcesses.ItemHeight = 32; // Adjust the height to fit the icon
            comboBoxProcesses.DrawItem += ComboBoxProcesses_DrawItem;
        }

        private void PopulateProcessesWithIcons()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                comboBoxProcesses.Items.Add(new ProcessItem(process));
            }
        }


        //Combox interface end

        //draw begin



        private void ComboBoxProcesses_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            // Get the ProcessItem at the specified index
            ProcessItem item = (ProcessItem)comboBoxProcesses.Items[e.Index];

            // Draw the process icon
            if (item.Icon != null)
            {
                e.Graphics.DrawIcon(item.Icon, e.Bounds.Left, e.Bounds.Top);
            }

            // Draw the process name next to the icon
            e.Graphics.DrawString(item.ProcessName, e.Font, Brushes.Black, e.Bounds.Left + 36, e.Bounds.Top + 8);

            e.DrawFocusRectangle();
        }




        //draw end

        //Begin of the process item!



        public class ProcessItem
        {
            public string ProcessName { get; }
            public Icon Icon { get; }
            public Process Process { get; }

            public ProcessItem(Process process)
            {
                Process = process;
                ProcessName = process.ProcessName;

                try
                {
                    // Extract the icon associated with the process executable
                    Icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                }
                catch
                {
                    // Use a default icon if extraction fails
                    Icon = SystemIcons.Application;
                }
            }

            public override string ToString()
            {
                return ProcessName;
            }
        }

        //prossitem end


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);



        public Form2()
        {
            InitializeComponent();
            InitializeComboBox();
            PopulateProcessesWithIcons();
        }


        private void ProcessItmemName()
        {
            // Get the list of processes
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                comboBoxProcesses.Items.Add(process.ProcessName);
            }
        }


        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form2));
            label5 = new Label();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            label7 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            btnSelectDLL = new Button();
            btnInject = new Button();
            comboBoxProcesses = new ComboBox();
            txtDllPath = new TextBox();
            ((ISupportInitialize)pictureBox4).BeginInit();
            ((ISupportInitialize)pictureBox3).BeginInit();
            ((ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((ISupportInitialize)pictureBox2).BeginInit();
            ((ISupportInitialize)pictureBox5).BeginInit();
            ((ISupportInitialize)pictureBox6).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(355, 21);
            label5.Name = "label5";
            label5.Size = new Size(168, 28);
            label5.TabIndex = 16;
            label5.Text = "TurboFPS - V 0.5";
            // 
            // pictureBox4
            // 
            pictureBox4.Cursor = Cursors.Hand;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(835, 16);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(40, 40);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 15;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(772, 16);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(40, 40);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 14;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(55, 56);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(55, 55, 55);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(625, 84);
            panel2.Name = "panel2";
            panel2.Size = new Size(250, 378);
            panel2.TabIndex = 17;
            panel2.Paint += panel2_Paint;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Cursor = Cursors.Hand;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.White;
            label7.Location = new Point(95, 219);
            label7.Name = "label7";
            label7.Size = new Size(59, 28);
            label7.TabIndex = 3;
            label7.Text = "INFO";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Cursor = Cursors.Hand;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(36, 118);
            label6.Name = "label6";
            label6.Size = new Size(186, 28);
            label6.TabIndex = 2;
            label6.Text = "Dll Injector - V 1.2";
            // 
            // panel1
            // 
            panel1.BackColor = Color.SlateGray;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox5);
            panel1.Controls.Add(pictureBox6);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(878, 63);
            panel1.TabIndex = 18;
            panel1.Paint += panel1_Paint;
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseMove += panel1_MouseMove;
            panel1.MouseUp += panel1_MouseUp;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(336, 16);
            label1.Name = "label1";
            label1.Size = new Size(158, 28);
            label1.TabIndex = 5;
            label1.Text = "HyperOP - V0.7";
            label1.Click += label1_Click;
            label1.MouseDown += label1_MouseDown;
            label1.MouseMove += label1_MouseMove;
            label1.MouseUp += label1_MouseUp;
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(826, 9);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(40, 40);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Cursor = Cursors.Hand;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(763, 9);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(40, 40);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 3;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(12, 3);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(55, 56);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 2;
            pictureBox6.TabStop = false;
            // 
            // btnSelectDLL
            // 
            btnSelectDLL.Location = new Point(292, 94);
            btnSelectDLL.Name = "btnSelectDLL";
            btnSelectDLL.Size = new Size(167, 81);
            btnSelectDLL.TabIndex = 19;
            btnSelectDLL.Text = "Select DLL";
            btnSelectDLL.UseVisualStyleBackColor = true;
            btnSelectDLL.Click += btnSelectDLL_Click;
            // 
            // btnInject
            // 
            btnInject.Location = new Point(292, 385);
            btnInject.Name = "btnInject";
            btnInject.Size = new Size(160, 77);
            btnInject.TabIndex = 20;
            btnInject.Text = "Inject";
            btnInject.UseVisualStyleBackColor = true;
            btnInject.Click += btnInject_Click;
            // 
            // comboBoxProcesses
            // 
            comboBoxProcesses.FormattingEnabled = true;
            comboBoxProcesses.Location = new Point(162, 206);
            comboBoxProcesses.Name = "comboBoxProcesses";
            comboBoxProcesses.Size = new Size(420, 28);
            comboBoxProcesses.TabIndex = 21;
            comboBoxProcesses.SelectedIndexChanged += comboBoxProcesses_SelectedIndexChanged;
            // 
            // txtDllPath
            // 
            txtDllPath.Location = new Point(162, 307);
            txtDllPath.Name = "txtDllPath";
            txtDllPath.Size = new Size(420, 27);
            txtDllPath.TabIndex = 22;
            txtDllPath.TextChanged += txtDllPath_TextChanged;
            // 
            // Form2
            // 
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(878, 488);
            Controls.Add(txtDllPath);
            Controls.Add(comboBoxProcesses);
            Controls.Add(btnInject);
            Controls.Add(btnSelectDLL);
            Controls.Add(panel1);
            Controls.Add(label5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form2";
            Text = "DLLTweaker";
            Load += Form2_Load;
            ((ISupportInitialize)pictureBox4).EndInit();
            ((ISupportInitialize)pictureBox3).EndInit();
            ((ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((ISupportInitialize)pictureBox2).EndInit();
            ((ISupportInitialize)pictureBox5).EndInit();
            ((ISupportInitialize)pictureBox6).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label5;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Label label7;
        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private Label label6;

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;          // Store the cursor's position
            dragFormPoint = this.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); // Calculate the difference
                this.Location = Point.Add(dragFormPoint, new Size(dif));                // Move the form
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;  // Stop dragging
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;  // Stop dragging

        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); // Calculate the difference
                this.Location = Point.Add(dragFormPoint, new Size(dif));                // Move the form
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;          // Store the cursor's position
            dragFormPoint = this.Location;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectDLL_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DLL Files (*.dll)|*.dll";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtDllPath.Text = openFileDialog.FileName;
            }
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            string processName = comboBoxProcesses.SelectedItem?.ToString();
            string dllPath = txtDllPath.Text;

            if (string.IsNullOrEmpty(processName) || string.IsNullOrEmpty(dllPath))
            {
                MessageBox.Show("Please select a process and a DLL file.");
                return;
            }

            try
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    MessageBox.Show("Process not found.");
                    return;
                }

                Process targetProcess = processes[0];
                IntPtr processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, targetProcess.Id);

                IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr allocMemAddress = VirtualAllocEx(processHandle, IntPtr.Zero, (uint)dllPath.Length, MEM_COMMIT, PAGE_READWRITE);

                WriteProcessMemory(processHandle, allocMemAddress, Encoding.ASCII.GetBytes(dllPath), (uint)dllPath.Length, out _);
                CreateRemoteThread(processHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);

                MessageBox.Show("DLL injected successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Injection failed: " + ex.Message);
            }
        }

        private void txtDllPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
