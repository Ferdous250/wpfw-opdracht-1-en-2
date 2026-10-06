# Portfolio Website - WPFW Opdracht 1 en 2

**Student:** Ferdous Uddin  
**Studentnummer:** 25033433  
**Klas:** Klas 1
**Datum:** September 2026  
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
* **GitHub Repository:** [https://github.com/Ferdous250/wpfw-opdracht-1-en-2/tree/develop]

## Mappenstructuur
```text
wpfw-opdracht-1 en 2 /
├── css/
│   ├── base.css        # Reset, basisstijlen en hoofdcontainer (alle pagina's)
│   ├── layout.css      # Header, navigatie en footer (alle pagina's)
│   ├── homepage.css    # Stijl voor index.html
│   ├── blog.css        # Stijl voor blog.html
│   ├── projecten.css   # Stijl voor projecten.html
│   └── contact.css     # Stijl voor contact.html
├── img/
│   ├── contactmail.png # Icoon van de contactknop
│   └── profielfoto.jpg # Profielfoto
├── js/
│   ├── blog.js         # Dynamische logica en interactie voor de blog/reflecties
│   ├── contact.js      # Validatie en verwerking van het contactformulier
│   ├── projecten.js    # Tonen, zoeken en filteren van projecten
│   └── weer.js         # Weerwidget
├── .gitignore          # Uitsluiten van systeembestanden
├── AI-Log.md           # Logboek AI-gebruik (AIAS-niveau 2)
├── blog.html           # Pagina 3: Blog-overzicht
├── contact.html        # Pagina 4: Contactformulier
├── index.html          # Pagina 1: WhoAmI / Homepage
├── projecten.html      # Pagina 2: Projecten-overzicht
└── README.md           # Projectinformatie