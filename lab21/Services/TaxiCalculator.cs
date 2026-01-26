namespace lab21
{
    public class TaxiCalculator
    {
        public decimal GetTotal(decimal dist, int time, ITaxiStrategy strategy)
        {
            if (strategy == null) return 0;
            return strategy.CalculatePrice(dist, time);
        }
    }
}