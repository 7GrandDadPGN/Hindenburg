using static HindenburgDll.Utils.CompileUtils;
using HindenburgDll.Structs;
using HindenburgDll.Utils;
using System.Net;
using Il2Cpp;

namespace HindenburgDll.Functions
{
	internal class Http : FunctionHolder
	{
		private static readonly Dictionary<string, HttpMethod> methodMap = new()
		{
			{ "GET", HttpMethod.Get },
			{ "PUT", HttpMethod.Put },
			{ "HEAD", HttpMethod.Head },
			{ "POST", HttpMethod.Post },
			{ "PATCH", HttpMethod.Patch },
			{ "DELETE", HttpMethod.Delete },
			{ "OPTIONS", HttpMethod.Options }
		};

		public static int request(IntPtr luaState)
		{
			api.luaL_checktype(luaState, 1, (int)LuaIApi.lua_Type.LUA_TTABLE);
			api.lua_getfield(luaState, 1, "Url");
			string url = api.lua_checkstring(luaState, -1);

			api.lua_getfield(luaState, 1, "Method");
			string method = api.lua_checkstring(luaState, -1);
			if (!methodMap.ContainsKey(method))
			{
				api.luaL_argerrorL(luaState, -1, "Invalid HTTP Method");
			}

			byte[]? body = null;
			api.lua_getfield(luaState, 1, "Body");
			if (!api.lua_isnoneornil(luaState, -1)) {
				body = api.lua_checkstringB(luaState, -1);
			}

			Dictionary<string, string> headers = new()
			{
				{ "User-Agent", "Airship" }
			};
			api.lua_getfield(luaState, 1, "Headers");
			if (!api.lua_isnoneornil(luaState, -1))
			{
				api.luaL_checktype(luaState, -1, (int)LuaIApi.lua_Type.LUA_TTABLE);
				LuauPluginRaw.PushNil(luaState);
				while (api.lua_next(luaState, -2) != 0)
				{
					string key = api.lua_tostring(luaState, -2);
					string value = api.lua_tostring(luaState, -1);
					LuauPluginRaw.Pop(luaState, 1);
					headers[key] = value;
				}
				LuauPluginRaw.Pop(luaState, 1);
			}

			HttpClient client = new();
			HttpRequestMessage request = new(methodMap[method], url);
			if (body != null)
			{
				request.Content = new ByteArrayContent(body);
			}
			foreach (var header in headers)
			{
				if (header.Key.ToLower() == "content-type" && request.Content != null)
				{
					request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(header.Value);
					continue;
				}
				request.Headers.Add(header.Key, header.Value); ;
			}

			return api.YieldThread(luaState, async () =>
			{
				var response = await client.SendAsync(request);
				var responseBody = await response.Content.ReadAsByteArrayAsync();

				return () =>
				{
					LuauPluginRaw.NewTable(luaState); // return table

					api.lua_pushlstringB(luaState, responseBody);
					api.lua_setfield(luaState, -2, "Body");

					LuauPluginRaw.PushInteger(luaState, (int)response.StatusCode);
					api.lua_setfield(luaState, -2, "StatusCode");

					LuauPluginRaw.PushBoolean(luaState, response.IsSuccessStatusCode);
					api.lua_setfield(luaState, -2, "Success");

					LuauPluginRaw.NewTable(luaState); // headers
					foreach(var header in response.Headers)
					{
						LuauPluginRaw.PushString(luaState, string.Join(",", header.Value.ToArray()));
						api.lua_setfield(luaState, -2, header.Key);
					}

					api.lua_setfield(luaState, -2, "Headers");
				};
			});
		}

		public static int httpget(IntPtr luaState)
		{
			string url = api.lua_checkstring(luaState, 1);
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			HttpClient client = new HttpClient(new HttpClientHandler()
			{
				AutomaticDecompression = DecompressionMethods.All
			});

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
