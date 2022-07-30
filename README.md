# DevkitLibrary
純正の開発キットを使用して、簡単に接続する機能を提供します。

## Usage
Connection example:
```
// Xbox 360
DevKits devkits = new DevKits();
devkits.SetTarget(DevkitTarget.Xbox360, 0);

// PS3
DevKits devKits = new  DevKits();
devkits.SetTarget(DevkitTarget.PS3, 0);

// Connect to target
ConnectionStatus status = await devkits.ConnectTargetAsync();
if (status == ConnectionStatus.Connected) {
  MessageBox.Show("Connected !");
}
```

## Screenshot
![demo-image](./docs/demo.png)

## Stay In Touch
 - [Website coreizer.dev](https://www.coreizer.dev)
 - [Twitter](https://www.twitter.com/coreizer)

## Author
coreizer

## License
[GPL v3 licensed.](LICENSE)
