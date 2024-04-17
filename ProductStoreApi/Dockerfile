# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the StoreDAL.csproj file and restore any dependencies
COPY StoreDAL/StoreDAL.csproj StoreDAL/
RUN dotnet restore "StoreDAL/StoreDAL.csproj"

# Copy the StoreBLL.csproj file and restore any dependencies
COPY StoreBLL/StoreBLL.csproj StoreBLL/
RUN dotnet restore "StoreBLL/StoreBLL.csproj"

# Copy the ProductStoreApi.csproj file and restore any dependencies
COPY ProductStoreApi/ProductStoreApi.csproj ProductStoreApi/
RUN dotnet restore "ProductStoreApi/ProductStoreApi.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/."
RUN dotnet build "WebApiEntityFrameworkDockerSqlServer.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "WebApiEntityFrameworkDockerSqlServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the port the application will run on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "ProductStoreApi.dll"]