// ==========================================
// 1.  HULPFUNCTIES 
// ==========================================

/**
 * Vertaalt Open-Meteo WMO-weercodes naar een leesbare tekst en icoon.
 * Bevat een complete dekking van alle WMO-weercodes.
 */
function vertaalWeercode(code) {
  // Exacte matches voor veelvoorkomende codes
  const exacteCodes = {
    0: { tekst: "Zonnig / Helder", icoon: "☀️" },
    1: { tekst: "Overwegend helder", icoon: "🌤️" },
    2: { tekst: "Half bewolkt", icoon: "⛅" },
    3: { tekst: "Bewolkt", icoon: "☁️" },
    45: { tekst: "Mist", icoon: "🌫️" },
    48: { tekst: "Rijpnevel", icoon: "🌫️" },
    61: { tekst: "Lichte regen", icoon: "🌧️" },
    63: { tekst: "Matige regen", icoon: "🌧️" },
    65: { tekst: "Zware regen", icoon: "🌧️" },
    80: { tekst: "Lichte buien", icoon: "🌦️" }, 
    81: { tekst: "Matige buien", icoon: "🌦️️" },
    82: { tekst: "Zware buien", icoon: "🌧️" },
    95: { tekst: "Onweersbui", icoon: "⛈️" },
    96: { tekst: "Onweer met lichte hagel", icoon: "⛈️" },
    99: { tekst: "Onweer met zware hagel", icoon: "⛈️" },
  };

  if (exacteCodes[code]) {  // Controleer of de code exact in de lijst staat
    return exacteCodes[code];
  }

  // Bereik-controles voor motregen (51-57) en sneeuw (71-77)
  if (code >= 51 && code <= 57) {
    return { tekst: "Motregen", icoon: "🌧️" };
  }
  if (code >= 71 && code <= 77) {
    return { tekst: "Sneeuwval", icoon: "❄️" };
  }
  if (code >= 85 && code <= 86) {
    return { tekst: "Sneeuwbuien", icoon: "🌨️" };
  }

  return { tekst: "Wisselvallig", icoon: "🌡️" };
}

// ==========================================
// 2. API / FETCH FUNCTIE 
// ==========================================

/**
 * Haalt de weerdata op van Open-Meteo.
 * Gebruikt de actuele API-parameter
 */
async function haalWeerData(lat = 52.07, lon = 4.30) {
  const apiUrl = `https://api.open-meteo.com/v1/forecast?latitude=${lat}&longitude=${lon}&current=temperature_2m,weather_code`;

  const response = await fetch(apiUrl); //haalt de status op en verbind

  if (!response.ok) {     //controlleren van de status
    throw new Error(`Netwerkfout: status ${response.status}`);
  }


  
  const data = await response.json();   //leest de inhoud die het krijgt en omzetten naar object

  if (!data.current) {    //hier controleerrd of de data erin zit.
    throw new Error("Geen actuele weergegevens gevonden in de API-respons.");
  }



  return {
    temperatuur: Math.round(data.current.temperature_2m),
    code: data.current.weather_code
  };
}

// ==========================================
// 3. DOM MANnIPULATIE FUNCTIES
// ==========================================

/**
 * Bouwt en toont de weerkaart in het DOM.
 */
function toonWeerData(container, temperatuur, weerInfo, plaatsnaam) {
  const kaart = document.createElement("div");
  kaart.className = "weer-kaart";

  const icoonSpan = document.createElement("span");
  icoonSpan.className = "weer-icoon";
  icoonSpan.setAttribute("aria-hidden", "true");
  icoonSpan.textContent = weerInfo.icoon;

  const tekstDiv = document.createElement("div");

  const tempP = document.createElement("p");
  tempP.className = "weer-temperatuur";
  tempP.textContent = `${temperatuur}°C`;

  const beschrijvingP = document.createElement("p");
  beschrijvingP.className = "weer-beschrijving";
  beschrijvingP.textContent = weerInfo.tekst;

  const locatieP = document.createElement("p");
  locatieP.className = "weer-locatie";
  locatieP.textContent = `Locatie: ${plaatsnaam}`;

  tekstDiv.appendChild(tempP);
  tekstDiv.appendChild(beschrijvingP);
  tekstDiv.appendChild(locatieP);

  kaart.appendChild(icoonSpan);
  kaart.appendChild(tekstDiv);

  // Veilig de container leegmaken en de nieuwe kaart invoegen zonder HTML
  container.replaceChildren(kaart);
}

/**
 * Toont een toegankelijke foutmelding in de container.
 */
function toonWeerFout(container, bericht) {
  const foutP = document.createElement("p");
  foutP.className = "weer-fout";
  foutP.setAttribute("role", "alert");
  foutP.textContent = `⚠ ${bericht}`;          // Voeg een waarschuwingssymbool toe voor visuele feedback

  container.replaceChildren(foutP);
}

/**
 * Zet de container in de laadstatus.
 */
function toonLaadStatus(container) {
  const laadP = document.createElement("p");
  laadP.className = "weer-status";
  laadP.textContent = "Weergegevens laden...";

  container.replaceChildren(laadP);
}

// ==========================================
// 4. INITIALISATIE & COORDINATIE
// ==========================================

async function Weer() {
  const container = document.getElementById("weer-container");
  if (!container) return;

  // Zorg dat aria-live aanwezig is voor toegankelijkheid
  container.setAttribute("aria-live", "polite");

  // Toon direct de laadstatus
  toonLaadStatus(container);

  const standaardPlaats = "Den Haag";

  try {
    // 1. Data ophalen (gescheiden functie)
    const { temperatuur, code } = await haalWeerData(52.07, 4.30); // Standaardcoördinaten voor Den Haag

    // 2. Transformatie (gescheiden functie)
    const weerInfo = vertaalWeercode(code);

    // 3. Renderen (gescheiden functie, met plaats als parameter)
    toonWeerData(container, temperatuur, weerInfo, standaardPlaats);

  } catch (error) {
    console.error("Fout bij het laden van het weerbericht:", error);
    toonWeerFout(container, "Het is niet gelukt om het actuele weer op te halen.");
  }
}

// Zorg dat het script correct start
if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", Weer);
} else {
  Weer();
}