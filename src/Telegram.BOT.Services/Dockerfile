#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Telegram.BOT.Services/Telegram.BOT.Services.csproj", "Telegram.BOT.Services/"]
RUN dotnet restore "Telegram.BOT.Services/Telegram.BOT.Services.csproj"
COPY . .
WORKDIR "/src/Telegram.BOT.Services"
RUN dotnet build "Telegram.BOT.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Telegram.BOT.Services.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Telegram.BOT.Services.dll"]