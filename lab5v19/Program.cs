using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console; // Опційно: dotnet add package Spectre.Console

// Припускаємо, що всі класи знаходяться в одному проекті
public class Program
{
    public static void Main(string[] args)
    {
        // 1. Налаштування та ініціалізація
        AnsiConsole.MarkupLine("[bold blue]Лабораторна робота №5: Варіант 19 - Пакування на складі[/]");
        AnsiConsole.MarkupLine("Generics (PriorityQueue), Композиція (PackPlan/BoxItem), Винятки.\n");

        var itemsToPack = new List<BoxItem>
        {
            new BoxItem("Laptop", 5.0, 1500m),
            new BoxItem("Monitor", 7.0, 500m),
            new BoxItem("Camera", 2.0, 300m),
            new BoxItem("Keyboard", 1.5, 100m),
            new BoxItem("Books Set", 3.0, 50m),
            new BoxItem("Mouse", 0.5, 20m),
            new BoxItem("Flash Drive", 0.1, 10m)
        };
        
        // Бокс має місткість 12.0
        var plan = new PackPlan("Shipping Box A-1", 12.0); 
        AnsiConsole.MarkupLine($"[yellow]Створено план для боксу '{plan.BoxName}' (Місткість: {plan.MaxCapacity}).[/]");

        // 2. Демонстрація обробки винятків (try-catch)
        AnsiConsole.MarkupLine("\n[bold]*** Демонстрація обробки винятків ***[/]");
        
        // 2.1. Обробка InvalidDataException (негативний розмір)
        try
        {
            var invalidItem = new BoxItem("Broken Item", -2.0, 10m); 
        }
        catch (InvalidDataException ex)
        {
            AnsiConsole.MarkupLine($"[red]Помилка валідації:[/]{ex.Message}");
        }
        
        // 2.2. Обробка ItemTooLargeException (предмет більший за бокс)
        var giantItem = new BoxItem("Giant Server", 15.0, 5000m);
        try
        {
            plan.AddItem(giantItem);
        }
        catch (ItemTooLargeException ex)
        {
            AnsiConsole.MarkupLine($"[red]Помилка пакування (власний виняток):[/] {ex.Message}");
        }

        // 3. Реалізація "Жадібного" алгоритму пакування (Generics + LINQ)
        AnsiConsole.MarkupLine("\n[bold]*** Запуск Жадібного алгоритму (пакування найцінніших) ***[/]");

        // Заповнюємо PriorityQueue<T>
        var priorityQueue = new PriorityQueue<PrioritizedBoxItem>();
        foreach (var item in itemsToPack)
        {
            priorityQueue.Enqueue(new PrioritizedBoxItem(item));
        }

        while (!priorityQueue.IsEmpty)
        {
            // Dequeue повертає предмет з найвищим пріоритетом (найбільшою цінністю)
            var prioritizedItem = priorityQueue.Dequeue().Item; 

            try
            {
                plan.AddItem(prioritizedItem);
                if (plan.Items.Contains(prioritizedItem)) // Перевірка, чи предмет справді додався
                {
                     AnsiConsole.MarkupLine($"[green] Додано:[/]{prioritizedItem.Name} ({prioritizedItem.Size:F1})");
                }
            }
            catch (ItemTooLargeException ex)
            {
                // Цей catch ловитиме виняток, якщо item.Size > MaxCapacity, 
                // хоча це було перевірено раніше. Додано для повноти демонстрації.
                AnsiConsole.MarkupLine($"[red]Критична помилка:[/]{ex.Message}");
            }
        }

        // 4. Обчислення та виведення результатів (LINQ)
        AnsiConsole.MarkupLine("\n[bold]*** Результати пакування та обчислення ***[/]");

        // Обчислення
        double remainingSpace = plan.RemainingSpace;
        double fillPercentage = plan.FillPercentage;
        decimal totalValue = plan.Items.Sum(item => item.Value);
        decimal averageValue = plan.Items.Any() ? plan.Items.Average(item => item.Value) : 0m;
        
        AnsiConsole.MarkupLine($"[cyan] Загальний розмір заповнення:[/]{plan.CurrentFillSize:F1} / {plan.MaxCapacity:F1}");
        AnsiConsole.MarkupLine($"[cyan] Залишковий простір:[/]{remainingSpace:F1}");
        AnsiConsole.MarkupLine($"[cyan] Відсоток заповнюваності:[/]{fillPercentage:F1}%");
        AnsiConsole.MarkupLine($"[cyan] Сумарна цінність вантажу:[/]{totalValue:C}");
        AnsiConsole.MarkupLine($"[cyan] Середня цінність за одиницю:[/]{averageValue:C}");
        // Виведення списку запакованих предметів
        var table = new Table().Title($"[bold white]Запаковані предмети в {plan.BoxName}[/]").Border(TableBorder.Heavy);
        table.AddColumn("Назва");
        table.AddColumn("Розмір");
        table.AddColumn("Цінність");

        foreach (var item in plan.Items)
        {
            table.AddRow(item.Name, item.Size.ToString("F1"), item.Value.ToString("C"));
        }
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("\n[bold green]Виконання лабораторної роботи завершено.[/]");
    }
}