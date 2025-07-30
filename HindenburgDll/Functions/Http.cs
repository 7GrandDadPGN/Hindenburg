using HindenburgDll.Utils;
using Il2Cpp;
using System.Net;
using static HindenburgDll.Utils.CompileUtils;
using static Il2Cpp.LuauCore;

namespace HindenburgDll.Functions
{
	internal class Http : FunctionHolder
	{

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

			/*var response = client.SendAsync(request);
			CancellationTokenSource token = new();
			Hindenburg.tokenList.Add(token);
			var test = api.lua_yield(luaState, 0);
			response.GetAwaiter().OnCompleted(async () =>
			{
				byte[] body = await response.Result.Content.ReadAsByteArrayAsync();
				if (token.IsCancellationRequested) {
					return;
				}
				api.lua_pushlstringB(luaState, body);
				LuauPlugin.LuauResumeThread(test);
				Hindenburg.tokenList.Remove(token);
			});*/
		}

		public override void CreateDefinitions()
		{
			Add("httpget", httpget);
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
