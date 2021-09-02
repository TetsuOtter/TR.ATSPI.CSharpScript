# XML形式設定ファイル記述マニュアル
## はじめに
このファイルは, ある程度コンピュータ(Windows)に詳しい方が読むことを想定して書いています.

絶対パス/相対パスやコピー&ペースト等, コンピュータ(Windows)用語を特に説明なく使用している場合がありますので, ご注意ください.

設定ファイル内では, 絶対パスと相対パスの両方を使用することができます.  なお, 相対パスは「設定ファイルからの相対パス」になりますので, ご注意ください.

## 初期状態
XML形式設定ファイルの初期状態 (ダウンロード直後の `TR.ATSPI.CScript.xml` ) は次の形です.

```
<?xml version="1.0" encoding="utf-8"?>
<ScriptPathListClass xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ScriptFileLists />
  <LoadScripts />
  <DisposeScripts />
  <SetVehicleSpecScripts />
  <InitializeScripts />
  <ElapseScripts />
  <SetPowerScripts />
  <SetBrakeScripts />
  <SetReverserScripts />
  <KeyDownScripts />
  <KeyUpScripts />
  <HornBlowScripts />
  <DoorOpenScripts />
  <DoorCloseScripts />
  <SetSignalScripts />
  <SetBeaconDataScripts />
  <GetPluginVersionScripts />
</ScriptPathListClass>
```

## スクリプトファイルを追加する
今回は, `TR.ATSPI.CScript.dll` と同じディレクトリに作成した `sampleScript.csx` というスクリプトファイルを, 何らかのATSキーが押下された際に実行されるように設定します.

ATSキーが押下された際にスクリプトを実行させるには, `KeyDownScripts`タグにパスを追加する必要があります.  初期状態のXMLファイルに `<KeyDownScripts />` とありますが, これを次のように修正します.

```
<KeyDownScripts>
  <string>sampleScript.csx</string>
</KeyDownScripts>
```

なお, 次のように記述することで, ATSキーが押下された際に `sampleScript.csx` のほかに `sampleScript2.csx` も呼ばれるようになります.  このように, `~~~Scripts` タグの内側に `string`タグで囲んだパスを好きなだけ書くことができ, 書けば書いた分だけスクリプトが呼ばれます.

```
<KeyDownScripts>
  <string>sampleScript.csx</string>
  <string>sampleScript2.csx</string>
</KeyDownScripts>
```

なお, スクリプトの実行は非同期に行われ, 例えば上記の例だと `sampleScript.csx` と `sampleScript2.csx`はほぼ同時に実行され, `sampleScript.csx` の処理が終了していないのに `sampleScript2.csx` の処理が終了している…といった現象も起こり得ます.

## XML形式設定ファイルを追加する
`ScriptFileLists` タグがありますが, これにXML形式設定ファイルへのパスを追加することで, 好きなだけ設定ファイルを追加できます.


### 例
あなたはスクリプトを用いて "A" というプラグインを公開するとします.  あなたの環境では次のようなファイル配置で開発をおこなっていたものとします.  なお, .dllファイルについては, `TR.ATSPI.CScript.dll` 以外の記載を省略しています.

```
\TR.ATSPI.CScript.dll
\TR.ATSPI.CScript.xml
\Plugin_A\load.csx
\Plugin_A\dispose.csx
```

すると, `TR.ATSPI.CScript.xml` の中身は次のような形になっていると思われます.

```
<?xml version="1.0" encoding="utf-8"?>
<ScriptPathListClass xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ScriptFileLists />
  <LoadScripts>
    <string>Plugin_A\load.csx</string>
  </LoadScripts>
  <DisposeScripts>
    <string>Plugin_A\dispose.csx</string>
  </DisposeScripts>
  <SetVehicleSpecScripts />
  <InitializeScripts />
  <ElapseScripts />
  <SetPowerScripts />
  <SetBrakeScripts />
  <SetReverserScripts />
  <KeyDownScripts />
  <KeyUpScripts />
  <HornBlowScripts />
  <DoorOpenScripts />
  <DoorCloseScripts />
  <SetSignalScripts />
  <SetBeaconDataScripts />
  <GetPluginVersionScripts />
</ScriptPathListClass>
```

このまま公開しても構いませんが, ユーザがあなたのスクリプトプラグイン以外も使用したいと考えた場合, 他のスクリプトプラグインの設定とごっちゃになってしまい, わかりづらくなってしまうことが予想されます.

そのため, あなたのスクリプトプラグインの設定ファイルを分離してしまいましょう.  `Plugin_A` フォルダ内に, `TR.ATSPI.CScript.xml` というファイルをコピペしましょう.

コピペしたら, 適当に名前を変更しましょう(変更しなくても大丈夫です).  今回は, `Plugin_A_ScriptList.xml`というファイル名に変更します.

現時点で, 次のようなファイル構成になっているはずです.

```
\TR.ATSPI.CScript.dll
\TR.ATSPI.CScript.xml
\Plugin_A\Plugin_A_ScriptList.xml
\Plugin_A\load.csx
\Plugin_A\dispose.csx
```

続いて, `Plugin_A_ScriptList.xml` を開き, 設定ファイルを移動したことに伴う相対パスの修正を行います.  実行後は次のようになります.

```
<?xml version="1.0" encoding="utf-8"?>
<ScriptPathListClass xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ScriptFileLists />
  <LoadScripts>
    <string>load.csx</string>
  </LoadScripts>
  <DisposeScripts>
    <string>dispose.csx</string>
  </DisposeScripts>
  <SetVehicleSpecScripts />
  <InitializeScripts />
  <ElapseScripts />
  <SetPowerScripts />
  <SetBrakeScripts />
  <SetReverserScripts />
  <KeyDownScripts />
  <KeyUpScripts />
  <HornBlowScripts />
  <DoorOpenScripts />
  <DoorCloseScripts />
  <SetSignalScripts />
  <SetBeaconDataScripts />
  <GetPluginVersionScripts />
</ScriptPathListClass>
```

では, この設定ファイルを使用する設定を行いましょう.  `TR.ATSPI.CScript.xml` を開き, スクリプトファイルの指定を削除します.  初期状態に戻してしまっても構いません.

```
<?xml version="1.0" encoding="utf-8"?>
<ScriptPathListClass xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ScriptFileLists />
  <LoadScripts>
  </LoadScripts>
  <DisposeScripts>
  </DisposeScripts>
  <SetVehicleSpecScripts />
  <InitializeScripts />
  <ElapseScripts />
  <SetPowerScripts />
  <SetBrakeScripts />
  <SetReverserScripts />
  <KeyDownScripts />
  <KeyUpScripts />
  <HornBlowScripts />
  <DoorOpenScripts />
  <DoorCloseScripts />
  <SetSignalScripts />
  <SetBeaconDataScripts />
  <GetPluginVersionScripts />
</ScriptPathListClass>
```

続いて, `Plugin_A_ScriptsList.xml` を読み込む設定を行いましょう.  スクリプトファイルのパス指定と同様に, `ScriptFileLists` タグにパスを指定します.  すると, `TR.ATSPI.CScript.xml` ファイルは次のような内容になっているはずです.

```
<?xml version="1.0" encoding="utf-8"?>
<ScriptPathListClass xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ScriptFileLists>
    <string>Plugin_A\Plugin_A_ScriptsList.xml</string>
  </ScriptFileLists>
  <LoadScripts>
  </LoadScripts>
  <DisposeScripts>
  </DisposeScripts>
  <SetVehicleSpecScripts />
  <InitializeScripts />
  <ElapseScripts />
  <SetPowerScripts />
  <SetBrakeScripts />
  <SetReverserScripts />
  <KeyDownScripts />
  <KeyUpScripts />
  <HornBlowScripts />
  <DoorOpenScripts />
  <DoorCloseScripts />
  <SetSignalScripts />
  <SetBeaconDataScripts />
  <GetPluginVersionScripts />
</ScriptPathListClass>
```

これでXML設定ファイルの分離を行うことができました.

このようにすることで, スクリプトファイルの指定がごっちゃになってしまうことを防ぐことができます.

なお, スクリプトファイルリストの指定がループになってしまうと, 無限ループ状態となり実行が不可能になります.  ご注意ください.