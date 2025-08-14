using System.Runtime.InteropServices;

namespace HindenburgDll.Structs
{
	public struct AwaitingTask
	{
		public IntPtr Thread;
		public int ThreadRef;
		public Task<Action> Task;
	}

	public struct EnvHolder
	{
		public IntPtr globalState;
		public IntPtr coreState;
		public IntPtr exploitState;
	}

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

	internal abstract class FunctionHolder
	{
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
