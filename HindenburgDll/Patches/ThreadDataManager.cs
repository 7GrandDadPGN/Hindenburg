using HarmonyLib;
using HindenburgDll.Structs;
using Il2Cpp;
using Il2CppLuau;

namespace HindenburgDll.Patches
{
	[HarmonyPatch(typeof(ThreadDataManager), "InvokeUpdate")]
	public static class TaskSchedulerPatch
	{
		public static readonly List<AwaitingTask> awaitingTasks = new List<AwaitingTask>();
		private static void Prefix()
		{
			for (var i = 0; i < awaitingTasks.Count; i++)
			{
				AwaitingTask awaitingTask = awaitingTasks[i];
				if (!awaitingTask.Task.IsCompleted) continue;

				awaitingTasks.RemoveAt(i);
				ResumeAsyncTask(awaitingTask);
				i--;
			}
		}

		public static async void ResumeAsyncTask(AwaitingTask awaitingTask, bool immediate = false)
		{
			IntPtr thread = awaitingTask.Thread;

			if (awaitingTask.ThreadRef != 0)
			{
				LuauPluginRaw.Unref(thread, awaitingTask.ThreadRef);
			}

			if (awaitingTask.Task.IsFaulted)
			{
				LuauPluginRaw.PushString(thread, $"Error: Exception thrown in {awaitingTask.Task.Exception.Message}");
				ThreadDataManager.Error(thread);
				LuauPlugin.LuauResumeThreadError(thread);
				return;
			}

			awaitingTask.Task.Result();
			if (!immediate)
			{
				try
				{
					LuauPlugin.LuauResumeThread(thread, 1);
				}
				catch
				{
					LuauPluginRaw.PushString(thread, $"Error: Exception thrown in");
					ThreadDataManager.Error(thread);
					LuauPlugin.LuauResumeThreadError(thread);
				}
			}
		}
	}

	[HarmonyPatch(typeof(ThreadDataManager), "DeleteObjectReference", new Type[] { typeof(int) })]
	public static class GarbageCollectorPatch
	{
		public static readonly Dictionary<int, object> gcList = new Dictionary<int, object>();
		private static void Prefix(int instanceId)
		{
			gcList.Remove(instanceId);
		}
	}

	[HarmonyPatch(typeof(ThreadDataManager), "DeleteObjectReferencesList", new Type[] { typeof(Il2CppSystem.ReadOnlySpan<int>) })]
	public static class GarbageCollectorPatchAll
	{
		private static void Prefix(Il2CppSystem.ReadOnlySpan<int> instanceIds)
		{
			foreach (var instanceId in instanceIds)
			{
				GarbageCollectorPatch.gcList.Remove(instanceId);
			}
		}
	}
}
