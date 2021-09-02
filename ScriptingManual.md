# スクリプト開発ガイド
## はじめに
C#スクリプトの開発は, ある程度C#プログラミングに詳しくなっている必要があります.

本マニュアルでは, if文やfor文, switch-case文等, 基本的な知識を持っている方が読んでいることを想定しています.

## 変数リスト
各変数にはすべてのスクリプトファイルからアクセス可能ですが, 値が更新されるのは「対応スクリプト」列にあるスクリプトが実行される直前です.  また, `~~~Return` という変数に対する値の代入は, Elapseのスクリプトでのみ意味を成します.

PanelおよびSoundへのアクセスもElapseのスクリプト以外のスクリプトから可能ですが, おすすめはしません.

|対応スクリプト|型|変数名|説明|
|---|---:|---|---|
|各スクリプト|`Dictionary<string, object>`|ObjectHolder|プラグイン全体でデータを共有するための変数|
|`SetVehicleSpec`|`int`|BrakeCount|車両の制動ハンドル可動範囲|
|`SetVehicleSpec`|`int`|PowerCount|車両の力行ハンドル可動範囲|
|`SetVehicleSpec`|`int`|ATSCheckPos|ATS確認操作を行うことができる制動ハンドル位置|
|`SetVehicleSpec`|`int`|B67Pos|ブレーキ弁で67°の位置に相当する制動ハンドル位置|
|`SetVehicleSpec`|`int`|CarCount|編成両数 [両]|
|`Elapse`|`double`|Location|列車の位置 [m]|
|`Elapse`|`float`|Speed|列車の速度 [km/h]|
|`Elapse`|`TimeSpan`|Time|0時からの経過時間|
|`Elapse`|`float`|BCPressure|BC圧 [kPa]|
|`Elapse`|`float`|MRPressure|MR圧 [kPa]|
|`Elapse`|`float`|ERPressure|ER圧 [kPa]|
|`Elapse`|`float`|BPPressure|BP圧 [kPa]|
|`Elapse`|`float`|SAPPressure|SAP圧 [kPa]|
|`Elapse`|`float`|Current|電流 [A]|
|`Elapse`|`int*`|Panel|パネル配列の先頭のポインタ|
|`Elapse`|`int*`|Panel|サウンド配列の先頭のポインタ|
|`SetBeaconData`|`int`|BeaconNum|地上子の番号|
|`SetBeaconData`|`int`|BeaconSignal|地上子に対応する信号機の現示|
|`SetBeaconData`|`float`|BeaconDistance|地上子に対応する信号機までの距離|
|`SetBeaconData`|`int`|BeaconData|地上子のオプションデータ|
|`Initialize`|`int`|InitializeNum|シナリオロード時の車両の状態|
|`SetPower`|`int`|SetPowerArg|ユーザによる力行ハンドルの操作|
|`SetBrake`|`int`|SetBrakeArg|ユーザによる制動ハンドルの操作|
|`SetReverser`|`int`|SetReverserArg|ユーザによる逆転ハンドルの操作|
|`KeyDown`|`int`|KeyDown|ユーザにより押下されたATSキーの番号|
|`KeyUp`|`int`|KeyUp|ユーザにより離されたATSキーの番号|
|`HornBlow`|`int`|HornBlow|吹鳴された汽笛の番号|
|`SetSignal`|`int`|SetSignal|信号の現示|
|`DoorOpen`, `DoorClose`|`bool`|IsDoorClosed|ドアが閉じているかどうか|
|(`Elapse`)|`int`|BrakePosReturn|プラグインから出力する制動ハンドル位置|
|(`Elapse`)|`int`|PowerPosReturn|プラグインから出力する力行ハンドル位置|
|(`Elapse`)|`int`|ReverserPosReturn|プラグインから出力する逆転ハンドル位置|
|(`Elapse`)|`int`|COnstSpeedStateReturn|プラグインから出力する定速制御状態|

---
## ObjectHolderについて
スクリプトファイル間で情報を共有したい場合に, ObjectHolder変数を使用して情報の共有を行います.

例として, `ObjectHolder["DataKey"] = 10;` といった処理が実行されたとします.  すると, 他のスクリプトで `int value = ObjectHolder["DataKey"]` という処理で `value` に `10` を入れることができます.

`Dictionary`型の使用方法については, ここでは紹介しません.

---
## usingについて
デフォルトで次の名前空間がusingされています.
```
System
System.Collecions.Generic
System.IO
System.Threading.Tasks
```

---
## unsafeコードについて
unsafeコードはunsafeブロックで囲むことで使用できます.

例えば, C#ではポインタを使用した処理はunsafeコードになるため, Panel配列及びSound配列へのアクセスを行うには, 次のように当該処理部分をunsafeブロックで囲む必要があります.
```
unsafe
{
  Panel[12] = 34;
  Sound[34] = 56;
}
```

Marshalクラスの関数を利用すればunsafeブロックも不要になりますが, ちょっと面倒です.

---
## mscorlib.dll以外のアセンブリ参照を追加する
スクリプトファイルでは, `#r "assemblyName"` という文をファイルの頭に書くことで, 任意のアセンブリをロードできます.

例えば, スクリプトで`System.Diagnostics.Debug.WriteLine` という関数を使用したいと考えた場合, この関数は `System.Runtime.dll` というアセンブリに実装されている ([参考](https://docs.microsoft.com/ja-jp/dotnet/api/system.diagnostics.debug.writeline)) ため, スクリプトの頭に次の一行を追加します.

```
#r "System.Runtime"
```

なお, 今回は.NET Frameworkに標準で付属するライブラリであったため拡張子(`.dll`)が不要でしたが, 任意のアセンブリをロードする場合は拡張子も必要になります.

例えば, `TR.ATSPI.CScript.dll` と同じディレクトリに配置した `TR.BIDSSMemLib.bve5.x64.dll` というアセンブリを読み込みたい場合, 追加する記述は次のようになります.
```
#r "TR.BIDSSMemLib.bve5.x64.dll"
```

nugetパッケージも追加できるらしいことをどこかで見かけた気がしますが, 動作未確認です.

---
## スクリプト例
### 例1 現在速度を取得する
```
float gotSpeed = Speed;
```
上記スクリプトにて, `gotSpeed` 変数に現在速度が代入されます.

### 例2 パネルを操作する
```
unsafe
{
  Panel[123] = 456;
}
```
上記スクリプトにて, `ats123` のパネルで `456` に対応する表示が行われます.  先述のように, PanelおよびSound配列へのアクセスを`Sound[123]`のように沿字にて行う場合, unsafeコンテキストで囲む必要があります.  ご注意ください.

### 例3 ユーザ入力とは異なるハンドル位置にする
```
BrakePosReturn = 999;
PowerPosReturn = 123;
```
Elapseスクリプトに上記処理を追記することで, ユーザの入力にかかわらず制動ハンドル位置 `999` , 力行ハンドル位置 `123` にセットすることができます.

なお, Return系変数に代入を行わなかった場合, ユーザによる操作と同一のハンドル位置が出力されます.

### 例4 実践的な例
例として, 次の実装を行ってみます.

- Elapse関数で実行するスクリプトである
- 20km/h以上を検知したら力行を解除して制動を行う
- 10km/h以下を検知したら制動を解除して力行を行う
- 上記以外の状態では以前の操作を継続する
- レバーサーは常に前にする
- ats255で, 現在速度を10倍した値を表示する

上記仕様を満たすスクリプトは, 次のようになります.

```
//使用するKeyを準備 (コーディングしやすいように)
const string BRec = "BrakeRec";
const string PRec = "PowerRec";

//キーが存在するかどうかチェック
//存在しなければ, そのKeyで新しいValueを登録する
if(!ObjectHolder.ContainsKey(BRec))
  ObjectHolder[BRec] = 0;
if(!ObjectHolder.ContainsKey(PRec))
  ObjectHolder[PRec] = 0;

if(Speed >= 20){
  //20km/h以上で力行を解除して制動を行う
  //ハンドル位置を記録して, 次のフレームでの実行でも値を使用できるようにする
  ObjectHolder[BRec] = 5;
  ObjectHolder[PRec] = 0;
}
else if(Speed <= 10)
{
  //10km/h以下で制動を解除して力行を行う
  //ハンドル位置を記録して, 次のフレームでの実行でも値を使用できるようにする
  ObjectHolder[BRec] = 0;
  ObjectHolder[PRec] = 3;
}

//ハンドル操作をセットする
BrakePosReturn = (int)ObjectHolder[BRec];
PowerPosReturn = (int)ObjectHolder[PRec];

unsafe
{
  Panel[255] = (int)(Speed * 10);
}

```
