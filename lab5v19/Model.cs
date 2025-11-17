// Models.cs
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Сутність, що представляє предмет, який потрібно спакувати.
/// </summary>
public class BoxItem
{
    public string Name { get; }
    public double Size { get; } 
    public decimal Value { get; } 

    public BoxItem(string name, double size, decimal value)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidDataException("Назва предмета не може бути порожньою.");
        if (size <= 0)
            throw new InvalidDataException("Розмір предмета має бути додатним.");
        if (value < 0)
            throw new InvalidDataException("Цінність предмета не може бути від'ємною.");

        Name = name;
        Size = size;
        Value = value;
    }

    public override string ToString() => $"[Item: {Name}, Size: {Size}, Value: {Value:C}]";
}

/// <summary>
/// Сутність, що представляє план пакування в один бокс. 
/// Реалізує Композицію: PackPlan містить List<BoxItem>.
/// </summary>
public class PackPlan
{
    public string BoxName { get; }
    public double MaxCapacity { get; }
    
    // Композиція: список предметів, що знаходяться в боксі
    private readonly List<BoxItem> _items; 
    public IReadOnlyList<BoxItem> Items => _items.AsReadOnly();

    public double CurrentFillSize => _items.Sum(item => item.Size);
    
    public double RemainingSpace => MaxCapacity - CurrentFillSize;
    
    public double FillPercentage => MaxCapacity > 0 ? (CurrentFillSize / MaxCapacity) * 100 : 0;

    public PackPlan(string boxName, double maxCapacity)
    {
        if (string.IsNullOrWhiteSpace(boxName))
            throw new InvalidDataException("Назва боксу не може бути порожньою.");
        if (maxCapacity <= 0)
            throw new InvalidDataException("Місткість боксу має бути додатним числом.");

        BoxName = boxName;
        MaxCapacity = maxCapacity;
        _items = new List<BoxItem>();
    }

    /// <summary>
    /// Додає предмет у бокс з перевіркою розміру.
    /// </summary>
    public void AddItem(BoxItem item)
    {
        if (item.Size > MaxCapacity)
        {
            // Виняток: предмет більший за максимальну місткість боксу
            throw new ItemTooLargeException(item.Name, item.Size, MaxCapacity);
        }
        
        if (RemainingSpace < item.Size)
        {
            // У цьому випадку ми не кидаємо виняток, а повідомляємо, що не поміщається
            Console.WriteLine($"\n[Попередження] Предмет '{item.Name}' не поміщається (потрібно: {item.Size:F1}, вільно: {RemainingSpace:F1}).");
            return;
        }

        _items.Add(item);
    }
}