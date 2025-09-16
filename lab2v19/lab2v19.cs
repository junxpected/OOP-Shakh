using System;

public class Queue<T>
{
    private T[] items;
    private int front;
    private int rear;
    private int capacity;

    public Queue(int size)
    {
        capacity = size;
        items = new T[size];
        front = 0;
        rear = -1;
    }

    public int Count { get { return rear - front + 1; } }

    public int Capacity { get { return capacity; } }

    public void Enqueue(T item)
    {
        if (rear + 1 >= capacity)
            throw new InvalidOperationException("Queue is full");
        items[++rear] = item;
    }

    public T Dequeue()
    {
        if (front > rear)
            throw new InvalidOperationException("Queue is empty");
        T item = items[front];
        items[front++] = default(T); // This is where CS8601 might trigger for reference types
        return item;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            return items[front + index];
        }
        set
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            items[front + index] = value;
        }
    }

    // Unary operator - to remove the first element
    public static Queue<T> operator -(Queue<T> queue)
    {
        if (queue.front > queue.rear)
            throw new InvalidOperationException("Queue is empty");
        queue.Dequeue();
        return queue;
    }

    // Binary + operator to add an element
    public static Queue<T> operator +(Queue<T> queue, T item)
    {
        queue.Enqueue(item);
        return queue;
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Queue<int> queue = new Queue<int>(5);

            // Демонстрація додавання елементів через оператор +
            Console.WriteLine("Додаємо елементи за допомогою оператора +:");
            queue = queue + 10;
            queue = queue + 20;
            queue = queue + 30;
            Console.WriteLine($"Кількість елементів: {queue.Count}");

            // Демонстрація доступу через індексатор
            Console.WriteLine("\nЕлементи черги через індексатор:");
            for (int i = 0; i < queue.Count; i++)
            {
                Console.WriteLine($"queue[{i}] = {queue[i]}");
            }

            // Зміна елемента через індексатор
            Console.WriteLine("\nЗмінюємо елемент за індексом 1:");
            queue[1] = 50;
            Console.WriteLine($"queue[1] = {queue[1]}");

            // Демонстрація видалення першого елемента через оператор -
            Console.WriteLine("\nВидаляємо перший елемент за допомогою оператора -:");
            queue = -queue; // Changed to unary operator
            Console.WriteLine($"Кількість елементів після видалення: {queue.Count}");
            Console.WriteLine("Залишені елементи:");
            for (int i = 0; i < queue.Count; i++)
            {
                Console.WriteLine($"queue[{i}] = {queue[i]}");
            }

            // Демонстрація властивостей
            Console.WriteLine($"\nПоточна кількість елементів: {queue.Count}");
            Console.WriteLine($"Максимальна місткість черги: {queue.Capacity}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nПомилка: {ex.Message}");
        }
    }
}