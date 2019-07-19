
using System;
using System.Net.Sockets;

using Cielo.IO;


using Cielo.Threading;


namespace Cielo.Network {
	public class TCPClient {
		public delegate void NetworkEvent(TCPClient sender);
		
		
		
		
		
		public bool Connected { get { return _client.Connected; } }
		
		public NetworkStream Stream { get { return _client.GetStream(); } }
		
		
		
		
		
		public event NetworkEvent OnConnect {
			add { _onConnect += value; }
			remove { _onConnect -= value; }
		}
		public event NetworkEvent OnReceive {
			add { _onReceive += value; }
			remove { _onReceive -= value; }
		}
		public event NetworkEvent OnDisconnect {
			add { _onDisconnect += value; }
			remove { _onDisconnect -= value; }
		}
		
		
		
		
		
		private readonly TcpClient _client;
		
		
		private event NetworkEvent _onConnect = delegate { };
		private event NetworkEvent _onReceive = delegate { };
		private event NetworkEvent _onDisconnect = delegate { };
		
		
		private readonly byte[] _buffer = new byte[0];
		
		
		
		
		
		
		public TCPClient() {
			_client = new TcpClient();
		}
		public TCPClient(TcpClient client) {
			_client = client;
		}
		
		
		
		
		
		public void Connect(string hostname, int port) {
			_client.Connect(hostname, port);
			
			_onConnect(this);
		}
		
		public void Close() {
			_client.Close();
		}
		
		public void Listen() {
			_ListenForReceive();
		}
		
		
		
		
		
		private void _ListenForReceive() {
			Stream.ReadAsync(_buffer, 0, 0).Then(
				(numBytesRead) => {
					if (Connected) {
						_onReceive(this);
						
						_ListenForReceive();
					} else {
						_onDisconnect(this);
					}
				}
			);
		}
	}
}
