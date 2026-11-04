# Portfolio Website - WPFW Opdracht 1,2 en 3

**Student:** Ferdous Uddin  
**Studentnummer:** 25033433  
**Klas:** Klas 1
**Datum:** November 2026  
**Opleiding:** HBO ICT / SE3 - De Haagse Hogeschool  

## Projectbeschrijving
Dit project vormt het fundament van mijn persoonlijke portfoliosite, opgebouwd tijdens het vak WPFW. De site is gerealiseerd met semantische HTML5 en responsive CSS3.

## opdracht 2: interactie
In opdracht 2 heb ik de site uit opdracht 1 interactief gemaakt met JavaScript. allee Javascript staan in losse bestanden in de map `js/`.

**de dynamische projectlijst'**
bestand: projecten.html
JS-bestand: js/projecten.js
wat het doet: maakt projecten uit de array projectData. je kan zoeken , filteren op categorie en sorteren op de titel.

**Inklapbare blog**
bestand: blog.html
JS-bestand: js/blog.js
Wat het doet: maakt weken uit de array blogData. Je kunt op een week klikken om deze open of dicht te klappen.

**Contactformulier met validatie**
bestand: contact.html
JS-bestand: js/contact.js
Wat het doet: Controleert de naam, het e-mailadres en het bericht. Het bericht moet minimaal 10 tekens bevatten. bij fouten komen ook duidelijke meldingen.

**Externe API (weer)**
bestand: index.html
JS-bestand: js/weer.js
Wat het doet: Haalt met fetch() het actuele weer op van Open-Meteo. Tijdens het laden en bij een fout wordt een passende melding weergegeven.

### Wat zit erin
**Projectenlijst** (projecten.html): de kaarten worden door JavaScript gemaakt uit een array. Je kunt zoeken, filteren op categorie en sorteren op titel.
**Contactformulier** (contact.html): het formulier controleert naam, e-mail en bericht (minimaal 10 tekens). Foutmeldingen zijn toegankelijk met aria-attributen. Er wordt bewust niets verstuurd, want een backend hoort bij opdracht 3.
**Weerwidget** (index.html): het actuele weer van Den Haag komt van de Open-Meteo API via `fetch`. Er is een laadtekst en een foutmelding.
**Blog** (blog.html): de weken komen uit een array en klappen open en dicht met een klik.

### JavaScript-bestanden
| Bestand           |Taak |
|
| js/projecten.js   | Projecten tonen, zoeken, filteren, sorteren |
| js/contact.js     | Formulier valideren |
| js/weer.js        | Weer ophalen en tonen |
| js/blog.js        | Blogkaarten maken en openklappen |

### Een project toevoegen
Zet een nieuw object in de array `projectenData` in `projecten.js`. De kaart verschijnt dan vanzelf.

## Live Demo & Repository
* **Live Website:** [https://ferdous250.github.io/wpfw-opdracht-1-en-2/]
* **GitHub Repository:** [https://github.com/Ferdous250/wpfw-opdracht-1-en-2/tree/main]


## opdracht 3 backend APi (.NET10)

Voor Opdracht 3 is de applicatie uitgebreid met een API in .NET 10 (`PortfolioApi`) waarmee de portfolio-gegevens via Entity Framework Core kunnen worden beheerd in een SQL Server database.

**vereisten**
--.NET 10 SDK
--SQL server (Localdb op windowsw voor macOS gebruikej je SQL server Docker)

**Minimal API vs controllers**
voor dit project heb ik gezkozen voor minimal API's inplaats van Controllers. bij controllers gebruik je aparte klassen met bijvoorbeeld het attribuut (ApiController of HTTPGet) en de klasse ControllerBasse. bij minimal APi definieer ik de routes direct via de endpoints. hierdoor hoef ik minder code te schrijven en blijft het voor kleine tot midelmatige projecten nog overzichtelijk.

**Reden van keuze:** Minimal API's zorgen voor minder boilerplate-code en overzichtelijke opzet. Omdat de backend is opgebouwd per feature, houdt dit de endpoints heel overzichtelijk en direct navigeerbaar. ook was tijdens de hoorcollege van les 6 verteld het om voor minimal API te gaan dat was ook een reden waarom ik voor dit heb gekozen.

## codestructuur
De code in de map PortfolioApi/ heb ik gemaakt op basis van verticale slices architectuur. Hierdoor staat alle relevante logica van één functionaliteit bij elkaar:

**PortfolioApi/Features/Blogposts/**: Bevat de entity, DTO's, service interface, service implementatie en endpoints voor blogposts.

**PortfolioApi/Features/Projects/**: Bevat de entity, DTO's, service interface, service implementatie en endpoints voor projecten.

**PortfolioApi/Data/**: Bevat de AppDbContext voor de databaseconfiguratie via EF Core.

**PortfolioApi/Migrations/**: Bevat de door Entity Framework Core gegenereerde migratiebestanden.

Door deze scheiding zijn de endpoints, de businesslogica (services) en de EF Core (DbContext) netjes ontkoppeld.

## verbinding 
**in PortfolioApi/appsettings.json staat de standaard verbinding voor Windows (LocalDB):**
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PortfolioDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}

bij het opstarten voert de appin program.cs automatisch contect.Databse.Migrate() uit. je hoeft de database dus niet zelf aan te maken: EF Core maakt PortfolioDb aan en past migraties uit de map PortfolioApi/Migrations/ toe.
met HasData worden ook de twee testprojecten toegeveoegd. voor blogpost heb ik geen testdata, die voeg je zelf toe via de API.

## Applicatie Starten
1. open de terminal en navigeer naar API-map: cd PortfolioApi
2. start de API: dotnet run
3. Open Swagger UI in de browser om de endpoints te testen: HTTP:
    http://localhost:5100/swagger
    HTTPS: https://localhost:7159/swagger

## API endpoints

De backend heeft 10 routes, verdeeld over de features Projects en Blogposts.

### Projects

| Methode           Endpoint                Beschrijving 
|
| GET               /api/projects          Haalt alle projecten op 
| GET               /api/projects/{id}       Haalt één project op 
| POST              /api/projects           Voegt een nieuw project toe 
| PUT               /api/projects/{id}       Werkt een bestaand project bij 
| DELETE            /api/projects/{id}       Verwijdert een project 

### Blogposts

| Methode       Endpoint                 Beschrijving 
|
| GET           /api/blogposts             Haalt alle blogposts op 
| GET            /api/blogposts/{id}      Haalt één blogpost op 
| POST          /api/blogposts`            Voegt een nieuwe blogpost toe 
| PUT           /api/blogposts/{id}      Werkt een bestaande blogpost bij
| DELETE        /api/blogposts/{id}       Verwijdert een blogpost 








## Mappen en codestructuur project
**mijn project heeft de website-bestdnen in de hoofmap en de backend in de map PortfolioApi. in die map heb ik de code per onderdeel, met eigen routes, services en DTO"s voor projecten en blogpost gemaakt.**

```text
wpfw-opdracht-1-en-2/
├── css/                           # Frontend stijlen (Opdracht 1 & 2)
│   ├── base.css                   # Reset, basisstijlen en hoofdcontainer
│   ├── layout.css                 # Header, navigatie en footer
│   ├── homepage.css               # Stijl voor index.html
│   ├── blog.css                   # Stijl voor blog.html
│   ├── projecten.css              # Stijl voor projecten.html
│   └── contact.css                # Stijl voor contact.html
├── img/                           # Afbeeldingen
│   ├── contactmail.png
│   └── profielfoto.jpg
├── js/                            # Frontend scripts (Opdracht 1 & 2)
│   ├── blog.js                    # Uitklapbare blogweken
│   ├── contact.js                 # Formuliervalidatie
│   ├── projecten.js               # Projecten tonen, zoeken, filteren
│   └── weer.js                    # Weerwidget
├── PortfolioApi/                  # Opdracht 3 Backend API (.NET 10)
│   ├── Data/
│   │   └── AppDbContext.cs        # EF Core DbContext-klasse
│   ├── Features/
│   │   ├── Projects/              # Verticale feature slice: Projecten
│   │   │   ├── Project.cs         # Entity model
│   │   │   ├── ProjectDtos.cs     # Invoer- en uitvoer-DTO's
│   │   │   ├── IProjectService.cs # Service-interface
│   │   │   ├── ProjectService.cs  # Businesslogica & EF Core datatoegang
│   │   │   └── ProjectEndpoints.cs# Minimal API route-handlers
│   │   └── Blogposts/             # Verticale feature slice: Blogposts
│   │       ├── Blogpost.cs        # Entity model
│   │       ├── BlogpostDtos.cs    # Invoer- en uitvoer-DTO's
│   │       ├── IBlogpostService.cs# Service-interface
│   │       ├── BlogpostService.cs # Businesslogica & EF Core datatoegang
│   │       └── BlogpostEndpoints.cs# Minimal API route-handlers
│   ├── Migrations/                # EF Core databasemigraties
│   ├── Properties/
│   │   └── launchSettings.json    # Server- en poortconfiguraties
│   ├── appsettings.json           # Instellingen en ConnectionStrings
│   └── Program.cs                 # Ingangspunt, Dependency Injection & Migraties
├── AI-Log.md                      # AI Verantwoording (AIAS-niveau 2)
├── blog.html                      # Frontend Blog
├── contact.html                   # Frontend Contact
├── index.html                     # Frontend Homepage
├── projecten.html                 # Frontend Projecten
└── README.md                      # Projectdocumentatie