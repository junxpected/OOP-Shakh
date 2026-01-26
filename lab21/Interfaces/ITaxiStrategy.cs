namespace lab21
{
    public interface ITaxiStrategy
    {
        decimal CalculatePrice(decimal distance, int waitTime);
    }
}