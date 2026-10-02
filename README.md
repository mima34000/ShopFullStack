# ShopFullStack

Det här är en fullstack-applikation för att rulla igång en enkel butikshantering med produkter och kategorier. Projektet är gjort som den andra inlämningsuppgiften i C# II-kursen och kör hela kedjan från databasen upp till frontend.

## Tekniker som använts

* **Backend:** ASP.NET Core Web API (.NET 8)
* **Frontend:** Blazor Server (.NET 8)
* **Databas:** SQL Server LocalDB med Entity Framework Core
* **Tester:** xUnit tillsammans med NSubstitute för mockning

## Funktionalitet & Arkitektur i korthet

* Full CRUD för produkter samt hantering av kategorier (en-till-många-relation).
* Implementerat Generic Repository-mönster och Dependency Injection rakt igenom.
* DTOs används för att mappa requests och responses.
* Unit-tester för servicelagret med mockade repositories.

## Projektets uppbyggnad

* `ShopFullStack.Api` – Backend delen med controllers, services, repositories och EF Core.
* `ShopFullStack.Web` – Frontend appen i Blazor Server.
* `ShopFullStack.Api.Tests` – xUnit-testerna för logiken.

## Så här kör du projektet

1. Klona repot.
2. Öppna `ShopFullStack.sln` i Visual Studio 2022.
3. Högerklicka på lösningen (Solution) -> *Configure Startup Projects* -> välj *Multiple startup projects* och sätt både `ShopFullStack.Api` och `ShopFullStack.Web` till "Start".
4. Tryck `Ctrl+F5` för att starta.

När allt snurrar hittar du API:et (Swagger) på `https://localhost:7000` och Blazor-frontenden på `https://localhost:7172`.
