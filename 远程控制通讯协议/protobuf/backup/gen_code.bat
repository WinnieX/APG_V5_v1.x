protoc.exe --java_out=. .\proto\Handshake.proto
protoc --descriptor_set_out=./csharp/protobin/Handshake.protobin --include_imports .\proto\Handshake.proto
protogen ./csharp/protobin/Handshake.protobin -output_directory=./csharp/Message/

protoc.exe --java_out=. .\proto\RemoteClientUpdate.proto
protoc --descriptor_set_out=./csharp/protobin/RemoteClientUpdate.protobin --include_imports .\proto\RemoteClientUpdate.proto
protogen ./csharp/protobin/RemoteClientUpdate.protobin -output_directory=./csharp/Message/

protoc.exe --java_out=. .\proto\RemoteControl.proto
protoc --descriptor_set_out=./csharp/protobin/RemoteControl.protobin --include_imports .\proto\RemoteControl.proto
protogen ./csharp/protobin/RemoteControl.protobin -output_directory=./csharp/Message/