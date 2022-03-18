FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app
COPY *.csproj ./
COPY . ./
RUN rm Servicos.db
RUN dotnet publish -c Release -o plataforma
RUN dotnet restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/plataforma .
ENTRYPOINT ["dotnet", "PlatformService.dll"]