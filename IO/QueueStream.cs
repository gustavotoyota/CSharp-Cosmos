
using System;
using System.IO;

namespace Cielo.IO {
	public class QueueStream : MemoryStream {
		public const int BufferSize = 1024;
		
		
		
		public long ReadPosition {
			get { return _readPosition; }
			set { _readPosition = value; }
		}
		public long WritePosition {
			get { return _writePosition; }
			set { _writePosition = value; }
		}
		
		
		
		private long _readPosition;
		private long _writePosition;
		
		
		
		
		public QueueStream() {
			_readPosition = 0;
			_writePosition = 0;
		}
		
		
		
		
		public override void Write(byte[] buffer, int offset, int count) {
			this.Position = _writePosition;
			
			base.Write(buffer, offset, count);
			
			_writePosition = this.Position;
		}
		public override int Read(byte[] buffer, int offset, int count) {
			this.Position = _readPosition;
			
			int numBytesRead = base.Read(buffer, offset, count);
			
			_readPosition = this.Position;
			
			
			
			if (_readPosition >= BufferSize) {
				int numUnreadBytes = (int)(this.Length - _readPosition);
				
				byte[] bytes = new byte[numUnreadBytes];
				
				base.Read(bytes, 0, numUnreadBytes);
				base.SetLength(0);
				base.Write(bytes, 0, numUnreadBytes);
				
				_readPosition = 0;
				_writePosition = numUnreadBytes;
			}
			
			
			
			return numBytesRead;
		}
	}
}
