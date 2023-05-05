# ASP.NET + JQuery Sales Cart
The project uses JavaScript and jQuery to retrieve and display products and customers. It includes functionality to add and remove items from the sales cart and calculate the `subtotal`, `tax`, `discount`, and `shipping` costs. 

The user interface includes an autocomplete search bar to search for products and a drop-down list to select customers. 

The project uses AJAX to retrieve the product and customer data from the server. 
Overall, it provides an easy-to-use interface for customers to select and purchase products online.

## Getting Started
To get started with the project, you will need to clone the repository and follow the installation steps below.

## Prerequisites
To run this application, you will need the following:

* NET 7 SDK installed on your machine
* Visual Studio or Visual Studio Code

## Installation
To install the application, follow these steps:

1. Clone the repository using the following command:
```bash
$ https://github.com/AMuriuki/dotnet-jquery-sales-cart.git
```

2. Run migrations and seed the DB
```bash
$ dotnet ef database update
```

3. Build the project using the following commands:
```bash
$ dotnet build
$ dotnet run
```