namespace ReqnrollQuickstart.Specs
{
    [Binding]
    public class PriceCalculationStepDefinitions
    {
        private readonly PriceCalculator _priceCalculator = new();
        private readonly Dictionary<string, int> _basket = new();
        public required Exception ThrownException = new("Product is out of stock");
        private decimal _calculatedPrice;

        // Background steps
        [Given(@"the following products and prices are available")]
        public void GivenTheFollowingProductsAndPricesAreAvailable(Table priceTable)
        {
            var priceList = priceTable.CreateSet<(string product, decimal price)>();
            foreach (var item in priceList)
            {
                _priceCalculator.SetCalculatedPrice(item.product, item.price);
            }
        }

        [Given(@"the client started shopping")]
        public void GivenTheClientStartedShopping()
        {
            _basket.Clear();
            _calculatedPrice = 0.0m;
        }

        // Single product calculation
        [Given(@"the client added (.*) pcs of ""(.*)"" to the basket")]
        public void GivenTheClientAddedPcsOfToTheBasket(int quantity, string product)
        {
            _basket.Add(product, quantity);
        }

        // Multiple products calculation & discount
        [When, Then(@"the client added")]
        public void WhenTheClientAdded(Table productTable)
        {
            var items = productTable.CreateSet<(string product, int quantity)>();
            foreach (var item in items)
            {
                _basket.Add(item.product, item.quantity);
            }
        }

        // Common step for all scenarios
        [When(@"the basket is prepared")]
        public void WhenTheBasketIsPrepared()
        {
            _calculatedPrice = _priceCalculator.CalculatePrice(_basket);
        }

        [Then(@"the basket price should be (.*)")]
        public void ThenTheBasketPriceShouldBe(string expectedPrice)
        {
            var expectedCurrency = Currency.FromString(expectedPrice);
            _calculatedPrice.Should().Be(expectedCurrency.Value);
        }


        [Then(@"the basket should be empty, and the price should be \$(.*)")]
        public void ThenTheBasketShouldBeEmptyAndThePriceShouldBe(decimal expectedPrice)
        {
            _calculatedPrice.Should().Be(expectedPrice);
        }
    }
}