using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Socket client;
        private void btnCon_Click(object sender, EventArgs e)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            client.Connect(ip, 1111);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          

        }

        private void btnBreak_Click(object sender, EventArgs e)
        {
            client.Shutdown(SocketShutdown.Both);

     
            client.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string str = this.textBox1.Text.Trim();
            client.Send(Encoding.Default.GetBytes(str));
        }
    }
}
