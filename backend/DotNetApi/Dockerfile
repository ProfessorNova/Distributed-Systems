FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dotnet-build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore DotNetApi.csproj
COPY . .
RUN dotnet publish DotNetApi.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=dotnet-build /app .
ENTRYPOINT ["dotnet", "DotNetApi.dll"]
