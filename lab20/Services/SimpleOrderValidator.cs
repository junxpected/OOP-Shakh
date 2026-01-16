using System;

public class SimpleOrderValidator : IOrderValidator
{
    public bool IsValid(Order order)
    {
        return order.TotalAmount > 0;
    }
}
