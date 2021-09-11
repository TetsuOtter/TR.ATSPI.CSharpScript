using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TR.ATSPI.CScript
{
	public class GlobalVariable
	{
		public Dictionary<string, object> ObjectHolder { get; internal set; } = new();

		#region SetVehicleSpec
		public int BrakeCount { get; internal set; }
		public int PowerCount { get; internal set; }
		public int ATSCheckPos { get; internal set; }
		public int B67Pos { get; internal set; }
		public int CarCount { get; internal set; }
		#endregion

		#region Elapse
		public double Location { get; internal set; }
		public float Speed { get; internal set; }
		public TimeSpan Time { get; internal set; }
		public float BCPressure { get; internal set; }
		public float MRPressure { get; internal set; }
		public float ERPressure { get; internal set; }
		public float BPPressure { get; internal set; }
		public float SAPPressure { get; internal set; }
		public float Current { get; internal set; }
		public UnmanagedArray Panel { get; internal set; } = new(IntPtr.Zero);
		public UnmanagedArray Sound { get; internal set; } = new(IntPtr.Zero);
		#endregion

		#region SetBeaconData
		public int BeaconNum { get; internal set; }
		public int BeaconSignal { get; internal set; }
		public float BeaconDistance { get; internal set; }
		public int BeaconData { get; internal set; }
		#endregion

		#region Action<public int> Args
		public int InitializeNum { get; internal set; }
		public int SetPowerArg { get; internal set; }
		public int SetBrakeArg { get; internal set; }
		public int SetReverserArg { get; internal set; }
		public int KeyDown { get; internal set; }
		public int KeyUp { get; internal set; }
		public int HornBlow { get; internal set; }
		public int SetSignal { get; internal set; }
		#endregion

		public bool IsDoorClosed { get; internal set; }

		#region Elapse returns
		public int BrakePosReturn { get; set; }
		public int PowerPosReturn { get; set; }
		public int ReverserPosReturn { get; set; }
		public int ConstSpeedStateReturn { get; set; }
		#endregion


		public GlobalVariable SetVehicleSpec(in Spec spec)
		{
			BrakeCount = spec.BrakeCount;
			PowerCount = spec.PowerCount;
			ATSCheckPos = spec.ATSCheckPos;
			B67Pos = spec.B67Pos;
			CarCount = spec.CarCount;
			return this;
		}

		public GlobalVariable Elapse(in State state, in IntPtr panel, in IntPtr sound)
		{
			Location = state.Location;
			Speed = state.Speed;
			Time = TimeSpan.FromMilliseconds(state.Time);
			BCPressure = state.BCPressure;
			MRPressure = state.MRPressure;
			ERPressure = state.ERPressure;
			BPPressure = state.BPPressure;
			SAPPressure = state.SAPPressure;
			Current = state.Current;

			Panel = new(panel);
			Sound = new(sound);

			BrakePosReturn = SetBrakeArg;
			PowerPosReturn = SetPowerArg;
			ReverserPosReturn = SetReverserArg;

			return this;
		}

		public GlobalVariable SetBeaconData(in Beacon beacon)
		{
			BeaconData = beacon.Data;
			BeaconDistance = beacon.Distance;
			BeaconNum = beacon.Num;
			BeaconSignal = beacon.Signal;

			return this;
		}

		public Hand GetHand()
			=> new()
			{
				BrakePos = BrakePosReturn,
				PowerPos = PowerPosReturn,
				ReverserPos = ReverserPosReturn,
				ConstSpeedStatus = ConstSpeedStateReturn
			};

		public record UnmanagedArray(IntPtr IntPtr)
		{
			public int this[int index]
			{
				get => Marshal.ReadInt32(IntPtr, index * sizeof(int));
				set => Marshal.WriteInt32(IntPtr, index * sizeof(int), value);
			}
		}
	}
}

#if NETFRAMEWORK
// ref : https://ufcpp.net/blog/2020/6/cs9vs16_7p3/
// IsExternalInit属性

namespace System.Runtime.CompilerServices
{
	internal class IsExternalInit
	{

	}
}
#endif