using Il2CppMirror;
using HarmonyLib;

namespace HindenburgDll.Patches
{
	[HarmonyPatch(typeof(NetworkManager), "SetupClient")]
	public static class NetworkManagerPatch
	{
		public static event Action teleportAction;
		private static void Prefix()
		{
			teleportAction?.Invoke();
		}
	}
}
