namespace lab21
{
    public class NightTaxiStrategy : ITaxiStrategy
    {
        public decimal CalculatePrice(decimal distance, int waitTime)
        {
            return ((distance * 10.0m) + (waitTime * 3.0m)) * 2.0m;
        }
    }
}