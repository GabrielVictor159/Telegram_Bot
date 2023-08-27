#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Telegram.BOT.TelegramJob/Telegram.BOT.TelegramJob.csproj", "Telegram.BOT.TelegramJob/"]
COPY ["Telegram.BOT.Application/Telegram.BOT.Application.csproj", "Telegram.BOT.Application/"]
COPY ["Telegram.BOT.Domain/Telegram.BOT.Domain.csproj", "Telegram.BOT.Domain/"]
COPY ["Telegram.BOT.Infrastructure/Telegram.BOT.Infrastructure.csproj", "Telegram.BOT.Infrastructure/"]
RUN dotnet restore "Telegram.BOT.TelegramJob/Telegram.BOT.TelegramJob.csproj"
COPY . .
WORKDIR "/src/Telegram.BOT.TelegramJob"
RUN dotnet build "Telegram.BOT.TelegramJob.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Telegram.BOT.TelegramJob.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Telegram.BOT.TelegramJob.dll"]