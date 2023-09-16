# 🍔 Food Facilities Web API

This project is a web API built 100% using .NET 6.0.

The API was made considering the specifications available on [Food facilities backend challenge](https://github.com/radaisystems/food-facilities-challenge)

## Running the Web API
### Run the Web API using docker-compose

Use docker-compose to run the web API. navigate to `/docker` and run the command below on Terminal (Linux) or PowerShell (Windows)

```bach
docker-compose up
```
The API will run at the address http://localhost:5000. To make sure it is running correctly, you can access the API swagger at http://localhost:5000/swagger.

> If you need to change some variable at `/src/RadAI.FoodFacilities.WebAPI/appsettings.json`, remember to delete the image `foodfacilities-web-api` from your Docker, as the image contains a snapshot of this file.

### Run the Web API using the command line

If you have dotnet SDK 6.0 installed on your machine, you can run the project from Terminal (Linux) or PowerShell (Windows).
Navigate to the directory root and run the commands below:

```bash
dotnet build RadAI.FoodFacilities.sln
cd .\src\RadAI.FoodFacilities.WebAPI\
dotnet run
```

The API will run at the address specified on the application logs. To verify it is running correctly, access the API swagger at [http://{{log.address}}/swagger]().

### Run the Web API using Visual Studio

If you have Visual Studio 2022 installed on your machine, you can run the project directly from it.
Open the solution `RadAI.FoodFacilities.sln`, set the project `RadAI.FoodFacilities.WebAPI` as the startup project, and run the project.

The API swagger will automatically open on your default browser.

---

## Documentation

Detailed information about the web API usage (endpoints, requests, and responses) is available at `/docs/REQUESTS.md`.

---

## Critique

The critique section is available at `/docs/CRITIQUE.md`

---

If you have any questions, please contact me by email [arthur@arthursoares.dev](mailto:arthur@arthursoares.dev).
