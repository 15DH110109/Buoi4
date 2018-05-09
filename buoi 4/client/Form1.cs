using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
namespace client
{
    public partial class Form1 : Form
    {
        byte[] data = new byte[1024];
        string input, stringData;
        IPEndPoint ip;
        TcpClient server;
        NetworkStream ns;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ip = new IPEndPoint(IPAddress.Parse(txtIP.Text),9050);
            try
            {
                server = new TcpClient();
            }
            catch (SocketException)
            {
                Console.WriteLine("Unable to connect to server");
                return;
            }
            server.Connect(ip);
            ns = server.GetStream();
            int recv = ns.Read(data, 0, data.Length);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            listBox1.Items.Add(stringData);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMS.Text != "")
            {
                ns.Write(Encoding.ASCII.GetBytes(txtMS.Text), 0, txtMS.Text.Length);
                ns.Flush();
                data = new byte[1024];
                int recv = ns.Read(data, 0, data.Length);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                listBox1.Items.Add(stringData);
                txtMS.Text = "";
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ns.Close();
            server.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
