# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

# Copy the project files and restore dependencies for each project
COPY ProductStore.Data/ProductStore.Data.csproj ProductStore.Data/
COPY ProductStore.Business/ProductStore.Business.csproj ProductStore.Business/
COPY ProductStore.WebApi/ProductStore.WebApi.csproj ProductStore.WebApi/
RUN dotnet restore "ProductStore.WebApi/ProductStore.WebApi.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR /src/ProductStore.WebApi
RUN dotnet build "ProductStore.WebApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ProductStore.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Generate self-signed certificate
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS certs
WORKDIR /https

ARG CERTIFICATE_PASSWORD
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p ${CERTIFICATE_PASSWORD}
RUN dotnet dev-certs https --trust



# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=certs /https/aspnetapp.pfx /https/
COPY --from=publish /app/publish .

# Copy seed images
COPY ProductStore.Data/SeedImages ProductStore.Data/SeedImages

# Set environment variables
ARG CERTIFICATE_PASSWORD
ENV IsDockerContainer=true
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=${CERTIFICATE_PASSWORD}

# Expose the port the application will run on
EXPOSE 80 443

# Start the application
ENTRYPOINT ["dotnet", "/app/ProductStore.WebApi.dll"]
