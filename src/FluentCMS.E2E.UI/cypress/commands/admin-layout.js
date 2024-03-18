Cypress.Commands.add("checkAdminSidebarNavigations", () => {

    cy.get('#adminSidebarAppsLink').click()
    cy.get('.f-page-header-title').should('have.text', 'Apps List')

    cy.get('#adminSidebarUsersLink').click()
    cy.get('.f-page-header-title').should('have.text', 'Users List')

    cy.get('#adminSidebarContentTypeLink').click()
    cy.get('.f-page-header-title').should('have.text', 'Content Types List')

    cy.get('#adminSidebarDocsLink a').should('have.attr', 'target', '_blank')
    cy.get('#adminSidebarDocsLink a').invoke('removeAttr', 'target').scrollIntoView().click()
    cy.url().should('include', '/doc/index.html')

    cy.go('back')
})

Cypress.Commands.add('checkAdminSidebarThemeToggle', () => {
    cy.get('body').should('not.have.class', 'dark')

    cy.get('#adminThemeButton').click().shortWait()
    cy.get('body').should('have.class', 'dark')

    cy.get('#adminThemeButton').click().shortWait()
    cy.get('body').should('not.have.class', 'dark')
})

Cypress.Commands.add('checkAdminHomeLinks', () => {
    cy.get('#adminSidebarLogoLink').click()
    cy.url().should('eq', Cypress.config().baseUrl + '/')

    cy.get('#adminSidebarLogoLink').click()
    cy.url().should('eq', Cypress.config().baseUrl + '/')
})

Cypress.Commands.add('adminSidebarLogoShouldAvailable', () => {
    cy.elementShouldAvailable('#adminSidebarLogoLink')
})

Cypress.Commands.add('adminSidebarShouldAvailable', () => {
    cy.elementShouldAvailable('#adminSidebar')

    cy.elementShouldAvailable('#adminSidebarHomeLink a')

    cy.elementShouldAvailable('#adminSidebarContentManagementLink a')

    cy.elementShouldAvailable('#adminSidebarAppsLink a')
    cy.elementShouldAvailable('#adminSidebarContentTypeLink a')
    cy.elementShouldAvailable('#adminSidebarUsersLink a')

    cy.elementShouldAvailable('#adminSidebarDocsLink a')
})

Cypress.Commands.add('checkAdminNavbar', () => {
    cy.viewport(600, 800)
    cy.mediumWait()
    cy.elementShouldAvailable('#adminNavbar')

    cy.elementShouldAvailable('#adminNavbarLogoLink')

    cy.elementShouldAvailable('#adminNavbarToggleButton')

    cy.get('#adminNavbarToggleButton').click()
    cy.elementShouldAvailable('#adminSidebar')

    cy.viewport(1366, 768)
})
