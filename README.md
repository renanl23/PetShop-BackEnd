# PetShop-BackEnd

## Configuração de Ambiente:

Após clonar o projeto é necessário instalar esses pacotes:

> dotnet add package Microsoft.EntityFrameworkCore.Sqlite

>dotnet tool install --global dotnet-ef

>dotnet add package Microsoft.EntityFrameworkCore.Design

> dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

> dotnet tool install -g dotnet-aspnet-codegenerator


Confiar no certificado de desenvolvimento HTTPS

> dotnet dev-certs https --trust

## Criar Endpoints

Criar classe em Modal(Example.cs) e Context(ExampleContext.cs)

Gerar Controller com a linha de comando abaixo:

> dotnet-aspnet-codegenerator controller -name ExampleController -async -api -m Example -dc ExampleContext -outDir Controllers

Realizar atualização no Banco de Dados

> dotnet ef migrations add --context ExampleContext AddSomething

>dotnet ef database update --context ExampleContext