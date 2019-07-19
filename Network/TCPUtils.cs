
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Cielo.Network {
	public static class TCPUtils {
		public static TcpState GetState(this TcpClient client) {
			var globalProps = IPGlobalProperties.GetIPGlobalProperties();
			
			var activeConns = globalProps.GetActiveTcpConnections();
			
			foreach (var activeConn in activeConns) {
				if (activeConn.LocalEndPoint.Equals(client.Client.LocalEndPoint)
			    && activeConn.RemoteEndPoint.Equals(client.Client.RemoteEndPoint)) {
					return activeConn.State;
				}
			}
			
			return TcpState.Unknown;
		}
	}
}
