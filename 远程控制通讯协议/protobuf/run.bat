echo off

rd /s /Q .\csharp\Message\
md .\csharp\Message\

rd /s /Q .\com\maudio\aec\rcs\message
md .\com\maudio\aec\rcs\message\

echo on

protoc.exe --java_out=. .\proto\Handshake.proto
protoc --descriptor_set_out=./csharp/protobin/Handshake.protobin --include_imports .\proto\Handshake.proto
protogen ./csharp/protobin/Handshake.protobin -output_directory=./csharp/Message/

protoc.exe --java_out=. .\proto\ClientConfig.proto
protoc --descriptor_set_out=./csharp/protobin/ClientConfig.protobin --include_imports .\proto\ClientConfig.proto
protogen ./csharp/protobin/ClientConfig.protobin -output_directory=./csharp/Message/

protoc.exe --java_out=. .\proto\ClientAudioDevice.proto
protoc --descriptor_set_out=./csharp/protobin/ClientAudioDevice.protobin --include_imports .\proto\ClientAudioDevice.proto
protogen ./csharp/protobin/ClientAudioDevice.protobin -output_directory=./csharp/Message/

::protoc.exe --java_out=. .\proto\RemoteClientUpdate.proto
::protoc --descriptor_set_out=./csharp/protobin/RemoteClientUpdate.protobin --include_imports .\proto\RemoteClientUpdate.proto
::protogen ./csharp/protobin/RemoteClientUpdate.protobin -output_directory=./csharp/Message/

protoc.exe --java_out=. .\proto\LcConnectToRc.proto
protoc --descriptor_set_out=./csharp/protobin/LcConnectToRc.protobin --include_imports .\proto\LcConnectToRc.proto
protogen ./csharp/protobin/LcConnectToRc.protobin -output_directory=./csharp/Message/

::protoc.exe --java_out=. .\proto\RemoteControl.proto
protoc --descriptor_set_out=./csharp/protobin/RemoteControl.protobin --include_imports .\proto\RemoteControl.proto
protogen ./csharp/protobin/RemoteControl.protobin -output_directory=./csharp/Message/

protoc --descriptor_set_out=./csharp/protobin/RCCommEventArgs.protobin --include_imports .\proto\RCCommEventArgs.proto
protogen ./csharp/protobin/RCCommEventArgs.protobin -output_directory=./csharp/Message/

echo "copy csharp source files to target"
xcopy .\csharp\Message\ D:\code\Projects\AEC\Windows\M-Audio\AEC\RemoteControl\Message\ /y
echo "copy java source files to target"
xcopy .\com\maudio\aec\rcs\message\ D:\code\Projects\AEC\MAudio_AEC_RCS\src\main\java\com\maudio\aec\rcs\message\ /y