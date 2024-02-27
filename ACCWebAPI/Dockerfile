#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ACCWebAPI/ACCWebAPI.csproj", "ACCWebAPI/"]
RUN dotnet restore "ACCWebAPI/ACCWebAPI.csproj"
COPY . .
WORKDIR "/src/ACCWebAPI"
RUN dotnet build "ACCWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ACCWebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACCWebAPI.dll"]