# Utilizamos la imagen base de .NET SDK para compilar nuestra aplicaci�n
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

# Copiamos el archivo de proyecto y restauramos las dependencias
FROM base AS build
WORKDIR /src
COPY SenaThreads.Web/*.csproj /src/SenaThreads.Web/
COPY SenaThreads.Infrastructure/*.csproj /src/SenaThreads.Infrastructure/
COPY SenaThreads.Domain/*.csproj /src/SenaThreads.Domain/
COPY SenaThreads.Application/*.csproj /src/SenaThreads.Application/
RUN dotnet restore SenaThreads.Web/SenaThreads.Web.csproj

# Copiamos el archivo .env
COPY SenaThreads.Web/.env /app/

# Copiamos y construimos la aplicaci�n
COPY . .
WORKDIR /src
RUN dotnet build SenaThreads.Web/SenaThreads.Web.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish SenaThreads.Web/SenaThreads.Web.csproj -c Release -o /app/publish /p:UseAppHost=false

# Utilizamos una imagen m�s ligera de ASP.NET Core para ejecutar la aplicaci�n
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /app/.env ./

# Exponemos el puerto en el que se ejecutar� la API
EXPOSE 8080

# Especificamos el comando de inicio de la aplicaci�n
ENTRYPOINT ["dotnet", "SenaThreads.Web.dll"]