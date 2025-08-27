using UnityEngine;

namespace HindenburgDll.Utils
{
	internal class AOBDumper
	{
		private static byte[] dllBytes = File.ReadAllBytes(Application.dataPath + "/Plugins/x86_64/LuauPlugin.dll");

		private static (byte[] bytes, bool[] mask) FromSignature(string sig)
		{
			string[] parsed = sig.Split(' ');
			byte[] bytes = new byte[parsed.Length];
			bool[] mask = new bool[parsed.Length];

			for (int i = 0; i < parsed.Length; i++)
			{
				bytes[i] = parsed[i].Contains("?") ? (byte)0x0 : Convert.ToByte(parsed[i], 16);
				mask[i] = !parsed[i].Contains("?");
			}

			return (bytes, mask);
		}

		public static int ScanFunction(string sig)
		{
			(byte[] bytes, bool[] mask) = FromSignature(sig);
			for (int i = 0; i < dllBytes.Length - bytes.Length + 1; i++)
			{
				bool found = true;
				for (int j = 0; j < bytes.Length; j++)
				{
					if (i + j >= dllBytes.Length || (mask[j] && dllBytes[i + j] != bytes[j]))
					{
						found = false;
						break;
					}
				}

				if (found)
				{
					return (i + 0xC00);
				}
			}
			return 0;
		}
	}
}
