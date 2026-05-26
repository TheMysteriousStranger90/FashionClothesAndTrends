FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]
COPY ["src/FashionClothesAndTrends.WebAPI/FashionClothesAndTrends.WebAPI.csproj", "FashionClothesAndTrends.WebAPI/"]
COPY ["src/FashionClothesAndTrends.Application/FashionClothesAndTrends.Application.csproj", "FashionClothesAndTrends.Application/"]
COPY ["src/FashionClothesAndTrends.Domain/FashionClothesAndTrends.Domain.csproj", "FashionClothesAndTrends.Domain/"]
COPY ["src/FashionClothesAndTrends.Infrastructure/FashionClothesAndTrends.Infrastructure.csproj", "FashionClothesAndTrends.Infrastructure/"]

RUN dotnet restore "FashionClothesAndTrends.WebAPI/FashionClothesAndTrends.WebAPI.csproj"

COPY src/ .
WORKDIR "/src/FashionClothesAndTrends.WebAPI"
RUN dotnet build "FashionClothesAndTrends.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build /p:TreatWarningsAsErrors=false

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FashionClothesAndTrends.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:TreatWarningsAsErrors=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /https
ENTRYPOINT ["dotnet", "FashionClothesAndTrends.WebAPI.dll"]