using System;
public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Лабораторна робота №4: Файлове сховище ===");

        // 1. Створення конкретних реалізацій (поліморфізм: посилання IFileOperation)
        IFileOperation fileSaver = new FileSave();
        IFileOperation fileLoader = new FileLoad();

        // 2. Використання Агрегації: створення сховища, передаючи залежності
        FileStorage storage = new FileStorage(fileSaver, fileLoader);

        Console.WriteLine("\n--- Виконання операцій ---");

        // Імітація роботи:
        storage.SaveFile("document.pdf");
        storage.LoadFile("image.png");
        storage.SaveFile("report.xlsx");
        storage.LoadFile("document.pdf");
        storage.SaveFile("data.json");
        storage.LoadFile("config.ini");
        storage.SaveFile(""); // Приклад невдалої операції (не збільшить лічильник)

        Console.WriteLine("\n--- Результати обчислень ---");

        // Обчислення: вивід даних зі статичних лічильників
        int savedCount = FileSave.SaveCount;
        int loadedCount = FileLoad.LoadCount;
        int totalCount = FileOperationBase.TotalOperationsCount;

        Console.WriteLine($"1. Кількість успішно ЗБЕРЕЖЕНИХ файлів: {savedCount}");
        Console.WriteLine($"2. Кількість успішно ЗАВАНТАЖЕНИХ файлів: {loadedCount}");
        Console.WriteLine($"3. Загальна кількість успішно виконаних операцій: {totalCount}");
    }
}