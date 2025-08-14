/*
 * This project is skidded from https://github.com/SecondNewtonLaw/RbxStu-V3/ just to get a simple executor in Airship lol.
 * All credits go to Dottik, Joe, Ragnar, MakeSureDudeDies, landervander, Lonegladiator (funny), and senS.
 */
using HindenburgDll;
using HindenburgDll.Patches;
using HindenburgDll.Structs;
using MelonLoader;
using System.Collections;
using System.IO.Pipes;
using System.Text;
using static HindenburgDll.Utils.CompileUtils;
using Action = System.Action;
using IntPtr = System.IntPtr;

[assembly: MelonInfo(typeof(Hindenburg), "Hindenburg", "1.0.1", "7GrandDad")]
namespace HindenburgDll
{
	public class Hindenburg : MelonMod
	{
		public static readonly Dictionary<int, object> gcList = new Dictionary<int, object>();
		public static readonly List<string> teleportQueue = new List<string>();
		private string autoExecutePath = "";
		private StringBuilder builder = new StringBuilder();
		private FunctionHolder[] env = new FunctionHolder[] {
			new Functions.Crypt(),
			new Functions.Closures(),
			new Functions.Debug(),
			new Functions.Globals(),
			new Functions.FileSystem(),
			new Functions.Http(),
			new Functions.Misc()
		};

		public override void OnInitializeMelon()
		{
			foreach (FunctionHolder obj in env)
			{
				obj.CreateDefinitions();
				obj.luaReg.Add(new luaL_Reg { name = IntPtr.Zero, func = IntPtr.Zero });
			}

			LuaContextPatch.reloadAction += ReloadHandlers;
			NetworkManagerPatch.teleportAction += HandleTeleport;

			Task.Run(() => {
				NamedPipeServerStream server = new NamedPipeServerStream("AirshipExecutor");
				server.WaitForConnection();

				LoggerInstance.Msg($"Executor initialized!");
				StreamReader reader = new StreamReader(server);
				while (server.IsConnected)
				{
					int lchar = reader.Read();
					if (lchar == -1 || lchar == 255)
					{
						if (autoExecutePath == "")
						{
							autoExecutePath = Path.GetFullPath(builder.ToString() + "/autoexec");
							Functions.FileSystem.basePath = Path.GetFullPath(builder.ToString() + "/workspace");
							builder.Clear();
							continue;
						}

						MelonCoroutines.Start(MainThreadCoroutine(() =>
						{
							string code = builder.ToString();
							builder.Clear();
							ExecuteScript(code, false);
						}));
					}
					else
					{
						builder.Append((char)lchar);
					}
				}
			});
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			if (envHolder.globalState == IntPtr.Zero)
			{
				ReloadHandlers();
			}

			if (sceneName == "MainMenu")
			{
				teleportQueue.Clear();
			}
		}
		private void HandleTeleport()
		{
			if (autoExecutePath != "")
			{
				FileInfo[] files = new DirectoryInfo(autoExecutePath).GetFiles();

				foreach (FileInfo file in files)
				{
					if (file.Extension == ".lua")
					{
						ExecuteScript(File.ReadAllText(file.FullName), false);
					}
				}

				foreach (string script in teleportQueue)
				{
					ExecuteScript(script, false);
				}
			}

			teleportQueue.Clear();
		}

		public void ReloadHandlers()
		{
			TaskSchedulerPatch.awaitingTasks.Clear();
			CreateHolder(env);
		}

		private static IEnumerator MainThreadCoroutine(Action action)
		{
			yield return null;
			action?.Invoke();
		}
	}
}