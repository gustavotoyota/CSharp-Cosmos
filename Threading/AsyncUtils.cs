
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Cielo.Threading {
	public static class Async {
		public static Task RunOnUI(Action action) {
			var task = new Task(action);
			
			task.Start(UIThread.Scheduler);
			
			return task;
		}
		
		
		
		
		
		public static Task Then(
			this Task task, Action<Task> action) {
			return task.ContinueWith(action);
		}
		
		public static Task Then<TResult>(
			this Task<TResult> task, Action<Task<TResult>> action) {
			return task.ContinueWith(action);
		}
		
		public static Task ThenOnUI(
			this Task task, Action<Task> action) {
			return task.ContinueWith(action, UIThread.Scheduler);
		}
		
		public static Task ThenOnUI<TResult>(
			this Task<TResult> task, Action<Task<TResult>> action) {
			return task.ContinueWith(action, UIThread.Scheduler);
		}
		
		
		
		
		
		public static ConfiguredTaskAwaitable Awaiter(
		this Task task, bool continueOnCapturedContext = false) {
			return task.ConfigureAwait(continueOnCapturedContext);
		}
		
		public static ConfiguredTaskAwaitable<TResult> Awaiter<TResult>(
		this Task<TResult> task, bool continueOnCapturedContext = false) {
			return task.ConfigureAwait(continueOnCapturedContext);
		}
	}
}