#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ServicesStore.Api.Basket/ServicesStore.Api.Basket.csproj", "ServicesStore.Api.Basket/"]
RUN dotnet restore "ServicesStore.Api.Basket/ServicesStore.Api.Basket.csproj"
COPY . .
WORKDIR "/src/ServicesStore.Api.Basket"
RUN dotnet build "ServicesStore.Api.Basket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServicesStore.Api.Basket.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicesStore.Api.Basket.dll"]