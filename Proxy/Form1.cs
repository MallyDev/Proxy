using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Proxy
{
    public partial class Form1 : Form
    {
        bool proxyEnabled;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(key, true);
            object value = RegKey.GetValue("ProxyEnable");

            if (value != null)
            {
                if (String.Compare(value.ToString(), "1") == 0)
                    proxyEnabled = true;
                else
                    proxyEnabled = false;

                if (proxyEnabled)
                {
                    RegKey.SetValue("ProxyEnable", 0);

                    proxyButton.Text = "Enable Proxy";
                    proxyLabel.Text = "Click on the button if you are INSIDE office";

                    proxyEnabled = false;
                }
                else
                {
                    //The random function is used to assign randomly the IP, in this way the traffic is shared between different ones.
                    Random random = new Random();
                    //Change this values according to your needs.
                    int rInt = random.Next(1, 10);
                    string rStr = rInt.ToString();

                    //Set here your base address
                    string serverName = "192.168.1." + rStr;
                    //Set here the port number
                    string port = "80";
                    string proxy = serverName + ":" + port;

                    RegKey.SetValue("ProxyServer", proxy);
                    RegKey.SetValue("ProxyEnable", 1);

                    proxyButton.Text = "Disable Proxy";
                    proxyLabel.Text = "Click on the button if you are OUTSIDE office";

                    proxyEnabled = true;
                }

                InternetSetOptionApi.RefreshWinInetProxySettings();

            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        private void Form1_VisibleChanged(object sender, EventArgs e) {
            check_status(e);
            this.notifyIcon1.Visible = !this.Visible;
        }

        protected override void OnLoad(EventArgs e)
        {
            check_status(e);
        }

        private void check_status(EventArgs e) {
            string key = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(key, true);
            object value = RegKey.GetValue("ProxyEnable");

            if (value != null)
            {
                if (String.Compare(value.ToString(), "1") == 0)
                {
                    this.proxyButton.Text = "Disable Proxy";
                    this.proxyLabel.Text = "Click the button if you are OUTSIDE office";
                }
                else
                {
                    this.proxyButton.Text = "Enable Proxy";
                    this.proxyLabel.Text = "Click the button if you are INSIDE office";
                }

                base.OnLoad(e);

            }
        }
    }
}
