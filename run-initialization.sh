# Wait to be sure that SQL Server came up
echo "Waiting for sql server to start."
sleep 30s

echo "Initializing sql server."

# Run the setup script to create the DB
/opt/mssql-tools/bin/sqlcmd -S localhost -U $SA_USERNAME -P $SA_PASSWORD -i /usr/src/app/create-database.sql
