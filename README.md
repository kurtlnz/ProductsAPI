# Xero Products API - V2

The challenge was to evaluate and re-factor an existing .NET Web API for managing products and their options in C#. 

The existing project has been converted to target .Net Core 3.1 with the existing data access methods moved out of
the models in favour of a service layer and a repository layer utilizing EF Core.

Considerations:
The repository layer is thin with EF Core here, but I still think it's worth putting data access behind
this abstraction as it makes it easier to replace data access in future and
provides an interface for easier mocking of the DAL in unit tests.

## Technologies:

- ASP .NET Core 3.1
- Microsoft Entity Framework Core
- NSwag
- Moq
- NUnit
- Sqlite

## Running the API:

1. cd XeroTechnicalTest.API project
3. Execute `dotnet run`

The API should now be running

## Running the tests:

1. In root of solution
3. Execute `dotnet test`

## Database

- Sqlite
- Location: XeroTechnicalTest.API/App_Data/products.db

Generating an empty database from the solution root: 
```
dotnet ef database update --project XeroTechnicalTest.Persistence --startup-project XeroTechnicalTest.API
```




~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

# refactor-this
The attached project is a poorly written products API in C#.

Please evaluate and refactor areas where you think can be improved. 

Consider all aspects of good software engineering and show us how you'll make it #beautiful and make it a production ready code.

## Getting started for applicants

There should be these endpoints:

1. `GET /products` - gets all products.
2. `GET /products?name={name}` - finds all products matching the specified name.
3. `GET /products/{id}` - gets the project that matches the specified ID - ID is a GUID.
4. `POST /products` - creates a new product.
5. `PUT /products/{id}` - updates a product.
6. `DELETE /products/{id}` - deletes a product and its options.
7. `GET /products/{id}/options` - finds all options for a specified product.
8. `GET /products/{id}/options/{optionId}` - finds the specified product option for the specified product.
9. `POST /products/{id}/options` - adds a new product option to the specified product.
10. `PUT /products/{id}/options/{optionId}` - updates the specified product option.
11. `DELETE /products/{id}/options/{optionId}` - deletes the specified product option.

All models are specified in the `/Models` folder, but should conform to:

**Product:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description",
  "Price": 123.45,
  "DeliveryPrice": 12.34
}
```

**Products:**
```
{
  "Items": [
    {
      // product
    },
    {
      // product
    }
  ]
}
```

**Product Option:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description"
}
```

**Product Options:**
```
{
  "Items": [
    {
      // product option
    },
    {
      // product option
    }
  ]
}
```
