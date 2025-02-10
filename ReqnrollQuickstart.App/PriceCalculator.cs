namespace ReqnrollQuickstart.App;

public class PriceCalculator
{
    private readonly Dictionary<string, decimal> _priceTable = new();

    public decimal CalculatePrice(Dictionary<string, int> basket)
    {
        var totalPrice = basket.Sum(item => _priceTable[item.Key] * item.Value);

        // Apply 10% discount if total exceeds $200
        return totalPrice > 200 ? totalPrice * 0.9m : totalPrice;
    }

    public void SetCalculatedPrice(string product, decimal price)
    {
        _priceTable[product] = price;
    }
}