using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;


namespace TurboFPS
{
    public partial class Form1 : Form
    {
        private bool dragging = false;     // Indicates whether the panel is being dragged
        private Point dragCursorPoint;     // Stores the position of the cursor
        private Point dragFormPoint;       // Stores the position of the form

        public Form1()
        {
            InitializeComponent();

            pictureBox12.Click += PictureBox12_Click;

        }

        //Cpu op begin


        private void PictureBox12_Click(object sender, EventArgs e)
        {
            OptimizeGPUAndCleanDisk();
            OptimizeCPU();
            OptimizeGPU();
            MessageBox.Show("CPU and GPU have been optimized for gaming!", "Optimization Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OptimizeCPU()
        {
            string[] nonEssentialApps = {
                "Teams",
                "OneDrive",
                "Skype",
                "Cortana",
                "MicrosoftEdge",
                "YourPhone",
                "GameBar",
                "OfficeClickToRun",
                "MicrosoftStore",
                "InkWorkspace",
                "WINWORD", // Microsoft Word
                "EXCEL",   // Microsoft Excel
                "POWERPNT", // Microsoft PowerPoint
                "Music.UI", // Groove Music
                "WebView2", // Microsoft Edge (WebView2)
                "SurfaceHub", // Surface Hub
                "Mail",    // Mail app
                "Calendar" // Calendar app
            };

            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    // Check if the process is in the non-essential apps list
                    if (nonEssentialApps.Contains(process.ProcessName))
                    {
                        // Warn the user before closing the app
                        DialogResult result = MessageBox.Show($"The process {process.ProcessName} is running. Do you want to close it to optimize CPU?", "Close Process?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            // Close the process if the user confirms
                            process.Kill();
                            process.WaitForExit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle exceptions if needed, like access denied errors
                    Console.WriteLine($"Could not close process {process.ProcessName}: {ex.Message}");
                }
            }
        }

        private void OptimizeGPU()
        {
            try
            {
                Process.Start("powercfg", "/s SCHEME_MIN");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to set high-performance power plan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void OptimizeGPUAndCleanDisk()
        {
            // Paths to delete temporary files and caches
            string[] tempDirectories = {
                Path.GetTempPath(), // System Temp directory
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp"), // Local Temp directory
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache), "Content.IE5") // Internet Explorer cache
            };

            // Clear temporary files
            foreach (var dir in tempDirectories)
            {
                DeleteFilesInDirectory(dir);
            }

            // Additional cleanup for GPU-related files (these paths can vary based on GPU driver and manufacturer)
            string[] gpuCacheDirectories = {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NVIDIA Corporation", "Drs"), // NVIDIA Driver Store
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AMD", "RadeonSettings") // AMD Radeon Settings
            };

            foreach (var dir in gpuCacheDirectories)
            {
                DeleteFilesInDirectory(dir);
            }

            // Optional: Run the Disk Cleanup tool
            RunDiskCleanup();
        }

        private void DeleteFilesInDirectory(string directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    var files = Directory.GetFiles(directoryPath);
                    var directories = Directory.GetDirectories(directoryPath);

                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }

                    foreach (var directory in directories)
                    {
                        Directory.Delete(directory, true); // Delete subdirectories and files
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions if needed
                Console.WriteLine($"Could not clean up directory {directoryPath}: {ex.Message}");
            }
        }

        private void RunDiskCleanup()
        {
            // Using Process to run Disk Cleanup tool with silent options
            Process.Start(new ProcessStartInfo
            {
                FileName = "cleanmgr.exe",
                Arguments = "/sagerun:1", // This runs the Disk Cleanup utility with the settings stored in "1"
                Verb = "runas", // Run as administrator
                UseShellExecute = true
            });

        }
            //end




            [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        // This method is triggered when pictureBox9 is clicked
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            CloseUnnecessaryProcesses();
            ClearMemory();
            MessageBox.Show("System optimized for gaming!", "FPS Booster", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;          // Store the cursor's position
            dragFormPoint = this.Location;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); // Calculate the difference
                this.Location = Point.Add(dragFormPoint, new Size(dif));                // Move the form
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;  // Stop dragging
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox5.Visible = false;
            pictureBox6.Visible = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            // Define the path to the executable
            string executablePath = Path.Combine(Application.StartupPath, "Recoures", "Chaching.exe");

            // Check if the file exists
            if (File.Exists(executablePath))
            {
                // Start the process
                Process.Start(executablePath);
            }
            else
            {
                MessageBox.Show("Executable not found.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            pictureBox8.Visible = false;
            pictureBox9.Visible = true;
        }




        private bool ConfirmCloseProcesses()
        {
            var securityProcesses = new string[]
            {
                "AvastUI", "McAfee", "NortonSecurity", "Bitdefender", "Kaspersky", "TrendMicro",
                "F-Secure", "Malwarebytes", "Webroot", "Sophos", "Avira", "Panda", "ZoneAlarm", "Comodo",
                "PrivateInternetAccess", "NordVPN", "ExpressVPN", "CyberGhost", "HotspotShield", "VPNService",
                "Surfshark"
            };

            var runningProcesses = Process.GetProcesses().Select(p => p.ProcessName).ToArray();
            var processesToClose = runningProcesses.Except(securityProcesses).ToArray();

            if (processesToClose.Length > 0)
            {
                var confirmResult = MessageBox.Show(
                    $"The following programs will be closed: {string.Join(", ", processesToClose)}.\n" +
                    "Do you want to continue?",
                    "Confirm Closure",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                return confirmResult == DialogResult.Yes;
            }

            return true;
        }

        private void CloseUnnecessaryProcesses()
        {
            string[] processesToClose = new string[]
            {
                "OneDrive", "Dropbox", "Skype", "Teams", "YourPhone", "iTunes", "Spotify", "Slack",
                "AdobeARM", "Acrotray", "AppleMobileDeviceService", "mDNSResponder", "GoogleCrashHandler",
                "GoogleUpdate", "QuickTimePlayer", "Winamp", "RealPlayer", "Steam", "Origin", "Uplay",
                "Battle.net", "EpicGamesLauncher", "RazerSynapse", "CorsairService", "LogitechGHub",
                "NZXT CAM", "Nvidia GeForce Experience", "AMDSettings", "RadeonSettings", "Catalyst Control Center",
                "TaskManager", "Cortana", "YourPhone", "MSN", "Weather", "News", "Mail", "Calendar", "StickyNotes",
                "Paint3D", "XboxGameOverlay", "XboxApp", "XboxGamingOverlay", "GameBarPresenceWriter", "WindowsStore",
                "Movies & TV", "MixedRealityPortal", "SkypeApp", "3DViewer", "Photos", "GrooveMusic", "SpotifyWebHelper",
                "SteamService", "EpicWebHelper", "GamingServices", "SecurityHealthService", "SearchUI", "ShellExperienceHost",
                "StartMenuExperienceHost", "WMPNetworkSvc", "QuickSet", "DellSystemDetect", "HP Support Assistant",
                "CCleaner", "TaskManager", "QuickSet", "DellSystemDetect", "HP Support Assistant"
            };

            foreach (var processName in processesToClose)
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes)
                {
                    try
                    {
                        process.Kill();
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception
                        Console.WriteLine($"Error closing {processName}: {ex.Message}");
                    }
                }
            }
        }

        private void ClearMemory()
        {
            foreach (Process proc in Process.GetProcesses())
            {
                try
                {
                    EmptyWorkingSet(proc.Handle);
                }
                catch
                {
                    // Handle or log any exceptions
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            // Show Form2
            form2.Show();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            pictureBox11.Visible = false;
            pictureBox12.Visible = true;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }
    }
}