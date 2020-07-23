# DevkitLibrary
純正の開発キットを使用して、簡単に接続する機能を提供します。

使用例:
```

// Xbox 360
DevKits devkits = new DevKits();
devkits.SetTarget(DevkitTarget.Xbox360, 0);

// PS3
DevKits devKits = new  DevKits();
devkits.SetTarget(DevkitTarget.PS3, 0);

ConnectState state = await devkits.ConnectTargetAsync();
```

### 作成者
* **Coreizer**

### License (ライセンス)
[Public License v3.0](LICENSE)
