using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HindenburgDll.Utils
{
	public struct AwaitingTask
	{
		public IntPtr Thread;
		public int ThreadRef;
		public Task<Action> Task;
	}
}
