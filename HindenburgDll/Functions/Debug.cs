using HindenburgDll.Structs;
using HindenburgDll.Utils;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
				IntPtr kPtr = new IntPtr(upvalues.ToInt64() + i * Marshal.SizeOf<TValue>());
				api.luaA_pushobject(luaState, kPtr);
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

			IntPtr kPtr = new IntPtr(upvalues.ToInt64() + (index - 1) * Marshal.SizeOf<TValue>());
			api.luaA_pushobject(luaState, kPtr);

			return 1;
		}

		public override void CreateDefinitions()
		{
			Add("getconstants", getconstants);
			Add("getconstant", getconstant);
			Add("getupvalues", getupvalues);
			Add("getupvalue", getupvalue);
			luaReg.Add(new luaL_Reg { name = IntPtr.Zero, func = IntPtr.Zero });
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
