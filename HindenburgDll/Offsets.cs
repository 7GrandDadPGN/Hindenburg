namespace HindenburgDll
{
	internal class Offsets
	{
		public static int LUA_REGISTRYINDEX = -8000 - 2000;
		public static int LUA_ENVIRONINDEX = -8000 - 2001;
		public static int LUA_GLOBALSINDEX = -8000 - 2002;

		public static int decrypt_routine = 0x23410;
		public static int lua_iscfunction = 0x914e0;
		public static int luaA_pushobject = 0x909e0;
		public static int lua_isnumber = 0x91550;
		public static int lua_isstring = 0x915f0;
		public static int lua_getfield = 0x91150;
		public static int lua_setfield = 0x92950;
		public static int lua_getmetatable = 0x91230;
		public static int lua_getreadonly = 0x91300;
		public static int lua_rawcheckstack = 0x92010;
		public static int lua_rawseti = 0x92500;
		public static int lua_pushlstring = 0x91d10;
		public static int lua_pushvalue = 0x91f00;
		public static int lua_newthread = 0x917c0;
		public static int lua_next = 0x91950;
		public static int lua_setmetatable = 0x92a30;
		public static int lua_setsafeenv = 0x92b90;
		public static int lua_settop = 0x92ca0;
		public static int lua_tointegerx = 0x92df0;
		public static int lua_tolstring = 0x92f10;
		public static int lua_topointer = 0x93100;
		public static int lua_type = 0x933a0;
		public static int lua_xmove = 0x93470;
		public static int lua_yield = 0x96840;
		public static int lua_getinfo = 0x97540;
		public static int luaL_argerrorL = 0x93ad0;
		public static int luaL_typeerrorL = 0x94900;
		public static int luaL_checkany = 0x93c50;
		public static int luaL_checktype = 0x93e10;
		public static int luaL_checkboolean = 0x93c90;
		public static int luaL_checklstring = 0x93d50;
		public static int luaL_checkinteger = 0x93d10;
		public static int luaL_checkudata = 0x93e50;
		public static int luaL_errorL = 0x93f70;
		public static int luaL_register = 0x94570;
		public static int luaL_sandboxthread = 0x94c40;
		public static int luaC_barrierback = 0x9b980;
		public static int luau_load = 0x986c0;
		public static int luau_compile = 0xf7630;
		public static int luaM_visitgco = 0xaab50;
	}
}
