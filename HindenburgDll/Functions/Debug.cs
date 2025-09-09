using HindenburgDll.Structs;
using HindenburgDll.Utils;
using Il2Cpp;
using System.Runtime.InteropServices;
using static HindenburgDll.Utils.CompileUtils;

namespace HindenburgDll.Functions
{
	internal class Debug : FunctionHolder
	{
		private static int getconstants(IntPtr luaState)
		{
			if (api.lua_type(luaState, 1) != (int)LuaIApi.lua_Type.LUA_TFUNCTION && !api.lua_isnumber(luaState, 1))
			{
				api.luaL_argerrorL(luaState, 1, "function or level expected");
			}

			if (api.lua_isnumber(luaState, 1))
			{
				IntPtr allocation = Marshal.AllocCoTaskMem(Marshal.SizeOf<lua_Debug>());
				bool success = api.lua_getinfo(luaState, api.lua_tointeger(luaState, 1), "f", allocation);
				Marshal.FreeCoTaskMem(allocation);

				if (!success)
				{
					api.luaL_argerrorL(luaState, 1, "level out of range");
				}
			}
			else
			{
				api.lua_pushvalue(luaState, 1);
			}

			if (api.lua_iscfunction(luaState, -1))
			{
				api.luaL_argerrorL(luaState, 1, "lua function expected");
			}

			lua_closure closure = Marshal.PtrToStructure<lua_closure>(api.lua_topointer(luaState, -1));
			Proto proto = Marshal.PtrToStructure<Proto>(closure.closures.l.p);
			LuauPluginRaw.NewTable(luaState);

			for (int i = 0; i < proto.sizek; i++)
			{
				IntPtr kPtr = new IntPtr(proto.k.ToInt64() + i * Marshal.SizeOf<TValue>());
				TValue k = Marshal.PtrToStructure<TValue>(kPtr);

				if (k.tt == (int)LuaIApi.lua_Type.LUA_TFUNCTION)
				{
					LuauPluginRaw.PushNil(luaState);
				}
				else
				{
					api.luaA_pushobject(luaState, kPtr);
				}

				api.lua_rawseti(luaState, -2, i + 1);
			}

			return 1;
		}
		private static int getconstant(IntPtr luaState)
		{
			if (api.lua_type(luaState, 1) != (int)LuaIApi.lua_Type.LUA_TFUNCTION && !api.lua_isnumber(luaState, 1))
			{
				api.luaL_argerrorL(luaState, 1, "function or level expected");
			}

			int index = api.luaL_checkinteger(luaState, 2);
			if (api.lua_isnumber(luaState, 1))
			{
				IntPtr allocation = Marshal.AllocCoTaskMem(Marshal.SizeOf<lua_Debug>());
				bool success = api.lua_getinfo(luaState, api.lua_tointeger(luaState, 1), "f", allocation);
				Marshal.FreeCoTaskMem(allocation);

				if (!success)
				{
					api.luaL_argerrorL(luaState, 1, "level out of range");
				}
			}
			else
			{
				api.lua_pushvalue(luaState, 1);
			}

			if (api.lua_iscfunction(luaState, -1))
			{
				api.luaL_argerrorL(luaState, 1, "lua function expected");
			}

			lua_closure closure = Marshal.PtrToStructure<lua_closure>(api.lua_topointer(luaState, -1));
			Proto proto = Marshal.PtrToStructure<Proto>(closure.closures.l.p);

			if (index < 1)
			{
				api.luaL_argerrorL(luaState, 2, "constant index starts at 1");
			}

			if (index > proto.sizek)
			{
				api.luaL_argerrorL(luaState, 2, "constant index is out of range");
			}

			IntPtr kPtr = new IntPtr(proto.k.ToInt64() + (index - 1) * Marshal.SizeOf<TValue>());
			TValue k = Marshal.PtrToStructure<TValue>(kPtr);

			if (api.iscollectable(k.tt))
			{
				api.luaC_threadbarrier(luaState);
			}

			if (k.tt == (int)LuaIApi.lua_Type.LUA_TFUNCTION)
			{
				LuauPluginRaw.PushNil(luaState);
			}
			else
			{
				api.luaA_pushobject(luaState, kPtr);
			}

			return 1;
		}

		private static int setconstant(IntPtr luaState)
		{
			if (api.lua_type(luaState, 1) != (int)LuaIApi.lua_Type.LUA_TFUNCTION && !api.lua_isnumber(luaState, 1))
			{
				api.luaL_argerrorL(luaState, 1, "function or level expected");
			}

			int index = api.luaL_checkinteger(luaState, 2);
			if (api.lua_isnumber(luaState, 1))
			{
				IntPtr allocation = Marshal.AllocCoTaskMem(Marshal.SizeOf<lua_Debug>());
				bool success = api.lua_getinfo(luaState, api.lua_tointeger(luaState, 1), "f", allocation);
				Marshal.FreeCoTaskMem(allocation);

				if (!success)
				{
					api.luaL_argerrorL(luaState, 1, "level out of range");
				}
			}
			else
			{
				api.lua_pushvalue(luaState, 1);
			}

			if (api.lua_iscfunction(luaState, -1))
			{
				api.luaL_argerrorL(luaState, 1, "lua function expected");
			}

			lua_closure closure = Marshal.PtrToStructure<lua_closure>(api.lua_topointer(luaState, -1));
			Proto proto = Marshal.PtrToStructure<Proto>(closure.closures.l.p);

			if (index < 1)
			{
				api.luaL_argerrorL(luaState, 2, "constant index starts at 1");
			}

			if (index > proto.sizek)
			{
				api.luaL_argerrorL(luaState, 2, "constant index is out of range");
			}

			IntPtr obj = api.luaA_toobject(luaState, 3);
			TValue replacement = Marshal.PtrToStructure<TValue>(obj);
			IntPtr kPtr = new IntPtr(proto.k.ToInt64() + (index - 1) * Marshal.SizeOf<TValue>());
			TValue k = Marshal.PtrToStructure<TValue>(kPtr);

			if (api.iscollectable(replacement.tt))
			{
				api.luaC_threadbarrier(luaState);
			}

			if (k.tt == replacement.tt && k.tt != (int)LuaIApi.lua_Type.LUA_TFUNCTION)
			{
				Marshal.WriteIntPtr(kPtr, replacement.value.p);
				Marshal.WriteInt32(kPtr + 8, replacement.extra);
			}
			else
			{
				api.luaL_argerrorL(luaState, 3, "mismatched type");
			}

			return 0;
		}

		private static int getupvalues(IntPtr luaState)
		{
			api.lua_limittop(luaState, 2);

			if (api.lua_type(luaState, 1) != (int)LuaIApi.lua_Type.LUA_TFUNCTION && !api.lua_isnumber(luaState, 1))
			{
				api.luaL_argerrorL(luaState, 1, "function or level expected");
			}

			if (api.lua_isnumber(luaState, 1))
			{
				IntPtr allocation = Marshal.AllocCoTaskMem(Marshal.SizeOf<lua_Debug>());
				bool success = api.lua_getinfo(luaState, api.lua_tointeger(luaState, 1), "f", allocation);
				Marshal.FreeCoTaskMem(allocation);

				if (!success)
				{
					api.luaL_argerrorL(luaState, 1, "level out of range");
				}
			}
			else
			{
				api.lua_pushvalue(luaState, 1);
			}

			if (api.lua_iscfunction(luaState, -1))
			{
				api.luaL_argerrorL(luaState, 1, "lua function expected");
			}

			IntPtr pointer = api.lua_topointer(luaState, -1);
			lua_closure closure = Marshal.PtrToStructure<lua_closure>(pointer);
			IntPtr upvalues = new IntPtr(pointer.ToInt64() + Marshal.SizeOf<blank_closure>() + (closure.isC != 0 ? Marshal.SizeOf<c_closure>() : Marshal.SizeOf<l_closure>()));
			LuauPluginRaw.NewTable(luaState);

			for (int i = 0; i < closure.nupvalues; i++)
			{
				IntPtr upPtr = new IntPtr(upvalues.ToInt64() + i * Marshal.SizeOf<TValue>());
				TValue up = Marshal.PtrToStructure<TValue>(upPtr);

				if (up.tt == (int)LuaIApi.lua_Type.LUA_TUPVAL)
				{
					UpVal pUp = Marshal.PtrToStructure<UpVal>(up.value.gc);
					up = Marshal.PtrToStructure<TValue>(pUp.v);
					upPtr = pUp.v;
				}

				api.lua_rawcheckstack(luaState, 1);
				if (api.iscollectable(up.tt))
				{
					api.luaC_threadbarrier(luaState);
				}

				api.luaA_pushobject(luaState, upPtr);
				api.lua_rawseti(luaState, -2, i + 1);
			}

			return 1;
		}

		private static int getupvalue(IntPtr luaState)
		{
			api.lua_limittop(luaState, 2);

			if (api.lua_type(luaState, 1) != (int)LuaIApi.lua_Type.LUA_TFUNCTION && !api.lua_isnumber(luaState, 1))
			{
				api.luaL_argerrorL(luaState, 1, "function or level expected");
			}

			int index = api.luaL_checkinteger(luaState, 2);
			if (api.lua_isnumber(luaState, 1))
			{
				IntPtr allocation = Marshal.AllocCoTaskMem(Marshal.SizeOf<lua_Debug>());
				bool success = api.lua_getinfo(luaState, api.lua_tointeger(luaState, 1), "f", allocation);
				Marshal.FreeCoTaskMem(allocation);

				if (!success)
				{
					api.luaL_argerrorL(luaState, 1, "level out of range");
				}
			}
			else
			{
				api.lua_pushvalue(luaState, 1);
			}

			IntPtr pointer = api.lua_topointer(luaState, -1);
			lua_closure closure = Marshal.PtrToStructure<lua_closure>(pointer);
			IntPtr upvalues = new IntPtr(pointer.ToInt64() + Marshal.SizeOf<blank_closure>() + (closure.isC != 0 ? Marshal.SizeOf<c_closure>() : Marshal.SizeOf<l_closure>()));

			if (index < 1)
			{
				api.luaL_argerrorL(luaState, 2, "upvalue index starts at 1");
			}

			if (index > closure.nupvalues)
			{
				api.luaL_argerrorL(luaState, 2, "upvalue index is out of range");
			}

			IntPtr upPtr = new IntPtr(upvalues.ToInt64() + (index - 1) * Marshal.SizeOf<TValue>());
			TValue up = Marshal.PtrToStructure<TValue>(upPtr);

			if (up.tt == (int)LuaIApi.lua_Type.LUA_TUPVAL)
			{
				UpVal pUp = Marshal.PtrToStructure<UpVal>(up.value.gc);
				up = Marshal.PtrToStructure<TValue>(pUp.v);
				upPtr = pUp.v;
			}

			api.lua_rawcheckstack(luaState, 1);
			if (api.iscollectable(up.tt))
			{
				api.luaC_threadbarrier(luaState);
			}

			api.luaA_pushobject(luaState, upPtr);

			return 1;
		}

		private static int getinfo(IntPtr luaState)
		{
			api.luaC_threadbarrier(luaState);
			api.luaL_checkany(luaState, 1);

			int level = 0;
			if (api.lua_isnumber(luaState, 1))
			{
				level = api.lua_tointeger(luaState, 1);
				if (level < 0)
				{
					api.luaL_argerrorL(luaState, 1, "level cannot be negative");
				}
			}
			else if (api.lua_type(luaState, 1) == (int)LuaIApi.lua_Type.LUA_TFUNCTION)
			{
				level = -LuauPluginRaw.GetTop(luaState);
			}

			IntPtr allocation = Marshal.AllocCoTaskMem(Marshal.SizeOf<lua_Debug>());
			if (!api.lua_getinfo(luaState, level, "fulasn", allocation))
			{
				api.luaL_argerrorL(luaState, 1, "invalid level");
			}

			lua_Debug data = Marshal.PtrToStructure<lua_Debug>(allocation);
			Marshal.FreeCoTaskMem(allocation);
			LuauPluginRaw.NewTable(luaState);

			LuauPluginRaw.PushString(luaState, Marshal.PtrToStringUTF8(data.source));
			api.lua_setfield(luaState, -2, "source");

			LuauPluginRaw.PushString(luaState, Marshal.PtrToStringUTF8(data.short_src));
			api.lua_setfield(luaState, -2, "short_src");

			api.lua_pushvalue(luaState, 1);
			api.lua_setfield(luaState, -2, "func");

			LuauPluginRaw.PushString(luaState, Marshal.PtrToStringUTF8(data.what));
			api.lua_setfield(luaState, -2, "what");

			LuauPluginRaw.PushInteger(luaState, data.currentline);
			api.lua_setfield(luaState, -2, "currentline");

			LuauPluginRaw.PushString(luaState, Marshal.PtrToStringUTF8(data.name) ?? "");
			api.lua_setfield(luaState, -2, "name");

			LuauPluginRaw.PushInteger(luaState, data.nupvals);
			api.lua_setfield(luaState, -2, "nups");

			LuauPluginRaw.PushInteger(luaState, data.nparams);
			api.lua_setfield(luaState, -2, "numparams");

			LuauPluginRaw.PushInteger(luaState, data.isvararg);
			api.lua_setfield(luaState, -2, "is_vararg");

			return 1;
		}

		public override void CreateDefinitions()
		{
			Add("getconstant", getconstant);
			Add("getconstants", getconstants);
			Add("getinfo", getinfo);
			Add("getupvalue", getupvalue);
			Add("getupvalues", getupvalues);
			Add("setconstant", setconstant);
		}

		public override string LibraryName()
		{
			return "debug";
		}

		public override bool PushToGlobal()
		{
			return false;
		}
	}
}
