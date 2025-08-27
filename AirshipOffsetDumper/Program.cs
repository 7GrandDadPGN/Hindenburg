// See https://aka.ms/new-console-template for more information
static (byte[] bytes, bool[] mask) FromSignature(string sig)
{
	string[] parsed = sig.Split(' ');
	byte[] bytes = new byte[parsed.Length];
	bool[] mask = new bool[parsed.Length];

	for (int i = 0; i < parsed.Length; i++)
	{
		bytes[i] = parsed[i].Contains("?") ? (byte)0x0 : Convert.ToByte(parsed[i], 16);
		mask[i] = !parsed[i].Contains("?");
	}

	return (bytes, mask);
}


static int ScanFunction(string sig)
{
	byte[] readData = File.ReadAllBytes("./LuauPlugin.dll");
	(byte[] bytes, bool[] mask) = FromSignature(sig);
	for (int i = 0; i < readData.Length - bytes.Length + 1; i++)
	{
		bool found = true;
		for (int j = 0; j < bytes.Length; j++)
		{
			// Check if bytes match (considering wildcards)
			if (i + j >= readData.Length || (mask[j] && readData[i + j] != bytes[j]))
			{
				found = false;
				break;
			}
		}

		if (found)
		{
			return (i + 0xC00);
		}
	}
	return 0;
}

Dictionary<string, int> offsets = new Dictionary<string, int>() {
	{"decrypt_routine", ScanFunction("4C 89 4C 24 20 53 55 57 41 54 41 56 41 57 48 83")},
	{"lua_iscfunction", ScanFunction("48 83 EC 28 85 D2 7E 23 4C 8B 41 10 48 8D 05 ?? ?? 0A 00 49 83 C0 F0 48 63 D2 48 C1 E2 04 4C 03 C2 4C 3B 41 08 49 0F 42 C0 EB 1A 81 FA F0 D8 FF FF 7E 0D 48 63 C2 48 C1 E0 04 48 03 41 08 EB 05 E8 6B 20")}, // kill me
	{"luaA_pushobject", ScanFunction("48 8B 41 08 0F 10 02 0F 11 00 48 83 41 08 10 C3 48 83 EC 28 4C 8D 15 ?? ?? 0A 00 85 D2 7E")},
	{"lua_isnumber", ScanFunction("48 83 EC 48 48 8B 05 ?? ?? 0D 00 48 33 C4 48 89 44 24 30 4C 8B C1")},
	{"lua_isstring", ScanFunction("48 83 EC 28 85 D2 7E 1F")},
	{"lua_getfield", ScanFunction("48 89 5C 24 20 55 56 57 48 83 EC 40 48 8B 05 ?? ?? 0D 00 48 33 C4 48 89 44 24 30 F6 41")},
	{"lua_setfield", ScanFunction("48 89 5C 24 20 55 56 57 48 83 EC 40 48 8B 05 ?? ?? 0D 00 48 33 C4 48 89 44 24 30 49 8B")},
	{"lua_getmetatable", ScanFunction("48 89 5C 24 08 57 48 83 EC 20 F6 41 01 04 48 8B D9 48 63 FA 74 0C 4C 8D 41 68 48 8B D1 E8 2E A7")},
	{"lua_getreadonly", ScanFunction("48 83 EC 28 85 D2 7E 2D")},
	{"lua_rawcheckstack", ScanFunction("48 89 5C 24 08 57 48 83 EC 20 4C 8B 41 28")},
	{"lua_rawseti", ScanFunction("48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 20 45")},
	{"lua_pushlstring", ScanFunction("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 20 4C 8B 49 18 49 8B")},
	{"lua_pushvalue", ScanFunction("48 89 5C 24 08 57 48 83 EC 20 F6 41 01 04 48 8B D9 48 63 FA 74 0C 4C 8D 41 68 48 8B D1 E8 5E 9A")},
	{"lua_newthread", ScanFunction("48 89 5C 24 08 57 48 83 EC 20 48 8B 51 18")},
	{"lua_next", ScanFunction("48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 20 F6 41 01 04 48 8B D9 48 63 F2 74 0C 4C 8D 41 68 48 8B D1 E8 09")},
	{"lua_setmetatable", ScanFunction("40 53 48 83 EC 20 48 8D 59")},

	{"lua_setsafeenv", ScanFunction("48 83 EC 28 45 8B D0 85 D2 7E 32 4C 8B 49 10 48 8D 05 ?? ?? 0A 00 49 83 C1 F0 48 63 D2 48 C1 E2 04 4C 03 CA 4C 3B 49 08 49 0F 42 C1 45 85 C0 0F 95 C1 48 8B 00 88 48 05")},
	{"lua_settop", ScanFunction("85 D2 78 3F 4C 8B")},
	{"lua_tointegerx", ScanFunction("40 53 48 83 EC 40 48 8B 05 ?? ?? 0D 00 48 33 C4 48 89 44 24 30 49 8B D8 4C 8B C1 85 D2 7E 23 48 63 CA 48 8D 05 ?? ?? 0A 00 49 8B 50 10 48 83 C2 F0 48 C1 E1 04 48 03 D1 49 3B 50 08 48 0F 42 C2 EB 1A 81 FA F0 D8 FF FF 7E 0D 48 63 C2 48 C1 E0 04 48 03 41 08 EB 05 E8 44 07 00 00 83 78 0C 03")},
	{"lua_tolstring", ScanFunction("48 89 5C 24 08 48 89 74 24 10 48 89 7C 24 18 41 56 48 83 EC 20 48 63 F2")},
	{"lua_topointer", ScanFunction("48 83 EC 28 85 D2 7E 23 4C 8B 41 10 48 8D 05 ?? ?? 0A 00 49 83 C0 F0 48 63 D2 48 C1 E2 04 4C 03 C2 4C 3B 41 08 49 0F 42 C0 EB 1A 81 FA F0 D8 FF FF 7E 0D 48 63 C2 48 C1 E0 04 48 03 41 08 EB 05 E8 4B 04 00 00 8B 48 0C")},
	{"lua_type", ScanFunction("48 83 EC 28 85 D2 7E 1A 48")},
	{"lua_xmove", ScanFunction("48 3B CA 0F 84 84 00 00 00 48 89")},
	{"lua_yield", ScanFunction("48 83 EC 28 0F B7 41 52 4C 8B C1 66 39 41 50")},
	{"lua_getinfo", ScanFunction("48 89 5C 24 10 48 89 74 24 18 57 41 56 41 57 48 83 EC 30")},

	{"luaL_argerrorL", ScanFunction("48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 30 49 8B F8 8B F2")},
	{"luaL_typeerrorL", ScanFunction("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 30 49 8B E8 8B F2")},
	{"luaL_checkany", ScanFunction("48 89 5C 24 08 57 48 83 EC 20 8B DA 48 8B F9 E8 3C F7 FF FF")},
	{"luaL_checktype", ScanFunction("48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 20 41 8B D8 8B FA 48 8B F1 E8 74 F5 FF FF")},
	{"luaL_checkboolean", ScanFunction("48 89 5C 24 08 57 48 83 EC 20 8B DA 48 8B F9 E8 FC F6 FF FF")},
	{"luaL_checklstring", ScanFunction("48 89 5C 24 08 57 48 83 EC 20 8B DA 48 8B F9 E8 AC F1 FF FF")},
	{"luaL_checkinteger", ScanFunction("48 89 5C 24 08 57 48 83 EC 20 4C 8D 44 24 40 8B DA 48 8B F9 E8 C7 F0 FF")},
	{"luaL_checkudata", ScanFunction("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 48 83 EC 20 49 8B E8 8B")},

	{"luaL_errorL", ScanFunction("48 89 54 24 10 4C 89 44 24 18 4C 89 4C 24 20 53 57 48 83 EC 28 BA 01 00")},
	{"luaL_register", ScanFunction("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 48 89 7C 24 20 41 56 48 83 EC 30 45 33 F6 49 8B D8")},
	{"luaL_sandboxthread", ScanFunction("40 53 48 83 EC 20 45 33 C0 33 D2 48 8B D9 E8 FD")},

	{"luaC_barrierback", ScanFunction("4C 8B 49 18 80 62 01 FB 49 8B 41 30 49 89")},

	{"luau_load", ScanFunction("48 89 5C 24 08 48 89 6C 24 10 48 89 74 24 18 57 41 56 41 57 48 81 EC 80 00 00 00 49 8B E9 4D 8B")},
	{"luau_compile", ScanFunction("40 55 53 56 57 41 54 41 56 41 57 48 8D 6C 24 C0")},

	{"luaM_visitgco", ScanFunction("40 56 41 54 41 57 48 83 EC 30 48 8B 41 18")},
};

foreach (var entry in offsets)
{
	Console.WriteLine($"public static int {entry.Key} = 0x{entry.Value.ToString("X").ToLower()};");
}