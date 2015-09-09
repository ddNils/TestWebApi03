# TestWebApi03
Trying to boiler-plate a simple ioc-ed project structure as a webapi example

## TestWebApi03.Entities
This project includes all persistence classes, which are used with EntityFramework to build the tables in the database.

## TestWebApi03.Contracts
Holds interfaces for entities so that the other layers dont always have to know the DAL (data-access-layer).

## TestWebApi03.Repos
First I wanted to go with repositories here. I then learned, that I would be using a unit of work pattern with the repositories and that the repositories were more or less integrated into EntityFrameworks 'DbSet'. So this project has to be renamed next time.

## TestWebApi03.WebApi
This one is a classic WebApi project from VS2015 without authorization. I want to add that with my next approach.

### TODO:
- I am still unsure, if I need to have a Dependency Resolver. - I think I don't need it, but let's see.
- of course the testing and mocking classes have not yet been included
- it's interesting how the controller reacts to empty collections or objects, this seems to have been improved greatly with MVC6. So maybe on my next try, I will go for mvc6.
