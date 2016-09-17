#Simple Client/Server Application using .Net Socket Class
##Overview
This Application consists of Windows Forms Client and Server Applications. Both Client and Server applications communicate with each other using Socket through TCP/IP. Server Application is setup to continuously run and listen on a port (default – 1234) and wait for the client connections and request messages. The client application can connect to the server on the specified port and send the request message and get the response back from the server.
##Server
By default, the server runs on port 1234 and keeps listening for incoming connection requests. It accepts Person Ids from 1 to 10. If the request message contains anything other than those Person Ids, the server will send back error message in the response. If the server receives a request with valid Person Ids, it will fetch corresponding Person object, serializes the object with XMLSerializer and sends back the xml string back to the client as a response.
If the user clicks “Restart” button on the server window, the server will restart. 
##Client
When the user runs the client application, it presents the user with a windows form. User can enter IP address and port in port field as required by the server application. By default, this field contains ‘localhost:1234”.  User can enter Person Ids from 1 to 10 and press “Send Request” button. The client application then establishes connection with the server, sends data to the server and gets the Person details in xml format and displays xml data in the “Response” field. In case of erroneous cases, it will show error message in the message box.
##Installation
Download ServerClientInstall.rar file from https://github.com/SwamyVaditya/ServerClient github repository. After unzipping, the folder will contain two separate folders called server and client. Run setup file from those folders and both client and server windows forms applications will be installed on your machine.
##Usage
After installation complete, first run the server application which keeps listening for incoming connections. Then you can run the client application to send the request and receive response back from the server.
