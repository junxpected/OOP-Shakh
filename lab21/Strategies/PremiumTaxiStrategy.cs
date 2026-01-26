namespace lab21
{
    public class PremiumTaxiStrategy : ITaxiStrategy
    {
        public decimal CalculatePrice(decimal distance, int waitTime)
        {
            return (distance * 20.0m) + (waitTime * 5.0m) + 50.0m;
        }
    }
}