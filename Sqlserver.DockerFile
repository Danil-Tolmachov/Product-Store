FROM mcr.microsoft.com/mssql/server:latest

# Create app directory
WORKDIR /usr/src/app

# Copy initialization scripts
COPY 'entrypoint.sh' /usr/src/app
COPY 'run-initialization.sh' /usr/src/app
COPY 'create-database.sql' /usr/src/app
             
# Set environment variables, not have to write them with the docker run command
ENV ACCEPT_EULA Y

# Expose port 1433 in case accessing from other container
# Expose port externally from docker-compose.yml
EXPOSE 1433

# Run Microsoft SQL Server and initialization script (at the same time)
WORKDIR /
CMD /usr/bin/bash /usr/src/app/entrypoint.sh