namespace HindenburgDll
{
	internal class Offsets
	{
		public static int LUA_REGISTRYINDEX = -8000 - 2000;
		public static int LUA_ENVIRONINDEX = -8000 - 2001;
		public static int LUA_GLOBALSINDEX = -8000 - 2002;

		public static int luaAirship_print = 0x10960;
		public static int luaAirship_warn = 0x10b70;
		public static int luaAirship_error = 0x10d70;
		public static int luaAirship_require = 0x10e30;
		public static int luaAirship_collectgarbage = 0x11450;
		public static int wrap_tointeger = 0x209a0;
		public static int wrap_unityobject = 0x209b0;

		public static int luaA_pushobject = 0x8fe80;
		public static int luaA_toobject = 0x8fe90;

		public static int lua_absindex = 0x8ff00;
		public static int lua_call = 0x8ff30;
		public static int lua_callbacks = 0x8ff80;
		public static int lua_checkstack = 0x8ff90;
		public static int lua_cleartable = 0x90070;
		public static int lua_concat = 0x900e0;
		public static int lua_costatus = 0x901a0;
		public static int lua_createtable = 0x901f0;
		public static int lua_encodepointer = 0x90270;
		public static int lua_equal = 0x902a0;
		public static int lua_error = 0x90390;
		public static int lua_gc = 0x903a0;
		public static int lua_getfenv = 0x90520;
		public static int lua_getfield = 0x905f0;
		public static int lua_getmetatable = 0x906d0;
		public static int lua_getreadonly = 0x907a0;
		public static int lua_gettable = 0x90810;
		public static int lua_getthreaddata = 0x908b0;
		public static int lua_gettop = 0x908c0;
		public static int lua_insert = 0x908d0;
		public static int lua_iscfunction = 0x90980;
		public static int lua_isnumber = 0x909f0;
		public static int lua_isstring = 0x90a90;
		public static int lua_isuserdata = 0x90b00;
		public static int lua_mainthread = 0x90b70;
		public static int lua_namecallatom = 0x90b80;
		public static int lua_newbuffer = 0x90bf0;
		public static int lua_newthread = 0x90c60;
		public static int lua_newuserdatadtor = 0x90ce0;
		public static int lua_newuserdatatagged = 0x90d70;
		public static int lua_next = 0x90df0;
		public static int lua_objlen = 0x90ea0;
		public static int lua_pcall = 0x90f30;
		public static int lua_pushboolean = 0x91010;
		public static int lua_pushcclosurek = 0x91030;
		public static int lua_pushfstringL = 0x91110;
		public static int lua_pushinteger = 0x91170;
		public static int lua_pushlightuserdatatagged = 0x91190;
		public static int lua_pushlstring = 0x911b0;
		public static int lua_pushnil = 0x91230;
		public static int lua_pushnumber = 0x91250;
		public static int lua_pushstring = 0x91270;
		public static int lua_pushthread = 0x91320;
		public static int lua_pushunsigned = 0x91370;
		public static int lua_pushvalue = 0x913a0;
		public static int lua_pushvector = 0x91430;
		public static int lua_pushvfstring = 0x91450;
		public static int lua_rawcheckstack = 0x914b0;
		public static int lua_rawequal = 0x91500;
		public static int lua_rawget = 0x915d0;
		public static int lua_rawgetfield = 0x91670;
		public static int lua_rawgeti = 0x91730;
		public static int lua_rawset = 0x917e0;
		public static int lua_rawsetfield = 0x918b0;
		public static int lua_rawseti = 0x919a0;
		public static int lua_ref = 0x91a70;
		public static int lua_remove = 0x91b80;
		public static int lua_replace = 0x91c10;
		public static int lua_setfenv = 0x91d30;
		public static int lua_setfield = 0x91df0;
		public static int lua_setmemcat = 0x91ec0;
		public static int lua_setmetatable = 0x91ed0;
		public static int lua_setreadonly = 0x91fb0;
		public static int lua_setsafeenv = 0x92030;
		public static int lua_settable = 0x920b0;
		public static int lua_setthreaddata = 0x92130;
		public static int lua_settop = 0x92140;
		public static int lua_status = 0x921a0;
		public static int lua_toboolean = 0x921b0;
		public static int lua_tobuffer = 0x92220;
		public static int lua_tointegerx = 0x92290;
		public static int lua_tolightuserdata = 0x92350;
		public static int lua_tolstring = 0x923b0;
		public static int lua_tonumberx = 0x924e0;
		public static int lua_topointer = 0x925a0;
		public static int lua_totalbytes = 0x92620;
		public static int lua_tothread = 0x92640;
		public static int lua_tounsignedx = 0x926a0;
		public static int lua_touserdata = 0x92760;
		public static int lua_tovector = 0x927d0;
		public static int lua_type = 0x92840;
		public static int lua_typename = 0x928a0;
		public static int lua_unref = 0x928c0;
		public static int lua_xmove = 0x92910;
		public static int lua_xpush = 0x929a0;

		public static int luaL_addlstring = 0x92c90;
		public static int luaL_addvalue = 0x92cf0;
		public static int luaL_addvalueany = 0x92d80;
		public static int luaL_argerrorL = 0x92f70;
		public static int luaL_buffinit = 0x92fc0;
		public static int luaL_buffinitsize = 0x92fe0;
		public static int luaL_callmeta = 0x93030;
		public static int luaL_checkany = 0x930f0;
		public static int luaL_checkboolean = 0x93130;
		public static int luaL_checkbuffer = 0x93170;
		public static int luaL_checkinteger = 0x931b0;
		public static int luaL_checklstring = 0x931f0;
		public static int luaL_checknumber = 0x93230;
		public static int luaL_checkstack = 0x93270;
		public static int luaL_checktype = 0x932b0;
		public static int luaL_checkudata = 0x932f0;
		public static int luaL_checkunsigned = 0x93390;
		public static int luaL_checkvector = 0x933d0;
		public static int luaL_errorL = 0x93410;
		public static int luaL_findtable = 0x93460;
		public static int luaL_getmetafield = 0x93590;
		public static int luaL_newmetatable = 0x93610;
		public static int luaL_optinteger = 0x936a0;
		public static int luaL_optlstring = 0x93700;
		public static int luaL_optnumber = 0x93780;
		public static int luaL_optvector = 0x937e0;
		public static int luaL_prepbuffsize = 0x93840;
		public static int luaL_pushresult = 0x93870;
		public static int luaL_pushresultsize = 0x93940;
		public static int luaL_register = 0x93a10;
		public static int luaL_tolstring = 0x93b50;
		public static int luaL_typeerrorL = 0x93da0;
		public static int luaL_typename = 0x93e50;
		public static int luaL_where = 0x93e80;
		public static int luaL_newstate = 0x93f70;
		public static int luaL_openlibs = 0x93f80;
		public static int luaL_sandbox = 0x93ff0;
		public static int luaL_sandboxthread = 0x940e0;

		public static int luaE_freethread = 0x942e0;
		public static int luaE_newthread = 0x94370;

		public static int lua_close = 0x94410;
		public static int lua_newstate = 0x94440;
		public static int lua_resetthread = 0x94e00;

		public static int luaD_call = 0x952e0;
		public static int luaD_checkCstack = 0x95470;
		public static int luaD_growCI = 0x954b0;
		public static int luaD_growstack = 0x95520;
		public static int luaD_pcall = 0x95540;
		public static int luaD_rawrunprotected = 0x956b0;
		public static int luaD_reallocCI = 0x956d0;
		public static int luaD_reallocstack = 0x95790;
		public static int luaD_throw = 0x95920;

		public static int lua_break = 0x95950;
		public static int lua_isyieldable = 0x95980;
		public static int lua_resume = 0x95990;
		public static int lua_resumeerror = 0x95b50;
		public static int lua_yield = 0x95ce0;

		public static int luaG_aritherror = 0x96330;
		public static int luaG_concaterror = 0x963a0;
		public static int luaG_forerrorL = 0x963e0;
		public static int luaG_getline = 0x96410;
		public static int luaG_indexerror = 0x96440;
		public static int luaG_methoderror = 0x964b0;
		public static int luaG_ordererror = 0x964e0;
		public static int luaG_pusherror = 0x96550;
		public static int luaG_readonlyerror = 0x96580;
		public static int luaG_runerrorL = 0x965a0;
		public static int luaG_typeerrorL = 0x96600;

		public static int lua_debugtrace = 0x96630;
		public static int lua_getinfo = 0x969e0;

		public static int luaV_getimport = 0x97a60;

		public static int luau_load = 0x97b60;

		public static int luaO_chunkid = 0x97f60;
		public static int luaO_log2 = 0x980e0;
		public static int luaO_pushfstring = 0x98110;
		public static int luaO_pushvfstring = 0x981f0;
		public static int luaO_rawequalKey = 0x982c0;
		public static int luaO_rawequalObj = 0x98360;
		public static int luaO_str2d = 0x98400;

		public static int luaS_buffinish = 0x984d0;
		public static int luaS_bufstart = 0x985e0;
		public static int luaS_free = 0x98650;
		public static int luaS_hash = 0x986c0;
		public static int luaS_newlstr = 0x98780;
		public static int luaS_resize = 0x988f0;

		public static int luaH_clear = 0x989f0;
		public static int luaH_clone = 0x98a80;
		public static int luaH_free = 0x98be0;
		public static int luaH_get = 0x98c80;
		public static int luaH_getn = 0x98d40;
		public static int luaH_getnum = 0x98e90;
		public static int luaH_getstr = 0x98f70;
		public static int luaH_new = 0x98fe0;
		public static int luaH_newkey = 0x99090;
		public static int luaH_next = 0x99110;
		public static int luaH_resizearray = 0x992f0;
		public static int luaH_resizehash = 0x99390;
		public static int luaH_set = 0x993a0;
		public static int luaH_setnum = 0x99510;
		public static int luaH_setstr = 0x995a0;

		public static int luaF_close = 0x9a220;
		public static int luaF_closeupval = 0x9a2a0;
		public static int luaF_findupval = 0x9a2e0;
		public static int luaF_freeclosure = 0x9a3a0;
		public static int luaF_freeproto = 0x9a3e0;
		public static int luaF_freeupval = 0x9a530;
		public static int luaF_newCclosure = 0x9a550;
		public static int luaF_newLclosure = 0x9a5d0;
		public static int luaF_newproto = 0x9a680;

		public static int luaC_barrierback = 0x9ae20;
		public static int luaC_barrierf = 0x9ae40;
		public static int luaC_barriertable = 0x9ae70;
		public static int luaC_freeall = 0x9aea0;
		public static int luaC_fullgc = 0x9aeb0;
		public static int luaC_step = 0x9b0a0;
		public static int luaC_upvalclosed = 0x9b2c0;

		public static int luaU_freeudata = 0x9c170;
		public static int luaU_newudata = 0x9c200;

		public static int luaV_callTM = 0x9d440;
		public static int luaV_concat = 0x9d570;
		public static int luaV_dolen = 0x9d830;
		public static int luaV_equalval = 0x9d950;
		public static int luaV_gettable = 0x9dae0;
		public static int luaV_lessequal = 0x9dbf0;
		public static int luaV_lessthan = 0x9dcd0;
		public static int luaV_prepareFORN = 0x9dd40;
		public static int luaV_settable = 0x9de40;
		public static int luaV_strcmp = 0x9e010;
		public static int luaV_tonumber = 0x9e090;
		public static int luaV_tostring = 0x9e0f0;
		public static int luaV_tryfuncTM = 0x9e170;

		public static int luai_numidiv = 0x9e1e0;
		public static int luai_nummod = 0x9e220;

		public static int luaB_freebuffer = 0x9e270;
		public static int luaB_newbuffer = 0x9e2b0;

		public static int luaT_gettm = 0x9e330;
		public static int luaT_gettmbyobj = 0x9e370;
		public static int luaT_init = 0x9e3d0;
		public static int luaT_objtypename = 0x9e4b0;
		public static int luaT_objtypenamestr = 0x9e560;

		public static int luai_num2str = 0x9e610;

		public static int luaB_next = 0x9efb0;
		public static int luaB_inext = 0x9f620;

		public static int luaopen_base = 0x9fa20;
		public static int luaopen_coroutine = 0xa0330;
		public static int luaopen_table = 0xa12d0;
		public static int luaopen_os = 0xa22b0;
		public static int luaopen_string = 0xa48a0;
		public static int luaopen_bit32 = 0xa6070;
		public static int luaopen_buffer = 0xa6dc0;
		public static int luaopen_utf8 = 0xa7660;
		public static int luaopen_math = 0xa8490;
		public static int luaopen_debug = 0xa8ee0;
		public static int luaopen_vector = 0xa97a0;

		public static int luaM_free_ = 0xa99e0;
		public static int luaM_freegco_ = 0xa9a50;
		public static int luaM_getnextpage = 0xa9bf0;
		public static int luaM_getpagewalkinfo = 0xa9c00;
		public static int luaM_new_ = 0xa9c60;
		public static int luaM_newgco_ = 0xa9d20;
		public static int luaM_realloc_ = 0xa9e70;
		public static int luaM_toobig = 0xa9fd0;
		public static int luaM_visitgco = 0xa9ff0;

		public static int luau_callhook = 0xb29b0;
		public static int luau_execute = 0xb2b50;
		public static int luau_poscall = 0xb2b60;
		public static int luau_precall = 0xb2c00;

		public static int lua_clock = 0xb2da0;

		public static int luau_codegen_compile = 0xb5820;
		public static int luau_codegen_create = 0xb5930;
		public static int luau_codegen_supported = 0xb5940;
		public static int luauConstant = 0xde950;
		public static int luauConstantAddress = 0xde980;
		public static int luauConstantTag = 0xde9b0;
		public static int luauConstantValue = 0xdea00;
		public static int luauReg = 0xdea40;
		public static int luauRegAddress = 0xdea70;
		public static int luauRegExtra = 0xdeaa0;
		public static int luauRegTag = 0xdeaf0;
		public static int luauRegValue = 0xdeb40;
		public static int luauRegValueInt = 0xdeb80;
		public static int luau_compile = 0xf6ad0;
	}
}
