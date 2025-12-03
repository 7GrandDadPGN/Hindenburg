using HindenburgDll.Patches;
using HindenburgDll.Structs;
using Il2Cpp;
using Il2CppLuau;
using System.Runtime.InteropServices;

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
		public delegate int StateAndIdAndSizeInteger(IntPtr luaState, int idx, IntPtr size);

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
		public delegate IntPtr register(IntPtr luaState, IntPtr libName, luaL_Reg[] luaReg);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr luauload(IntPtr luaState, IntPtr chunkName, IntPtr bytecode, int bytecodeSize, int native);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr luaucompile(IntPtr code, int idx, IntPtr options, ref int size);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool getinfo(IntPtr luaState, int idx, IntPtr str, IntPtr debug);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr barrierback(IntPtr luaState, IntPtr luaStateGC, IntPtr gclist);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr visitgco(IntPtr luaState, IntPtr gcx, IntPtr callback);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool gcovoid(IntPtr gcx, IntPtr luaPage, IntPtr gcObj);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr decrypt(IntPtr data, long dataSize, IntPtr key, ref long size);

		public static TDelegate GetFunction<TDelegate>(string sig)
		{
			int offset = AOBDumper.ScanFunction(sig);
			if (offset == 0)
			{
				MelonLoader.MelonLogger.Msg($"failed! {sig}");
			}

			return Marshal.GetDelegateForFunctionPointer<TDelegate>(new IntPtr(handle.ToInt64() + offset));
		}

		public decrypt decrypt_routine = GetFunction<decrypt>("4C 89 4C 24 20 53 55 57 41 54 41 56 41 57 48 83");
		public StateAndIdBool lua_iscfunction = GetFunction<StateAndIdBool>("48 83 EC 28 85 D2 7E 23 4C 8B 41 10 48 8D 05 ?? ?? 0A 00 49 83 C0 F0 48 63 D2 48 C1 E2 04 4C 03 C2 4C 3B 41 08 49 0F 42 C0 EB 1A 81 FA F0 D8 FF FF 7E 0D 48 63 C2 48 C1 E0 04 48 03 41 08 EB 05 E8 ?? ?? 00 00 83 78 0C 07 75 13 48 8B");
		public luaerror luaA_pushobject = GetFunction<luaerror>("48 8B 41 08 0F 10 02 0F 11 00 48 83 41 08 10 C3 48 83 EC 28 4C 8D 15 ?? ?? 0A 00 85 D2 7E");
		public StateAndIdPointer luaA_toobject = GetFunction<StateAndIdPointer>("48 83 EC 28 4C 8D 15 ?? ?? 0A 00 85 D2 7E");
		public StateAndIdBool lua_isnumber = GetFunction<StateAndIdBool>("48 83 EC 48 48 8B 05 ?? ?? 0D 00 48 33 C4 48 89 44 24 30 4C 8B C1");
		public StateAndIdBool lua_isstring = GetFunction<StateAndIdBool>("48 83 EC 28 85 D2 7E 1F");
		private FieldPointer getfield = GetFunction<FieldPointer>("48 89 5C 24 20 55 56 57 48 83 EC 40 48 8B 05 ?? ?? 0D 00 48 33 C4 48 89 44 24 30 F6 41");
		private FieldPointer setfield = GetFunction<FieldPointer>("48 89 5C 24 20 55 56 57 48 83 EC 40 48 8B 05 ?? ?? 0D 00 48 33 C4 48 89 44 24 30 49 8B");
		public StateAndIdPointer lua_getmetatable = GetFunction<StateAndIdPointer>("48 89 5C 24 08 57 48 83 EC 20 F6 41 01 04 48 8B D9 48 63 FA 74 0C 4C 8D 41 68 48 8B D1 E8 6E B1");
		public StateAndIdBool lua_getreadonly = GetFunction<StateAndIdBool>("48 83 EC 28 85 D2 7E 2D");
		public StateAndIdInteger lua_rawcheckstack = GetFunction<StateAndIdInteger>("48 89 5C 24 08 57 48 83 EC 20 4C 8B 41 28");
		public StackIndexPointer lua_rawseti = GetFunction<StackIndexPointer>("48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 20 45");
		public StringPointer lua_pushlstring = GetFunction<StringPointer>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 20 4C 8B 49 18 49 8B");
		public StateAndIdPointer lua_pushvalue = GetFunction<StateAndIdPointer>("48 89 5C 24 08 57 48 83 EC 20 F6 41 01 04 48 8B D9 48 63 FA 74 0C 4C 8D 41 68 48 8B D1 E8 0E A4");
		public StatePointer lua_newthread = GetFunction<StatePointer>("48 89 5C 24 08 57 48 83 EC 20 48 8B 51 18");
		public StateAndIdInteger lua_next = GetFunction<StateAndIdInteger>("48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 20 F6 41 01 04 48 8B D9 48 63 F2 74 0C 4C 8D 41 68 48 8B D1 E8 B9");
		public StateAndIdPointer lua_setmetatable = GetFunction<StateAndIdPointer>("40 53 48 83 EC 20 48 8D 59");

		public setsafeenv lua_setsafeenv = GetFunction<setsafeenv>("48 83 EC 28 45 8B D0 85 D2 7E 32 4C 8B 49 10 48 8D 05 ?? ?? 0A 00 49 83 C1 F0 48 63 D2 48 C1 E2 04 4C 03 CA 4C 3B 49 08 49 0F 42 C1 45 85 C0 0F 95 C1 48 8B 00 88 48 05");
		public StateAndIdPointer lua_settop = GetFunction<StateAndIdPointer>("85 D2 78 3F 4C 8B");
		public StateAndIdAndSizeInteger lua_tointegerx = GetFunction<StateAndIdAndSizeInteger>("40 53 48 83 EC 40 48 8B 05 ?? ?? 0D 00 48 33 C4 48 89 44 24 30 49 8B D8 4C 8B C1 85 D2 7E 23 48 63 CA 48 8D 05 ?? ?? 0A 00 49 8B 50 10 48 83 C2 F0 48 C1 E1 04 48 03 D1 49 3B 50 08 48 0F 42 C2 EB 1A 81 FA F0 D8 FF FF 7E 0D 48 63 C2 48 C1 E0 04 48 03 41 08 EB 05 E8 B4 ?? 00 00 83 78 0C 03");
		public StringReturn lua_tolstring = GetFunction<StringReturn>("48 89 5C 24 08 48 89 74 24 10 48 89 7C 24 18 41 56 48 83 EC 20 48 63 F2");
		public StateAndIdPointer lua_topointer = GetFunction<StateAndIdPointer>("48 83 EC 28 85 D2 7E 23 4C 8B 41 10 48 8D 05 ?? ?? 0A 00 49 83 C0 F0 48 63 D2 48 C1 E2 04 4C 03 C2 4C 3B 41 08 49 0F 42 C0 EB 1A 81 FA F0 D8 FF FF 7E 0D 48 63 C2 48 C1 E0 04 48 03 41 08 EB 05 E8 ?? ?? 00 00 8B 48 0C 83 F9 02 74 1D 83 F9 08");
		public StateAndIdInteger lua_type = GetFunction<StateAndIdInteger>("48 83 EC 28 85 D2 7E 1A 48");
		public xmove lua_xmove = GetFunction<xmove>("48 3B CA 0F 84 84 00 00 00 48 89");
		public StateAndIdInteger lua_yield = GetFunction<StateAndIdInteger>("48 83 EC 28 0F B7 41 52 4C 8B C1 66 39 41 50");
		public getinfo lua_getinfoC = GetFunction<getinfo>("48 89 5C 24 10 48 89 74 24 18 57 41 56 41 57 48 83 EC 30");

		public FieldPointer luaL_argerror = GetFunction<FieldPointer>("48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 30 49 8B F8 8B F2");
		public FieldPointer luaL_typeerror = GetFunction<FieldPointer>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 30 49 8B E8 8B F2");
		public StateAndIdPointer luaL_checkany = GetFunction<StateAndIdPointer>("48 89 5C 24 08 57 48 83 EC 20 8B DA 48 8B F9 E8 CC F6 FF FF");
		public StackIndexPointer luaL_checktype = GetFunction<StackIndexPointer>("48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 20 41 8B D8 8B FA 48 8B F1 E8 04 F5 FF FF");
		public StateAndIdBool luaL_checkboolean = GetFunction<StateAndIdBool>("48 89 5C 24 08 57 48 83 EC 20 8B DA 48 8B F9 E8 8C F6 FF FF");
		public StringReturn luaL_checklstring = GetFunction<StringReturn>("48 89 5C 24 08 57 48 83 EC 20 8B DA 48 8B F9 E8 3C EF FF FF");
		public StateAndIdInteger luaL_checkinteger = GetFunction<StateAndIdInteger>("48 89 5C 24 08 57 48 83 EC 20 4C 8D 44 24 40 8B DA 48 8B F9 E8 57 ?? FF");
		public FieldPointer luaL_checkudataC = GetFunction<FieldPointer>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 20 49 8B E8 8B");

		public luaerror luaL_errorC = GetFunction<luaerror>("48 89 54 24 10 4C 89 44 24 18 4C 89 4C 24 20 53 57 48 83 EC 28 BA 01 00");
		public register luaL_register = GetFunction<register>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 48 89 7C 24 20 41 56 48 83 EC 30 45 33 F6 49 8B D8");
		public StatePointer luaL_sandboxthread = GetFunction<StatePointer>("40 53 48 83 EC 20 45 33 C0 33 D2 48 8B D9 E8");

		public barrierback luaC_barrierback = GetFunction<barrierback>("4C 8B 49 18 80 62 01 FB 49 8B 41 30 49 89");

		public luauload luau_load = GetFunction<luauload>("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 41 56 41 57 48 81 EC 80 00 00 00 49 8B E9 4D 8B");
		public luaucompile luau_compile = GetFunction<luaucompile>("40 55 53 56 57 41 54 41 56 41 57 48 8D 6C 24 C0");

		public visitgco luaM_visitgco = GetFunction<visitgco>("40 56 41 54 41 57 48 83 EC 30 48 8B 41 18");

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
		public int lua_tointeger(IntPtr thread, int idx)
		{
			return lua_tointegerx(thread, idx, IntPtr.Zero);
		}
		public void luaC_threadbarrier(IntPtr thread)
		{
			if ((Marshal.ReadByte(thread, 1) & 4) != 0) // marked bit in common header, 4 is the black bit
			{
				luaC_barrierback(thread, thread, new IntPtr(thread.ToInt64() + 13)); // gc list
			}
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

		public bool iscollectable(int type)
		{
			return type >= (int)lua_Type.LUA_TSTRING;
		}

		public byte[] FixBytecode(byte[] bytecode)
		{
			IntPtr keyString = Marshal.StringToCoTaskMemUTF8("afea643bcd75491f");
			IntPtr data = Marshal.AllocCoTaskMem(bytecode.Length);
			Marshal.Copy(bytecode, 0, data, bytecode.Length);

			long size = 0;
			IntPtr tData = decrypt_routine(data + 8, bytecode.Length - 8, keyString, ref size);

			byte[] fixedBytecode = new byte[size];
			Marshal.Copy(tData, fixedBytecode, 0, (int)size);

			Marshal.FreeCoTaskMem(keyString);
			Marshal.FreeCoTaskMem(data);
			Marshal.FreeCoTaskMem(tData);
			return fixedBytecode;
		}

		public void PushUnityObject(IntPtr luaState, Il2CppSystem.Object obj)
		{
			int id = ThreadDataManager.AddObjectReference(luaState, obj);
			if (!GarbageCollectorPatch.gcList.ContainsKey(id))
			{
				GarbageCollectorPatch.gcList.Add(id, obj);
			}

			LuauCore.WritePropertyToThread(luaState, obj, obj.GetIl2CppType());
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

				Patches.TaskSchedulerPatch.ResumeAsyncTask(realTask, true);
				return 0;
			}

			LuauPluginRaw.PushThread(luaState);
			realTask.ThreadRef = LuauPluginRaw.Ref(luaState, -1);
			LuauPluginRaw.Pop(luaState, 1);
			Patches.TaskSchedulerPatch.awaitingTasks.Add(realTask);

			return lua_yield(luaState, 0);
		}
	}
}
