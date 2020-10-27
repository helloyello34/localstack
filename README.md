# Setup

To run this project you need to have these dependencies installed
* Docker
* docker-compose
* .NET Core 3.1.x

## Run the project
go to path/of/project and run 
```
TMPDIR=/private$TMPDIR docker-compose up -d
```
TMPDIR=/private$TMPDIR only if you're running the project on a mac.

and then run
```
dotnet run --project LocalStack.Web
```

and try using endpoints to use the mocked dynamodb

Endpoints:
* POST http://localhost:5000/api/v1/local/text?text=
* GET  http://localhost:5000/api/c1/local/text 