
using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cielo.Threading {
	public static class UIThread {
		public static SynchronizationContext Context {
			get { return _context; }
		}
		public static TaskScheduler Scheduler {
			get { return _scheduler; }
		}
		
		
		
		private static SynchronizationContext _context;
		private static TaskScheduler _scheduler;
		
		
		
		
		public static void Init() {
			_context = SynchronizationContext.Current;
			_scheduler = TaskScheduler.FromCurrentSynchronizationContext();
		}
		
		
		
		
		public static void Post(Action action) {
			Context.Post(_ => action(), null);
		}
		
		public static void Send(Action action) {
			Context.Send(_ => action(), null);
		}
	}
}
