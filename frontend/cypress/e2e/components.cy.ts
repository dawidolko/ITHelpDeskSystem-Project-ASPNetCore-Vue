// Component showcase tests for BattleStation

describe("Desktop: Component showcase", () => {
  beforeEach(() => {
    cy.viewport("macbook-15");
  });

  it("displays all main components", () => {
    cy.visit("/components");
    cy.get('[data-test="component-pc"]').should("be.visible");
    cy.get('[data-test="component-laptop"]').should("be.visible");
    cy.get('[data-test="component-monitors"]').should("be.visible");
    cy.get('[data-test="component-peripherals"]').should("be.visible");
    cy.get('[data-test="component-audio"]').should("be.visible");
    cy.get('[data-test="component-furniture"]').should("be.visible");
  });

  it("shows PC specifications when clicked", () => {
    cy.visit("/components");
    cy.get('[data-test="component-pc"]').click();
    cy.get('[data-test="component-details"]')
      .should("be.visible")
      .and("contain", "Phanteks NV7")
      .and("contain", "Ryzen 7 5800X")
      .and("contain", "RTX 3060Ti")
      .and("contain", "64GB DDR4");
  });

  it("shows detailed PC component breakdown", () => {
    cy.visit("/components");
    cy.get('[data-test="component-pc"]').click();
    cy.get('[data-test="pc-specs-modal"]').should("be.visible");
    cy.get('[data-test="cpu-specs"]').should("contain", "AMD Ryzen 7 5800X");
    cy.get('[data-test="gpu-specs"]').should("contain", "NVIDIA RTX 3060Ti");
    cy.get('[data-test="ram-specs"]').should(
      "contain",
      "64GB DDR4 3200MHz Corsair"
    );
    cy.get('[data-test="storage-specs"]')
      .should("contain", "Lexar 890 Pro M.2")
      .and("contain", "SSD 512GB");
    cy.get('[data-test="motherboard-specs"]').should(
      "contain",
      "ASUS Strix ROG B550 White"
    );
    cy.get('[data-test="cooling-specs"]').should("contain", "AORUS 360 AIO");
    cy.get('[data-test="psu-specs"]').should("contain", "SilentiumPC 700W");
  });

  it("shows laptop details", () => {
    cy.visit("/components");
    cy.get('[data-test="component-laptop"]').click();
    cy.get('[data-test="component-details"]')
      .should("be.visible")
      .and("contain", "MacBook Pro M3")
      .and("contain", "16 inch")
      .and("contain", "512GB")
      .and("contain", "18GB RAM");
  });

  it("shows monitor setup configuration", () => {
    cy.visit("/components");
    cy.get('[data-test="component-monitors"]').click();
    cy.get('[data-test="monitor-specs-modal"]').should("be.visible");
    cy.get('[data-test="monitor-primary"]').should(
      "contain",
      "IIYAMA G-MASTER GB2530HSU"
    );
    cy.get('[data-test="monitor-secondary"]').should("contain", "HP 25x 144Hz");
    cy.get('[data-test="monitor-count"]').should("contain", "3 monitors total");
  });

  it("shows peripherals breakdown", () => {
    cy.visit("/components");
    cy.get('[data-test="component-peripherals"]').click();
    cy.get('[data-test="peripheral-specs-modal"]').should("be.visible");
    cy.get('[data-test="keyboard-specs"]').should(
      "contain",
      "Razer BlackWidow Pro V4"
    );
    cy.get('[data-test="mouse-specs"]').should("contain", "Razer Viper V2 Pro");
    cy.get('[data-test="laptop-mouse-specs"]').should(
      "contain",
      "Logitech MX3"
    );
    cy.get('[data-test="headset-specs"]').should(
      "contain",
      "Razer BlackShark V2 Pro"
    );
    cy.get('[data-test="mousepad-specs"]').should(
      "contain",
      "Razer Mouse Bungee"
    );
  });

  it("shows audio equipment details", () => {
    cy.visit("/components");
    cy.get('[data-test="component-audio"]').click();
    cy.get('[data-test="audio-specs-modal"]').should("be.visible");
    cy.get('[data-test="speakers-specs"]').should(
      "contain",
      "EDIFIER R1855DB 2.0"
    );
    cy.get('[data-test="microphone-specs"]').should("contain", "Novox NC1");
    cy.get('[data-test="headset-specs"]').should(
      "contain",
      "Razer BlackShark V2 Pro"
    );
  });

  it("shows furniture and desk setup", () => {
    cy.visit("/components");
    cy.get('[data-test="component-furniture"]').click();
    cy.get('[data-test="furniture-specs-modal"]').should("be.visible");
    cy.get('[data-test="desk-specs"]')
      .should("contain", "Autorskie biurko")
      .and("contain", "Dębowy blat")
      .and("contain", "60cm - 125cm");
    cy.get('[data-test="chair-specs"]').should("contain", "Fotel ergonomiczny");
  });
});

describe("Mobile: Component showcase", () => {
  beforeEach(() => {
    cy.viewport("iphone-7");
  });

  it("displays components in mobile layout", () => {
    cy.visit("/components");
    cy.get('[data-test="component-grid"]').should("be.visible");
    cy.get('[data-test="component-pc"]').should("be.visible");
    cy.get('[data-test="component-laptop"]').should("be.visible");
  });

  it("opens PC specs in mobile modal", () => {
    cy.visit("/components");
    cy.get('[data-test="component-pc"]').click();
    cy.get('[data-test="mobile-component-modal"]')
      .should("be.visible")
      .and("contain", "Ryzen 7 5800X");
    cy.get('[data-test="mobile-modal-close"]').click();
    cy.get('[data-test="mobile-component-modal"]').should("not.be.visible");
  });

  it("navigates through component carousel", () => {
    cy.visit("/components");
    cy.get('[data-test="component-carousel"]').should("be.visible");
    cy.get('[data-test="carousel-next"]').click();
    cy.get('[data-test="carousel-indicator"]').should("contain", "2");
    cy.get('[data-test="carousel-prev"]').click();
    cy.get('[data-test="carousel-indicator"]').should("contain", "1");
  });
});

describe("Setup tour functionality", () => {
  beforeEach(() => {
    cy.viewport("macbook-11");
  });

  it("starts virtual room tour", () => {
    cy.visit("/setup");
    cy.get('[data-test="start-tour-button"]').click();
    cy.get('[data-test="tour-modal"]').should("be.visible");
    cy.get('[data-test="tour-step-1"]').should("be.visible");
  });

  it("navigates through desk tour steps", () => {
    cy.visit("/setup");
    cy.get('[data-test="start-tour-button"]').click();
    cy.get('[data-test="tour-next"]').click();
    cy.get('[data-test="tour-step-desk"]')
      .should("be.visible")
      .and("contain", "Autorskie biurko");
    cy.get('[data-test="tour-next"]').click();
    cy.get('[data-test="tour-step-monitors"]')
      .should("be.visible")
      .and("contain", "IIYAMA");
  });

  it("closes tour and returns to setup view", () => {
    cy.visit("/setup");
    cy.get('[data-test="start-tour-button"]').click();
    cy.get('[data-test="tour-close"]').click();
    cy.get('[data-test="tour-modal"]').should("not.be.visible");
    cy.url().should("include", "/setup");
  });
});
