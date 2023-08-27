#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Telegram.BOT.WebApi/Telegram.BOT.WebApi.csproj", "Telegram.BOT.WebApi/"]
COPY ["Telegram.BOT.Domain/Telegram.BOT.Domain.csproj", "Telegram.BOT.Domain/"]
COPY ["Telegram.BOT.Infrastructure/Telegram.BOT.Infrastructure.csproj", "Telegram.BOT.Infrastructure/"]
COPY ["Telegram.BOT.Application/Telegram.BOT.Application.csproj", "Telegram.BOT.Application/"]
RUN dotnet restore "Telegram.BOT.WebApi/Telegram.BOT.WebApi.csproj"
COPY . .
WORKDIR "/src/Telegram.BOT.WebApi"
RUN dotnet build "Telegram.BOT.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Telegram.BOT.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Telegram.BOT.WebApi.dll"]