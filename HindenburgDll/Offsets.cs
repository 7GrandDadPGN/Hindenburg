namespace HindenburgDll
{
	internal class Offsets
	{
		public static int LUA_REGISTRYINDEX = -8000 - 2000;
		public static int LUA_ENVIRONINDEX = -8000 - 2001;
		public static int LUA_GLOBALSINDEX = -8000 - 2002;

		public static int luaAirship_require = 0x10610;

		public static int wrap_tointeger = 0x20030;

		public static int lua_iscfunction = 0x8ff00;
		public static int luaA_pushobject = 0x8f440;
		public static int lua_isnumber = 0x8ff70;
		public static int lua_isstring = 0x90010;
		public static int lua_getfield = 0x8fb70;
		public static int lua_getmetatable = 0x8fc50;
		public static int lua_getreadonly = 0x8fd20;
		public static int lua_newthread = 0x901e0;
		public static int lua_next = 0x90370;
		public static int lua_pushlstring = 0x90730;
		public static int lua_pushvalue = 0x90920;
		public static int lua_rawseti = 0x90f20;
		public static int lua_setfield = 0x91370;
		public static int lua_setmetatable = 0x91450;
		public static int lua_setsafeenv = 0x915b0;
		public static int lua_settop = 0x916c0;
		public static int lua_tolstring = 0x91930;
		public static int lua_topointer = 0x91b20;
		public static int lua_type = 0x91dc0;
		public static int lua_xmove = 0x91eb0;
		public static int lua_yield = 0x95200;
		public static int lua_getinfo = 0x96120;

		public static int luaL_argerrorL = 0x92500;
		public static int luaL_checkany = 0x92680;
		public static int luaL_checkboolean = 0x926c0;
		public static int luaL_checkinteger = 0x92740;
		public static int luaL_checklstring = 0x92780;
		public static int luaL_checktype = 0x92840;
		public static int luaL_checkudata = 0x92880;
		public static int luaL_error = 0x929a0;
		public static int luaL_optlstring = 0x92c90;
		public static int luaL_register = 0x92fa0;
		public static int luaL_typeerrorL = 0x93330;
		public static int luaL_sandboxthread = 0x93670;

		public static int luau_load = 0x972d0;
	}
}
