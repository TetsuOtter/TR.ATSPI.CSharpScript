using System.Runtime.InteropServices;

namespace TR.ATSPI.CSharpScript
{
	/// <summary>車両のスペック</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Spec
	{
		/// <summary>ブレーキ段数</summary>
		public int BrakeCount;
		/// <summary>ノッチ段数</summary>
		public int PowerCount;
		/// <summary>ATS確認段数</summary>
		public int ATSCheckPos;
		/// <summary>常用最大段数</summary>
		public int B67Pos;
		/// <summary>編成車両数</summary>
		public int CarCount;
	};

	/// <summary>車両の状態</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct State
	{
		/// <summary>列車位置[m]</summary>
		public double Location;
		/// <summary>列車速度[km/h]</summary>
		public float Speed;
		/// <summary>0時からの経過時間[ms]</summary>
		public int Time;
		/// <summary>BC圧力[kPa]</summary>
		public float BCPressure;
		/// <summary>MR圧力[kPa]</summary>
		public float MRPressure;
		/// <summary>ER圧力[kPa]</summary>
		public float ERPressure;
		/// <summary>BP圧力[kPa]</summary>
		public float BPPressure;
		/// <summary>SAP圧力[kPa]</summary>
		public float SAPPressure;
		/// <summary>電流[A]</summary>
		public float Current;
	};

	/// <summary>車両のハンドル位置</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Hand
	{
		/// <summary>ブレーキハンドル位置</summary>
		public int BrakePos;
		/// <summary>ノッチハンドル位置</summary>
		public int PowerPos;
		/// <summary>レバーサーハンドル位置</summary>
		public int ReverserPos;
		/// <summary>定速制御状態</summary>
		public int ConstSpeedStatus;
	};

	/// <summary>Beaconに関する構造体</summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct Beacon
	{
		/// <summary>Beaconの番号</summary>
		public int Num;
		/// <summary>対応する閉塞の現示番号</summary>
		public int Signal;
		/// <summary>対応する閉塞までの距離[m]</summary>
		public float Distance;
		/// <summary>Beaconの第三引数の値</summary>
		public int Data;
	};
}
