# Étape 1 : Build de l'application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /app

# Copier les fichiers .csproj et restaurer les dépendances
COPY *.csproj ./
RUN dotnet restore

# Copier le reste des fichiers et compiler le projet
COPY . ./
RUN dotnet publish -c Release -o out

# Étape 2 : Image finale pour exécution
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copier les fichiers publiés depuis l'étape build
COPY --from=build /app/out ./

# Exposer le port 5000 par défaut
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

# Lancer l'application
ENTRYPOINT ["dotnet", "VueClient.dll"]
