using NUnit.Framework;

using System.IO;
using System.Threading.Tasks;

namespace TR.ATSPI.CSharpScript.Tests
{
	public class ManagedATSPITests
	{
		const int CreateActionFromScriptStringTest_Number = 123;
		static readonly string CreateActionFromScriptStringTest_Script = $"BrakePosReturn = {CreateActionFromScriptStringTest_Number}";

		const string DebugTestScriptFilePath = "./scripts/DebugTest.csx";

		[Test]
		public async Task CreateActionFromScriptStringTest()
		{
			var func = ManagedATSPI.CreateActionFromScriptString(CreateActionFromScriptStringTest_Script);

			Assert.IsNotNull(func);

			if (func is null)
				return;

			GlobalVariable gv = new();
			gv.BrakePosReturn = 20;
			await func.Invoke(gv);
			Assert.AreEqual(CreateActionFromScriptStringTest_Number, gv.BrakePosReturn);
		}

		[Test, Explicit]
		public async Task CreateActionFromScriptFileAndDebugTest()
		{
			string csFilePath = GetCallerFilePath();
			//ファイル名空白は異常
			Assert.IsNotEmpty(csFilePath);

			string csFileDirectory = Path.GetDirectoryName(csFilePath);
			string scriptFilePath = Path.Combine(csFileDirectory, DebugTestScriptFilePath);

			//スクリプトファイルが存在しないのは異常
			Assert.IsTrue(File.Exists(scriptFilePath));

			var func = ManagedATSPI.CreateActionFromScriptString(File.ReadAllText(scriptFilePath), scriptFilePath, true);

			Assert.IsNotNull(func);

			if (func is null)
				return;

			GlobalVariable gv = new();
			gv.ReverserPosReturn = 1;
			await func.Invoke(gv);

			Assert.Pass();
		}

		static string GetCallerFilePath([System.Runtime.CompilerServices.CallerFilePath] string csFileName = "") => csFileName;
	}
}