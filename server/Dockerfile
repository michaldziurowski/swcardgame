FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY src/SWCardGame.Core/*.csproj ./src/SWCardGame.Core/
COPY src/SWCardGame.Persistence/*.csproj ./src/SWCardGame.Persistence/
COPY src/SWCardGame.WebApi/*.csproj ./src/SWCardGame.WebApi/
COPY tests/SWCardGame.Core.Tests/*.csproj ./tests/SWCardGame.Core.Tests/
COPY tests/SWCardGame.WebApi.Tests/*.csproj ./tests/SWCardGame.WebApi.Tests/
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SWCardGame.WebApi.dll"]
