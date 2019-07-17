
using System;
using System.Net.Sockets;

using Cielo.IO;


namespace Cielo.Network {
	public class TCPClient {
		public NetworkStream Stream { get { return _client.GetStream(); } }
		
		
		
		private readonly TcpClient _client;
		
		
		
		public TCPClient() {
			_client = new TcpClient();
		}
		public TCPClient(TcpClient client) {
			_client = client;
		}
		
		
		
		public void Connect(string hostname, int port) {
			_client.Connect(hostname, port);
		}
		
		
		
		public void Close() {
			_client.Close();
		}
	}
}
