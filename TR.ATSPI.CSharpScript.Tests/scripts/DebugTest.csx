#r "mscorlib"
#r "System.Runtime"

using System;
using System.Diagnostics;

//ReverserPosReturnプロパティをフラグとして使用する
if (ReverserPosReturn == 1 && !Debugger.IsAttached)
	Debugger.Launch();

Debug.WriteLine("(Debug) Hello Debugger!");
Console.WriteLine("(Console) Hello Debugger!");

//DebuggerがAttatchされている場合のみBreakする
Debugger.Break();
