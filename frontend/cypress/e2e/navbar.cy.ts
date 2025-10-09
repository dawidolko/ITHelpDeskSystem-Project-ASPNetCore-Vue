// Navigation tests for BattleStation showcase

describe("Navigation on small screens", () => {
  beforeEach(() => {
    cy.viewport("iphone-xr");
  });

  it("toggles and excludes path on home", () => {
    cy.visit("/");
    cy.get('[data-test="hamburger"]').click();
    cy.get('[data-test="nav-mobile"]').contains("Close").should("be.visible");
    cy.get('[data-test="nav-mobile"]')
      .contains("Home")
      .should("not.be.visible");
    cy.get('[data-test="nav-mobile"]').contains("Setup").should("be.visible");
    cy.get('[data-test="nav-mobile"]')
      .contains("Components")
      .should("be.visible");
    cy.get('[data-test="nav-mobile"]').contains("Specs").should("be.visible");
    cy.get('[data-test="close-hamburger"]').click();
    cy.get('[data-test="nav-mobile"]').should("not.exist");
  });

  it("toggles and excludes path on setup", () => {
    cy.visit("/setup");
    cy.get('[data-test="hamburger"]').click();
    cy.get('[data-test="nav-mobile"]').contains("Close").should("be.visible");
    cy.get('[data-test="nav-mobile"]').contains("Home").should("be.visible");
    cy.get('[data-test="nav-mobile"]')
      .contains("Setup")
      .should("not.be.visible");
    cy.get('[data-test="nav-mobile"]')
      .contains("Components")
      .should("be.visible");
    cy.get('[data-test="nav-mobile"]').contains("Specs").should("be.visible");
    cy.get('[data-test="close-hamburger"]').click();
    cy.get('[data-test="nav-mobile"]').should("not.exist");
  });

  it("toggles and excludes path on components", () => {
    cy.visit("/components");
    cy.get('[data-test="hamburger"]').click();
    cy.get('[data-test="nav-mobile"]').contains("Close").should("be.visible");
    cy.get('[data-test="nav-mobile"]').contains("Home").should("be.visible");
    cy.get('[data-test="nav-mobile"]').contains("Setup").should("be.visible");
    cy.get('[data-test="nav-mobile"]')
      .contains("Components")
      .should("not.be.visible");
    cy.get('[data-test="nav-mobile"]').contains("Specs").should("be.visible");
    cy.get('[data-test="close-hamburger"]').click();
    cy.get('[data-test="nav-mobile"]').should("not.exist");
  });

  it("toggles and excludes path on specs", () => {
    cy.visit("/specs");
    cy.get('[data-test="hamburger"]').click();
    cy.get('[data-test="nav-mobile"]').contains("Close").should("be.visible");
    cy.get('[data-test="nav-mobile"]').contains("Home").should("be.visible");
    cy.get('[data-test="nav-mobile"]').contains("Setup").should("be.visible");
    cy.get('[data-test="nav-mobile"]')
      .contains("Components")
      .should("be.visible");
    cy.get('[data-test="nav-mobile"]')
      .contains("Specs")
      .should("not.be.visible");
    cy.get('[data-test="close-hamburger"]').click();
    cy.get('[data-test="nav-mobile"]').should("not.exist");
  });
});

describe("Desktop Navigation", () => {
  beforeEach(() => {
    cy.viewport("macbook-11");
  });

  it("displays all navigation links", () => {
    cy.visit("/");
    cy.get('[data-test="nav-home"]').should("be.visible");
    cy.get('[data-test="nav-setup"]').should("be.visible");
    cy.get('[data-test="nav-components"]').should("be.visible");
    cy.get('[data-test="nav-specs"]').should("be.visible");
  });

  it("navigates to setup page", () => {
    cy.visit("/");
    cy.get('[data-test="nav-setup"]').click();
    cy.url().should("include", "/setup");
  });

  it("navigates to components page", () => {
    cy.visit("/");
    cy.get('[data-test="nav-components"]').click();
    cy.url().should("include", "/components");
  });

  it("navigates to specs page", () => {
    cy.visit("/");
    cy.get('[data-test="nav-specs"]').click();
    cy.url().should("include", "/specs");
  });

  it("logo navigates back to home", () => {
    cy.visit("/setup");
    cy.get('[data-test="nav-logo"]').click();
    cy.url().should("eq", Cypress.config().baseUrl + "/");
  });
});

describe("Component interactions", () => {
  beforeEach(() => {
    cy.viewport("macbook-11");
  });

  it("shows PC component details", () => {
    cy.visit("/components");
    cy.get('[data-test="component-pc"]').click();
    cy.get('[data-test="component-details"]')
      .should("be.visible")
      .and("contain", "Ryzen 7 5800X")
      .and("contain", "RTX 3060Ti");
  });

  it("shows laptop specifications", () => {
    cy.visit("/components");
    cy.get('[data-test="component-laptop"]').click();
    cy.get('[data-test="component-details"]')
      .should("be.visible")
      .and("contain", "MacBook Pro M3")
      .and("contain", "16 inch");
  });

  it("displays monitor setup details", () => {
    cy.visit("/components");
    cy.get('[data-test="component-monitors"]').click();
    cy.get('[data-test="spec-modal"]')
      .should("be.visible")
      .and("contain", "IIYAMA G-MASTER")
      .and("contain", "HP 25x 144Hz");
    cy.get('[data-test="spec-close"]').click();
    cy.get('[data-test="spec-modal"]').should("not.be.visible");
  });
});
