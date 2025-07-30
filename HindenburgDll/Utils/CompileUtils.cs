// credits to https://github.com/SecondNewtonLaw/RbxStu-V3/ because I cannot code lol, skidding!

using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using Il2CppLuau;
using Il2Cpp;

namespace HindenburgDll.Utils
{
	static class CompileUtils
	{
		public static LuaIApi api = new LuaIApi();
		public static EnvHolder envHolderInst;
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
			IntPtr result = LuauPlugin.LuauCompileCode(dataPointer, Encoding.UTF8.GetByteCount(data), chunkPointer, data.Length, LuauPlugin.LuauOptimizationLevel.Max);
			Marshal.FreeCoTaskMem(chunkPointer);
			Marshal.FreeCoTaskMem(dataPointer);

			CompilationResult compilationResult = Marshal.PtrToStructure<CompilationResult>(result);
			return compilationResult;
		}

		public static EnvHolder CreateHolder(FunctionHolder[] env)
		{
			GameObject obj = new GameObject(RandomString(8));
			GameObject objCore = new GameObject(RandomString(8));
			AirshipScript initScript = CompileScript("", "main");
			AirshipScript initScriptCore = CompileScript("", "main");
			IntPtr mainThread = LuauScript.LoadScript(obj, LuauContext.Game, LuauScriptCacheMode.NotCached, initScript);
			IntPtr coreThread = LuauScript.LoadScript(objCore, LuauContext.Protected, LuauScriptCacheMode.NotCached, initScriptCore);
			IntPtr exploitThread = IntPtr.Zero;

			if (mainThread != IntPtr.Zero)
			{
				exploitThread = api.lua_newthread(mainThread);
				LuauPluginRaw.Ref(exploitThread, -1);
				LuauPluginRaw.Pop(exploitThread, 1);

				api.luaL_sandboxthread(exploitThread);
				LuauPluginRaw.NewTable(exploitThread);
				api.lua_setglobal(exploitThread, "_G");
				LuauPluginRaw.NewTable(exploitThread);
				api.lua_setglobal(exploitThread, "shared");

				int top = LuauPluginRaw.GetTop(exploitThread);
				foreach (FunctionHolder holder in env)
				{
					FunctionHolder.luaL_Reg[] funcs = holder.luaReg.ToArray();

					if (!holder.PushToGlobal())
					{
						LuauPluginRaw.NewTable(exploitThread);
						api.luaL_register(exploitThread, IntPtr.Zero, funcs);
						LuauPluginRaw.SetReadonly(exploitThread, -1, true);
						api.lua_setglobal(exploitThread, holder.LibraryName());
					}
					else
					{
						api.lua_pushvalue(exploitThread, Offsets.LUA_GLOBALSINDEX);
						api.luaL_register(exploitThread, IntPtr.Zero, funcs);
					}
				}
				api.lua_settop(exploitThread, top);
			}

			return new EnvHolder
			{
				globalState = mainThread,
				coreState = coreThread,
				exploitState = exploitThread
			};
		}

		public static void ExecuteScript(string code, EnvHolder holder, bool core)
		{
			string chunkName = RandomString(8);
			CompilationResult data = CompileScriptData(code, chunkName);

			if (data.Compiled)
			{
				IntPtr chunkPointer = Marshal.StringToCoTaskMemUTF8(chunkName);
				IntPtr executeThread = api.lua_newthread(core ? holder.coreState : holder.exploitState);
				LuauPluginRaw.Pop(core ? holder.coreState : holder.exploitState, 1);
				api.luaL_sandboxthread(executeThread);

				if (api.luau_load(executeThread, chunkPointer, data.Data, (int)data.DataSize, 0) == IntPtr.Zero)
				{
					LuauScript.ExecuteScript(executeThread);
				}

				Marshal.FreeCoTaskMem(chunkPointer);
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
