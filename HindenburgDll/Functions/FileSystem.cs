using System.Runtime.InteropServices;
using static HindenburgDll.Utils.CompileUtils;
using static HindenburgDll.Utils.LuaIApi;
using HindenburgDll.Utils;
using Il2Cpp;
using System.Text;

namespace HindenburgDll.Functions
{
	internal class FileSystem : FunctionHolder
	{
		public static string basePath = @"";
		private readonly static List<string> whitelistedExtentions = new List<string>
		{
			".txt", ".json", ".lua", ".luau",
			".dat", ".bin", ".luac",
			".png", ".jpg", ".webp", ".jpeg", ".bmp",
			".ogg", ".mp3", ".mid",
			".mp4", ".webm", ".mov", ""
		};

		private static string GetSafePath(IntPtr luaState)
		{
			string destPath = api.lua_checkstring(luaState, 1); // error on invalid type (in this case string)
			string inputPath = Path.GetFullPath(basePath + "/" + destPath);

			if (!inputPath.StartsWith(basePath))
			{
				api.luaL_argerrorL(luaState, 1, "Invalid path");
			}

			return inputPath;
		}

		public static int isfolder(IntPtr luaState)
		{
			string folderPath = GetSafePath(luaState);
			LuauPluginRaw.PushBoolean(luaState, Directory.Exists(folderPath));
			return 1;
		}

		public static int makefolder(IntPtr luaState)
		{
			string folderPath = GetSafePath(luaState);

			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}

			LuauPluginRaw.PushBoolean(luaState, Directory.Exists(folderPath));
			return 1;
		}

		public static int delfolder(IntPtr luaState)
		{
			string folderPath = GetSafePath(luaState);

			if (!folderPath.Equals(basePath))
			{
				if (Directory.Exists(folderPath))
				{
					Directory.Delete(folderPath, true);
				}
			}
			else
			{
				api.luaL_error(luaState, "Cannot delete workspace folder");
			}

			return 0;
		}

		public static int isfile(IntPtr luaState)
		{
			string filePath = GetSafePath(luaState);
			LuauPluginRaw.PushBoolean(luaState, File.Exists(filePath));
			return 1;
		}

		public static int readfile(IntPtr luaState)
		{
			string filePath = GetSafePath(luaState);

			if (File.Exists(filePath))
			{
				api.lua_pushlstringB(luaState, File.ReadAllBytes(filePath));
			}
			else
			{
				api.luaL_argerrorL(luaState, 1, "invalid path");
			}

			return 1;
		}

		public static int loadfile(IntPtr luaState)
		{
			string filePath = GetSafePath(luaState);

			if (File.Exists(filePath))
			{
				string code = File.ReadAllText(filePath);
				string chunkName = api.luaL_optlstring(luaState, 2, RandomString(8));
				api.lua_limittop(luaState, 1);
				CompilationResult compilationResult = CompileScriptData(code, chunkName);

				if (!compilationResult.Compiled)
				{
					LuauPluginRaw.PushNil(luaState);
					return 1;
				}

				IntPtr namePointer = Marshal.StringToCoTaskMemUTF8(chunkName);
				api.luau_load(luaState, namePointer, compilationResult.Data, (int)compilationResult.DataSize, 0);
				api.lua_setsafeenv(luaState, Offsets.LUA_GLOBALSINDEX, false);
				Marshal.FreeCoTaskMem(namePointer);
			}
			else
			{
				api.luaL_argerrorL(luaState, 1, "invalid path");
			}

			return 1;
		}

		public static int appendfile(IntPtr luaState)
		{
			string filePath = GetSafePath(luaState);
			byte[] inputData = api.lua_checkstringB(luaState, 2);
			string fileExtension = Path.GetExtension(filePath);

			if (!whitelistedExtentions.Contains(fileExtension))
			{
				api.luaL_argerrorL(luaState, 1, $"invalid extension type {fileExtension}");
			}

			if (File.Exists(filePath))
			{
				byte[] fileData = File.ReadAllBytes(filePath);
				byte[] combinedData = new byte[fileData.Length + inputData.Length];
				Buffer.BlockCopy(fileData, 0, combinedData, 0, fileData.Length);
				Buffer.BlockCopy(inputData, 0, combinedData, fileData.Length, inputData.Length);
				File.WriteAllBytes(filePath, combinedData);
			}
			else
			{
				api.luaL_argerrorL(luaState, 1, "invalid path");
			}

			return 0;
		}

		public static int writefile(IntPtr luaState)
		{
			string filePath = GetSafePath(luaState);
			byte[] inputData = api.lua_checkstringB(luaState, 2);
			string fileExtension = Path.GetExtension(filePath);

			if (!whitelistedExtentions.Contains(fileExtension))
			{
				api.luaL_argerrorL(luaState, 1, $"invalid extension type {fileExtension}");
			}

			File.WriteAllBytes(filePath, inputData);
			return 0;
		}

		public static int listfiles(IntPtr luaState)
		{
			string filePath = GetSafePath(luaState);

			if (Directory.Exists(filePath))
			{
				FileSystemInfo[] Files = new DirectoryInfo(filePath).GetFileSystemInfos();
				LuauPluginRaw.NewTable(luaState);

				for (int i = 0; i < Files.Length; i++)
				{
					FileSystemInfo File = Files[i];
					LuauPluginRaw.PushString(luaState, File.FullName.Substring(basePath.Length + 1));
					api.lua_rawseti(luaState, -2, i);
				}
			}
			else
			{
				api.luaL_argerrorL(luaState, 1, "invalid path");
			}

			return 1;
		}

		public override void CreateDefinitions()
		{
			Add("isfolder", isfolder);
			Add("makefolder", makefolder);
			Add("delfolder", delfolder);
			Add("isfile", isfile);
			Add("readfile", readfile);
			Add("loadfile", loadfile);
			Add("appendfile", appendfile);
			Add("writefile", writefile);
			Add("listfiles", listfiles);
			luaReg.Add(new luaL_Reg { name = IntPtr.Zero, func = IntPtr.Zero });
		}

		public override string LibraryName()
		{
			return "filesystem";
		}

		public override bool PushToGlobal()
		{
			return true;
		}
	}
}
