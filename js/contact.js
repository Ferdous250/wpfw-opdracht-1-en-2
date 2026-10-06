// ==========================================
// 1. PURE VALIDATIEFUNCTIES (Logica)
// ==========================================

// Controleert of naam niet leeg is (spaties negeren via .trim())
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
// 2. DOM MANIPULATIE FUNCTIES (Toegankelijkheid & UI)
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

// ==========================================
// 3. HOOFDFUNCTIE & EVENT LISTENERS
// ==========================================

function ContactForm() {
  const form = document.getElementById("contact-form");
  if (!form) return;

  // Configuratie-array van de te valideren velden
  const veldenConfig = [
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

  const statusMelding = document.getElementById("contact-melding");

  // Live fouten wissen wanneer de gebruiker tikt en het veld herstelt
  veldenConfig.forEach((item) => {
    item.input.addEventListener("input", () => {
      if (item.validator(item.input.value)) {
        wisFout(item.input, item.foutEl);
      }
    });
  });

  // Afhandeling bij het verzenden van het formulier
  form.addEventListener("submit", (event) => {
    event.preventDefault(); // Voorkom standaard browser-verzending

    // Reset statusmelding
    statusMelding.textContent = "";
    statusMelding.className = "status-melding";

    let isFormulierGeldig = true;
    let eersteFouteVeld = null;

    // Door alle velden lopen en valideren
    veldenConfig.forEach((item) => {
      const geldigeInvoer = valideerVeld(item);
      if (!geldigeInvoer) {
        isFormulierGeldig = false;
        if (!eersteFouteVeld) {
          eersteFouteVeld = item.input;
        }
      }
    });

    // Bij fouten: focus op het eerste ongeldige veld
    if (!isFormulierGeldig) {
      if (eersteFouteVeld) eersteFouteVeld.focus();
      return;
    }

    // Bij succes: Toon bevestiging en reset het formulier
    statusMelding.textContent = "Bedankt! Je bericht is correct verwerkt.";
    statusMelding.classList.add("succes");
    
    form.reset();
  });
}

// Script pas uitvoeren als de DOM geladen is
if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", initContactForm);
} else {
  ContactForm();
}