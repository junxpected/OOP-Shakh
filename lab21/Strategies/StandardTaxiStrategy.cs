namespace lab21
{
    public class StandardTaxiStrategy : ITaxiStrategy
    {
        public decimal CalculatePrice(decimal distance, int waitTime)
        {
            return (distance * 10.0m) + (waitTime * 3.0m);
        }
    }
}