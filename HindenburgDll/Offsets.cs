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
		public static int luaAirship_collectgarbage = 0x11440;
		public static int wrap_tointeger = 0x20990;
		public static int wrap_unityobject = 0x209a0;

		public static int luaA_pushobject = 0x8fdb0;
		public static int luaA_toobject = 0x8fdc0;

		public static int lua_absindex = 0x8fe30;
		public static int lua_call = 0x8fe60;
		public static int lua_callbacks = 0x8feb0;
		public static int lua_checkstack = 0x8fec0;
		public static int lua_cleartable = 0x8ffa0;
		public static int lua_concat = 0x90010;
		public static int lua_costatus = 0x900d0;
		public static int lua_createtable = 0x90120;
		public static int lua_encodepointer = 0x901a0;
		public static int lua_equal = 0x901d0;
		public static int lua_error = 0x902c0;
		public static int lua_gc = 0x902d0;
		public static int lua_getfenv = 0x90450;
		public static int lua_getfield = 0x90520;
		public static int lua_getmetatable = 0x90600;
		public static int lua_getreadonly = 0x906d0;
		public static int lua_gettable = 0x90740;
		public static int lua_getthreaddata = 0x907e0;
		public static int lua_gettop = 0x907f0;
		public static int lua_insert = 0x90800;
		public static int lua_iscfunction = 0x908b0;
		public static int lua_isnumber = 0x90920;
		public static int lua_isstring = 0x909c0;
		public static int lua_isuserdata = 0x90a30;
		public static int lua_mainthread = 0x90aa0;
		public static int lua_namecallatom = 0x90ab0;
		public static int lua_newbuffer = 0x90b20;
		public static int lua_newthread = 0x90b90;
		public static int lua_newuserdatadtor = 0x90c10;
		public static int lua_newuserdatatagged = 0x90ca0;
		public static int lua_next = 0x90d20;
		public static int lua_objlen = 0x90dd0;
		public static int lua_pcall = 0x90e60;
		public static int lua_pushboolean = 0x90f40;
		public static int lua_pushcclosurek = 0x90f60;
		public static int lua_pushfstringL = 0x91040;
		public static int lua_pushinteger = 0x910a0;
		public static int lua_pushlightuserdatatagged = 0x910c0;
		public static int lua_pushlstring = 0x910e0;
		public static int lua_pushnil = 0x91160;
		public static int lua_pushnumber = 0x91180;
		public static int lua_pushstring = 0x911a0;
		public static int lua_pushthread = 0x91250;
		public static int lua_pushunsigned = 0x912a0;
		public static int lua_pushvalue = 0x912d0;
		public static int lua_pushvector = 0x91360;
		public static int lua_pushvfstring = 0x91380;
		public static int lua_rawcheckstack = 0x913e0;
		public static int lua_rawequal = 0x91430;
		public static int lua_rawget = 0x91500;
		public static int lua_rawgetfield = 0x915a0;
		public static int lua_rawgeti = 0x91660;
		public static int lua_rawset = 0x91710;
		public static int lua_rawsetfield = 0x917e0;
		public static int lua_rawseti = 0x918d0;
		public static int lua_ref = 0x919a0;
		public static int lua_remove = 0x91ab0;
		public static int lua_replace = 0x91b40;
		public static int lua_setfenv = 0x91c60;
		public static int lua_setfield = 0x91d20;
		public static int lua_setmemcat = 0x91df0;
		public static int lua_setmetatable = 0x91e00;
		public static int lua_setreadonly = 0x91ee0;
		public static int lua_setsafeenv = 0x91f60;
		public static int lua_settable = 0x91fe0;
		public static int lua_setthreaddata = 0x92060;
		public static int lua_settop = 0x92070;
		public static int lua_status = 0x920d0;
		public static int lua_toboolean = 0x920e0;
		public static int lua_tobuffer = 0x92150;
		public static int lua_tointegerx = 0x921c0;
		public static int lua_tolightuserdata = 0x92280;
		public static int lua_tolstring = 0x922e0;
		public static int lua_tonumberx = 0x92410;
		public static int lua_topointer = 0x924d0;
		public static int lua_totalbytes = 0x92550;
		public static int lua_tothread = 0x92570;
		public static int lua_tounsignedx = 0x925d0;
		public static int lua_touserdata = 0x92690;
		public static int lua_tovector = 0x92700;
		public static int lua_type = 0x92770;
		public static int lua_typename = 0x927d0;
		public static int lua_unref = 0x927f0;
		public static int lua_xmove = 0x92840;
		public static int lua_xpush = 0x928d0;

		public static int luaL_addlstring = 0x92bc0;
		public static int luaL_addvalue = 0x92c20;
		public static int luaL_addvalueany = 0x92cb0;
		public static int luaL_argerrorL = 0x92ea0;
		public static int luaL_buffinit = 0x92ef0;
		public static int luaL_buffinitsize = 0x92f10;
		public static int luaL_callmeta = 0x92f60;
		public static int luaL_checkany = 0x93020;
		public static int luaL_checkboolean = 0x93060;
		public static int luaL_checkbuffer = 0x930a0;
		public static int luaL_checkinteger = 0x930e0;
		public static int luaL_checklstring = 0x93120;
		public static int luaL_checknumber = 0x93160;
		public static int luaL_checkstack = 0x931a0;
		public static int luaL_checktype = 0x931e0;
		public static int luaL_checkudata = 0x93220;
		public static int luaL_checkunsigned = 0x932c0;
		public static int luaL_checkvector = 0x93300;
		public static int luaL_errorL = 0x93340;
		public static int luaL_findtable = 0x93390;
		public static int luaL_getmetafield = 0x934c0;
		public static int luaL_newmetatable = 0x93540;
		public static int luaL_optinteger = 0x935d0;
		public static int luaL_optlstring = 0x93630;
		public static int luaL_optnumber = 0x936b0;
		public static int luaL_optvector = 0x93710;
		public static int luaL_prepbuffsize = 0x93770;
		public static int luaL_pushresult = 0x937a0;
		public static int luaL_pushresultsize = 0x93870;
		public static int luaL_register = 0x93940;
		public static int luaL_tolstring = 0x93a80;
		public static int luaL_typeerrorL = 0x93cd0;
		public static int luaL_typename = 0x93d80;
		public static int luaL_where = 0x93db0;
		public static int luaL_newstate = 0x93ea0;
		public static int luaL_openlibs = 0x93eb0;
		public static int luaL_sandbox = 0x93f20;
		public static int luaL_sandboxthread = 0x94010;

		public static int luaE_freethread = 0x94210;
		public static int luaE_newthread = 0x942a0;

		public static int lua_close = 0x94340;
		public static int lua_newstate = 0x94370;
		public static int lua_resetthread = 0x94d30;

		public static int luaD_call = 0x95210;
		public static int luaD_checkCstack = 0x953a0;
		public static int luaD_growCI = 0x953e0;
		public static int luaD_growstack = 0x95450;
		public static int luaD_pcall = 0x95470;
		public static int luaD_rawrunprotected = 0x955e0;
		public static int luaD_reallocCI = 0x95600;
		public static int luaD_reallocstack = 0x956c0;
		public static int luaD_throw = 0x95850;

		public static int lua_break = 0x95880;
		public static int lua_isyieldable = 0x958b0;
		public static int lua_resume = 0x958c0;
		public static int lua_resumeerror = 0x95a80;
		public static int lua_yield = 0x95c10;

		public static int luaG_aritherror = 0x96260;
		public static int luaG_concaterror = 0x962d0;
		public static int luaG_forerrorL = 0x96310;
		public static int luaG_getline = 0x96340;
		public static int luaG_indexerror = 0x96370;
		public static int luaG_methoderror = 0x963e0;
		public static int luaG_ordererror = 0x96410;
		public static int luaG_pusherror = 0x96480;
		public static int luaG_readonlyerror = 0x964b0;
		public static int luaG_runerrorL = 0x964d0;
		public static int luaG_typeerrorL = 0x96530;

		public static int lua_debugtrace = 0x96560;
		public static int lua_getinfo = 0x96910;

		public static int luaV_getimport = 0x97990;

		public static int luau_load = 0x97a90;

		public static int luaO_chunkid = 0x97e90;
		public static int luaO_log2 = 0x98010;
		public static int luaO_pushfstring = 0x98040;
		public static int luaO_pushvfstring = 0x98120;
		public static int luaO_rawequalKey = 0x981f0;
		public static int luaO_rawequalObj = 0x98290;
		public static int luaO_str2d = 0x98330;

		public static int luaS_buffinish = 0x98400;
		public static int luaS_bufstart = 0x98510;
		public static int luaS_free = 0x98580;
		public static int luaS_hash = 0x985f0;
		public static int luaS_newlstr = 0x986b0;
		public static int luaS_resize = 0x98820;

		public static int luaH_clear = 0x98920;
		public static int luaH_clone = 0x989b0;
		public static int luaH_free = 0x98b10;
		public static int luaH_get = 0x98bb0;
		public static int luaH_getn = 0x98c70;
		public static int luaH_getnum = 0x98dc0;
		public static int luaH_getstr = 0x98ea0;
		public static int luaH_new = 0x98f10;
		public static int luaH_newkey = 0x98fc0;
		public static int luaH_next = 0x99040;
		public static int luaH_resizearray = 0x99220;
		public static int luaH_resizehash = 0x992c0;
		public static int luaH_set = 0x992d0;
		public static int luaH_setnum = 0x99440;
		public static int luaH_setstr = 0x994d0;

		public static int luaF_close = 0x9a150;
		public static int luaF_closeupval = 0x9a1d0;
		public static int luaF_findupval = 0x9a210;
		public static int luaF_freeclosure = 0x9a2d0;
		public static int luaF_freeproto = 0x9a310;
		public static int luaF_freeupval = 0x9a460;
		public static int luaF_newCclosure = 0x9a480;
		public static int luaF_newLclosure = 0x9a500;
		public static int luaF_newproto = 0x9a5b0;

		public static int luaC_barrierback = 0x9ad50;
		public static int luaC_barrierf = 0x9ad70;
		public static int luaC_barriertable = 0x9ada0;
		public static int luaC_freeall = 0x9add0;
		public static int luaC_fullgc = 0x9ade0;
		public static int luaC_step = 0x9afd0;
		public static int luaC_upvalclosed = 0x9b1f0;

		public static int luaU_freeudata = 0x9c0a0;
		public static int luaU_newudata = 0x9c130;

		public static int luaV_callTM = 0x9d370;
		public static int luaV_concat = 0x9d4a0;
		public static int luaV_dolen = 0x9d760;
		public static int luaV_equalval = 0x9d880;
		public static int luaV_gettable = 0x9da10;
		public static int luaV_lessequal = 0x9db20;
		public static int luaV_lessthan = 0x9dc00;
		public static int luaV_prepareFORN = 0x9dc70;
		public static int luaV_settable = 0x9dd70;
		public static int luaV_strcmp = 0x9df40;
		public static int luaV_tonumber = 0x9dfc0;
		public static int luaV_tostring = 0x9e020;
		public static int luaV_tryfuncTM = 0x9e0a0;

		public static int luai_numidiv = 0x9e110;
		public static int luai_nummod = 0x9e150;

		public static int luaB_freebuffer = 0x9e1a0;
		public static int luaB_newbuffer = 0x9e1e0;

		public static int luaT_gettm = 0x9e260;
		public static int luaT_gettmbyobj = 0x9e2a0;
		public static int luaT_init = 0x9e300;
		public static int luaT_objtypename = 0x9e3e0;
		public static int luaT_objtypenamestr = 0x9e490;

		public static int luai_num2str = 0x9e540;

		public static int luaB_next = 0x9eee0;
		public static int luaB_inext = 0x9f550;

		public static int luaopen_base = 0x9f950;
		public static int luaopen_coroutine = 0xa0260;
		public static int luaopen_table = 0xa1200;
		public static int luaopen_os = 0xa21e0;
		public static int luaopen_string = 0xa47d0;
		public static int luaopen_bit32 = 0xa5fa0;
		public static int luaopen_buffer = 0xa6cf0;
		public static int luaopen_utf8 = 0xa7590;
		public static int luaopen_math = 0xa83c0;
		public static int luaopen_debug = 0xa8e10;
		public static int luaopen_vector = 0xa96d0;

		public static int luaM_free_ = 0xa9910;
		public static int luaM_freegco_ = 0xa9980;
		public static int luaM_getnextpage = 0xa9b20;
		public static int luaM_getpagewalkinfo = 0xa9b30;
		public static int luaM_new_ = 0xa9b90;
		public static int luaM_newgco_ = 0xa9c50;
		public static int luaM_realloc_ = 0xa9da0;
		public static int luaM_toobig = 0xa9f00;
		public static int luaM_visitgco = 0xa9f20;

		public static int luau_callhook = 0xb28e0;
		public static int luau_execute = 0xb2a80;
		public static int luau_poscall = 0xb2a90;
		public static int luau_precall = 0xb2b30;

		public static int lua_clock = 0xb2cd0;

		public static int luau_codegen_compile = 0xb5750;
		public static int luau_codegen_create = 0xb5860;
		public static int luau_codegen_supported = 0xb5870;
		public static int luauConstant = 0xde880;
		public static int luauConstantAddress = 0xde8b0;
		public static int luauConstantTag = 0xde8e0;
		public static int luauConstantValue = 0xde930;
		public static int luauReg = 0xde970;
		public static int luauRegAddress = 0xde9a0;
		public static int luauRegExtra = 0xde9d0;
		public static int luauRegTag = 0xdea20;
		public static int luauRegValue = 0xdea70;
		public static int luauRegValueInt = 0xdeab0;
		public static int luau_compile = 0xf6a00;
	}
}
