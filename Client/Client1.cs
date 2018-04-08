using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Client1 : Form
    {
        Socket server;
        Socket client;
        IPEndPoint ipe;
        byte[] data = new byte[1024];
        public Client1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ipe = new IPEndPoint(IPAddress.Parse("169.254.126.217"), 9050);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.BeginConnect(ipe, new AsyncCallback(Connected),client);
        }
        private static void Connected(IAsyncResult iar)
        {
             Socket client = (Socket)iar.AsyncState;
            client.EndConnect(iar);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(rTB1.Text);
            listBox1.Items.Add("Client: " + rTB1.Text);
            rTB1.Text = "";
            client.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendData), client);
            data = new byte[1024];
            client.Receive(data);
            string text = Encoding.ASCII.GetString(data);
            listBox1.Items.Add("Server: " + text);
        }
        private void SendData(IAsyncResult iar)
        {
            server = (Socket)iar.AsyncState;
            int sent = server.EndSend(iar);
        }

    }
}
