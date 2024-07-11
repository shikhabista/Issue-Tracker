FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["QB_Brain/QB_Brain.csproj", "QB_Brain/"]
RUN dotnet restore "QB_Brain/QB_Brain.csproj"
COPY . .
WORKDIR "/src/QB_Brain"
RUN dotnet build "QB_Brain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QB_Brain.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QB_Brain.dll"]
