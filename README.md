# TestDrivenHotel
Projektets Mål
- Utveckla en del av ett Hotellbokningssystem med Razorpages, Använd TDD under utveckligen och alternativt ett CI/CD-pipeline för deployment till Azure.

**1. Förstå och definiera kraven**

Mål: Definiera funktioner för ditt system.

Aktiviteter:
- Formulera användarberättelse(r) (minst 1!)
- Lista funktioner.

  **AnvändarBerättelser**
- Som en hotellgäst vill jag kunna söka och boka tillgängliga rum via en webbsida.
   -	Möjlighet att välja in- och utcheckningsdatum och rumstyp (enkel-, dubbelrum) på en Razor Page-sida.
   - Visa en lista över tillgängliga rum som matchar kriterierna.
   - Möjlighet att boka ett rum genom webbgränssnittet
- Som hotellgäst vill jag kunna se vad Hotellet erbjuder via webbsidan
   - kunna se bilder på olika typer av rum
   - kunna välja ett rum för att se vad rummet erbjuder och pris
- Som hotell gäst vill jag kunna läsa om hotellet och kontakta personal
  -	Visa hotel info på en kontakt sida
  - Möjlighet att läsa om hotellet på en About sida


**2. Designa systemarkitekturen**

Mål: Skapa en plan för ditt system.

Aktiviteter:
- Utveckla en högnivåarkitektur, affärslogik och Razor Pages-gränssnitt (minst 2 lager).
- Definiera datastrukturer och modeller.

**3. Skriv testfall med xUnit och FluentAssertions**

Mål: Skriva testfall för varje funktion och aspekt av systemet.

Aktiviteter:
- Skapa ett testprojekt och skriv testfall för varje funktion du definerat ovan (dina Användarberättelser).
- Fundera på vilka “edge cases” varje funktion har, och skriv test för dessa.

**4. Implementera funktionalitet med TDD**

Mål: Utveckla systemet enligt TDD-processen (Red-Green-Refactor).

Aktiviteter:
- Skriv kod för att få testen att passera.
- Refaktorera koden för förbättringar.

**5. Skapa enkelt användargränssnitt**

Mål: Skapa ett enkelt användargränssnitt med ASP.NET Core Razor Pages.

Aktiviteter:
- Använd Razor Pages för att bygga gränssnittet för ditt system.
- Integrera gränssnittet med bakomliggande affärslogik för din(a) användarberättelse(r).

**6. VG: CI/CD**

Mål: Introducera kontinuerlig integration, där dina testkörningar och kodincheckningar hanteras automatiskt
via GitHub Actions.

Aktiviteter:
- Skapa en CI/CD pipeline som kör alla tester när en push till main branchen görs.
- Om testerna går igenom, publicera projektet till Azure som en web app.

**Noteringar**
- Det är inte ett krav att implementera en databas i projektet, använd gärna istället en statisk lista för att
hantera datan.
- När appen publiceras om kommer den statiska listan att rensas, men det är okej
