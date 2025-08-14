/*
 * This project is skidded from https://github.com/SecondNewtonLaw/RbxStu-V3/ just to get a simple executor in Airship lol.
 * All credits go to Dottik, Pixeluted, Joe, MakeSureDudeDies, landervander, Lonegladiator (funny), senS
 */
using MelonLoader;

using static HindenburgDll.Utils.CompileUtils;
using Action = System.Action;
using IntPtr = System.IntPtr;
using HindenburgDll.Utils;
using System.Collections;
using System.IO.Pipes;
using HindenburgDll;
using System.Text;
using HarmonyLib;
using Il2Cpp;
using Il2CppLuau;
using HindenburgDll.Structs;
using Il2CppMirror;
using UnityEngine;
using HindenburgDll.Patches;

[assembly: MelonInfo(typeof(Hindenburg), "Hindenburg", "1.0.0", "7GrandDad")]
namespace HindenburgDll
{
	public class Hindenburg : MelonMod
	{
		public static readonly Dictionary<int, object> gcList = new Dictionary<int, object>();
		public static int pingDelay = 0;
		private string autoExecutePath = "";
		private StringBuilder builder = new StringBuilder();
		private FunctionHolder[] env = new FunctionHolder[] {
			new Functions.Crypt(),
			new Functions.Closures(),
			new Functions.Debug(),
			new Functions.Globals(),
			new Functions.FileSystem(),
			new Functions.Http()
		};
		private EnvHolder envHolder;

		private static IEnumerator MainThreadCoroutine(Action action)
		{
			yield return null;
			action?.Invoke();
		}

		public override void OnInitializeMelon()
		{
			foreach (FunctionHolder obj in env)
			{
				obj.CreateDefinitions();
			}

			LuaContextPatch.reloadAction += ReloadHandlers;

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

							if (envHolder.globalState != IntPtr.Zero)
							{
								LoggerInstance.Msg($"executed {code}");
								ExecuteScript(code, envHolder, false);
							}
						}));
					}
					else
					{
						builder.Append((char)lchar);
					}
				}
			});
		}

		public void ReloadHandlers()
		{
			TaskSchedulerPatch.awaitingTasks.Clear();
			envHolder = CreateHolder(env);
			CompileUtils.envHolderInst = envHolder;
		}

		public override void OnSceneWasLoaded(int buildIndex, string sceneName)
		{
			if (envHolder.globalState == IntPtr.Zero)
			{
				ReloadHandlers();
			}

			if (autoExecutePath != "" && sceneName == "CoreScene" && buildIndex == 1)
			{
				FileInfo[] files = new DirectoryInfo(autoExecutePath).GetFiles();

				foreach (FileInfo file in files)
				{
					if (file.Extension == ".lua")
					{
						ExecuteScript(File.ReadAllText(file.FullName), envHolder, false);
					}
				}
			}
		}
	}
}