# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

# Copy the project files and restore dependencies for each project
COPY StoreDAL/StoreDAL.csproj StoreDAL/
COPY StoreBLL/StoreBLL.csproj StoreBLL/
COPY ProductStoreApi/ProductStoreApi.csproj ProductStoreApi/
RUN dotnet restore "ProductStoreApi/ProductStoreApi.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR /src/ProductStoreApi
RUN dotnet build "ProductStoreApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ProductStoreApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV IsDockerContainer=true

# Copy seed images
COPY StoreDAL/SeedImages StoreDAL/SeedImages

# Expose the port the application will run on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "/app/ProductStoreApi.dll"]
