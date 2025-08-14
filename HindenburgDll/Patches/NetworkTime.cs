using HarmonyLib;
using Il2CppMirror;

namespace HindenburgDll.Patches
{
	[HarmonyPatch(typeof(NetworkTime), "OnClientPing", new Type[] { typeof(NetworkPingMessage) })]
	public static class NetworkTimePatch
	{
		public static int pingDelay = 0;
		private static bool Prefix(NetworkPingMessage message)
		{
			if (pingDelay <= 0) return true;
			NetworkPongMessage msg = new NetworkPongMessage(
				message.localTime - ((double)pingDelay / 1000),
				0, 0
			);
			NetworkClient.Send(msg, 1);

			return false;
		}
	}
}
