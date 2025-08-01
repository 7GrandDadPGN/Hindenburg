using HindenburgDll.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HindenburgDll.Utils.CompileUtils;

namespace HindenburgDll.Functions
{
	internal class Debug : FunctionHolder
	{
		private static int getconstants(IntPtr luaState)
		{
			api.luaL_checkany(luaState, 1);

			if (api.lua_type(luaState, 1) != (int)LuaIApi.lua_Type.LUA_TFUNCTION && !api.lua_isnumber(luaState, 1))
			{
				api.luaL_argerrorL(luaState, 1, "function or level expected");
			}



			return 1;
		}

		public override void CreateDefinitions()
		{
			Add("getconstants", getconstants);
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
