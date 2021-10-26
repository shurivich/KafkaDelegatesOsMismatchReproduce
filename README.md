﻿# Reproduce delegates load mismatch on alpine docker images for Confluent's Kafka library
## Description
This is a reproduce for the edge case, when Confluent.Kafka fails to to load proper delegates for alpine.
Instead of loading "NativeMethods_Alpine" it loads "NativeMethods_Centos6".
The issue reproduces, when following line is added to the docker file.
```csharp
RUN ln -s /lib/libc.musl-x86_64.so.1 /lib/ld-linux-x86-64.so.2
```

In order to reproduce, project should be compiled with "Release" configuration.
