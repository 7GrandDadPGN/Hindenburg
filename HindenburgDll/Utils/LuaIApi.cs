using HindenburgDll.Utils;
using HindenburgDll;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace HindenburgDll.Utils
{
    internal class LuaIApi
    {
        // lol pasted from luau github
        public enum lua_Type
        {
            LUA_TNIL = 0,     // must be 0 due to lua_isnoneornil
            LUA_TBOOLEAN = 1, // must be 1 due to l_isfalse

            LUA_TLIGHTUSERDATA,
            LUA_TNUMBER,
            LUA_TVECTOR,

            LUA_TSTRING, // all types above this must be value types, all types below this must be GC types - see iscollectable

            LUA_TTABLE,
            LUA_TFUNCTION,
            LUA_TUSERDATA,
            LUA_TTHREAD,
            LUA_TBUFFER,

            // values below this line are used in GCObject tags but may never show up in TValue type tags
            LUA_TPROTO,
            LUA_TUPVAL,
            LUA_TDEADKEY,

            // the count of TValue type tags
            LUA_T_COUNT = LUA_TPROTO
        };

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibraryA(string dllToLoad);
        private static IntPtr handle = LoadLibraryA("LuauPlugin.dll");

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr StatePointer(IntPtr luaState);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr StateAndIdPointer(IntPtr luaState, int idx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool StateAndIdBool(IntPtr luaState, int idx);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr FieldPointer(IntPtr luaState, int idx, IntPtr str);

        public StatePointer luaAirship_require = Marshal.GetDelegateForFunctionPointer<StatePointer>(new IntPtr(handle.ToInt64() + Offsets.luaAirship_require));

        public StateAndIdBool lua_iscfunction = Marshal.GetDelegateForFunctionPointer<StateAndIdBool>(new IntPtr(handle.ToInt64() + Offsets.lua_iscfunction));
        private FieldPointer getfield = Marshal.GetDelegateForFunctionPointer<FieldPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_getfield));
        public IntPtr lua_getfield(IntPtr thread, int idx, string str)
        {
            IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
            IntPtr res = getfield(thread, idx, strPtr);
            Marshal.FreeCoTaskMem(strPtr);
            return res;
        }

        private FieldPointer setfield = Marshal.GetDelegateForFunctionPointer<FieldPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_setfield));
        public void lua_setfield(IntPtr thread, int idx, string str)
        {
            IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
            setfield(thread, idx, strPtr);
            Marshal.FreeCoTaskMem(strPtr);
        }

        public StateAndIdPointer lua_getmetatable = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_getmetatable));
        public StateAndIdBool lua_getreadonly = Marshal.GetDelegateForFunctionPointer<StateAndIdBool>(new IntPtr(handle.ToInt64() + Offsets.lua_getreadonly));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr rawseti(IntPtr luaState, int idx, int tIdx);
        public checktype lua_rawseti = Marshal.GetDelegateForFunctionPointer<checktype>(new IntPtr(handle.ToInt64() + Offsets.lua_rawseti));

        public StateAndIdPointer lua_pushvalue = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_pushvalue));
        public StatePointer lua_newthread = Marshal.GetDelegateForFunctionPointer<StatePointer>(new IntPtr(handle.ToInt64() + Offsets.lua_newthread));
        public StateAndIdPointer lua_setmetatable = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_setmetatable));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr setsafeenv(IntPtr luaState, int idx, bool safe);
        public setsafeenv lua_setsafeenv = Marshal.GetDelegateForFunctionPointer<setsafeenv>(new IntPtr(handle.ToInt64() + Offsets.lua_setsafeenv));

        public StateAndIdPointer lua_settop = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.lua_settop));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int type(IntPtr luaState, int idx);
        public type lua_type = Marshal.GetDelegateForFunctionPointer<type>(new IntPtr(handle.ToInt64() + Offsets.lua_type));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr xmove(IntPtr luaState, IntPtr destState, int idx);
        public xmove lua_xmove = Marshal.GetDelegateForFunctionPointer<xmove>(new IntPtr(handle.ToInt64() + Offsets.lua_xmove));



        public StateAndIdPointer luaL_checkany = Marshal.GetDelegateForFunctionPointer<StateAndIdPointer>(new IntPtr(handle.ToInt64() + Offsets.luaL_checkany));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr checktype(IntPtr luaState, int idx, int type);
        public checktype luaL_checktype = Marshal.GetDelegateForFunctionPointer<checktype>(new IntPtr(handle.ToInt64() + Offsets.luaL_checktype));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool checkboolean(IntPtr luaState, int idx);
        public checkboolean luaL_checkboolean = Marshal.GetDelegateForFunctionPointer<checkboolean>(new IntPtr(handle.ToInt64() + Offsets.luaL_checkboolean));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr checklstring(IntPtr luaState, int idx, ref int size);
        public checklstring luaL_checklstring = Marshal.GetDelegateForFunctionPointer<checklstring>(new IntPtr(handle.ToInt64() + Offsets.luaL_checklstring));
        public string lua_checkstring(IntPtr thread, int idx)
        {
            int size = 0;
            IntPtr str = luaL_checklstring(thread, idx, ref size);
            return Marshal.PtrToStringUTF8(str, size);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr checkudata(IntPtr luaState, int idx, IntPtr str);
        public checkudata checkudataC = Marshal.GetDelegateForFunctionPointer<checkudata>(new IntPtr(handle.ToInt64() + Offsets.luaL_checkudata));
        public IntPtr luaL_checkudata(IntPtr thread, int idx, string str)
        {
            IntPtr strPtr = Marshal.StringToCoTaskMemUTF8(str);
            IntPtr res = checkudataC(thread, idx, strPtr);
            Marshal.FreeCoTaskMem(strPtr);
            return res;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr register(IntPtr luaState, IntPtr libName, FunctionHolder.luaL_Reg[] luaReg);
        public register luaL_register = Marshal.GetDelegateForFunctionPointer<register>(new IntPtr(handle.ToInt64() + Offsets.luaL_register));
        public StatePointer luaL_sandboxthread = Marshal.GetDelegateForFunctionPointer<StatePointer>(new IntPtr(handle.ToInt64() + Offsets.luaL_sandboxthread));



        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr luauload(IntPtr luaState, IntPtr chunkName, IntPtr bytecode, int bytecodeSize, int native);
        public luauload luau_load = Marshal.GetDelegateForFunctionPointer<luauload>(new IntPtr(handle.ToInt64() + Offsets.luau_load));

        public bool lua_isnoneornil(IntPtr thread, int idx)
        {
            return lua_type(thread, idx) <= (int)lua_Type.LUA_TNIL;
        }

        public IntPtr lua_getglobal(IntPtr thread, string str)
        {
            return lua_getfield(thread, Offsets.LUA_GLOBALSINDEX, str);
        }

        public void lua_setglobal(IntPtr thread, string str)
        {
            lua_setfield(thread, Offsets.LUA_GLOBALSINDEX, str);
        }

        public void lua_limittop(IntPtr thread, int max)
        {
            if (LuauPluginRaw.GetTop(thread) > max)
            {
                lua_settop(thread, max);
            }
        }

        public bool luaL_optboolean(IntPtr thread, int idx, bool extra)
        {
            return lua_isnoneornil(thread, idx) ? extra : luaL_checkboolean(thread, idx);
        }
        public string luaL_optlstring(IntPtr thread, int idx, string extra)
        {
            return lua_isnoneornil(thread, idx) ? extra : lua_checkstring(thread, idx);
        }
    }
}
