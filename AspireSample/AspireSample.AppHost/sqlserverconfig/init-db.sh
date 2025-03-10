#!/bin/bash

# Loop through the .sql files in the /docker-entrypoint-initdb.d and execute them with sqlcmd
for f in /mnt/sql-init/*.sql
do
    echo "Processing $f file..."
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -d master -i "$f"
done