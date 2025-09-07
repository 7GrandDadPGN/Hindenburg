using HarmonyLib;
using Il2Cpp;
using UnityEngine;

namespace HindenburgDll.Patches
{
	[HarmonyPatch(typeof(TransferManager), "Disconnect", new Type[] { typeof(bool), typeof(string) })]
	internal class TransferManagerPatch
	{
		private static bool Prefix(bool kicked, string kickMessage)
		{
			if (kicked && CrossSceneState.kickForceLogout)
			{
				CrossSceneState.kickForceLogout = false;
				return false;
			}

			return true;
		}
	}
}
