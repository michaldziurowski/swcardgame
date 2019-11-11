# SW Card Game

![](https://github.com/michaldziurowski/swcardgame/workflows/Main%20CI/badge.svg)

## Description

'SW Card Game' is a simple application allowing user to compare two cards of specific definition based on selected property. Application displays the result of comparison of properties by they numeric value, declares winner and counts score.

## Glossary

| Term                | Description                                                                                                                                                                   |
| ------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Card definition** | Defines properties available on cards (e.g. _people_ card definition defines that on every card of this definition two properties are available: _noOfHeads_ and _noOfLegs_). |
| **Card**            | Named group of properties and their values assigned to single definiton (e.g. _spock_ card has properties _noOfLegs_ = 1 and _noOfHeads_ = 1)                                 |
| **Deal**            | Single round in game.                                                                                                                                                         |
| **Game**            | Group of rounds for which scores are calculated.                                                                                                                              |

## Solution

The application is made of ASP.NET Core based server and React based client.
Server is using EntityFramework Core as an ORM and Postgres as a database.

### Server architecture

Server code structure is inspired by [Clean architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) and consists of following projects:

-   `src/SWCardGame.Core` - responsible for application logic, domain objects and definiton of interfaces which applicaton logic uses to fulfill its usecases.
-   `src/SWCardGame.Persistance` - responsible for implementation of persistance interfaces defined in core.
-   `src/SWCardGame.WebApi` - provides RESTful api and is considered an entrypoint to the application. Currently api only allowes operations on Cards but handling of Card definitions could be easly added.

### Client architecture

Client structure consist of following folders:

-   `components` - the place where React components are defined. Each subfolder is representing screen in application and consists of React components which are the building blocks of this screen. There is also _index.ts_ which exposes main component.

    Components can be divided into two groups:

    -   `Container` components - responsible for data fetching and logic and provision of those to presentational components.
    -   `Presentational` components - responsible for defining how component should be presented.

-   `api` - here is the code responsible for communication with the server (for now server address is harcoded and of course it shouldn't be but since this is just an demo app let it stay as it is).

To provide type safety code on client side is written in TypeScript.

### Unit tests

> Unit tests in this application are ment to show an approach not to provide full test coverage.

#### Server

Server unit tests are located in `tests` directory and are divided into projects matching server architecture. Test methods naming convention follows the rule that it should have information about whats being tested, under what conditions and with what result.

`NUnit` is being used as a test framework and `Nsubstitute` as a mock/stub library.

To run the tests use following command from inside of `server` folder (requires .net core 3 sdk to be installed on the machine)

```
> dotnet test
```

#### Client

Client unit test are located in `*.test.tsx` files next to tested component.
`Jest` and `react-testing-library` are being used as test framework.

To run the tests use following command from inside of `react-client` folder (requires nodejs with npm > 5.2.x to be installed on the machine)

```
> npx yarn install
> npx yarn test
```

### e2e tests

> e2e tests only work if the application is running locally. See `Deployment` section for more informations.

For end to end tests `Cypress` framework is used. Tests are located in `*.spec.js` files inside of `react-client/e2e` directory.

To run the tests use following command from inside of 'react-client' folder (requirements are the same as for client unit tests)

```
> npx cypress open
```

or use docker if we dont care about cypress dashboard view

```
docker run --rm --network="host" -it -v $PWD:/e2e -w /e2e cypress/included:3.2.0
```

### API documentation

Api documentation can be viewed under `http://<serverurl>/docs` endpoint.

It is created using `Swagger` tools and `Swashbuckle.AspNetCore` library.

### CI

Continous Integration pipeline is running on GitHub Actions. Currently there is one workflow defined:

-   `Main CI` - builds and runs unit tests for server and client on every push to master branch.

### Deployment

Currently application is not deployed anywhere.
To run it locally use docker (from root folder):

```
> docker-compose up
```

This sets up client, server and postgres containers. When containers are running following urls can be used to access application:

-   `http://localhost:5500` - client application
-   `http://localhost:5000/api/v1` - web api
-   `http://localhost:5000/docs` - web api documentation

Before application can be used database must be prepared. For this use following endpoint:

```
GET http://localhost:5000/api/v1/seed
```
