FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /src

COPY ["src/CubicIntersection.Api/CubicIntersection.Api.csproj", "src/CubicIntersection.Api/CubicIntersection.Api.csproj"]
COPY ["src/CubicIntersection.Application/CubicIntersection.Application.csproj", "src/CubicIntersection.Application/CubicIntersection.Application.csproj"]
COPY ["src/CubicIntersection.Domain/CubicIntersection.Domain.csproj", "src/CubicIntersection.Domain/CubicIntersection.Domain.csproj"]
COPY ["src/CubicIntersection.Infrastructure/CubicIntersection.Infrastructure.csproj", "src/CubicIntersection.Infrastructure/CubicIntersection.Infrastructure.csproj"]

RUN dotnet restore "src/CubicIntersection.Api/CubicIntersection.Api.csproj"

COPY . .

WORKDIR "/src/CubicIntersection"

RUN dotnet build "CubicIntersection.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "CubicIntersection.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CubicIntersection.dll"]
