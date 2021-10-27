# Reproduce delegates load mismatch on alpine docker images for Confluent's Kafka library
## Description
This program reproduces a crash, where Confluent.Kafka-Dotnet fails on alpine linux, when musl library is installed.
You can see what happens in crash callstack:

```
[InlinedCallFrame: 00007fff8420c370] Confluent.Kafka.Impl.NativeMethods.NativeMethods_Centos6.rd_kafka_conf_new()
[InlinedCallFrame: 00007fff8420c370] Confluent.Kafka.Impl.NativeMethods.NativeMethods_Centos6.rd_kafka_conf_new()
ILStubClass.IL_STUB_PInvoke()
Confluent.Kafka.Impl.Librdkafka.conf_new()
Confluent.Kafka.Impl.SafeConfigHandle.Create()
Confluent.Kafka.Producer`2[[System.__Canon, System.Private.CoreLib],[System.__Canon, fluent.Kafka.ProducerBuilder`2<System.__Canon,System.__Canon>)
Confluent.Kafka.ProducerBuilder`2[[System.__Canon, System.Private.CoreLib],[System.__Canon, System.Private.CoreLib]].Build()
KafkaDelegatesOsMismatchReproduce.Program.Main(System.String[])
[GCFrame: 00007fff8420c958]
[GCFrame: 00007fff8420ce40]
```

Instead of loading "NativeMethods_Alpine" it loads "NativeMethods_Centos6", which lead to crash in the PInvoke call.
The issue reproduces, when following line is added to the docker file.
```
RUN ln -s /lib/libc.musl-x86_64.so.1 /lib/ld-linux-x86-64.so.2
```

Project should be compiled with "Release" configuration.

## Expected result
> Succeed to build
> We finished

## Actual result
No console output, app crashes
