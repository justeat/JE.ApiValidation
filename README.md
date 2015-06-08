# JE.ApiValidation.Library

## Features
### Standard error response DTO
Clients shouldn't have to parse different response bodies to find out what went wrong.

 Request validation can happen outside of your application logic (in a filter, or interceptor), making your code easier to understand.
* Error processing can happen outside of your application logic, by running `_validator.ValidateAndThrow(thingToProcess);` - the `ValidationException` is caught and transformed to the standard error response DTO
* A hook to log what went wrong before serving back the bad request.

## Motivations
* API consumers want to be able to handle errors in a standard way
  * anything else is wasted time
    * implementing
    * debugging
    * supporting
    * operating

## Development

Please follow the guidelines in [CONTRIBUTING.md](CONTRIBUTING.md).

* [CI](http://ci.je-labs.com/project.html?projectId=Jalfrezi_Packages_JeApiValidationLibrary)

## Getting Started

### WebApi
See the example in `src/JE.ApiValidation.Examples.WebApi`, and the tests for that in `src/JE.ApiValidation.Examples.WebApi.Tests`.

### OpenRasta
PR for example gratefully accepted. It should be possible to re-use the same tests via `OpenRasta.Hosting.Owin`.
