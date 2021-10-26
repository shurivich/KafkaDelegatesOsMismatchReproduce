#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["KafkaDelegatesOsMismatchReproduce/KafkaDelegatesOsMismatchReproduce.csproj", "KafkaDelegatesOsMismatchReproduce/"]
RUN dotnet restore "KafkaDelegatesOsMismatchReproduce/KafkaDelegatesOsMismatchReproduce.csproj"
COPY . .
WORKDIR "/src/KafkaDelegatesOsMismatchReproduce"
RUN dotnet build "KafkaDelegatesOsMismatchReproduce.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KafkaDelegatesOsMismatchReproduce.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#Following line will cause Kafka to pick up NativeMethods_Centos6 delegates instead of NativeMethods_Alpine
RUN ln -s /lib/libc.musl-x86_64.so.1 /lib/ld-linux-x86-64.so.2
ENTRYPOINT ["dotnet", "KafkaDelegatesOsMismatchReproduce.dll"]