namespace lab21
{
    public class EconomyTaxiStrategy : ITaxiStrategy
    {
        public decimal CalculatePrice(decimal distance, int waitTime)
        {
            return (distance * 7.0m) + (waitTime * 2.0m);
        }
    }
}