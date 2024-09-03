FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FashionClothesAndTrends.WebAPI/FashionClothesAndTrends.WebAPI.csproj", "FashionClothesAndTrends.WebAPI/"]

RUN pwd && ls
COPY ["FashionClothesAndTrends.Application/FashionClothesAndTrends.Application.csproj", "FashionClothesAndTrends.Application"]
COPY ["FashionClothesAndTrends.Domain/FashionClothesAndTrends.Domain.csproj", "FashionClothesAndTrends.Domain"]
COPY ["FashionClothesAndTrends.Infrastructure/FashionClothesAndTrends.Infrastructure.csproj", "FashionClothesAndTrends.Infrastructure"]

RUN dotnet restore "FashionClothesAndTrends.WebAPI/FashionClothesAndTrends.WebAPI.csproj"
COPY . .
WORKDIR "/src/FashionClothesAndTrends.WebAPI"
RUN dotnet build "FashionClothesAndTrends.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FashionClothesAndTrends.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionClothesAndTrends.WebAPI.dll"]
