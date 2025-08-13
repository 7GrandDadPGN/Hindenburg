using Il2CppLuau;

namespace HindenburgDll
{
	internal class Offsets
	{
		public static int LUA_REGISTRYINDEX = -8000 - 2000;
		public static int LUA_ENVIRONINDEX = -8000 - 2001;
		public static int LUA_GLOBALSINDEX = -8000 - 2002;

		public static int wrap_tointeger = 0x20a00;

		public static int lua_iscfunction = 0x90a60;
		public static int luaA_pushobject = 0x8ffa0;
		public static int lua_isnumber = 0x90ad0;
		public static int lua_isstring = 0x90b70;
		public static int lua_getfield = 0x906d0;
		public static int lua_getmetatable = 0x907b0;
		public static int lua_getreadonly = 0x90880;
		public static int lua_newthread = 0x90d40;
		public static int lua_next = 0x90ed0;
		public static int lua_pushlstring = 0x91290;
		public static int lua_pushvalue = 0x91480;
		public static int lua_rawcheckstack = 0x91590;
		public static int lua_rawseti = 0x91a80;
		public static int lua_setfield = 0x91ed0;
		public static int lua_setmetatable = 0x91fb0;
		public static int lua_setsafeenv = 0x92110;
		public static int lua_settop = 0x92220;
		public static int lua_tolstring = 0x92490;
		public static int lua_topointer = 0x92680;
		public static int lua_type = 0x92920;
		public static int lua_xmove = 0x92a10;
		public static int lua_yield = 0x95d60;
		public static int lua_getinfo = 0x96c80;

		public static int luaL_argerrorL = 0x93060;
		public static int luaL_checkany = 0x931e0;
		public static int luaL_checkboolean = 0x93220;
		public static int luaL_checkinteger = 0x932a0;
		public static int luaL_checklstring = 0x932e0;
		public static int luaL_checktype = 0x933a0;
		public static int luaL_checkudata = 0x933e0;
		public static int luaL_error = 0x93500;
		public static int luaL_optlstring = 0x937f0;
		public static int luaL_register = 0x93b00;
		public static int luaL_typeerrorL = 0x93e90;
		public static int luaL_sandboxthread = 0x941d0;

		public static int luaC_barrierback = 0x9bfb0;

		public static int luau_load = 0x97e30;

		public static int luaM_visitgco = 0xaae00;
	}
}
