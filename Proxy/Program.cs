using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proxy
{
    static class Program
    {
        /// <summary>
        /// This application is designed to help the users based in Lyca group offices
        /// to disable the proxy on their system in order to let possible
        /// the web navigation outside the office's network.
        /// Eventually is possible to enable the proxy on their system 
        /// from the same interface when they are in the office.
        /// 
        /// Designed and developed by: Maria Laura Bisogno
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var controller = new Proxy.ApplicationController(new Form1());
            controller.Run(Environment.GetCommandLineArgs());
        }
    }
}
