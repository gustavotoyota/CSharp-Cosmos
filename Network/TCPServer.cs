
using System;
using System.Net;
using System.Net.Sockets;

using Cielo.Threading;
using System.Threading.Tasks;

namespace Cielo.Network {
	public class TCPServer {
		public delegate void ClientConnect(TCPServer sender, TCPClient client);
		
		
		
		
		public event ClientConnect OnClientConnect {
			add { _onClientConnect += value; }
			remove { _onClientConnect -= value; }
		}
		
		
		
		
		
		private readonly TcpListener _listener;
		
		private event ClientConnect _onClientConnect;
		
		
		
		
		
		public TCPServer(int port) {
			_listener = new TcpListener(IPAddress.Any, port);
		}
		
		
		
		public void Listen() {
			_listener.Start();
			
			_ListenForConnect();
		}
		
		
		
		private void _ListenForConnect() {
			_listener.AcceptTcpClientAsync().Then(
				(task) => {
					_ListenForConnect();
					
					_onClientConnect(this, new TCPClient(task.Result));
				}
			);
		}
	}
}
