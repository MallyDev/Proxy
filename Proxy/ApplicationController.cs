using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace Proxy
{
    class ApplicationController : WindowsFormsApplicationBase{
        private Form1 mainForm;

        public ApplicationController(Form1 form) {
            mainForm = form;
            this.IsSingleInstance = true;
            this.StartupNextInstance += this_StartupNextInstance;
        }

        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            //Here we bring application to front
            e.BringToForeground = true;
            mainForm.ShowInTaskbar = true;
            mainForm.WindowState = FormWindowState.Minimized;
            mainForm.Show();
            mainForm.WindowState = FormWindowState.Normal;
        }

        protected override void OnCreateMainForm()
        {
            this.MainForm = mainForm;
        }
    }
}
