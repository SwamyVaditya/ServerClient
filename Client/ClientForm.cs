using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void btnSendRequest_Click(object sender, EventArgs e)
        {
            string serverInfo = txtServer.Text;
            int port = 0;
            string server = string.Empty;
            
            // Parse server info to derive server and port information
            ParseServerInfo(serverInfo, ref port, ref server);

            // Create socket and connect to server 
            IPHostEntry ipHost = Dns.Resolve(server);
            //IPHostEntry ipHost = Dns.Resolve("127.0.0.1");
            IPAddress ipAddress = ipHost.AddressList[0];
            
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            
            Socket client = new Socket(AddressFamily.InterNetwork,
                                       SocketType.Stream,
                                       ProtocolType.Tcp);

            client.Connect(ipEndPoint);

            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("ID field cannot be empty. Enter Valid ID");
            }
            else
            {
                string msg = txtID.Text;

                byte[] msgBytes = Encoding.ASCII.GetBytes(msg);

                client.Send(msgBytes);

                // Receive response from server
                //byte[] receivedBytes = new byte[1024];
                //int totalBytesReceived = client.Receive(receivedBytes);
                //string response = Encoding.ASCII.GetString(receivedBytes, 0, totalBytesReceived);


                NetworkStream ns = new NetworkStream(client);
                StreamReader sr = new StreamReader(ns);
                string response = sr.ReadToEnd();

                if (response.Contains("Error"))
                {
                    MessageBox.Show(response);
                }

                txtResponse.Text = response;
            }

            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        private static void ParseServerInfo(string serverInfo, ref int port, ref string server)
        {
            if ((serverInfo.Contains('.') || serverInfo.Contains("localhost"))
                && serverInfo.Contains(':'))
            {
                string[] parts = serverInfo.Split(':');

                //MessageBox.Show("parts: " + parts[0] + " " + parts[1]);
                
                if (parts[0] == "localhost")
                {
                    server = "127.0.0.1";
                }
                else
                {
                    server = parts[0];
                }

                if (!string.IsNullOrEmpty(parts[1]))
                {
                    port = int.Parse(parts[1]);
                }
                else
                {
                    port = 1234;
                }
            }
            else if (serverInfo.Contains('.'))
            {
                server = serverInfo;
                port = 1234; // Default port or sever port
            }
            else
            {
                server = "127.0.0.1";
                port = 1234;
            }
        }
    }
}
