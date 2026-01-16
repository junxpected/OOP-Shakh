using System;
using System.Collections.Generic;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<int, Order> _orders = new();

    public void Save(Order order)
    {
        _orders[order.Id] = order;
        Console.WriteLine($"Order {order.Id} saved to memory");
    }

    public Order GetById(int id)
    {
        return _orders.ContainsKey(id) ? _orders[id] : null;
    }
}
