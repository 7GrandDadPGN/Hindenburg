// credits to https://github.com/SecondNewtonLaw/RbxStu-V3/ because I cannot code lol, skidding!

using HindenburgDll.Structs;
using Il2Cpp;
using Il2CppLuau;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace HindenburgDll.Utils
{
	static class CompileUtils
	{
		public static LuaIApi api = new LuaIApi();
		public static EnvHolder envHolder;
		public struct CompilationResult
		{
			public IntPtr Data;
			public long DataSize;
			public bool Compiled;
		};

		public static AirshipScript CompileScript(string data, string chunkName)
		{
			AirshipScript airshipScript = ScriptableObject.CreateInstance<AirshipScript>();
			CompilationResult compilationResult = CompileScriptData(data, chunkName);
			airshipScript.m_path = chunkName;
			airshipScript.m_compiled = compilationResult.Compiled;
			airshipScript.m_compilationError = compilationResult.Compiled ? "none" : Marshal.PtrToStringUTF8(compilationResult.Data, (int)compilationResult.DataSize);

			byte[] scriptBytecode = new byte[compilationResult.DataSize];
			Marshal.Copy(compilationResult.Data, scriptBytecode, 0, (int)compilationResult.DataSize);
			airshipScript.m_bytes = scriptBytecode;

			return airshipScript;
		}

		public static CompilationResult CompileScriptData(string data, string chunkName)
		{
			IntPtr chunkPointer = Marshal.StringToCoTaskMemUTF8(chunkName);
			IntPtr dataPointer = Marshal.StringToCoTaskMemUTF8(data);
			IntPtr result = LuauPlugin.CompileCode(dataPointer, Encoding.UTF8.GetByteCount(data), chunkPointer, data.Length, LuauPlugin.LuauOptimizationLevel.Max);
			Marshal.FreeCoTaskMem(chunkPointer);
			Marshal.FreeCoTaskMem(dataPointer);

			CompilationResult compilationResult = Marshal.PtrToStructure<CompilationResult>(result);
			if (data != "" && compilationResult.Compiled)
			{
				long size = 0;
				IntPtr keyString = Marshal.StringToCoTaskMemUTF8("afea643bcd75491f");
				IntPtr finalData = api.decrypt_routine(compilationResult.Data + 8, compilationResult.DataSize - 8, keyString, ref size);
				compilationResult.Data = finalData;
				compilationResult.DataSize = size;
				Marshal.FreeCoTaskMem(keyString);
			}

			return compilationResult;
		}

		public static IntPtr CreateExploitThread(IntPtr baseThread, FunctionHolder[] env)
		{
			IntPtr newThread = api.lua_newthread(baseThread);
			LuauPluginRaw.Ref(newThread, -1);
			LuauPluginRaw.Pop(newThread, 1);

			api.luaL_sandboxthread(newThread);
			LuauPluginRaw.NewTable(newThread);
			api.lua_setglobal(newThread, "_G");
			LuauPluginRaw.NewTable(newThread);
			api.lua_setglobal(newThread, "shared");

			int top = LuauPluginRaw.GetTop(newThread);
			foreach (FunctionHolder holder in env)
			{
				luaL_Reg[] funcs = holder.luaReg.ToArray();

				if (!holder.PushToGlobal())
				{
					LuauPluginRaw.NewTable(newThread);
					api.luaL_register(newThread, IntPtr.Zero, funcs);
					LuauPluginRaw.SetReadonly(newThread, -1, true);
					api.lua_setglobal(newThread, holder.LibraryName());
				}
				else
				{
					api.lua_pushvalue(newThread, Offsets.LUA_GLOBALSINDEX);
					api.luaL_register(newThread, IntPtr.Zero, funcs);
				}
			}
			api.lua_settop(newThread, top);

			return newThread;
		}

		public static void CreateHolder(FunctionHolder[] env)
		{
			GameObject obj = new GameObject(RandomString(8));
			AirshipScript initScript = CompileScript("", "");
			IntPtr mainThread = LuauScript.LoadScript(obj, LuauContext.Game, LuauScriptCacheMode.NotCached, initScript);
			IntPtr coreThread = LuauScript.LoadScript(obj, LuauContext.Protected, LuauScriptCacheMode.NotCached, initScript);
			IntPtr exploitThread = IntPtr.Zero;
			GameObject.Destroy(initScript);
			GameObject.Destroy(obj);

			if (mainThread != IntPtr.Zero)
			{
				envHolder = new EnvHolder
				{
					globalState = mainThread,
					coreState = CreateExploitThread(coreThread, env),
					exploitState = CreateExploitThread(mainThread, env)
				};
			}

			ExecuteScript(@"
				setreadonly(debug, false)
				for i, v in getrenv().debug do 
					debug[i] = v
				end
				setreadonly(debug, true)
			", false);
		}

		public static void ExecuteScript(string code, bool core)
		{
			if (envHolder.globalState != IntPtr.Zero)
			{
				string chunkName = RandomString(8);
				CompilationResult data = CompileScriptData(code, chunkName);

				if (data.Compiled)
				{
					IntPtr chunkPointer = Marshal.StringToCoTaskMemUTF8(chunkName);
					IntPtr executeThread = api.lua_newthread(core ? envHolder.coreState : envHolder.exploitState);
					LuauPluginRaw.Pop(core ? envHolder.coreState : envHolder.exploitState, 1);
					api.luaL_sandboxthread(executeThread);

					if (api.luau_load(executeThread, chunkPointer, data.Data, (int)data.DataSize, 0) == IntPtr.Zero)
					{
						LuauScript.ExecuteScript(executeThread);
					}

					Marshal.FreeCoTaskMem(chunkPointer);
				}
				else
				{
					Debug.LogWarning(Marshal.PtrToStringUTF8(data.Data, (int)data.DataSize));
				}
			}
		}

		private static readonly string randomChars = "0123456789abcdefghijklmnopqrstuvwxyz";
		private static readonly System.Random rand = new System.Random();

		public static string RandomString(int length)
		{
			StringBuilder builder = new StringBuilder();
			while (0 < length--)
			{
				builder.Append(randomChars[rand.Next(randomChars.Length)]);
			}
			return builder.ToString();
		}
	}
}
