using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BedrockLauncher.Bootstrap
{
    public static class Bootstrap
    {
        public static Main BootstrapWindow;

        public static void CheckForNETRuntime()
        {
            Thread.Sleep(500);
            bool result = false;
            string minimumVersionS = "8.0";
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\dotnet\\Setup\\InstalledVersions\\x64\\sharedfx\\Microsoft.WindowsDesktop.App"))
                {
                    Version currentVersion;
                    Version minimumVersion = new Version(minimumVersionS);
                    if (key != null) 
                    {
                        foreach (var item in key.GetValueNames())
                        {
                            if (Version.TryParse(item, out currentVersion))
                            {
                                if (currentVersion.CompareTo(minimumVersion) >= 0)
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception) { }


            if (!result)
            {
                ShowMessageBox("You need .NET Desktop Runtime " + minimumVersionS + " or higher to run this application! Please download it!");
                Application.Exit();
            }
        }
        public static void CheckForVCRuntime()
        {
            Thread.Sleep(500);
            bool result = false;
            string minimumVersionS = "14.14.26405.0";
            
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\VisualStudio\\14.0\\VC\\Runtimes\\x64"))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue("Version");
                        if (o != null)
                        {
                            Version currentVersion = new Version((o as String).Replace("v", ""));
                            Version minimumVersion = new Version(minimumVersionS);
                            if (currentVersion.CompareTo(minimumVersion) >= 0) result = true;
                        }

                    }

                }
            }
            catch (Exception) { }

            if (!result)
            {
                ShowMessageBox("You need VC++ Runtime " + minimumVersionS + " or higher to run this application! Please download it!");
                Application.Exit();
            }
        }
        public static void CheckForWindowsVersion()
        {
            Thread.Sleep(500);
            bool result = false;
            string minimumVersionS = "10.0.19041.0";

            try
            {
                Version currentVersion = Environment.OSVersion.Version;
                Version minimumVersion = new Version(minimumVersionS);
                if (currentVersion.CompareTo(minimumVersion) >= 0) result = true;
            }
            catch (Exception) { }

            if (!result)
            {
                ShowMessageBox("This application only works on Windows version " + minimumVersionS + " or above!");
                Application.Exit();
            }
        }
        public static void StartLauncher()
        {
            Thread.Sleep(500);

            string currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string launcherExePath1 = Path.Combine(currentDir, "app", "BedrockLauncher.exe");
            string launcherExePath2 = Path.Combine(currentDir, "app", "launcher.exe");

            if (File.Exists(launcherExePath1)) StartProcess(launcherExePath1);
            else if (File.Exists(launcherExePath2)) StartProcess(launcherExePath2);
            else
            {
                ShowMessageBox("Launcher files missing!");
                Application.Exit();
            }



            void StartProcess(string path)
            {
                var startInfo = new ProcessStartInfo(path)
                {
                    //Arguments = string.Join(" ", Environment.GetCommandLineArgs()),
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(startInfo);
                Application.Exit();
            }
        }

        private static void ShowMessageBox(string message)
        {
            BootstrapWindow.Invoke((MethodInvoker)delegate
            {
                BootstrapWindow.TopMost = false;
            });

            MessageBox.Show(message);

            BootstrapWindow.Invoke((MethodInvoker)delegate
            {
                BootstrapWindow.TopMost = true;
            });
        }
    }
}
