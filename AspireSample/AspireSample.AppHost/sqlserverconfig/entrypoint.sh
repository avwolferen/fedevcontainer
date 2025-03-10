#!/bin/bash

# Adapted from: https://github.com/microsoft/mssql-docker/blob/80e2a51d0eb1693f2de014fb26d4a414f5a5add5/linux/preview/examples/mssql-customize/entrypoint.sh

set -e

log_file="/mnt/sql-init/entrypoint.log"

echo "Starting permissions check..." | tee -a $log_file
/opt/mssql/bin/permissions_check.sh /opt/mssql/bin/sqlservr | tee -a $log_file

echo "Running init-db script..." | tee -a $log_file
/usr/config/init-db.sh | tee -a $log_file