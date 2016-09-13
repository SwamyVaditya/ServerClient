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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Server
{
	public partial class ServerForm : Form
	{
        Socket serverSocket = null;
        int port = 0;

		public ServerForm()
		{
			InitializeComponent();
		}

		private void btnRestart_Click(object sender, EventArgs e)
		{
            serverSocket.Close();
            Listen();

            Task.Factory.StartNew(() => AccessConnection(serverSocket));
		}

        private void ServerForm_Load(object sender, EventArgs e)
        {
            Listen();

            Task.Factory.StartNew(() => AccessConnection(serverSocket));
        }

        private void Listen()
        {
            int.TryParse(txtPort.Text, out port);

            if (port == 0)
            {
                MessageBox.Show("Invalid port number");
            }

            //Create the socket
            serverSocket = new Socket(AddressFamily.InterNetwork,
                                             SocketType.Stream,
                                             ProtocolType.Tcp);

            // bind the socket to the port to listen
            //IPAddress hostIP = (Dns.Resolve(IPAddress.Any.ToString())).AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);

            try
            {
                serverSocket.Bind(endPoint);

                // Start Listening
                int backlog = 5;
                serverSocket.Listen(backlog);
            }
            catch (SocketException se)
            {
                MessageBox.Show("Error Occured: " + se.Message);
            }

            txtStatus.Text = "Server is listening now!";
        }

        private void AccessConnection(Socket serverSocket)
        {
            
            while (true)
            {
                Socket socket = null;

                try
                {
                    socket = serverSocket.Accept();
                }
                catch (SocketException se)
                {
                    if (se.Message.IndexOf("WSACancelBlockingCall") > 0)
                        continue;
                    else
                    {
                        MessageBox.Show("Error Occured: " + se.Message);
                    }
                }



                string receivedValue = string.Empty;
                byte[] receivedBytes = new byte[1024];

                int numBytes;

                numBytes = socket.Receive(receivedBytes);
                receivedValue += Encoding.ASCII.GetString(receivedBytes, 0, numBytes);


                string replyMessage = string.Empty;

                if (String.IsNullOrEmpty(receivedValue))
                {
                    replyMessage = "Error: Invalid request. Send Person Ids between 1 and 10.";
                }

                int personId = 0;
                int.TryParse(receivedValue, out personId);

                if (personId == 0 || personId > 10)
                {
                    replyMessage = "Error: Invalid Person ID. Person Id must be between 1 and 10.";
                }

                if (replyMessage != string.Empty)
                {
                    byte[] replyBytes = Encoding.ASCII.GetBytes(replyMessage);
                    socket.Send(replyBytes);
                }
                else
                {

                    XmlSerializer xs = new XmlSerializer(typeof(Person));
                    NetworkStream ns = new NetworkStream(socket);
                    StreamWriter stream = new StreamWriter(ns);

                    Person person = Person.People.Where(p => p.ID == personId).SingleOrDefault();
                    xs.Serialize(stream, person);
                }


                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            serverSocket.Close();
            Listen();

            Task.Factory.StartNew(() => AccessConnection(serverSocket));
        }
	}
}
