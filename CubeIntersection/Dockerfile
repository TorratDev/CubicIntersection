FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CubeIntersection/CubeIntersection.csproj", "CubeIntersection/"]
RUN dotnet restore "CubeIntersection/CubeIntersection.csproj"
COPY . .
WORKDIR "/src/CubeIntersection"
RUN dotnet build "CubeIntersection.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CubeIntersection.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CubeIntersection.dll"]
