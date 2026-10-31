protoc --descriptor_set_out=./protobin/Handshake.protobin --include_imports Handshake.proto
protogen ./protobin/Handshake.protobin -output_directory=./csharp/Message/