FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/CubicIntersection.Api/CubicIntersection.Api.csproj", "src/CubicIntersection.Api/"]
COPY ["src/CubicIntersection.Application/CubicIntersection.Application.csproj", "src/CubicIntersection.Application/"]
COPY ["src/CubicIntersection.Domain/CubicIntersection.Domain.csproj", "src/CubicIntersection.Domain/"]
COPY ["src/CubicIntersection.Infrastructure/CubicIntersection.Infrastructure.csproj", "src/CubicIntersection.Infrastructure/"]

RUN dotnet restore "src/CubicIntersection.Api/CubicIntersection.Api.csproj" -s https://api.nuget.org/v3/index.json

COPY . ./

RUN dotnet build "src/CubicIntersection.Api/CubicIntersection.Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "src/CubicIntersection.Api/CubicIntersection.Api.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:$PORT

ENTRYPOINT ["dotnet", "CubicIntersection.Api.dll"]
