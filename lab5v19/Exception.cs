// Exceptions.cs
using System;

/// <summary>
/// Виняток, що виникає, коли предмет більший за доступний простір у боксі.
/// </summary>
public class ItemTooLargeException : Exception
{
    public double ItemSize { get; }
    public double BoxCapacity { get; }
    
    public ItemTooLargeException(string itemName, double itemSize, double boxCapacity) 
        : base($"Предмет '{itemName}' (розмір: {itemSize}) занадто великий для боксу (місткість: {boxCapacity}).")
    {
        ItemSize = itemSize;
        BoxCapacity = boxCapacity;
    }
    
    public ItemTooLargeException(string message, Exception innerException) 
        : base(message, innerException) {}
}

/// <summary>
/// Базовий виняток для невалідних вхідних даних.
/// </summary>
public class InvalidDataException : Exception
{
    public InvalidDataException(string message) : base(message) {}
}