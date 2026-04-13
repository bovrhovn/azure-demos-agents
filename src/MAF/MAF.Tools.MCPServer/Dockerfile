FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MAF.Tools.MCPServer/MAF.Tools.MCPServer.csproj", "MAF.Tools.MCPServer/"]
RUN dotnet restore "MAF.Tools.MCPServer/MAF.Tools.MCPServer.csproj"
COPY . .
WORKDIR "/src/MAF.Tools.MCPServer"
RUN dotnet build "./MAF.Tools.MCPServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MAF.Tools.MCPServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_HTTP_PORTS=80
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MAF.Tools.MCPServer.dll"]
