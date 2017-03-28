# Perfe Channel ShoppingCart API
Programming test creating a shopping cart API based in web api 2 and asp.net MVC

## 1. Time to implement this project
For the solution and the questions I spent almost 4 hrs.

## 2. Compilation instructions
The solution is consisting of
* Web Service
* Business Layer
* Unit tests for Web service and Business Layer
* Commmon library for Csv parsing

The first time the solution is built nuget will pull all the packages mentioned in the packages.json files from the nuget
repository.

The webservice project has the following value in the web.config
```
<add key="ShoppingCart.CsvFilePath" value="C:\projects\ShoppingCart\src\ShoppingCart.Service\products.csv" />
```
This is the path of the CSV file that the application needs to load the products and is the only dependency of the 
web service.
The application can be tested using:
* ShoppingCart.Business.Tests project for mock testing the business layer
* ShoppingCart.Service.Tests project for mock testing the service layer

The entry point of the application is the ShoppingCart.Service project which loads up the MVC projec with the web api service.

The Web Api can be tested using the following examples:
### Product service
* http://localhost/api/products To get all the products
* http://localhost/api/products/1001 To Get a product by Id 1001

### Shopping Cart Service
* http://localhost/api/ShoppingBasket/{cartname} To get the shopping cart details of the given cart name
* http://localhost/api/ShoppingBasket/{cartname}/Add/{productId}/{quantity} To Add a product to the shopping cart with the given name, product id and quantity
* http://localhost/api/ShoppingBasket/{cartname}/Checkout To checkout a shopping cart by the name.

All the service methods are available using HTTP Get verb.

## 3. Assumptions
* I made the assumption that the products and the shopping carts will be stored in memory in static properties. After the products are
loaded for the first time from the csv file everything is stored in memory and all the stock reducing operations and updates are performed in the memory and not in the csv file.
* While creating this project I made the assumption that the application will need the csvfile to run.
* I made the assumption that the shopping cart identifier will be a string. The identifier can be user's name for example or any other string. If the shopping cart with the given identifier does not exist when adding a product a new cart will be created.
* I made the assumption that a third party library for CSV parsing can be used instead of implementing my own
* Every method even for checkout and addproduct will be using an HTTP verb for testing purposes.
* No client is needed at the time being, the web service can be tested directly from the browser.
* The stock is reduced from the datasource when the shopping cart is being checked out. As in a real world scenario the stock is not reduced by adding to the shopping cart only. The stock is checked before checkout to ensure that it has sufficient quantity to execute the order.

## 4. Design
The reason I chose this design is because I wanted every layer of the solution to be replacable with a different implementation.
I approached this project using the stairway pattern thus I created different layers for each application area and an abstractions projects where all the interfaces reside.
The web service has the dependency of the business layer, the business layer the dependency of the storage and the storage
the dependency of the csv parser.
The benefit is that every layer can be unit tested separately while mocking out all of its dependencies.
In the web service layer all the dependencies are resolved using SimpleInjector IoC container.

## 5. Future work
I believe I completed all 3 user stories. However if I had more time I would add the following:
* All methods to be implemented using async
* Exception handling in the controller responding back appropriate status codes.
* I would create integration unit tests.
* Specflow tests for user story scenarios.
