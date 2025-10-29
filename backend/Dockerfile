FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["GamePlatform.sln", "./"]
COPY ["GamePlatform.API/GamePlatform.API.csproj", "GamePlatform.API/"]
COPY ["GamePlatform.Core/GamePlatform.Core.csproj", "GamePlatform.Core/"]
COPY ["GamePlatform.Data/GamePlatform.Data.csproj", "GamePlatform.Data/"]

# Restore dependencies
RUN dotnet restore "GamePlatform.sln"

# Copy all source files
COPY . .

# Build and publish
WORKDIR "/src/GamePlatform.API"
RUN dotnet publish "GamePlatform.API.csproj" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "GamePlatform.API.dll"]
