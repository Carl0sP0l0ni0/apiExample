# Usamos la imagen oficial de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Establecemos el directorio de trabajo dentro del contenedor
WORKDIR /src

# Copiamos el archivo .csproj y restauramos las dependencias
COPY ["ApiExample/ApiExample.csproj", "ApiExample/"]
RUN dotnet restore "ApiExample/ApiExample.csproj"

# Copiamos el resto del código y lo publicamos
COPY . .
WORKDIR "/src/ApiExample"
RUN dotnet publish "ApiExample.csproj" -c Release -o /app/publish

# Usamos la imagen de .NET Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Copiamos los archivos publicados desde el contenedor de build
COPY --from=build /app/publish .

# Establecemos el comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ApiExample.dll"]
