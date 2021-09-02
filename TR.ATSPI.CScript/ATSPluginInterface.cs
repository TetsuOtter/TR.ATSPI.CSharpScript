using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TR.ATSPI.CScript
{
	public static class ATSPluginInterface
	{
		static public uint ATSPluginInterfaceVersionNum = 0x00020000;

		static ATSPluginInterface()
		{
#if DEBUG
			if(!Debugger.IsAttached)
				Debugger.Launch();
#endif

			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		static string CurrentDllDirectoryPath { get; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

		private static Assembly? CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			AssemblyName asmName = new(args.Name);

			string path = Path.Combine(CurrentDllDirectoryPath, asmName.Name + ".dll");

			//ファイルが存在するか確認
			if (!File.Exists(path))
				return null;

			/* FullName一致の判定はとりあえず保留 (バージョンが面倒)
			//とりあえず, 実行できない方法(依存関係等も解決しない)で読み込む
			var asm = Assembly.ReflectionOnlyLoadFrom(path);

			//FullNameが一致しないなら読み込まない
			if (asm.FullName != asmName.FullName)
				return null;
			*/

			//正式にロードして返す
			return Assembly.LoadFrom(path);
		}

		static ManagedATSPI? pi { get; set; }

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void Dispose()
		{
			pi?.Dispose();
			pi = null;
		}

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void DoorClose() => pi?.DoorClose();

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void DoorOpen() => pi?.DoorOpen();

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static Hand Elapse(State s, IntPtr Pa, IntPtr So) => pi?.Elapse(s, Pa, So) ?? default;

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static uint GetPluginVersion() => pi?.GetPluginVersion() ?? ATSPluginInterfaceVersionNum;

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void HornBlow(int k) => pi?.HornBlow(k);

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void Initialize(int s) => pi?.Initialize(s);

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void KeyDown(int k) => pi?.KeyDown(k);

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void KeyUp(int k) => pi?.KeyUp(k);

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void Load()
		{
			Debug.WriteLine("Load method");
			pi = new();

			//スクリプトをロードできなければ, 以降何も実行しない
			if(!pi.LoadScriptPathListsAndScripts())
				pi = null;
			Debug.WriteLine($"Load()...pi:{pi}");
			pi?.Load();
		}

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void SetBeaconData(Beacon b) => pi?.SetBeaconData(b);

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void SetBrake(int b) => pi?.SetBrake(b);

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void SetPower(int p) => pi?.SetPower(p);

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void SetReverser(int r) => pi?.SetReverser(r);

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void SetSignal(int s) => pi?.SetSignal(s);

		[DllExport(CallingConvention = CallingConvention.StdCall)]
		public static void SetVehicleSpec(Spec s) => pi?.SetVehicleSpec(s);

	}
}
