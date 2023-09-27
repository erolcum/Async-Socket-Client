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

namespace AsyncWinForms
{
    public partial class Form1 : Form
    {
        private Socket client;
        private byte[] data = new byte[1024];
        private int size = 1024;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(txtIpNo.Text), Convert.ToInt32(txtPort.Text));
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);
        }

        private void Connected(IAsyncResult ar)
        {
            client = (Socket)ar.AsyncState;
            try
            {
                client.EndConnect(ar);
                if (txtReceived.InvokeRequired) 
                {
                    txtReceived.Invoke(new MethodInvoker(delegate { txtReceived.Text = "Connected to: " + client.RemoteEndPoint.ToString() + "\r\n"; }));
                }

                client.BeginReceive(data, 0, size, SocketFlags.None, new AsyncCallback(ReceiveData), client);
            }
            catch (SocketException)
            {
                if (txtReceived.InvokeRequired)
                {
                    txtReceived.Invoke(new MethodInvoker(delegate { txtReceived.Text = "Error connecting" + "\r\n"; }));
                }
               
            }
        }

        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                Socket remote = (Socket)ar.AsyncState;
                int recv = remote.EndReceive(ar);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                int count = txtReceived.Text.Split('\n').Length;             
                if (count > 14) 
                {
                    this.BeginInvoke((MethodInvoker)delegate ()
                    { txtReceived.Text = ""; });
                }
                if (txtReceived.InvokeRequired)
                {
                    txtReceived.Invoke(new MethodInvoker(delegate { txtReceived.Text += stringData + "\r\n"; }));
                }
                // Another way :
                //this.BeginInvoke((MethodInvoker)delegate ()
                //{ txtReceived.Text += stringData + "\r\n"; });

                // restart reading data
                remote.BeginReceive(data, 0, size, SocketFlags.None, new AsyncCallback(ReceiveData), remote);
            }
            catch (Exception)
            {
                client.Close();     
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            client.Close();
            client.Dispose();
            txtSent.Text = "Disconnected";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes(txtSent.Text+"\n");
                if(client != null)
                client.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(SendData), client);
            }
            catch (Exception ex)
            {
                txtReceived.Text = ex.Message;
            }
        }

        private void SendData(IAsyncResult ar)
        {
            try
            {
                Socket remote = (Socket)ar.AsyncState;
                int sent = remote.EndSend(ar);
                // restart reading data
                remote.BeginReceive(data, 0, size, SocketFlags.None, new AsyncCallback(ReceiveData), remote);
            }
            catch (Exception ex)
            {
                txtReceived.Text =ex.Message;
            }
        }
    }
}
