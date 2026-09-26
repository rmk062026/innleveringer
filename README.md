# MovieLibrary REST API

A REST API built with C#, .NET 10 and ASP.NET Core Controllers as part of the C# Intermediate course at Kodehode.

## Features

- Retrieve all movies or a movie by ID.
- Create movies with input validation.
- Filter movies by genre and release year.
- Sort by title or release year.
- Paginate results.
- Asynchronous service methods.
- Standard HTTP status codes and validation error responses.

## Technologies

- C# / .NET 10
- ASP.NET Core Web API
- xUnit
- In-memory storage

## Getting started

Requirements: .NET 10 SDK.

From the solution directory:

```bash
dotnet restore
dotnet build
dotnet run --project MovieLibrary.Api
```

Use the localhost URL shown in the terminal.

## API endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | /api/movies | Get movies |
| GET | /api/movies/{id} | Get a movie by ID |
| POST | /api/movies | Create a movie |

### Query parameters

The GET endpoint supports the following optional parameters:

| Parameter | Description | Default |
|---|---|---|
| genre | Filter by genre | All |
| year | Filter by release year | All |
| sortBy | title, title_desc, year, year_desc | title |
| page | Page number | 1 |
| pageSize | Number of results per page (1–100) | 10 |

Example:

`GET /api/movies?genre=Sci-Fi&sortBy=year_desc&page=1&pageSize=5`

### Create a movie

POST `/api/movies`

Request body:

```json
{
  "title": "Interstellar",
  "director": "Christopher Nolan",
  "releaseYear": 2014,
  "genre": "Sci-Fi"
}
```

A successful request returns HTTP 201 Created, the new movie and a Location header.

Invalid input returns HTTP 400 with validation details. Requesting an unknown movie returns HTTP 404.

## Testing

Run the automated tests:

```bash
dotnet test
```

The endpoints can also be tested using PowerShell, cURL or Postman.

Example:

```bash
curl http://localhost:5074/api/movies
```

Replace port 5074 with the port displayed when starting the API.

## Data storage

Movies are currently stored in memory. All data is lost when the application restarts.

A possible future improvement is SQL Server integration using Entity Framework Core and asynchronous database operations.