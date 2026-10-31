echo off
echo "copy csharp source files to target"
xcopy .\csharp\Message\ D:\code\Projects\AEC\Windows\V-Series\AEC\RemoteControl\Message\ /y
echo "copy java source files to target"
xcopy .\com\meng\aecs\message\ D:\code\Projects\AEC\AECS\src\main\java\com\meng\aecs\message\ /y