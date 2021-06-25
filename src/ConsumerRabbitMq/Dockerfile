#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/ConsumerRabbitMq/ConsumerRabbitMq/ConsumerRabbitMq.csproj", "src/ConsumerRabbitMq/ConsumerRabbitMq/"]
RUN dotnet restore "src/ConsumerRabbitMq/ConsumerRabbitMq/ConsumerRabbitMq.csproj"
COPY . .
WORKDIR "/src/src/ConsumerRabbitMq/ConsumerRabbitMq"
RUN dotnet build "ConsumerRabbitMq.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsumerRabbitMq.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsumerRabbitMq.dll"]