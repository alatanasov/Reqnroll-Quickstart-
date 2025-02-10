@pricing
@regression
Feature: Price calculation

    Background:
        Given the following products and prices are available
          | product         | price |
          | Electric guitar | 180.0 |
          | Guitar pick     | 1.5   |
        And the client started shopping

    Rule: Single product calculation

        @single-product
        Scenario: Client has a single item in the basket
            And the client added 1 pcs of "Electric guitar" to the basket
            When the basket is prepared
            Then the basket price should be $180.0

    Rule: Multiple products calculation

        @multiple-products
        Scenario: Client has multiple items in the basket
            When the client added
              | product         | quantity |
              | Electric guitar | 1        |
              | Guitar pick     | 10       |
            And the basket is prepared
            Then the basket price should be $195.0

    Rule: 10% discount on orders over $200

        @discount-products
        Scenario: Client has discount for items in basket
            When the client added
              | product         | quantity |
              | Electric guitar | 2        |
              | Guitar pick     | 10       |
            And the basket is prepared
            Then the basket price should be $337.50

    Rule: Empty basket calculation

        @empty-basket
        Scenario: Client has no items in the basket
            When the basket is prepared
            Then the basket should be empty, and the price should be $0