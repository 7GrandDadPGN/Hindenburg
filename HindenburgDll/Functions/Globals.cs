using HindenburgDll.Patches;
using HindenburgDll.Structs;
using HindenburgDll.Utils;
using Il2Cpp;
using Il2CppLuau;
using Il2CppSystem.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using static HindenburgDll.Utils.CompileUtils;
using static HindenburgDll.Utils.LuaIApi;
using Object = UnityEngine.Object;

namespace HindenburgDll.Functions
{
	internal class Globals : FunctionHolder
	{
		private static SHA384 sha = SHA384.Create();

		private struct UnityObjectStruct
		{
			public int context;
			public int objectId;
		}

		public static int backtrack(IntPtr luaState)
		{
			int target = api.luaL_checkinteger(luaState, 1);
			NetworkTimePatch.pingDelay = target;
			return 0;
		}

		public static int getgenv(IntPtr luaState)
		{
			api.lua_pushvalue(envHolder.exploitState, Offsets.LUA_GLOBALSINDEX);
			if (luaState != envHolder.exploitState)
			{
				api.lua_xmove(envHolder.exploitState, luaState, 1);
			}

			return 1;
		}

		public static int getrenv(IntPtr luaState)
		{
			api.lua_pushvalue(envHolder.globalState, Offsets.LUA_GLOBALSINDEX);
			api.lua_xmove(envHolder.globalState, luaState, 1);
			return 1;
		}

		public static int getreg(IntPtr luaState)
		{
			api.lua_pushvalue(luaState, Offsets.LUA_REGISTRYINDEX);
			return 1;
		}

		public static int getrawmetatable(IntPtr luaState)
		{
			api.luaL_checkany(luaState, 1);
			if (api.lua_getmetatable(luaState, 1) == IntPtr.Zero)
			{
				LuauPluginRaw.PushNil(luaState);
			}
			return 1;
		}

		public static int getscripts(IntPtr luaState)
		{
			bool coreScripts = api.luaL_optboolean(luaState, 1, false);
			AirshipScript[] list = Object.FindObjectsByType<AirshipScript>(FindObjectsSortMode.None);
			LuauPluginRaw.NewTable(luaState);

			int i = 1;
			foreach (AirshipScript obj in list)
			{
				if (obj.m_path == null || obj.name == "")
				{
					continue;
				}

				if (!coreScripts && obj.m_path.StartsWith("assets/airshippackages"))
				{
					continue;
				}

				api.PushUnityObject(luaState, obj);
				api.lua_rawseti(luaState, -2, i);
				i++;
			}

			return 1;
		}

		public static int getscriptbytecode(IntPtr luaState)
		{
			IntPtr instanceId = api.luaL_checkudata(luaState, 1, "UnityObject");
			UnityObjectStruct uStruct = Marshal.PtrToStructure<UnityObjectStruct>(instanceId);
			Il2CppSystem.Object obj = ThreadDataManager.GetObjectReference(luaState, uStruct.objectId);

			if (obj is AirshipScript)
			{
				AirshipScript compObj = (AirshipScript)obj;
				api.lua_pushlstringB(luaState, api.FixBytecode(compObj.m_bytes));
			}
			else
			{
				LuauPluginRaw.PushNil(luaState);
			}

			return 1;
		}

		public static int getscripthash(IntPtr luaState)
		{
			IntPtr instanceId = api.luaL_checkudata(luaState, 1, "UnityObject");
			UnityObjectStruct uStruct = Marshal.PtrToStructure<UnityObjectStruct>(instanceId);
			Il2CppSystem.Object obj = ThreadDataManager.GetObjectReference(luaState, uStruct.objectId);

			if (obj is AirshipScript)
			{
				AirshipScript compObj = (AirshipScript)obj;
				byte[] hash = sha.ComputeHash(api.FixBytecode(compObj.m_bytes));
				LuauPluginRaw.PushString(luaState, BitConverter.ToString(hash).Replace("-", "").ToLower());
			}
			else
			{
				LuauPluginRaw.PushNil(luaState);
			}

			return 1;
		}

		public static int getinstances(IntPtr luaState)
		{
			GameObject[] list = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
			LuauPluginRaw.NewTable(luaState);

			int i = 1;
			foreach (GameObject obj in list)
			{
				if (AirshipBehaviourRootV2.HasId(obj))
				{
					api.PushUnityObject(luaState, obj);
					api.lua_rawseti(luaState, -2, i);
					i++;
				}
			}

			return 1;
		}

		public static int getfpscap(IntPtr luaState)
		{
			LuauPluginRaw.PushInteger(luaState, UnityEngine.Application.targetFrameRate);
			return 1;
		}

		public static bool visitgc(IntPtr gcx, IntPtr luaPage, IntPtr gcObj)
		{
			GCOContext context = Marshal.PtrToStructure<GCOContext>(gcx);
			byte tt = Marshal.ReadByte(gcObj);
			
			if (tt < (int)lua_Type.LUA_TPROTO && tt >= (int)lua_Type.LUA_TSTRING && (tt != (int)lua_Type.LUA_TTABLE || context.accessTables))
			{
				IntPtr topOffset = new IntPtr(context.state.ToInt64() + 16);
				IntPtr topData = new IntPtr(Marshal.ReadIntPtr(topOffset).ToInt64() + LuauPluginRaw.GetTop(context.state) * 16);

				LuauPluginRaw.PushNil(context.state);
				Marshal.WriteIntPtr(topData, gcObj);
				Marshal.WriteByte(new IntPtr(topData.ToInt64() + 12), tt);
				Marshal.WriteInt32(new IntPtr(gcx.ToInt64() + 12), context.itemsFound + 1);
				api.lua_rawseti(context.state, -2, context.itemsFound + 1);
			}

			return false;
		}

		public static int getgc(IntPtr luaState)
		{
			bool addTables = api.luaL_optboolean(luaState, 1, false);
			api.lua_limittop(luaState, 1);
			LuauPluginRaw.NewTable(luaState);

			State luaData = Marshal.PtrToStructure<State>(luaState);
			IntPtr visitgcPointer = Marshal.GetFunctionPointerForDelegate<gcovoid>(visitgc);
			IntPtr gcPointer = new IntPtr(luaData.global.ToInt64() + Marshal.SizeOf<global_State>());
			long old = Marshal.ReadInt64(gcPointer);

			Marshal.WriteInt64(gcPointer, 0xffffffff);
			GCOContext ctx = new GCOContext { state = luaState, accessTables = addTables, itemsFound = 0 };
			IntPtr ctxPointer = Marshal.AllocCoTaskMem(Marshal.SizeOf<GCOContext>());
			Marshal.StructureToPtr(ctx, ctxPointer, false);

			api.luaM_visitgco(luaState, ctxPointer, visitgcPointer);

			Marshal.WriteInt64(gcPointer, old);
			Marshal.FreeCoTaskMem(ctxPointer);

			return 1;
		}

		public static int run_protected(IntPtr luaState)
		{
			string code = api.lua_checkstring(luaState, 1);
			ExecuteScript(code, true);
			return 0;
		}

		public static int setclipboard(IntPtr luaState)
		{
			string data = api.lua_checkstring(luaState, 1);
			Clipboard.SetDataObject(data);
			return 0;
		}

		public static int setrawmetatable(IntPtr luaState)
		{
			api.luaL_checkany(luaState, 1);
			api.luaL_checktype(luaState, 2, (int)LuaIApi.lua_Type.LUA_TTABLE);
			if (LuauPluginRaw.GetTop(luaState) != 2)
			{
				api.lua_pushvalue(luaState, 2);
			}

			api.lua_setmetatable(luaState, 1);
			api.lua_pushvalue(luaState, 1);
			return 1;
		}

		public static int setreadonly(IntPtr luaState)
		{
			api.luaL_checktype(luaState, 1, (int)LuaIApi.lua_Type.LUA_TTABLE);
			LuauPluginRaw.SetReadonly(luaState, 1, api.luaL_optboolean(luaState, 2, false));
			return 0;
		}

		public static int setfpscap(IntPtr luaState)
		{
			int target = api.luaL_checkinteger(luaState, 1);
			UnityEngine.Application.targetFrameRate = target;
			return 0;
		}

		public static int isreadonly(IntPtr luaState)
		{
			api.luaL_checktype(luaState, 1, (int)LuaIApi.lua_Type.LUA_TTABLE);
			LuauPluginRaw.PushBoolean(luaState, api.lua_getreadonly(luaState, 1));
			return 1;
		}

		public static int identifyexecutor(IntPtr luaState)
		{
			LuauPluginRaw.PushString(luaState, "Hindenburg");
			LuauPluginRaw.PushString(luaState, "v1.0.1");
			return 2;
		}

		public override void CreateDefinitions()
		{
			Add("backtrack", backtrack);
			Add("base64encode", Crypt.base64encode);
			Add("base64decode", Crypt.base64decode);
			Add("getgenv", getgenv);
			Add("getgc", getgc);
			Add("getfpscap", getfpscap);
			Add("getrenv", getrenv);
			Add("getreg", getreg);
			Add("getrawmetatable", getrawmetatable);
			Add("getscripts", getscripts);
			Add("getscriptbytecode", getscriptbytecode);
			Add("getscripthash", getscripthash);
			Add("getinstances", getinstances);
			Add("run_protected", run_protected);
			Add("setclipboard", setclipboard);
			Add("setrawmetatable", setrawmetatable);
			Add("setreadonly", setreadonly);
			Add("setfpscap", setfpscap);
			Add("isreadonly", isreadonly);
			Add("identifyexecutor", identifyexecutor);
		}

		public override string LibraryName()
		{
			return "globals";
		}

		public override bool PushToGlobal()
		{
			return true;
		}
	}
}
