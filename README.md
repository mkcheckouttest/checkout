# README

## What is in this repository

`OfficeShoppingList` - This is the completion of the API for adding items to a shopping list for the office.
`checkout-net-library` - Forked and then modified library for 

## How long did I spend and what has been implemented?

- Roughly 5 hours
- Implemented of `OfficeShoppingList`:
	- API Endpoints
	- Domain Model and Unit tests for behaviour of a Shopping List
	- Application Services for interacting with Domain Model and using data transfer objects
	- In Memory representation of Persisteted Shopping Lists
	- Exception based Resposne message handling for Web API
	- Inclusion of third party library for returning partial JSON media - see (here)[https://github.com/dotarj/PartialResponse] for details of this library.
	- Very Basic API Key validation (based on the Public key that is used within `checkout-net-library`)
	- Unit Tests for Domain Models 
	- Unit Tests for Services
	- Unit Tests for Exception Handling
- Implemented for `checkout-net-library`:
	- `OfficeShoppingListService` and tests
	- Added to `ApiUrls` and `AppSettings` required information.

## What else would you have done if you had more time?

- Not cut corners with working in a TDD manner with some of the features.
- Added in tests for Web API layer
- Added in full test coverage for Exception Handlers
- Expanded API Key validation and made more robust.
- Updated the `ApiHttpClient` to accept other Status codes than `OK` to provide model conversion.