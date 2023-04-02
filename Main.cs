using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BedrockLauncher.Bootstrap
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Task.Run(RunBootstrap);
        }

        public void RunBootstrap()
        {
            Thread.Sleep(2000);

            UpdateProgressDetails("Checking Current NET Version...");
            UpdateProgressBar(25);
            Bootstrap.CheckForNETRuntime();
 
            UpdateProgressDetails("Checking Current VC++ Version...");
            UpdateProgressBar(25);
            Bootstrap.CheckForVCRuntime();
     
            UpdateProgressDetails("Checking Current Windows Version...");
            UpdateProgressBar(25);
            Bootstrap.CheckForWindowsVersion();

            UpdateProgressDetails("Starting...");
            UpdateProgressBar(25);
            Bootstrap.StartLauncher();

        }

        public void UpdateProgressDetails(string details)
        {
            progressDetails.Invoke((MethodInvoker)delegate
            {
                progressDetails.Text = details;
            });
        }

        public void UpdateProgressBar(int incrementAmount, bool immediate = true)
        {
            Task.Run(() =>
            {
                if (immediate)
                {
                    progressBar1.Invoke((MethodInvoker)delegate
                    {
                        progressBar1.Value += incrementAmount;
                    });
                }
                else
                {
                    for (int i = 0; i < incrementAmount; i++)
                    {
                        progressBar1.Invoke((MethodInvoker)delegate
                        {
                            progressBar1.Value += 1;
                        });
                        Thread.Sleep(25);
                    }
                }

            });
        }
    }
}
