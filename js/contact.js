// ==========================================
// 1. VALIDATIEFUNCTIES 
// ==========================================

// Controleert of naam niet leeg is ( .trim() erbij gezet om spatie te negeren)
function isNaamGeldig(waarde) {
  return waarde.trim().length > 0;
}


// Controleert e-mailadres (iets@iets.iets)
function isEmailGeldig(waarde) {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; //
  return emailRegex.test(waarde.trim()); 
}


// Controleert of bericht minimaal 10 tekens bevat
function isBerichtGeldig(waarde) {
  return waarde.trim().length >= 10;
}

// ==========================================
// 2. DOM MANIPULATIE 
// ==========================================

function toonFout(inputElement, foutElement, bericht) {
  inputElement.setAttribute("aria-invalid", "true");
  foutElement.textContent = `⚠ ${bericht}`;
}

function wisFout(inputElement, foutElement) {
  inputElement.removeAttribute("aria-invalid");
  foutElement.textContent = "";
}

// Hulpfunctie voor het valideren van één los veld
function valideerVeld(veldConfig) {
  const isGeldig = veldConfig.validator(veldConfig.input.value);
  if (!isGeldig) {
    toonFout(veldConfig.input, veldConfig.foutEl, veldConfig.foutTekst);
  } else {
    wisFout(veldConfig.input, veldConfig.foutEl);
  }
  return isGeldig;
}

// Valideert alle velden en geeft het eerste ongeldige veld terug (of null)
function valideerAlleVelden(veldenConfig) {
  let eersteFouteVeld = null;

  veldenConfig.forEach((veld) => {
    const isGeldig = valideerVeld(veld);
    if (!isGeldig && !eersteFouteVeld) {
      eersteFouteVeld = veld.input;
    }
  });

  return eersteFouteVeld;
}

// ==========================================
// 3. HOOFDFUNCTIE & EVENT LISTENERS
// ==========================================

function maakVelden() {
 //hier maak 
  return [
    {
      input: document.getElementById("naam"),
      foutEl: document.getElementById("naam-fout"),
      validator: isNaamGeldig,
      foutTekst: "Vul je naam in."
    },
    {
      input: document.getElementById("email"),
      foutEl: document.getElementById("email-fout"),
      validator: isEmailGeldig,
      foutTekst: "Vul een geldig e-mailadres in, zoals naam@voorbeeld.nl."
    },
    {
      input: document.getElementById("bericht"),
      foutEl: document.getElementById("bericht-fout"),
      validator: isBerichtGeldig,
      foutTekst: "Je bericht moet minimaal 10 tekens bevatten."
    }
  ];
}

function liveValidatie(veldenConfig) {
  // Live fouten wissen wanneer de gebruiker tikt en het veld herstelt
  veldenConfig.forEach((item) => {
    item.input.addEventListener("input", () => {
      if (item.validator(item.input.value)) {
        wisFout(item.input, item.foutEl);
      }
    });
  });
}



function resetStatusMelding(statusMelding) {
  statusMelding.textContent = "";
  statusMelding.className = "status-melding";
}

function toonSucces(statusMelding) {
  statusMelding.textContent = "Bedankt! Je bericht is correct verwerkt.";
  statusMelding.classList.add("succes");
}



function verwerkVerzending(event, form, veldenConfig, statusMelding) {
  event.preventDefault(); // Voorkom standaard browser-verzending voor criteria 2 

  resetStatusMelding(statusMelding);

  const eersteFouteVeld = valideerAlleVelden(veldenConfig);

  // Bij fouten: focus op het eerste ongeldige veld
  if (eersteFouteVeld) {
    eersteFouteVeld.focus();
    return;
  }

  // Bij succes: Toon bevestiging en reset het formulier
  toonSucces(statusMelding);
  
  form.reset();
}



function ContactForm() {
  const form = document.getElementById("contact-form");  //zoekt formulier op
  if (!form) return;                        //als pagina niet bestaat stopt code 

  const veldenConfig = maakVelden();


  const statusMelding = document.getElementById("contact-melding");

  liveValidatie(veldenConfig);

  // Afhandeling bij het verzenden van het formulier
  form.addEventListener("submit", (event) => {
    verwerkVerzending(event, form, veldenConfig, statusMelding);
  });
}

// Script pas uitvoeren als de DOM geladen is
if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", ContactForm);
} else {
  ContactForm();
}


















