# ASP.NET + JQuery Sales Cart
This is simple sales-invoicing project that uses .NET Core and Jquery:

## Getting Started
To get started with the project, you will need to clone the repository and follow the installation steps below.

## Prerequisites
To run this application, you will need the following:

* NET 7 SDK installed on your machine

## Installation
To install the application, follow these steps:

1. Clone the repository using the following command & build the project:
```bash
$ https://github.com/AMuriuki/dotnet-jquery-sales-cart.git

$ cd dotnet-jquery-sales-cart.git

$ dotnet build
```

2. Update `appsettings.json` with the below configuration
```
"ConnectionStrings": {
    "SalesContext": "Data Source=sales.db"
}
```

3. Run migrations and seed the DB
```bash
$ dotnet ef database update 
# you'll need to install dotnet-ef
# dotnet tool install --global dotnet-ef
```

3. Launch the project
```bash
$ dotnet run
```

4. Navigate to `http://localhost:5100` on your browser