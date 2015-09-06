using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;

namespace RemoteWget
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxServer.Text = Properties.Settings.Default.Server;
            textBoxUserName.Text = Properties.Settings.Default.UserName;
            textBoxPassword.Text = Properties.Settings.Default.UserPassword;
            textBoxLocation.Text = Properties.Settings.Default.Location;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string host = textBoxServer.Text.Trim();
            string userName = textBoxUserName.Text.Trim();
            string psw = textBoxPassword.Text.Trim();
            string url = textBoxCoomand.Text.Trim();
            string location = @" -P  " + textBoxLocation.Text.Trim();
            string finalCommand = @"wget -bqc '" + url + "' " + location + " ";
            ConnectionInfo conInfo = new ConnectionInfo(host, 22, userName, new AuthenticationMethod[]{
                new PasswordAuthenticationMethod(userName,psw)
            });
            SshClient client = new SshClient(conInfo);
            try
            {
                client.Connect();
                var outptu = client.RunCommand(finalCommand);
            }
            catch (Exception ex)
            {
                textBoxDisplay.Text = ex.Message;
                throw;
            }

            client.Disconnect();
            client.Dispose();
            SetLastValues(host, userName, psw, textBoxLocation.Text.Trim());
        }

        private void SetLastValues(string server, string userName, string password, string location)
        {
            Properties.Settings.Default.Server = server;
            Properties.Settings.Default.UserName = userName;
            Properties.Settings.Default.UserPassword = password;
            Properties.Settings.Default.Location = location;
            Properties.Settings.Default.Save();
        }
    }
}
