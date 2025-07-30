using static HindenburgDll.Utils.CompileUtils;
using HindenburgDll.Utils;
using System.Net;
using Il2Cpp;

namespace HindenburgDll.Functions
{
	internal class Http : FunctionHolder
	{
		//print(request({Url = "test", Method = "test1"}))
		public static int request(IntPtr luaState)
		{
			api.luaL_checktype(luaState, 1, (int)LuaIApi.lua_Type.LUA_TTABLE);
			api.lua_getfield(luaState, 1, "Url");
			string url = api.lua_checkstring(luaState, -1);

			api.lua_getfield(luaState, 1, "Method");
			string method = api.lua_checkstring(luaState, -1);

			string? body = null;
			api.lua_getfield(luaState, 1, "Body");
			if (!api.lua_isnoneornil(luaState, -1)) {
				body = api.lua_checkstring(luaState, -1);
			}
			return 1;
		}

		public static int httpget(IntPtr luaState)
		{
			string url = api.lua_checkstring(luaState, 1);
			HttpClientHandler handler = new()
			{
				AutomaticDecompression = DecompressionMethods.All
			};

			HttpClient client = new(handler);
			HttpRequestMessage request = new(HttpMethod.Get, url);

			return api.YieldThread(luaState, async () =>
			{
				var response = await client.SendAsync(request);
				var body = await response.Content.ReadAsByteArrayAsync();
				return () =>
				{
					api.lua_pushlstringB(luaState, body);
				};
			});
		}

		public override void CreateDefinitions()
		{
			Add("httpget", httpget);
			Add("request", request);
			luaReg.Add(new luaL_Reg { name = IntPtr.Zero, func = IntPtr.Zero });
		}

		public override string LibraryName()
		{
			return "http";
		}

		public override bool PushToGlobal()
		{
			return true;
		}
	}
}
