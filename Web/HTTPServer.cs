
using System;
using System.Net;
using System.Net.Sockets;

namespace Cielo.Web {
	public class HTTPServer {
		private TcpListener _listener;
		
		
		public HTTPServer(int port) {
			_listener = new TcpListener(IPAddress.Any, port);
			
			_listener.Start();
		}
	}
}
