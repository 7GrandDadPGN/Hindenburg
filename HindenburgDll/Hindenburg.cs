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

[assembly: MelonInfo(typeof(Hindenburg), "Hindenburg", "1.0.0", "7GrandDad")]
namespace HindenburgDll
{
	public class Hindenburg : MelonMod
	{
		public static readonly List<AwaitingTask> awaitingTasks = new List<AwaitingTask>();
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

			LuaPatch.reloadAction += ReloadHandlers;

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
			awaitingTasks.Clear();
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

	[HarmonyPatch(typeof(LuauCore), "ResetContext", new Type[] { typeof(LuauContext) })]
	public static class LuaPatch
	{
		public static event Action reloadAction;
		private static void Postfix(LuauContext context)
		{
			if (context == LuauContext.Protected)
			{
				reloadAction?.Invoke();
			}
		}
	}

	[HarmonyPatch(typeof(ThreadDataManager), "InvokeUpdate")]
	public static class TaskPatch
	{
		private static void Prefix()
		{
			for (var i = 0; i < Hindenburg.awaitingTasks.Count; i++)
			{
				AwaitingTask awaitingTask = Hindenburg.awaitingTasks[i];
				if (!awaitingTask.Task.IsCompleted) continue;

				Hindenburg.awaitingTasks.RemoveAt(i);
				ResumeAsyncTask(awaitingTask);
				i--;
			}
		}

		public static async void ResumeAsyncTask(AwaitingTask awaitingTask, bool immediate = false)
		{
			IntPtr thread = awaitingTask.Thread;

			if (awaitingTask.ThreadRef != 0)
			{
				LuauPluginRaw.Unref(thread, awaitingTask.ThreadRef);
			}

			if (awaitingTask.Task.IsFaulted)
			{
				LuauPluginRaw.PushString(thread, $"Error: Exception thrown in {awaitingTask.Task.Exception.Message}");
				ThreadDataManager.Error(thread);
				LuauPlugin.LuauResumeThreadError(thread);
				return;
			}

			awaitingTask.Task.Result();
			if (!immediate)
			{
				try
				{
					LuauPlugin.LuauResumeThread(thread, 1);
				}
				catch
				{
					LuauPluginRaw.PushString(thread, $"Error: Exception thrown in");
					ThreadDataManager.Error(thread);
					LuauPlugin.LuauResumeThreadError(thread);
				}
			}
		}
	}
}