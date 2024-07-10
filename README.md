# RekrutacjaNetPc


### Opis

Projekt składa się z 3 oddzielnych aplikacji:
- API (ASP.NET CORE Web API)
- Klienta (Blazor Server App)
- Serwera autoryzacyjnego użytkowników (IdentityServer4)

oraz korzysta z bazy danych MSSQL

### Przygotowanie projektu

1. Na początku trzeba ustawić ConnectionString w projektach:
   - Rekrutacja.Api
   - Rekrutacja.Server

2. Odpalić migracje z projektów/bibliotek:
   - Rekrutacja.Infrastructure
   - Rekrutacja.Server

Jeżeli chodzi o bibliotekę Rekrutacja.Infrastructure to trzeba ustawić projekt startowy na Rekrutacja.Api i następnie odpalić migrację. Można zrobić to na dwa sposoby:
  - z konsoli PowerShell przechodzimy do katalogu biblioteki Rekrutacja.Infrastructure, następnie odpalamy migrację komendą "dotnet ef database update --startup-project ..\Rekrutacja.Api\"
  - z konsoli Package Manager przechodzimy do katalogu biblioteki Rekrutacja.Infrastructure, ustawiamy Default project na Rekrutacja.Infrastructure, ale jako startowy Rekrutacja.Api i odpalamy migrację komendą "upate-database"

Migracje z projektu Rekrutacja.Server również można odpalić na dwa sposoby z użyciem dwóch poprzednich środowisk, jednak będziemy używać kontekstów:
  - pierwsza migracja to "dotnet ef database update -c PersistedGrantDbContext", druga "dotnet ef database update -c ConfigurationDbContext", trzecia "dotnet ef database update -c AspNetIdentityDbContext"
  - analogicznie z konsoli Package Manager pierwsza "upate-database -Context PersistedGrantDbContext", druga "upate-database -Context ConfigurationDbContext" i trzecia "upate-database -Context AspNetIdentityDbContext"

3. Po zakończeniu migracji trzeba ustawić trzy projekty startowe na raz
  - Rekrutacja.Server
  - Rekrutacja.Api
  - Rekrutacja.Client

### Wykorzystane biblioteki

1. API oraz biblioteki z nim powiązane (Rekrutacja.Application, Rekrutacja.Domain, Rekrutacja.Infrastructure)
  - IdentityServer4.AccessTokenValidation
  - Microsoft.EntityFrameworkCore oraz powiązane z nim dodatkowe biblioteki
  - Newtonsoft.Json
  - FluentValidation.AspNetCore

2. Klient
  - Blazor.Bootstrap
  - IdentityModel
  - Microsoft.AspNetCore.Authentication.OpenIdConnect
  - Newtonsoft.Json

3. Serwer autoryzacyjny:
  - IdentityServer4 oraz powiązane z nim dodatkowe biblioteki
  - Microsoft.AspNetCore.Authentication.OpenIdConnect
  - Microsoft.EntityFrameworkCore.SqlServer
  - AutoMapper
  - IdentityModel

### Opis metod

1. API
  - W warstwie webowej zostały użyte 3 kontrolery o adresach:
    a) api/categories - pobieranie listy
    b) api/contacts - pełny crud
    c) api/categories/id/subcategory - pobieranie listy
  - został także dodany serwis "IPasswordHasher" haszujący hasła kontaków
  - W warstwie Application zostału umieszczone takie elementy jak DTO, mappery z obiektów domenowych na DtO oraz requesty, helpery (filtry) do zapytań GET oraz walidatory do request'ów
  - W warstwie Domain są trzymane tylko encje, które odzwierciedlają obiekty bazy danych
  - W warstwie Infrastructure zawarta jest konfiguracja encji z biblioteką EFCore oraz seedowanie wstępnych danych do bazy danych. Zawarte są tu także migracje oraz repozytoria, ponieważ został użyty w aplikacji wzorzec projektowy Repository.

2. Klient
  - W folderze Services znajduje się model do przechowywania potrzebnych ustawień, by pobrać token z serweru autoryzującego oraz serwis "TokenService", dzięki któremu jesteśmy w stanie pobrać token.
  - W folderze Pages mamy takie komponenty jak:
    a) ContactList - jest to lista kontaktów, dostępne są na niej różne akcje crudowe dopiero po zalogowaniu się przez użytkownika, będąc niezalogowanym możemy jedynie przejrzeć listę oraz szczegóły kontaktu.
    b) ContactDetail - jest to modal z szczegółami danego kontaktu
    c) Są też komponenty przekierowywujące nas do strony logującej oraz jest możliwość wylogowania użytkownika
