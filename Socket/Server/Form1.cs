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
using System.Threading;

namespace Server
{
    public partial class Form1 : Form
    {
        
        Socket server;
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType);
            //Socket serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            server.Bind(new IPEndPoint(ip, 1111));
            server.Listen(10);

            Thread t = new Thread(Listen);
            t.IsBackground = true;
            t.Start();

        }
        void Listen()
        {
            while (true)
            {
                Socket s = server.Accept();
                EndPoint point = s.RemoteEndPoint;

                AddNode(point.ToString());

                Thread t = new Thread(Receive);
                t.IsBackground = true;
                t.Start(s);

            }
        }
        delegate void AddNodeDel(string str);
        void AddNode(string str)
        {
            if (this.treeView1.InvokeRequired)
            {

                AddNodeDel del = new AddNodeDel(AddNode);
                this.treeView1.Invoke(del, str);
            }
            else
            {
                this.treeView1.Nodes.Add(new TreeNode(str));
            }

        }


        void Receive(object obj)
        {
            Socket s = obj as Socket;
            while (true)
            {
                byte[] buff = new byte[1024 * 1024];
               int result= s.Receive(buff);
               if (result > 0)
               {
                   string str = Encoding.Default.GetString(buff);
                   this.textBox1.AppendText("===");
                   this.textBox1.AppendText(s.RemoteEndPoint.ToString() + " :");
                   this.textBox1.AppendText(str + "\r\n");
               } 

            }
        }
    }




}
