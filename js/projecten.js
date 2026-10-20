// 1. Data-bron: Array van project-objecten
const projectenData = [
  {
    id: 1,
    titel: "Avond4daagse",
    beschrijving: "Een applicatie voor de Avond4daagse met inschrijvingen, routes en deelnemersbeheer.",
    categorie: "Applicatie",
    technieken: ["HTML5", "CSS3", "JavaScript", "Documentatie"],
    documenten: [
      { titel: "Samenwerkingsovereenkomst", url: "docs/samenwerkingsovereenkomst.pdf" }
    ]
  },
  {
    id: 2,
    titel: "Hotel Simulatie",
    beschrijving: "een opdracht om een simulatie te bouwen waarin gasten in/uitchecken en activiteiten kunnen gaan doen.",
    categorie: "Applicatie",
    technieken: ["HTML5", "CSS3", "JavaScript", "DOM"]
  },
  {
    id: 3,
    titel: "Database Opdrachten",
    beschrijving: "Database-ontwerpen, ERD's, normalisatie en geavanceerde SQL-queries uitgewerkt voor studie-opdrachten.",
    categorie: "Database",
    technieken: ["SQL", "MySQL", "Database Design", "UML"]
  },
  {
    id: 4,
    titel: "Portfolio Website",
    beschrijving: "Mijn persoonlijke responsive portfolio-website opgebouwd volgens WCAG-richtlijnen en responsive principes.",
    categorie: "Web",
    technieken: ["HTML5", "CSS3", "JavaScript", "Git"]
  }
];

// DOM Elementen selecteren
const projectenContainer = document.getElementById("projecten-container");
const searchInput = document.getElementById("search-input"); // Input veld voor zoektermen
const categoryFilter = document.getElementById("category-filter");

const sortSelect = document.getElementById("sort-select");

// 2. Functie om project-cards dynamisch te maken en te tonen
function renderProjecten(projecten) {
  // Container leegmaken
  projectenContainer.innerHTML = "";

  if (projecten.length === 0) {
    const noResults = document.createElement("p");  
    noResults.className = "no-results";
    noResults.textContent = "Geen projecten gevonden die voldoen aan de zoekcriteria.";
    projectenContainer.appendChild(noResults);  
    return;
  }

  // Elke kasart bouwen met createElement/textContent
  projecten.forEach((project) => {
    const card = document.createElement("article");
    card.className = "project-card";

    const title = document.createElement("h3");
    title.textContent = project.titel;

    const desc = document.createElement("p");
    desc.textContent = project.beschrijving;

    const tagsContainer = document.createElement("div");
    tagsContainer.className = "tags";

    project.technieken.forEach((tech) => {
      const tag = document.createElement("span");
      tag.className = "tag";
      tag.textContent = tech;
      tagsContainer.appendChild(tag);
    });

    card.appendChild(title); // titel,desc en tagscontainer onderelkaar toevoegen en in projectcontainer word het opgebouwd .
    card.appendChild(desc);
    card.appendChild(tagsContainer);

    projectenContainer.appendChild(card);
  });
}

// 3. Functie voor filteren en sorteren
function filterAndSortProjecten() {
  const searchTerm = searchInput.value.toLowerCase().trim();
  const selectedCategory = categoryFilter.value;
  const sortOrder = sortSelect.value;

  // Filteren
  let gefilterd = projectenData.filter((project) => {
    const matchesSearch =
      project.titel.toLowerCase().includes(searchTerm) ||
      project.beschrijving.toLowerCase().includes(searchTerm) ||
      project.technieken.some((t) => t.toLowerCase().includes(searchTerm));

    const matchesCategory =
      selectedCategory === "all" || project.categorie === selectedCategory;

    return matchesSearch && matchesCategory;
  });

  // Sorteren
  gefilterd.sort((a, b) => {
    if (sortOrder === "title-asc") {
      return a.titel.localeCompare(b.titel);
    } else if (sortOrder === "title-desc") {
      return b.titel.localeCompare(a.titel);
    }
    return 0;
  });

  renderProjecten(gefilterd);
}

// 4. Event Listeners koppelen
searchInput.addEventListener("input", filterAndSortProjecten);  
categoryFilter.addEventListener("change", filterAndSortProjecten); 
sortSelect.addEventListener("change", filterAndSortProjecten);

// Eerste keer renderen bij laden van pagina
document.addEventListener("DOMContentLoaded", () => {
  renderProjecten(projectenData);
});