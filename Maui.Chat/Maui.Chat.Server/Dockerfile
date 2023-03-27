FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Maui.Chat.Server/Maui.Chat.Server.csproj", "Maui.Chat.Server/"]
RUN dotnet restore "Maui.Chat.Server/Maui.Chat.Server.csproj"
COPY . .
WORKDIR "/src/Maui.Chat.Server"
RUN dotnet build "Maui.Chat.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maui.Chat.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maui.Chat.Server.dll"]
