using System.Runtime.InteropServices;

namespace HindenburgDll.Structs
{
	internal abstract class FunctionHolder
	{
		public struct luaL_Reg
		{
			public luaL_Reg(string bname, IntPtr bfunc)
			{
				name = Marshal.StringToCoTaskMemUTF8(bname);
				func = bfunc;
			}

			public IntPtr name;
			public IntPtr func;
		};
		public List<luaL_Reg> luaReg = new List<luaL_Reg>();
		public List<luaFuncC> gcReg = new List<luaFuncC>();
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int luaFuncC(IntPtr luaState);

		public abstract void CreateDefinitions();
		public abstract bool PushToGlobal();
		public abstract string LibraryName();

		public void Add(string bname, luaFuncC bfunc)
		{
			gcReg.Add(bfunc);
			luaReg.Add(new luaL_Reg(bname, Marshal.GetFunctionPointerForDelegate(bfunc)));
		}
	}
}
