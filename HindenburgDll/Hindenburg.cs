/*
 * This project is skidded from https://github.com/SecondNewtonLaw/RbxStu-V3/ just to get a simple executor in Airship lol.
 * All credits go to Dottik, Pixeluted, Joe, MakeSureDudeDies, landervander, Lonegladiator (funny), senS
 */
using MelonLoader;

using HindenburgDll;
using System.IO.Pipes;
using System.Text;
using HindenburgDll.Utils;
using static HindenburgDll.Utils.CompileUtils;
using IntPtr = System.IntPtr;
using Action = System.Action;
using System.Collections;
using Il2CppLuau;
using Il2Cpp;
using HarmonyLib;

[assembly: MelonInfo(typeof(Hindenburg), "Hindenburg", "1.0.0", "7GrandDad")]
namespace HindenburgDll
{
    public class Hindenburg : MelonMod
    {
        private StringBuilder builder = new StringBuilder();
        private FunctionHolder[] env = new FunctionHolder[] {
            new Functions.Closures(),
            new Functions.Globals()
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

                LoggerInstance.Msg($"pipe connected!");
                StreamReader reader = new StreamReader(server);
                while (server.IsConnected)
                {
                    int lchar = reader.Read();
                    if (lchar == -1 || lchar == 255)
                    {
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
            envHolder = CreateHolder(env);
            CompileUtils.envHolderInst = envHolder;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (envHolder.globalState == IntPtr.Zero)
            {
                ReloadHandlers();
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
}