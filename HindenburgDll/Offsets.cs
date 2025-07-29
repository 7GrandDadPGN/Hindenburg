using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HindenburgDll
{
    internal class Offsets
    {
        public static int LUA_REGISTRYINDEX = -8000 - 2000;
        public static int LUA_ENVIRONINDEX = -8000 - 2001;
        public static int LUA_GLOBALSINDEX = -8000 - 2002;

        public static int luaAirship_require = 0x10610;

        public static int lua_iscfunction = 0x8ff00;
        public static int lua_getfield = 0x8fb70;
        public static int lua_getmetatable = 0x8fc50;
        public static int lua_getreadonly = 0x8fd20;
        public static int lua_newthread = 0x901e0;
        public static int lua_pushvalue = 0x90920;
        public static int lua_rawseti = 0x90f20;
        public static int lua_setfield = 0x91370;
        public static int lua_setmetatable = 0x91450;
        public static int lua_setsafeenv = 0x915b0;
        public static int lua_settop = 0x916c0;
        public static int lua_type = 0x91dc0;
        public static int lua_xmove = 0x91eb0;

        public static int luaL_checkany = 0x92680;
        public static int luaL_checkboolean = 0x926c0;
        public static int luaL_checklstring = 0x92780;
        public static int luaL_checktype = 0x92840;
        public static int luaL_checkudata = 0x92880;
        public static int luaL_optlstring = 0x92c90;
        public static int luaL_register = 0x92fa0;
        public static int luaL_sandboxthread = 0x93670;

        public static int luau_load = 0x972d0;
    }
}
