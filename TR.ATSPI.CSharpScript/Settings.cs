using System.Collections.Generic;

namespace TR.ATSPI.CSharpScript
{
	public interface ISettings<T>
	{
		List<T> LoadScripts { get; }
		List<T> DisposeScripts { get; }
		List<T> SetVehicleSpecScripts { get; }
		List<T> InitializeScripts { get; }
		List<T> ElapseScripts { get; }
		List<T> SetPowerScripts { get; }
		List<T> SetBrakeScripts { get; }
		List<T> SetReverserScripts { get; }
		List<T> KeyDownScripts { get; }
		List<T> KeyUpScripts { get; }
		List<T> HornBlowScripts { get; }
		List<T> DoorOpenScripts { get; }
		List<T> DoorCloseScripts { get; }
		List<T> SetSignalScripts { get; }
		List<T> SetBeaconDataScripts { get; }
		List<T> GetPluginVersionScripts { get; }
	}

	public class ScriptPathListClass : ISettings<string>
	{
		[System.Xml.Serialization.XmlIgnore]
		public string CurrentScriptFileListPath { get; set; } = string.Empty;
		public List<string> ScriptFileLists { get; set; } = new();
		public List<string> LoadScripts { get; set; } = new();
		public List<string> DisposeScripts { get; set; } = new();
		public List<string> SetVehicleSpecScripts { get; set; } = new();
		public List<string> InitializeScripts { get; set; } = new();
		public List<string> ElapseScripts { get; set; } = new();
		public List<string> SetPowerScripts { get; set; } = new();
		public List<string> SetBrakeScripts { get; set; } = new();
		public List<string> SetReverserScripts { get; set; } = new();
		public List<string> KeyDownScripts { get; set; } = new();
		public List<string> KeyUpScripts { get; set; } = new();
		public List<string> HornBlowScripts { get; set; } = new();
		public List<string> DoorOpenScripts { get; set; } = new();
		public List<string> DoorCloseScripts { get; set; } = new();
		public List<string> SetSignalScripts { get; set; } = new();
		public List<string> SetBeaconDataScripts { get; set; } = new();
		public List<string> GetPluginVersionScripts { get; set; } = new();
	}
}
