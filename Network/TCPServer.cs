
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

using System.Windows.Forms;
using Cielo.Threading;
using System.Threading.Tasks;

namespace Cielo.Network {
	public class TCPServer {
		public delegate void ClientEvent(TCPServer sender, TCPClient client);
		
		
		
		
		
		public List<TCPClient> Clients { get { return _clients; } }
		
		
		
		
		
		public event ClientEvent OnClientConnect {
			add { _onClientConnect += value; }
			remove { _onClientConnect -= value; }
		}
		public event ClientEvent OnReceive {
			add { _onReceive += value; }
			remove { _onReceive -= value; }
		}
		public event ClientEvent OnClientDisconnect {
			add { _onClientDisconnect += value; }
			remove { _onClientDisconnect -= value; }
		}
		
		
		
		
		
		private readonly TcpListener _listener;
			
		
		private readonly List<TCPClient> _clients;
		
		
		private event ClientEvent _onClientConnect = delegate { };
		private event ClientEvent _onReceive = delegate { };
		private event ClientEvent _onClientDisconnect = delegate { };
		
		
		private readonly byte[] _buffer = new byte[0];
		
		
		
		
		
		public TCPServer(int port) {
			_listener = new TcpListener(IPAddress.Any, port);
			
			_clients = new List<TCPClient>();
		}
		
		public void Listen() {
			_listener.Start();
			
			_ListenForConnect();
		}
		
		
		
		
		private void _ListenForConnect() {
			_listener.AcceptTcpClientAsync().Then(
				(task) => {
					_ListenForConnect();
					
					TCPClient client = new TCPClient(task.Result);
					
					_clients.Add(client);
					
					client.OnReceive += _ClientReceive;
					client.OnDisconnect += _ClientDisconnect;
					
					_onClientConnect(this, client);
				}
			);
		}
		
		
		
		
		
		private void _ClientReceive(TCPClient client) {
			_onReceive(this, client);
		}
		
		private void _ClientDisconnect(TCPClient client) {
			_onClientDisconnect(this, client);
		}
	}
}
