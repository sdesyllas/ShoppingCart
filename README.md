# ShoppingCart API
Shopping cart basic API

## 1. For the solution I spent 3 hours

## 2. Compilation instructions
The solution is consisting of
- Web Service
- Business Layer
- Unit tests for Web service and Business Layer
- Commmon library for Csv parsing

The webservice project has the following value in the web.config
```
<add key="ShoppingCart.CsvFilePath" value="C:\projects\ShoppingCart\src\ShoppingCart.Service\products.csv" />
```
This is the path of the CSV file that the application needs to load the products and is the only dependency of the 
web service.

## 3. Assumptions
* While creating this project I made the assumption that the application will need the csvfile to run.
* I made the assumption that no information about the pricing will be processed during checkout
* I made the assumption that a third party library for CSV parsing can be used instead of implementing my own
* Every method even for checkout and addproduct will be using an HTTP verb for testing purposes.

## 4. Design
The reason I chose this design is because I wanted every layer of the solution to be replacable with a different implementation.
I approached this project using the stairway pattern thus I created different layers for each application task.
The web service has the dependency of the business layer, the business layer the dependency of the storage and the storage
the dependency of the csv parser.
