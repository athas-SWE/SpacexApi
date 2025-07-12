# 🚀 SpaceX Launch Tracker API

This ASP.NET Core 8 Web API fetches and caches SpaceX launch data from the public [SpaceX API](https://github.com/r-spacex/SpaceX-API). It stores data in a local MS SQL Server database using raw SQL , and exposes two RESTful endpoints to retrieve that data.

## 📋 Features

- ✅ GET `/api/launches` – Returns a list of launches from the local database
- ✅ GET `/api/launches/{id}` – Returns a single launch by ID
  - If not available in the DB, it fetches from the SpaceX API, stores it, and returns it.
- ✅ Raw SQL only (ADO.NET) – no Entity Framework or ORM
- ✅ Error handling for HTTP, SQL, and unexpected issues

- ## ⚙️ Prerequisites

- [.NET 8 SDK]
- [SQL Server 2017+]
- Optional: [Postman](https://www.postman.com/) 

---




## 🛠️ Setup & Run Instructions
### 📌 Step 1: Clone the Repository

```bash
git clone (https://github.com/athas-SWE/SpacexApi/)
cd SpacexApi


2. Create the SQL Server Database
Open SpaceXLaunch.sql in SSMS or Azure Data Studio and execute:

CREATE DATABASE SpaceXDb;
GO

USE SpaceXDb;
GO

CREATE TABLE Launches (
    Id VARCHAR(100) PRIMARY KEY,
    Name NVARCHAR(255),
    DateUtc DATETIME,
    Rocket NVARCHAR(255)
);

 3. Configure the Connection String
In appsettings.json, update the connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SpaceXDb;Trusted_Connection=True;TrustServerCertificate=True"
}


4. Build and Run the API

dotnet restore
dotnet build
dotnet run

5. Test the Endpoints
Open in browser:

https://localhost:7178/swagger
