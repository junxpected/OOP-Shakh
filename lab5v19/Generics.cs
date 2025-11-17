using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Узагальнена черга з пріоритетом на основі SortedSet.
/// </summary>
/// <typeparam name="T">Тип елементів, які повинні реалізувати IComparable<T>.</typeparam>
public class PriorityQueue<T> where T : IComparable<T>
{
    // SortedSet зберігає елементи у відсортованому порядку
    private readonly SortedSet<T> _data = new SortedSet<T>();

    /// <summary>
    /// Додає елемент у чергу.
    /// </summary>
    public void Enqueue(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        _data.Add(item);
    }

    /// <summary>
    /// Видаляє та повертає елемент з найвищим пріоритетом (SortedSet.Min).
    /// </summary>
    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Черга з пріоритетом порожня.");

        // Виправлення: використовуємо '!' для ігнорування попередження NRT.
        T highestPriorityItem = _data.Min!; // CS8600 виправлено тут
        
        // Виправлення: передаємо non-nullable значення (з '!')
        _data.Remove(highestPriorityItem); // CS8604 виправлено тут
        return highestPriorityItem;
    }

    /// <summary>
    /// Перевіряє, чи порожня черга.
    /// </summary>
    public bool IsEmpty => _data.Count == 0;
}

// Допоміжний клас-обгортка для BoxItem, щоб реалізувати IComparable<T> 
// і відсортувати за 'Value' (для жадібного алгоритму)
public class PrioritizedBoxItem : IComparable<PrioritizedBoxItem>
{
    public BoxItem Item { get; }

    public PrioritizedBoxItem(BoxItem item) => Item = item;

    /// <summary>
    /// Порівнює елементи для PriorityQueue. 
    /// Сортуємо за спаданням Value (більше Value -> вищий пріоритет).
    /// </summary>
    public int CompareTo(PrioritizedBoxItem? other)
    {
        if (other == null) return 1;

        // Інвертуємо порівняння: other.Value.CompareTo(Item.Value)
        // забезпечує, що більша цінність буде ближче до Min (найвищий пріоритет)
        int valueComparison = other.Item.Value.CompareTo(Item.Value); 
        
        if (valueComparison != 0)
            return valueComparison;
        
        // Додаткове правило для унікальності в SortedSet
        return Item.Name.CompareTo(other.Item.Name);
    }
    
    public override string ToString() => Item.ToString();
}