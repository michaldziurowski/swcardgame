describe('SW Card Game', function() {
    it('Goes through happy path - sees two cards displayed', function() {
        cy.visit('http://localhost:5500');
        cy.contains('Starships').click();
        cy.contains('speed').click();
        cy.get('p:contains("Score")').should('have.length', 2);
        cy.get('.MuiCardHeader-root').should('have.length', 2);
        cy.get('.MuiCardContent-root').should('have.length', 2);
    });

    it('Goes through happy path - can trigger new deal', function() {
        cy.visit('http://localhost:5500');
        cy.contains('Starships').click();
        cy.contains('speed').click();
        cy.contains('Next').click();
        cy.get('h4').should($h => {
            expect($h).to.have.length(1);
            expect($h.eq(0)).to.contain('Select property');
        });
    });

    it('Goes through happy path - can reset game', function() {
        cy.visit('http://localhost:5500');
        cy.contains('Starships').click();
        cy.contains('speed').click();
        cy.contains('Reset').click();
        cy.get('h4').should($h => {
            expect($h).to.have.length(1);
            expect($h.eq(0)).to.contain('Select card definition');
        });
    });
});
