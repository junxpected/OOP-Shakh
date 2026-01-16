using System;

public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine("Processing order...");

        if (order.TotalAmount <= 0)
        {
            Console.WriteLine("Order is invalid");
            return;
        }

        Console.WriteLine("Saving order to database...");
        Console.WriteLine("Sending email to customer...");

        order.Status = OrderStatus.Processed;
        Console.WriteLine("Order processed successfully");
    }
}
