using HarmonyLib;
using Il2CppLuau;

namespace HindenburgDll.Patches
{
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
