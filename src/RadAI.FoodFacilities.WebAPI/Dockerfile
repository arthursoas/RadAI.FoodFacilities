#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RadAI.FoodFacilities.WebAPI/RadAI.FoodFacilities.WebAPI.csproj", "RadAI.FoodFacilities.WebAPI/"]
RUN dotnet restore "RadAI.FoodFacilities.WebAPI/RadAI.FoodFacilities.WebAPI.csproj"
COPY . .
WORKDIR "/src/RadAI.FoodFacilities.WebAPI"
RUN dotnet build "RadAI.FoodFacilities.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RadAI.FoodFacilities.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RadAI.FoodFacilities.WebAPI.dll"]