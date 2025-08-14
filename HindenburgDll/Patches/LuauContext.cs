using HarmonyLib;
using Il2Cpp;

namespace HindenburgDll.Patches
{
	[HarmonyPatch(typeof(LuauCore), "ResetContext", new Type[] { typeof(LuauContext) })]
	public static class LuaContextPatch
	{
		public static event Action reloadAction;
		private static void Postfix(LuauContext context)
		{
			if (context == LuauContext.Protected)
			{
				reloadAction?.Invoke();
			}
		}
	}
}
