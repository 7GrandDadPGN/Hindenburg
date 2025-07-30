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

			var response = client.SendAsync(request);
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
			});
			return 1;
		}

		public static int waitsec(IntPtr luaState)
		{
			Il2CppSystem.Action value = (Il2CppSystem.Action)(async () =>
			{
				await Il2CppSystem.Threading.Tasks.Task.Delay(1000);
				//LuauPluginRaw.PushString(luaState, "hi");
			});

			Il2CppSystem.Threading.Tasks.Task ourTask = Il2CppSystem.Threading.Tasks.Task.Run(value);
			AwaitingTask realTask = new()
			{
				Thread = luaState,
				ThreadRef = 0,
				Task = ourTask,
				Method = value.Method,
				Context = LuauContext.Game,
				Type = value.GetIl2CppType()
			};

			LuauPluginRaw.PushThread(luaState);
			realTask.ThreadRef = LuauPluginRaw.Ref(luaState, -1);
			LuauPluginRaw.Pop(luaState, 1);
			_awaitingTasks.Add(realTask);

			ourTask.Wait();

			return 0;
		}

		public override void CreateDefinitions()
		{
			Add("httpget", httpget);
			Add("waitsec", waitsec);
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
