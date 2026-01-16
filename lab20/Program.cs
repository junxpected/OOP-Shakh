using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== BEFORE SRP (bad example) ===");
        var oldProcessor = new OrderProcessor();
        var badOrder = new Order(1, "Ivan", -100);
        oldProcessor.ProcessOrder(badOrder);

        Console.WriteLine("\n=== AFTER SRP (refactored) ===");

        IOrderValidator validator = new SimpleOrderValidator();
        IOrderRepository repository = new InMemoryOrderRepository();
        IEmailService emailService = new ConsoleEmailService();

        var orderService = new OrderService(validator, repository, emailService);

        var goodOrder = new Order(2, "Anna", 2500);
        var invalidOrder = new Order(3, "Petro", -50);

        Console.WriteLine("\nValid order:");
        orderService.ProcessOrder(goodOrder);

        Console.WriteLine("\nInvalid order:");
        orderService.ProcessOrder(invalidOrder);
    }
}
