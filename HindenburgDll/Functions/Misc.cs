using HindenburgDll.Structs;
using Il2Cpp;
using System.Runtime.InteropServices;
using static HindenburgDll.Utils.CompileUtils;

namespace HindenburgDll.Functions
{
	internal class Misc : FunctionHolder
	{
		private delegate bool EnumThreadDelegate(IntPtr hwnd, IntPtr lParam);

		[DllImport("kernel32.dll")]
		private static extern int GetCurrentThreadId();

		[DllImport("user32.dll")]
		private static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

		[DllImport("user32.dll", EntryPoint = "MessageBoxA")]
		public static extern int MessageBoxA([In()] IntPtr hWnd, [In()][MarshalAs(UnmanagedType.LPStr)] string lpText, [In()][MarshalAs(UnmanagedType.LPStr)] string lpCaption, uint uType);

		[DllImport("user32.dll", EntryPoint = "SetWindowText")]
		public static extern bool SetWindowText([In()] IntPtr hwnd, [In()][MarshalAs(UnmanagedType.LPStr)] string lpString);

		// https://stackoverflow.com/questions/13074421/c-sharp-equivalent-of-messageboxahwnd-desktop-msg-alarm-mb-ok-mb-iconwar
		private static IntPtr GetWindowHandle()
		{
			IntPtr returnHwnd = IntPtr.Zero;
			int threadId = GetCurrentThreadId();
			EnumThreadWindows(threadId, (hWnd, lParam) =>
			{
				if (returnHwnd == IntPtr.Zero)
				{
					returnHwnd = hWnd;
				}
				return true;
			}, IntPtr.Zero);
			return returnHwnd;
		}

		public static int messagebox(IntPtr luaState)
		{
			string text = api.lua_tostring(luaState, 1);
			string title = api.lua_tostring(luaState, 2);
			int type = api.lua_tointeger(luaState, 3);
			int box = MessageBoxA(GetWindowHandle(), text, title, (uint)type);

			LuauPluginRaw.PushInteger(luaState, box);

			return 1;
		}

		public static int setwindowtitle(IntPtr luaState)
		{
			string title = api.lua_tostring(luaState, 1);
			SetWindowText(GetWindowHandle(), title);
			return 0;
		}

		public static int queue_on_teleport(IntPtr luaState)
		{
			string code = api.lua_tostring(luaState, 1);
			Hindenburg.teleportQueue.Add(code);
			return 0;
		}

		public static int clear_teleport_queue(IntPtr luaState)
		{
			Hindenburg.teleportQueue.Clear();
			return 0;
		}

		public override void CreateDefinitions()
		{
			Add("messagebox", messagebox);
			Add("setwindowtitle", setwindowtitle);
			Add("queue_on_teleport", queue_on_teleport);
			Add("clear_teleport_queue", clear_teleport_queue);
		}

		public override string LibraryName()
		{
			return "misc";
		}

		public override bool PushToGlobal()
		{
			return true;
		}
	}
}
