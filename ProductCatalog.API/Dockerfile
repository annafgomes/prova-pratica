# -------- ETAPA 1: BUILD --------

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copia tudo da solução

COPY . .

# Publica a API

RUN dotnet publish ProductCatalog.API.csproj -c Release -o /app/publish

# -------- ETAPA 2: RUNTIME --------

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ProductCatalog.API.dll"]

