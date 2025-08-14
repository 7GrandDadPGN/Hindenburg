using Il2CppLuau;

namespace HindenburgDll
{
	internal class Offsets
	{
		public static int LUA_REGISTRYINDEX = -8000 - 2000;
		public static int LUA_ENVIRONINDEX = -8000 - 2001;
		public static int LUA_GLOBALSINDEX = -8000 - 2002;

		public static int luaAirship_print = 0x109b0;
		public static int luaAirship_warn = 0x10bc0;
		public static int luaAirship_error = 0x10dc0;
		public static int luaAirship_require = 0x10e80;
		public static int luaAirship_collectgarbage = 0x114a0;
		public static int wrap_tointeger = 0x20a00;
		public static int wrap_unityobject = 0x20a10;

		public static int luaA_pushobject = 0x8ffa0;
		public static int luaA_toobject = 0x8ffb0;

		public static int lua_absindex = 0x90020;
		public static int lua_call = 0x90050;
		public static int lua_callbacks = 0x900a0;
		public static int lua_checkstack = 0x900b0;
		public static int lua_cleartable = 0x90150;
		public static int lua_concat = 0x901c0;
		public static int lua_costatus = 0x90280;
		public static int lua_createtable = 0x902d0;
		public static int lua_encodepointer = 0x90350;
		public static int lua_equal = 0x90380;
		public static int lua_error = 0x90470;
		public static int lua_gc = 0x90480;
		public static int lua_getfenv = 0x90600;
		public static int lua_getfield = 0x906d0;
		public static int lua_getmetatable = 0x907b0;
		public static int lua_getreadonly = 0x90880;
		public static int lua_gettable = 0x908f0;
		public static int lua_getthreaddata = 0x90990;
		public static int lua_gettop = 0x909a0;
		public static int lua_insert = 0x909b0;
		public static int lua_iscfunction = 0x90a60;
		public static int lua_isnumber = 0x90ad0;
		public static int lua_isstring = 0x90b70;
		public static int lua_isuserdata = 0x90be0;
		public static int lua_mainthread = 0x90c50;
		public static int lua_namecallatom = 0x90c60;
		public static int lua_newbuffer = 0x90cd0;
		public static int lua_newthread = 0x90d40;
		public static int lua_newuserdatadtor = 0x90dc0;
		public static int lua_newuserdatatagged = 0x90e50;
		public static int lua_next = 0x90ed0;
		public static int lua_objlen = 0x90f80;
		public static int lua_pcall = 0x91010;
		public static int lua_pushboolean = 0x910f0;
		public static int lua_pushcclosurek = 0x91110;
		public static int lua_pushfstringL = 0x911f0;
		public static int lua_pushinteger = 0x91250;
		public static int lua_pushlightuserdatatagged = 0x91270;
		public static int lua_pushlstring = 0x91290;
		public static int lua_pushnil = 0x91310;
		public static int lua_pushnumber = 0x91330;
		public static int lua_pushstring = 0x91350;
		public static int lua_pushthread = 0x91400;
		public static int lua_pushunsigned = 0x91450;
		public static int lua_pushvalue = 0x91480;
		public static int lua_pushvector = 0x91510;
		public static int lua_pushvfstring = 0x91530;
		public static int lua_rawcheckstack = 0x91590;
		public static int lua_rawequal = 0x915e0;
		public static int lua_rawget = 0x916b0;
		public static int lua_rawgetfield = 0x91750;
		public static int lua_rawgeti = 0x91810;
		public static int lua_rawset = 0x918c0;
		public static int lua_rawsetfield = 0x91990;
		public static int lua_rawseti = 0x91a80;
		public static int lua_ref = 0x91b50;
		public static int lua_remove = 0x91c60;
		public static int lua_replace = 0x91cf0;
		public static int lua_setfenv = 0x91e10;
		public static int lua_setfield = 0x91ed0;
		public static int lua_setmemcat = 0x91fa0;
		public static int lua_setmetatable = 0x91fb0;
		public static int lua_setreadonly = 0x92090;
		public static int lua_setsafeenv = 0x92110;
		public static int lua_settable = 0x92190;
		public static int lua_setthreaddata = 0x92210;
		public static int lua_settop = 0x92220;
		public static int lua_status = 0x92280;
		public static int lua_toboolean = 0x92290;
		public static int lua_tobuffer = 0x92300;
		public static int lua_tointegerx = 0x92370;
		public static int lua_tolightuserdata = 0x92430;
		public static int lua_tolstring = 0x92490;
		public static int lua_tonumberx = 0x925c0;
		public static int lua_topointer = 0x92680;
		public static int lua_totalbytes = 0x92700;
		public static int lua_tothread = 0x92720;
		public static int lua_tounsignedx = 0x92780;
		public static int lua_touserdata = 0x92840;
		public static int lua_tovector = 0x928b0;
		public static int lua_type = 0x92920;
		public static int lua_typename = 0x92980;
		public static int lua_unref = 0x929a0;
		public static int lua_xmove = 0x92a10;
		public static int lua_xpush = 0x92aa0;

		public static int luaL_addlstring = 0x92d80;
		public static int luaL_addvalue = 0x92de0;
		public static int luaL_addvalueany = 0x92e70;
		public static int luaL_argerrorL = 0x93060;
		public static int luaL_buffinit = 0x930b0;
		public static int luaL_buffinitsize = 0x930d0;
		public static int luaL_callmeta = 0x93120;
		public static int luaL_checkany = 0x931e0;
		public static int luaL_checkboolean = 0x93220;
		public static int luaL_checkbuffer = 0x93260;
		public static int luaL_checkinteger = 0x932a0;
		public static int luaL_checklstring = 0x932e0;
		public static int luaL_checknumber = 0x93320;
		public static int luaL_checkstack = 0x93360;
		public static int luaL_checktype = 0x933a0;
		public static int luaL_checkudata = 0x933e0;
		public static int luaL_checkunsigned = 0x93480;
		public static int luaL_checkvector = 0x934c0;
		public static int luaL_errorL = 0x93500;
		public static int luaL_findtable = 0x93550;
		public static int luaL_getmetafield = 0x93680;
		public static int luaL_newmetatable = 0x93700;
		public static int luaL_optinteger = 0x93790;
		public static int luaL_optlstring = 0x937f0;
		public static int luaL_optnumber = 0x93870;
		public static int luaL_optvector = 0x938d0;
		public static int luaL_prepbuffsize = 0x93930;
		public static int luaL_pushresult = 0x93960;
		public static int luaL_pushresultsize = 0x93a30;
		public static int luaL_register = 0x93b00;
		public static int luaL_tolstring = 0x93c40;
		public static int luaL_typeerrorL = 0x93e90;
		public static int luaL_typename = 0x93f40;
		public static int luaL_where = 0x93f70;
		public static int luaL_newstate = 0x94060;
		public static int luaL_openlibs = 0x94070;
		public static int luaL_sandbox = 0x940e0;
		public static int luaL_sandboxthread = 0x941d0;

		public static int luaE_freethread = 0x943d0;
		public static int luaE_newthread = 0x94460;

		public static int lua_close = 0x94500;
		public static int lua_newstate = 0x94530;
		public static int lua_resetthread = 0x94ef0;

		public static int luaD_call = 0x952e0;
		public static int luaD_checkCstack = 0x954d0;
		public static int luaD_growCI = 0x95510;
		public static int luaD_growstack = 0x95580;
		public static int luaD_pcall = 0x955a0;
		public static int luaD_rawrunprotected = 0x95730;
		public static int luaD_reallocCI = 0x95750;
		public static int luaD_reallocstack = 0x95810;
		public static int luaD_throw = 0x959a0;

		public static int lua_break = 0x959d0;
		public static int lua_isyieldable = 0x95a00;
		public static int lua_resume = 0x95a10;
		public static int lua_resumeerror = 0x95bd0;
		public static int lua_yield = 0x95d60;

		public static int luaG_aritherror = 0x963e0;
		public static int luaG_concaterror = 0x96450;
		public static int luaG_forerrorL = 0x96490;
		public static int luaG_getline = 0x964c0;
		public static int luaG_indexerror = 0x964f0;
		public static int luaG_methoderror = 0x96560;
		public static int luaG_ordererror = 0x96590;
		public static int luaG_pusherror = 0x96600;
		public static int luaG_readonlyerror = 0x96630;
		public static int luaG_runerrorL = 0x96650;
		public static int luaG_typeerrorL = 0x966b0;

		public static int lua_debugtrace = 0x966e0;
		public static int lua_getinfo = 0x96c80;

		public static int luaV_getimport = 0x97d30;

		public static int luau_load = 0x97e30;
		public static int luau_load_DEPRECATED = 0x97fa0;

		public static int luaO_chunkid = 0x99110;
		public static int luaO_log2 = 0x99290;
		public static int luaO_pushfstring = 0x992c0;
		public static int luaO_pushvfstring = 0x993a0;
		public static int luaO_rawequalKey = 0x99470;
		public static int luaO_rawequalObj = 0x99510;
		public static int luaO_str2d = 0x995b0;

		public static int luaS_buffinish = 0x99680;
		public static int luaS_bufstart = 0x99790;
		public static int luaS_free = 0x99800;
		public static int luaS_hash = 0x99870;
		public static int luaS_newlstr = 0x99930;
		public static int luaS_resize = 0x99aa0;

		public static int luaH_clear = 0x99ba0;
		public static int luaH_clone = 0x99c30;
		public static int luaH_free = 0x99d90;
		public static int luaH_get = 0x99e30;
		public static int luaH_getn = 0x99ef0;
		public static int luaH_getnum = 0x9a040;
		public static int luaH_getstr = 0x9a120;
		public static int luaH_new = 0x9a190;
		public static int luaH_newkey = 0x9a240;
		public static int luaH_next = 0x9a2c0;
		public static int luaH_resizearray = 0x9a4a0;
		public static int luaH_resizehash = 0x9a540;
		public static int luaH_set = 0x9a550;
		public static int luaH_setnum = 0x9a6c0;
		public static int luaH_setstr = 0x9a750;

		public static int luaF_close = 0x9b3d0;
		public static int luaF_closeupval = 0x9b450;
		public static int luaF_findupval = 0x9b490;
		public static int luaF_freeclosure = 0x9b550;
		public static int luaF_freeproto = 0x9b590;
		public static int luaF_freeupval = 0x9b6e0;
		public static int luaF_newCclosure = 0x9b700;
		public static int luaF_newLclosure = 0x9b780;
		public static int luaF_newproto = 0x9b830;

		public static int luaC_barrierback = 0x9bfb0;
		public static int luaC_barrierf = 0x9bfd0;
		public static int luaC_barriertable = 0x9c000;
		public static int luaC_freeall = 0x9c030;
		public static int luaC_fullgc = 0x9c040;
		public static int luaC_step = 0x9c220;
		public static int luaC_upvalclosed = 0x9c440;

		public static int luaU_freeudata = 0x9cf00;
		public static int luaU_newudata = 0x9cf90;

		public static int luaV_callTM = 0x9e1d0;
		public static int luaV_concat = 0x9e300;
		public static int luaV_dolen = 0x9e5c0;
		public static int luaV_equalval = 0x9e6e0;
		public static int luaV_gettable = 0x9e870;
		public static int luaV_lessequal = 0x9e980;
		public static int luaV_lessthan = 0x9ea60;
		public static int luaV_prepareFORN = 0x9ead0;
		public static int luaV_settable = 0x9ebd0;
		public static int luaV_strcmp = 0x9eda0;
		public static int luaV_tonumber = 0x9ee20;
		public static int luaV_tostring = 0x9ee80;
		public static int luaV_tryfuncTM = 0x9ef00;

		public static int luai_numidiv = 0x9ef70;
		public static int luai_nummod = 0x9efb0;

		public static int luaB_freebuffer = 0x9f000;
		public static int luaB_newbuffer = 0x9f040;

		public static int luaT_gettm = 0x9f0c0;
		public static int luaT_gettmbyobj = 0x9f100;
		public static int luaT_init = 0x9f160;
		public static int luaT_objtypename = 0x9f240;
		public static int luaT_objtypenamestr = 0x9f2f0;

		public static int luai_num2str = 0x9f3a0;

		public static int luaB_next = 0x9fd40;
		public static int luaB_inext = 0xa03b0;

		public static int luaopen_base = 0xa0800;
		public static int luaopen_coroutine = 0xa1110;
		public static int luaopen_table = 0xa20b0;
		public static int luaopen_os = 0xa3090;
		public static int luaopen_string = 0xa56b0;
		public static int luaopen_bit32 = 0xa6e80;
		public static int luaopen_buffer = 0xa7bd0;
		public static int luaopen_utf8 = 0xa8470;
		public static int luaopen_math = 0xa92a0;
		public static int luaopen_debug = 0xa9cf0;
		public static int luaopen_vector = 0xaa5b0;

		public static int luaM_free_ = 0xaa7f0;
		public static int luaM_freegco_ = 0xaa860;
		public static int luaM_getnextpage = 0xaaa00;
		public static int luaM_getpagewalkinfo = 0xaaa10;
		public static int luaM_new_ = 0xaaa70;
		public static int luaM_newgco_ = 0xaab30;
		public static int luaM_realloc_ = 0xaac80;
		public static int luaM_toobig = 0xaade0;
		public static int luaM_visitgco = 0xaae00;

		public static int luau_callhook = 0xb37c0;
		public static int luau_execute = 0xb3a50;
		public static int luau_poscall = 0xb3a60;
		public static int luau_precall = 0xb3b00;

		public static int lua_clock = 0xb3ca0;

		public static int luau_codegen_compile = 0xb6720;
		public static int luau_codegen_create = 0xb6830;
		public static int luau_codegen_supported = 0xb6840;
		public static int luauConstant = 0xdf4b0;
		public static int luauConstantAddress = 0xdf4e0;
		public static int luauConstantTag = 0xdf510;
		public static int luauConstantValue = 0xdf560;
		public static int luauReg = 0xdf5a0;
		public static int luauRegAddress = 0xdf5d0;
		public static int luauRegExtra = 0xdf600;
		public static int luauRegTag = 0xdf650;
		public static int luauRegValue = 0xdf6a0;
		public static int luauRegValueInt = 0xdf6e0;
		public static int luau_compile = 0xf7540;
	}
}
