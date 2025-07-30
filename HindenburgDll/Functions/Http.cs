using HindenburgDll.Utils;

namespace HindenburgDll.Functions
{
	internal class Http : FunctionHolder
	{

		public override void CreateDefinitions()
		{
			//Add("base64encode", Crypt.base64encode);
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
