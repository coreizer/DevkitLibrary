# DevkitLibrary
PS3, Xbox 360 Devkit

how to use
```
DevKits devkits = new  DevKits();
devkits.SetTarget(DevkitTarget.PS3, 0);

ConnectStat state = await devkits.ConnectTarget();
```
