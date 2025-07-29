using System.Runtime.InteropServices;
using static HindenburgDll.Utils.CompileUtils;
using static HindenburgDll.Utils.LuaIApi;
using HindenburgDll.Utils;
using Il2Cpp;

namespace HindenburgDll.Functions
{
    internal class Closures : FunctionHolder
    {
        public static int loadstring(IntPtr luaState)
        {
            string code = api.lua_checkstring(luaState, 1);
            string chunkName = api.luaL_optlstring(luaState, 2, RandomString(8));
            api.lua_limittop(luaState, 1);
            CompilationResult compilationResult = CompileScriptData(code, chunkName);

            if (!compilationResult.Compiled)
            {
                LuauPluginRaw.PushNil(luaState);
                LuauPluginRaw.PushString(luaState, Marshal.PtrToStringUTF8(compilationResult.Data, (int)compilationResult.DataSize));
                return 2;
            }

            IntPtr namePointer = Marshal.StringToCoTaskMemUTF8(chunkName);
            api.luau_load(luaState, namePointer, compilationResult.Data, (int)compilationResult.DataSize, 0);
            api.lua_setsafeenv(luaState, Offsets.LUA_GLOBALSINDEX, false);
            Marshal.FreeCoTaskMem(namePointer);
            return 1;
        }

        public static int iscclosure(IntPtr luaState)
        {
            api.luaL_checktype(luaState, 1, (int)lua_Type.LUA_TFUNCTION);
            api.lua_limittop(luaState, 1);
            LuauPluginRaw.PushBoolean(luaState, api.lua_iscfunction(luaState, 1));
            return 1;
        }

        public static int islclosure(IntPtr luaState)
        {
            api.luaL_checktype(luaState, 1, (int)lua_Type.LUA_TFUNCTION);
            api.lua_limittop(luaState, 1);
            LuauPluginRaw.PushBoolean(luaState, !api.lua_iscfunction(luaState, 1));
            return 1;
        }

        public override void CreateDefinitions()
        {
            Add("loadstring", loadstring);
            Add("iscclosure", iscclosure);
            Add("islclosure", islclosure);
            luaReg.Add(new luaL_Reg { name = IntPtr.Zero, func = IntPtr.Zero });
        }

        public override string LibraryName()
        {
            return "Closures";
        }

        public override bool PushToGlobal()
        {
            return true;
        }
    }
}
