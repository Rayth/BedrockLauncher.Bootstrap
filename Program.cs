using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BedrockLauncher.Bootstrap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.BootstrapWindow = new Main();
            Application.EnableVisualStyles();
            Application.Run(Bootstrap.BootstrapWindow);
        }
    }
}
