namespace lab21
{
    public static class TaxiFactory
    {
        public static ITaxiStrategy CreateTaxi(string type)
        {
            switch (type.ToLower())
            {
                case "economy": return new EconomyTaxiStrategy();
                case "standard": return new StandardTaxiStrategy();
                case "premium": return new PremiumTaxiStrategy();
                case "night": return new NightTaxiStrategy();
                default: return null;
            }
        }
    }
}