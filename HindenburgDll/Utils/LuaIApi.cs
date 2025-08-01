using System.Runtime.InteropServices;
using HindenburgDll.Structs;
using Il2Cpp;

namespace HindenburgDll.Utils
{
	internal class LuaIApi
	{
		// lol pasted from luau github
		public enum lua_Type
		{
			LUA_TNIL = 0,     // must be 0 due to lua_isnoneornil
			LUA_TBOOLEAN = 1, // must be 1 due to l_isfalse

			LUA_TLIGHTUSERDATA,
			LUA_TNUMBER,
			LUA_TVECTOR,

			LUA_TSTRING, // all types above this must be value types, all types below this must be GC types - see iscollectable

			LUA_TTABLE,
			LUA_TFUNCTION,
			LUA_TUSERDATA,
			LUA_TTHREAD,
			LUA_TBUFFER,

			// values below this line are used in GCObject tags but may never show up in TValue type tags
			LUA_TPROTO,
			LUA_TUPVAL,
			LUA_TDEADKEY,

			// the count of TValue type tags
			LUA_T_COUNT = LUA_TPROTO
		};

		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibraryA(string dllToLoad);
		private static IntPtr handle = LoadLibraryA("LuauPlugin.dll");

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr StatePointer(IntPtr luaState);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr StateAndIdPointer(IntPtr luaState, int idx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool StateAndIdBool(IntPtr luaState, int idx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int StateAndIdInteger(IntPtr luaState, int idx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr FieldPointer(IntPtr luaState, int idx, IntPtr str);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr StackIndexPointer(IntPtr luaState, int idx, int tIdx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr StringPointer(IntPtr luaState, IntPtr str, int length);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr StringReturn(IntPtr luaState, int idx, ref int size);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr xmove(IntPtr luaState, IntPtr destState, int idx);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr setsafeenv(IntPtr luaState, int idx, bool safe);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr luaerror(IntPtr luaState, IntPtr str);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr register(IntPtr luaState, IntPtr libName, FunctionHolder.luaL_Reg[] luaReg);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr luauload(IntPtr luaState, IntPtr chunkName, IntPtr bytecode, int bytecodeSize, int native);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool getinfo(IntPtr luaState, int idx, IntPtr str, IntPtr debug);

		public StatePointer luaAirship_require = Marshal.GetDelegateForFunctionPointer<StatePointer>(new IntPtr(handle.ToInt64() + Offsets.luaAirship_require));
		public StateAndIdBool lua_iscfunction = Marshal.GetDelegateForFunctionPointer<StateAndIdBool>(new IntPtr(handle.ToInt64() + Offsets.lua_iscfunction));
		public luaerror luaA_pushobject = Marshal.GetDelegateForFunctionPointer<luaerror>(new IntPtr(handle.ToInt64() + Offsets.luaA_pushobject));
		public StateAndIdBool lua_isnumber = Marshal.GetDelegateForFunctionPointer<StateAndIdBool>(new IntPtr(handle.ToInt64() + Offsets.lua_isnumber));
		public StateAndIdBool lua_isstring = Marshal.GetDelegateForFunctionPointer<StateAndIdBool>(new IntPtr(handle.ToInt64() + Offsets.lua_isstring));
		private FieldPointer getfield = Marshal.GetDelegateForFunctionPointer<FieldPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_getfield));
		private FieldPointer setfield = Marshal.GetDelegateForFunctionPointer<FieldPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_setfield));
		public StateAndIdPointer lua_getmetatable = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_getmetatable));
		public StateAndIdBool lua_getreadonly = Marshal.GetDelegateForFunctionPointer<StateAndIdBool>(new IntPtr(handle.ToInt64() + Offsets.lua_getreadonly));
		public StackIndexPointer lua_rawseti = Marshal.GetDelegateForFunctionPointer<StackIndexPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_rawseti));
		public StringPointer lua_pushlstring = Marshal.GetDelegateForFunctionPointer<StringPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_pushlstring));
		public StateAndIdPointer lua_pushvalue = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_pushvalue));
		public StatePointer lua_newthread = Marshal.GetDelegateForFunctionPointer<StatePointer>(new IntPtr(handle.ToInt64() + Offsets.lua_newthread));
		public StateAndIdInteger lua_next = Marshal.GetDelegateForFunctionPointer<StateAndIdInteger>(new IntPtr(handle.ToInt64() + Offsets.lua_next));
		public StateAndIdPointer lua_setmetatable = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_setmetatable));

		public setsafeenv lua_setsafeenv = Marshal.GetDelegateForFunctionPointer<setsafeenv>(new IntPtr(handle.ToInt64() + Offsets.lua_setsafeenv));
		public StateAndIdPointer lua_settop = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_settop));
		public StateAndIdInteger lua_tointeger = Marshal.GetDelegateForFunctionPointer<StateAndIdInteger>(new IntPtr(handle.ToInt64() + Offsets.wrap_tointeger));
		public StringReturn lua_tolstring = Marshal.GetDelegateForFunctionPointer<StringReturn>(new IntPtr(handle.ToInt64() + Offsets.lua_tolstring));
		public StateAndIdPointer lua_topointer = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_topointer));
		public StateAndIdInteger lua_type = Marshal.GetDelegateForFunctionPointer<StateAndIdInteger>(new IntPtr(handle.ToInt64() + Offsets.lua_type));
		public xmove lua_xmove = Marshal.GetDelegateForFunctionPointer<xmove>(new IntPtr(handle.ToInt64() + Offsets.lua_xmove));
		public StateAndIdInteger lua_yield = Marshal.GetDelegateForFunctionPointer<StateAndIdInteger>(new IntPtr(handle.ToInt64() + Offsets.lua_yield));
		public getinfo lua_getinfoC = Marshal.GetDelegateForFunctionPointer<getinfo>(new IntPtr(handle.ToInt64() + Offsets.lua_getinfo));

		public FieldPointer luaL_argerror = Marshal.GetDelegateForFunctionPointer<FieldPointer>(new IntPtr(handle.ToInt64() + Offsets.luaL_argerrorL));
		public FieldPointer luaL_typeerror = Marshal.GetDelegateForFunctionPointer<FieldPointer>(new IntPtr(handle.ToInt64() + Offsets.luaL_typeerrorL));
		public StateAndIdPointer luaL_checkany = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.luaL_checkany));
		public StackIndexPointer luaL_checktype = Marshal.GetDelegateForFunctionPointer<StackIndexPointer>(new IntPtr(handle.ToInt64() + Offsets.luaL_checktype));
		public StateAndIdBool luaL_checkboolean = Marshal.GetDelegateForFunctionPointer<StateAndIdBool>(new IntPtr(handle.ToInt64() + Offsets.luaL_checkboolean));
		public StringReturn luaL_checklstring = Marshal.GetDelegateForFunctionPointer<StringReturn>(new IntPtr(handle.ToInt64() + Offsets.luaL_checklstring));
		public StateAndIdInteger luaL_checkinteger = Marshal.GetDelegateForFunctionPointer<StateAndIdInteger>(new IntPtr(handle.ToInt64() + Offsets.luaL_checkinteger));
		public FieldPointer luaL_checkudataC = Marshal.GetDelegateForFunctionPointer<FieldPointer>(new IntPtr(handle.ToInt64() + Offsets.luaL_checkudata));

		public luaerror luaL_errorC = Marshal.GetDelegateForFunctionPointer<luaerror>(new IntPtr(handle.ToInt64() + Offsets.luaL_error));
		public register luaL_register = Marshal.GetDelegateForFunctionPointer<register>(new IntPtr(handle.ToInt64() + Offsets.luaL_register));
		public StatePointer luaL_sandboxthread = Marshal.GetDelegateForFunctionPointer<StatePointer>(new IntPtr(handle.ToInt64() + Offsets.luaL_sandboxthread));

		public luauload luau_load = Marshal.GetDelegateForFunctionPointer<luauload>(new IntPtr(handle.ToInt64() + Offsets.luau_load));

		public string lua_checkstring(IntPtr thread, int idx)
		{
			int size = 0;
			IntPtr str = luaL_checklstring(thread, idx, ref size);
			return Marshal.PtrToStringUTF8(str, size);
		}

		public byte[] lua_checkstringB(IntPtr thread, int idx)
		{
			int size = 0;
			IntPtr str = luaL_checklstring(thread, idx, ref size);
			byte[] ret = new byte[size];
			Marshal.Copy(str, ret, 0, size);
			return ret;
		}

		public IntPtr lua_getfield(IntPtr thread, int idx, string str)
		{
			IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
			IntPtr res = getfield(thread, idx, strPtr);
			Marshal.FreeCoTaskMem(strPtr);
			return res;
		}

		public IntPtr lua_getglobal(IntPtr thread, string str)
		{
			return lua_getfield(thread, Offsets.LUA_GLOBALSINDEX, str);
		}
		public bool lua_isnoneornil(IntPtr thread, int idx)
		{
			return lua_type(thread, idx) <= (int)lua_Type.LUA_TNIL;
		}
		public void lua_limittop(IntPtr thread, int max)
		{
			if (LuauPluginRaw.GetTop(thread) > max)
			{
				lua_settop(thread, max);
			}
		}

		public bool lua_getinfo(IntPtr thread, int idx, string str, IntPtr debug)
		{
			IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
			bool res = lua_getinfoC(thread, idx, strPtr, debug);
			Marshal.FreeCoTaskMem(strPtr);
			return res;
		}

		public void lua_pushlstringB(IntPtr luaState, byte[] bytes)
		{
			IntPtr data = Marshal.AllocCoTaskMem(bytes.Length);
			Marshal.Copy(bytes, 0, data, bytes.Length);
			lua_pushlstring(luaState, data, bytes.Length);
			Marshal.FreeCoTaskMem(data);
		}

		public void lua_setfield(IntPtr thread, int idx, string str)
		{
			IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
			setfield(thread, idx, strPtr);
			Marshal.FreeCoTaskMem(strPtr);
		}
		public void lua_setglobal(IntPtr thread, string str)
		{
			lua_setfield(thread, Offsets.LUA_GLOBALSINDEX, str);
		}

		public string lua_tostring(IntPtr thread, int idx)
		{
			int size = 0;
			IntPtr str = lua_tolstring(thread, idx, ref size);
			return Marshal.PtrToStringUTF8(str, size);
		}
		public IntPtr luaL_argerrorL(IntPtr thread, int idx, string str)
		{
			IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
			IntPtr res = luaL_argerror(thread, idx, strPtr);
			Marshal.FreeCoTaskMem(strPtr);
			return res;
		}

		public IntPtr luaL_checkudata(IntPtr thread, int idx, string str)
		{
			IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
			IntPtr res = luaL_checkudataC(thread, idx, strPtr);
			Marshal.FreeCoTaskMem(strPtr);
			return res;
		}

		public IntPtr luaL_error(IntPtr thread, string str)
		{
			IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
			IntPtr res = luaL_errorC(thread, strPtr);
			Marshal.FreeCoTaskMem(strPtr);
			return res;
		}
		public bool luaL_optboolean(IntPtr thread, int idx, bool extra)
		{
			return lua_isnoneornil(thread, idx) ? extra : luaL_checkboolean(thread, idx);
		}

		public string luaL_optlstring(IntPtr thread, int idx, string extra)
		{
			return lua_isnoneornil(thread, idx) ? extra : lua_checkstring(thread, idx);
		}

		public IntPtr luaL_typeerrorL(IntPtr thread, int idx, string str)
		{
			IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
			IntPtr res = luaL_typeerror(thread, idx, strPtr);
			Marshal.FreeCoTaskMem(strPtr);
			return res;
		}

		public int YieldThread(IntPtr luaState, Func<Task<Action>> callback)
		{
			AwaitingTask realTask = new AwaitingTask
			{
				Thread = luaState,
				ThreadRef = 0
			};

			realTask.Task = Task.Run(callback);
			if (realTask.Task.IsCompleted)
			{
				if (realTask.Task.IsFaulted)
				{
					luaL_error(luaState, "lol exception");
					return 0;
				}

				TaskPatch.ResumeAsyncTask(realTask, true);
				return 0;
			}

			LuauPluginRaw.PushThread(luaState);
			realTask.ThreadRef = LuauPluginRaw.Ref(luaState, -1);
			LuauPluginRaw.Pop(luaState, 1);
			Hindenburg.awaitingTasks.Add(realTask);

			return lua_yield(luaState, 0);
		}
	}
}
