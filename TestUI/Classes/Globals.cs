using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ContentInjector
{
	// Token: 0x02000006 RID: 6
	public static class Globals
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00004230 File Offset: 0x00002430
		public static double GetDirectorySize(string directory)
		{
			string[] directories = Directory.GetDirectories(directory);
			for (int i = 0; i < directories.Length; i++)
			{
				Globals.GetDirectorySize(directories[i]);
			}
			foreach (FileInfo fileInfo in new DirectoryInfo(directory).GetFiles())
			{
				Globals.size += (double)fileInfo.Length;
			}
			return Globals.size;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004290 File Offset: 0x00002490
		public static string HandleSize(long Byte)
		{
			string[] array = new string[]
			{
				"B",
				"KB",
				"MB",
				"GB",
				"TB",
				"PB",
				"EB"
			};
			if (Byte == 0L)
			{
				return "0" + array[0];
			}
			long num = Math.Abs(Byte);
			int num2 = Convert.ToInt32(Math.Floor(Math.Log((double)num, 1024.0)));
			double num3 = Math.Round((double)num / Math.Pow(1024.0, (double)num2), 2);
			return ((double)Math.Sign(Byte) * num3).ToString() + array[num2];
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004340 File Offset: 0x00002540
		public static void ClearStrings()
		{
			Globals.GODbytes = 0L;
			Globals.Pushing = false;
			Globals.Connecting = false;
			Globals.sw.Reset();
			Globals.GODDataConsoleDir = "";
			Globals.PushingtoConsole = "";
			Globals.file = "";
			Globals.TitleId = "";
			Globals.Type = "";
			Globals.size = 0.0;
			Globals.Failed = false;
			Globals.Filename = "";
			Globals.Fileinfo = "";
			Globals.ProfileId = "";
			Globals.consolePath = "";
			Globals.consoleFile = "";
			Globals.fileSize = "";
			Globals.isGamerPic = false;
			Globals.isGOD = false;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000043F8 File Offset: 0x000025F8
		public static string Hex2Text(string hexString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i <= hexString.Length - 2; i += 2)
			{
				stringBuilder.Append(Convert.ToChar(int.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004444 File Offset: 0x00002644
		public static void changeLine(RichTextBox RTB, int line, string text)
		{
			int firstCharIndexFromLine = RTB.GetFirstCharIndexFromLine(line);
			int num = (line < RTB.Lines.Length - 1) ? (RTB.GetFirstCharIndexFromLine(line + 1) - 1) : RTB.Text.Length;
			RTB.Select(firstCharIndexFromLine, num - firstCharIndexFromLine);
			RTB.SelectedText = text;
		}

        // Token: 0x0400000E RID: 14
        public static string settings = "";

		// Token: 0x0400000F RID: 15
		public static bool AtMS = false;

		// Token: 0x04000010 RID: 16
		public static bool Back2MS = false;

		// Token: 0x04000011 RID: 17
		public static string Details;

		// Token: 0x04000012 RID: 18
		public static bool UpdChecking = false;

		// Token: 0x04000013 RID: 19
		public static bool Updatable = false;

		// Token: 0x04000014 RID: 20
		public static Version currentver;

		// Token: 0x04000015 RID: 21
		public static Version updatever;

		// Token: 0x04000016 RID: 22
		public static bool KeyExit = false;

		// Token: 0x04000017 RID: 23
		public static long GODbytes = 0L;

		// Token: 0x04000018 RID: 24
		public static string file;

		// Token: 0x04000019 RID: 25
		public static string TitleId;

		// Token: 0x0400001A RID: 26
		public static string Type;

		// Token: 0x0400001B RID: 27
		public static bool Failed = false;

		// Token: 0x0400001C RID: 28
		public static string Filename;

		// Token: 0x0400001D RID: 29
		public static string Fileinfo;

		// Token: 0x0400001E RID: 30
		public static string ProfileId = null;

		// Token: 0x0400001F RID: 31
		public static string consolePath;

		// Token: 0x04000020 RID: 32
		public static string consoleFile;

		// Token: 0x04000021 RID: 33
		public static bool Pushing = false;

		// Token: 0x04000022 RID: 34
		public static bool Connecting = false;

		// Token: 0x04000023 RID: 35
		public static string fileSize;

		// Token: 0x04000024 RID: 36
		public static string[] args;

		public static bool Connected = true;

		// Token: 0x04000025 RID: 37
		public static string[] lines = new string[]
		{
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			""
		};

		// Token: 0x04000026 RID: 38
		public static bool isGamerPic = false;

		// Token: 0x04000027 RID: 39
		public static bool isGOD = false;

		// Token: 0x04000028 RID: 40
		public static bool isConnected = false;

		// Token: 0x04000029 RID: 41
		public static string GODDataConsoleDir;

		// Token: 0x0400002A RID: 42
		public static string PushingtoConsole;

		// Token: 0x0400002B RID: 43
		public static double size = 0.0;

		// Token: 0x0400002C RID: 44
		public static Stopwatch sw = new Stopwatch();
	}
}
