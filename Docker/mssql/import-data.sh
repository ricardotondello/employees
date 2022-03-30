# wait for the SQL Server to come up
sleep 15s
#run the setup script to create the DB and the schema in the DB
[[ -f setup.sql ]] && echo "Restoring database......." && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "P@ssw0rd!Tt2019" -i setup.sql

[[ ! -f setup.sql ]] && echo "Nothing to restore, all good :D......."

#rename the file in order to execute just once
[[ -f setup.sql ]] && echo "Done Restoring database......." && mv setup.sql setup-executed.sql