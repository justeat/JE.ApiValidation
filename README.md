# JE.ApiValidation

[![Build status](https://ci.appveyor.com/api/projects/status/6wigrr8a3b99mn0s?svg=true)](https://ci.appveyor.com/project/justeattech/je-apivalidation)

## Features
### Standard error response DTO
* Clients shouldn't have to parse different response bodies to find out what went wrong.
  * **`400 Bad Request` will give back response bodies complying with the standard contract.**
* Clients shouldn't have to parse text to figure out what sort of error happened.
  * **errors have a unique integer ID, split into ranges to denote error classes**

### Request validation outside of your application logic
...(in a filter, or interceptor), making your code easier to understand.

### Error processing outside of your application logic
... by running `_validator.ValidateAndThrow(thingToProcess);` - the `ValidationException` is caught and transformed to the standard error response DTO

### Hooks to log errors
Each filter or interceptor has a virtual method to allow you to log a warning in your logging framework of choice.

## Motivations
* API consumers want to be able to handle errors in a standard way
  * anything else is wasted time
    * implementing
    * debugging
    * supporting
    * operating

## Development

Please follow the guidelines in [CONTRIBUTING.md](CONTRIBUTING.md).

* [CI](https://ci.appveyor.com/project/justeattech/je-apivalidation)
* To build, currently, use Visual Studio. We plan to open-source our build scripts, but haven't yet.

## Getting Started

### Framework agnostic

#### Request validation
0. Define your API contract as normal.
0. Define a class inheriting from `AbstractValidator<TDto>`.
0. Make that class visible to IoC.

-> validation will occur within model-binding, and if the DTO is invalid, a `400 Bad Request` will result, with a standard error response, with code `40000`.

#### Response processing rules errors
0. Define a class inheriting from `AbstractValidator<TThingYouWantToApplyRulesTo>`.
0. Make that class visible to IoC, take a dependency on it.
0. Call `_validator.ValidateAndThrow(input);` in your controller action or handler or service class, or wherever you need to apply rules.

-> A `ValidationException` will be thrown and caught & handled by the filter/interceptor, and a `400 Bad Request` will result, with a standard error response, with code `45000`.

### WebApi
See the example in `src/JE.ApiValidation.Examples.WebApi`, and the tests for that in `src/JE.ApiValidation.Examples.Tests.WebApi`.

### OpenRasta
See the example in `src/JE.ApiValidation.Examples.OpenRasta`, and the tests for that in `src/JE.ApiValidation.Examples.Tests.OpenRasta`.
