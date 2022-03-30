# Employees API

## Running on Docker

require Docker to be installed.

go to folder: `Docker\mssql`

in a commandline tool run the following command in order to create a MSSQL image:

`docker build -t mssql-tondas:qa .`

once created run the following command to run the image in a container

`docker-compose up -d`

go to the EmployeesAPI folder and run the following commands:

`dotnet restore`

`dotnet build`

`dotnet run`

the application should have been launched at this point.