namespace ReqnrollQuickstart.App
{
    public record Currency(string Symbol, decimal Value)
    {
        public static Currency FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Invalid currency format.");

            value = value.Replace("$", ""); // Remove the dollar symbol if present

            if (decimal.TryParse(value, out var amount))
            {
                return new Currency("$", amount); // Assume $ as the currency symbol here
            }

            throw new ArgumentException("Invalid currency format.");
        }

        public override string ToString()
        {
            return $"{Symbol}{Value:N2}";
        }
    }
}