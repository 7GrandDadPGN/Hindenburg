using HindenburgDll.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HindenburgDll.Functions
{
	internal class Debug : FunctionHolder
	{

		public override void CreateDefinitions()
		{
			
		}

		public override string LibraryName()
		{
			return "debug";
		}

		public override bool PushToGlobal()
		{
			return false;
		}
	}
}
