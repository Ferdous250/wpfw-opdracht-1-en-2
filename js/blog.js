// 1. Databron met informatie per week (Array van objecten)
const blogData = [ 
  {
    week: "03-09-26  Week 1",
    titel: "Opzet Portfolio & HTML-structuur",
    inhoud: `week 1 was het even weer terug gaan naar de basis denk bijvoorbeeld aan hoe DNS domeinnamen omzet naar IP-adressen, en hoe TCP met een three-way handschake verbindingen opzet. ook ontdekte ik hoe HTTP(S) wekt, inclusief verzoeken, antwoorden, statuscodes (zoals 200 en 404) en URL-structuren. 
             in de praktijk lessen heb lokaal een 'Hllo, world' pagina gemaakt en getest in de browser.`
  },
  {
    week: "09-09-26  Week 2",
    titel: "CSS Layout & Responsive Design",
    inhoud: `In de tweede week ben ik aan de slag gegaan met HTML en CSS. Ik heb ontdekt dat je met HTML de structuur en de inhoud van je pagina opbouwt. Door semantische elementen te gebruiken zoals <header>, <nav>, <main>, <section> en <footer> kan ik de inhoud beter organiseren en de toegankelijkheid verbeteren. 
             Ook heb ik geleerd hoe je bestanden juist aan elkaar koppelt met paden en hoe je media zoals afbeeldingen en video's toevoegt. Daarnaast heb ik geleerd hoe je een site mooi maakt met CSS via een los stijlbestand. Ik snap nu hoe het 'box model' werkt met randen en afstanden (padding en margin), en dat box-sizing: border-box voorkomt dat vakjes onverwachts groter worden dan je wilt. Om onderdelen netjes naast elkaar te zetten, heb ik Flexbox gebruikt.`
  },
  {
    week: "16-09-26  Week 3",
    titel: "DOM-interactie & Dynamische Content",
    inhoud: `In week 3 ben ik aan de slag gegaan met JavaScript (ES6+) en de DOM om mijn pagina interactief te maken. Ik heb als eerste gekeken naar het verschil met Java en hoe je kortere code schrijft met let en const. 
             Daarnaast heb ik geleerd om met de DOM elementen op te zoeken, teksten of stijlen aan te passen en nieuwe elementen toe te voegen of te verwijderen. Ook heb ik ontdekt hoe je met event listeners kunt reageren op acties van de gebruiker, zoals een klik op een knop.`
  },
  {
    week: "23-09-26  Week 4",
    titel: "TypeScript, Form & Fetch API",
    inhoud: `In week 4 heb ik geleerd hoe TypeScript werkt om fouten in je code te voorkomen met types, hoe je formulieren goed valideert en foutmeldingen toegankelijk maakt voor schermlezers met aria-attributen, en hoe je met de Fetch API en async/await live data van het internet ophaalt.
             Ook snap ik nu hoe HTTP-methoden zoals GET en POST werken om data op te vragen of op te slaan.`
  },
];

// Knop maken om de kaart in te klappen
function maakWeekKnop(post) {
  const button = document.createElement("button");
  button.classList.add("week-header");
  button.setAttribute("aria-expanded", "false");
  button.innerHTML = `
      <span><strong>${post.week}</strong>: ${post.titel}</span> 
      <span class="pijl">▼</span>
    `;
  return button;
}

// Inhoudsblok aanmaken voor de tekst van de week
function maakWeekInhoud(post) {
  const content = document.createElement("div");
  content.classList.add("week-content");
  
  const text = document.createElement("p");
  text.textContent = post.inhoud;
  content.appendChild(text);
  return content;
}

// Zorg ervoor dat het in- en uitklappen werkt bij een klik op de knop
function koppelToggle(card, button, content) {
  button.addEventListener("click", () => {

    const isOpen = card.classList.toggle("open");
    button.setAttribute("aria-expanded", isOpen ? "true" : "false");
    
    content.style.maxHeight = isOpen ? content.scrollHeight + "px" : "0";
  });
}

// Alles samenvoegen tot één complete week-kaart
function maakBlogKaart(post) {
  const card = document.createElement("div");
  card.classList.add("week-card");

  const button = maakWeekKnop(post);
  const content = maakWeekInhoud(post);
  koppelToggle(card, button, content);

  
  card.appendChild(button); //hier voegt ik de knop toe aan de kaart
  card.appendChild(content); //hier voeg ik de inhoud toe aan de kaart
  return card;
}

// Alle blogs ophalen en op de pagina zetten
function renderBlog() {
  const container = document.getElementById("weken-container") || document.getElementById("blog-container");
  if (!container) return;

  // Eerst de container leegmaken om dubbele gegevens te voorkkomen
  container.innerHTML = "";

  // Door de lijst lopen en alle kaarten toevoegen
  blogData.forEach((card) => {  
    container.appendChild(maakBlogKaart(card));//
  });
}

// Meteen uitvoeren als de pagina geladen is
document.addEventListener("DOMContentLoaded", renderBlog);

