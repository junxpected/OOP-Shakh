using System;

public class OrderService
{
    private readonly IOrderValidator _validator;
    private readonly IOrderRepository _repository;
    private readonly IEmailService _emailService;

    public OrderService(IOrderValidator validator, IOrderRepository repository, IEmailService emailService)
    {
        _validator = validator;
        _repository = repository;
        _emailService = emailService;
    }

    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Processing order {order.Id}");

        if (!_validator.IsValid(order))
        {
            Console.WriteLine("Order validation failed");
            order.Status = OrderStatus.Cancelled;
            return;
        }

        order.Status = OrderStatus.Processed;
        _repository.Save(order);
        _emailService.SendOrderConfirmation(order);

        Console.WriteLine("Order processed successfully");
    }
}
