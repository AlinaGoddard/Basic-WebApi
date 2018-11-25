# Refactoring Assessment


## Endpoints


The api is composed of the following endpoints:

| Verb     | Path                                   | Description
|----------|----------------------------------------|--------------------------------------------------------
| `GET`    | `/api/Accounts`                        | Gets the list of all accounts
| `GET`    | `/api/Accounts/{id:guid}`              | Gets an account by the specified id
| `POST`   | `/api/Accounts`                        | Creates a new account
| `PUT`    | `/api/Accounts/{id:guid}`              | Updates an account
| `DELETE` | `/api/Accounts/{id:guid}`              | Deletes an account
| `GET`    | `/api/Accounts/{id:guid}/Transactions` | Gets the list of transactions for an account
| `POST`   | `/api/Accounts/{id:guid}/Transactions` | Adds a transaction to an account, and updates the amount of money in the account

Models should conform to the following formats:

**Account**
```
{
    "Id": "01234567-89ab-cdef-0123-456789abcdef",
	"Name": "Savings",
	"Number": "012345678901234",
	"Amount": 123.4
}
```	

**Transaction**
```
{
    "Date": "2018-09-01",
    "Amount": -12.3
}
```

# What I Changed:

I focused on adding abstraction layers to make the solution more pluggable and Improved database access.

I used Entity Framework to create entities using the local database provided.  I then separated out the database classes from the context and the model.  I used separate projects for this so that the solution would be more pluggable and the projects could be used independant of each other.

## DataAccess Project

I added another constructor for the connection so that the connection string could be passed in.  This allows another database to be used if required so not just the supplied database can be used so long as it has the same schema definition.

I added two repository classes for accounts and transactions respectively in the DataAccess project.  These are used to perform operations on the database.  They return the model that the api uses in most cases and take the api model as parameters.  So they handle the transaltion between the two models.  I think some further abstraction could be done here.

## DataEntities Project

This stores the classes created by entity framework corresponding to the two database tables of Accounts and Transactions.  They can be updated easily if changes are made to the database by using the run tool.

## ApiModels Project

This is the models from the Web Api.  I modified these classes to remove database access logic as this is now handled by the new database layers.  The previous database queries did not use parameters so they left a vulnerability to SQL Injection in addition database access commands were not in a consistent location. 

## LocalDatabase Project

This is what was left of the original project that I renamed.  I thought the database could be in its own project for plugability.  It is better to have layers loosly coupled so that a different component could easily be plugged in without much reworking.  For example a different database with different data could be added or connected to.

## WebApi Project

The purpose for this project is self explainatory.  All that was needed was the web api config and the controllers together with the web.config where I stored the current connection string.  I accessed the repositories I created in the DataAccess layer here to perform database operations and return the Api model classes.

## Error Handling

This is something I would improve on if I had more time.  Currently the api returns either an Ok repsonse or a BadRequest response.  This could be imporoved by making the response include a message for the api (human readable) and a message to give more information for a tech person.  Also more than just a BadResponse could be returned.  For example if would be good to know if the error was because of bad user data in the case of BadRequest.  Or if the error was a server side problem not caused by the user e.g. could not connect to the database.

## Logging

I think it would add value to put logging in.  It would be good to monitor the kind of traffic and the times to see if there is any patterns and need to add more resources such as server provisioning. Also errors thrown would be good to monitor for patterns.  Perhaps the site had several malicious hack attempts.  It would be good to monitor this so appropriate actions could be taken. 

## Unit Tests

I would also add unit tests so that expected outcomes could be asserted and the code would all be covered by tests.  That way if changes were made in the future, consideration should be given to any failing tests to make sure anything didn't get broken unexpectedly.  Then modifications could be made to the tests if business rules had changed. 

## Web.config

I stored the connection string here as it is easy to encrypt and is a standard way to store connection strings.  I removed the vb compile sections as the solution contains no vb code.

## NuGet and References

I tried to remove NuGet packages and references that were not needed by the particular project.  However I may not have remembered all of them.  If I had time I would spend more time and make sure all packages and references were necessary.  

## Sorting and Filtering

This wasn't done so it would be good to determine if and what types of filtering and sorting would be useful to the user.

## Authorization and Authentication

If this was to be used in a real world application some kind of authentication and authorization would be needed so attributes and login capability would need to be added for this.  Authentication so only accepted users could log on and authoriztion so that only particular roles could perform certain types of actions.  For example you might not want all roles adding accounts.