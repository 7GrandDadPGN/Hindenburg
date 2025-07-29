using HindenburgDll.Utils;
using Il2Cpp;
using System.Runtime.InteropServices;
using static HindenburgDll.Utils.CompileUtils;
using static HindenburgDll.Utils.LuaIApi;
using HindenburgDll.Utils;
using Il2CppLuau;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEditor;
using static MelonLoader.MelonLogger;
using System.Text;
using Il2CppSystem.Windows.Forms;

namespace HindenburgDll.Functions
{
    internal class Globals : FunctionHolder
    {
        private struct UnityObjectStruct
        {
            public int context;
            public int objectId;
        }

        public static int getgenv(IntPtr luaState)
        {
            api.lua_pushvalue(envHolderInst.exploitState, Offsets.LUA_GLOBALSINDEX);
            if (luaState != envHolderInst.exploitState)
            {
                api.lua_xmove(envHolderInst.exploitState, luaState, 1);
            }

            return 1;
        }

        public static int getrenv(IntPtr luaState)
        {
            api.lua_pushvalue(envHolderInst.globalState, Offsets.LUA_GLOBALSINDEX);
            api.lua_xmove(envHolderInst.globalState, luaState, 1);
            return 1;
        }

        public static int getreg(IntPtr luaState)
        {
            api.lua_pushvalue(luaState, Offsets.LUA_REGISTRYINDEX);
            return 1;
        }

        public static int getrawmetatable(IntPtr luaState)
        {
            api.luaL_checkany(luaState, 1);
            if (api.lua_getmetatable(luaState, 1) == IntPtr.Zero)
            {
                LuauPluginRaw.PushNil(luaState);
            }
            return 1;
        }

        public static int getscripts(IntPtr luaState)
        {
            List<string> alreadyDone = new List<string>();
            GameObject[] list = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            LuauPluginRaw.NewTable(luaState);

            int i = 1;
            foreach (GameObject obj in list)
            {
                AirshipComponent comp;

                if (obj.TryGetComponent(out comp) && !alreadyDone.Contains(comp.scriptPath) && comp.context == LuauContext.Game && AirshipBehaviourRootV2.HasId(obj))
                {
                    alreadyDone.Add(comp.scriptPath);
                    LuauCore.WritePropertyToThread(luaState, comp, comp.GetIl2CppType());
                    api.lua_rawseti(luaState, -2, i);
                    i++;
                }
            }

            alreadyDone.Clear();
            return 1;
        }

        public static int getscriptbytecode(IntPtr luaState)
        {
            IntPtr instanceId = api.luaL_checkudata(luaState, 1, "UnityObject");
            UnityObjectStruct uStruct = Marshal.PtrToStructure<UnityObjectStruct>(instanceId);
            Il2CppSystem.Object obj = ThreadDataManager.GetObjectReference(luaState, uStruct.objectId);

            if (obj is AirshipComponent)
            {
                AirshipComponent compObj = (AirshipComponent)obj;
                LuauPluginRaw.PushString(luaState, Encoding.UTF8.GetString(compObj.script.m_bytes));
            }
            else
            {
                LuauPluginRaw.PushNil(luaState);
            }

            return 1;
        }

        public static int getinstances(IntPtr luaState)
        {
            GameObject[] list = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            LuauPluginRaw.NewTable(luaState);

            int i = 1;
            foreach (GameObject obj in list)
            {
                if (AirshipBehaviourRootV2.HasId(obj))
                {
                    LuauCore.WritePropertyToThread(luaState, obj, obj.GetIl2CppType());
                    api.lua_rawseti(luaState, -2, i);
                    i++;
                }
            }

            return 1;
        }

        public static int run_protected(IntPtr luaState)
        {
            string code = api.lua_checkstring(luaState, 1);
            ExecuteScript(code, envHolderInst, true);
            return 0;
        }

        public static int setclipboard(IntPtr luaState)
        {
            string data = api.lua_checkstring(luaState, 1);
            Clipboard.SetText(data);
            return 0;
        }

        public static int setrawmetatable(IntPtr luaState)
        {
            api.luaL_checkany(luaState, 1);
            api.luaL_checktype(luaState, 1, (int)LuaIApi.lua_Type.LUA_TTABLE);
            if (LuauPluginRaw.GetTop(luaState) != 2)
            {
                api.lua_pushvalue(luaState, 2);
            }

            api.lua_setmetatable(luaState, 1);
            api.lua_pushvalue(luaState, 1);
            return 1;
        }

        public static int setreadonly(IntPtr luaState)
        {
            api.luaL_checktype(luaState, 1, (int)LuaIApi.lua_Type.LUA_TTABLE);
            LuauPluginRaw.SetReadonly(luaState, 1, api.luaL_optboolean(luaState, 2, false));
            return 0;
        }

        public static int isreadonly(IntPtr luaState)
        {
            api.luaL_checktype(luaState, 1, (int)LuaIApi.lua_Type.LUA_TTABLE);
            LuauPluginRaw.PushBoolean(luaState, api.lua_getreadonly(luaState, 1));
            return 1;
        }

        public static int identifyexecutor(IntPtr luaState)
        {
            LuauPluginRaw.PushString(luaState, "Hindenburg");
            LuauPluginRaw.PushString(luaState, "v1.0.0");
            return 2;
        }

        public override void CreateDefinitions()
        {
            Add("getgenv", getgenv);
            Add("getrenv", getrenv);
            Add("getreg", getreg);
            Add("getrawmetatable", getrawmetatable);
            Add("getscripts", getscripts);
            Add("getscriptbytecode", getscriptbytecode);
            Add("getinstances", getinstances);
            Add("run_protected", run_protected);
            Add("setclipboard", setclipboard);
            Add("setrawmetatable", setrawmetatable);
            Add("setreadonly", setreadonly);
            Add("isreadonly", isreadonly);
            Add("identifyexecutor", identifyexecutor);
            luaReg.Add(new luaL_Reg { name = IntPtr.Zero, func = IntPtr.Zero });
        }

        public override string LibraryName()
        {
            return "Global";
        }

        public override bool PushToGlobal()
        {
            return true;
        }
    }
}
