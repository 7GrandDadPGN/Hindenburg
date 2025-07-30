using System.Runtime.InteropServices;
using static HindenburgDll.Utils.CompileUtils;
using static HindenburgDll.Utils.LuaIApi;
using HindenburgDll.Utils;
using Il2Cpp;
using System.Text;

namespace HindenburgDll.Functions
{
    internal class Crypt : FunctionHolder
    {
        public static int base64encode(IntPtr luaState)
        {
            byte[] inputData = api.lua_checkstringB(luaState, 1);
            LuauPluginRaw.PushString(luaState, Convert.ToBase64String(inputData));
            return 1;
        }

        public static int base64decode(IntPtr luaState)
        {
            string inputData = api.lua_checkstring(luaState, 1);

            try
            {
                byte[] data = Convert.FromBase64String(inputData);
                api.lua_pushlstringB(luaState, data);
            }
            catch
            {
                api.luaL_error(luaState, "Invalid base64 data");
            }


            return 1;
        }

        public override void CreateDefinitions()
        {
            Add("base64encode", base64encode);
            Add("base64decode", base64decode);
            luaReg.Add(new luaL_Reg { name = IntPtr.Zero, func = IntPtr.Zero });
        }

        public override string LibraryName()
        {
            return "crypt";
        }

        public override bool PushToGlobal()
        {
            return false;
        }
    }
}
