using System;
using System.Collections.Generic;
using System.Linq;

public abstract class TimeSpanBase
{
    // Властивості
    public TimeSpan StartTime { get; set; } // Час початку інтервалу
    public TimeSpan Duration { get; protected set; } // Тривалість інтервалу

    // Конструктор
    public TimeSpanBase(TimeSpan startTime)
    {
        StartTime = startTime;
    }

    // Абстрактний метод: має бути реалізований у похідних класах
    public abstract TimeSpan GetDuration();

    // Допоміжний метод для отримання часу завершення
    public TimeSpan GetEndTime()
    {
        return StartTime.Add(Duration);
    }

    // Віртуальний метод для демонстрації типу інтервалу
    public virtual string GetIntervalType()
    {
        return "Інтервал часу";
    }

    // Метод для відображення інформації
    public void DisplayInfo()
    {
        Console.WriteLine($"\n{GetIntervalType()}:");
        Console.WriteLine($"  Початок: {StartTime:hh\\:mm}");
        Console.WriteLine($"  Тривалість: {Duration.TotalMinutes} хв.");
        Console.WriteLine($"  Завершення: {GetEndTime():hh\\:mm}");
    }
}

// ---

// 2. Похідний клас 1: Час заняття
public class LessonTime : TimeSpanBase
{
    // Фіксована тривалість заняття (наприклад, 90 хвилин)
    private const int DefaultLessonDurationMinutes = 90; 

    public string Subject { get; set; } // Назва предмету

    // Конструктор
    public LessonTime(TimeSpan startTime, string subject) : base(startTime)
    {
        Subject = subject;
        // Встановлюємо тривалість через перевизначений метод
        Duration = GetDuration(); 
    }

    // Реалізація абстрактного методу
    public override TimeSpan GetDuration()
    {
        // Для уроку завжди повертаємо фіксовану тривалість
        return TimeSpan.FromMinutes(DefaultLessonDurationMinutes); 
    }

    // Перевизначення методу типу
    public override string GetIntervalType()
    {
        return $"Урок ({Subject})";
    }
}

// ---

// 3. Похідний клас 2: Час перерви
public class BreakTime : TimeSpanBase
{
    // Змінна тривалість перерви
    public int BreakMinutes { get; } 

    // Конструктор
    public BreakTime(TimeSpan startTime, int breakMinutes) : base(startTime)
    {
        BreakMinutes = breakMinutes;
        // Встановлюємо тривалість через перевизначений метод
        Duration = GetDuration(); 
    }

    // Реалізація абстрактного методу
    public override TimeSpan GetDuration()
    {
        // Для перерви повертаємо задану в конструкторі тривалість
        return TimeSpan.FromMinutes(BreakMinutes); 
    }

    public override string GetIntervalType()
    {
        return "Перерва";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // 1. Створення розкладу дня
        Console.WriteLine("--- Формування розкладу дня ---");
        
        // Початок дня о 9:00
        TimeSpan currentTime = new TimeSpan(9, 0, 0); 
        
        List<TimeSpanBase> dailySchedule = new List<TimeSpanBase>();

        // Урок 1 (9:00 - 10:30)
        dailySchedule.Add(new LessonTime(currentTime, "Математика"));
        currentTime = dailySchedule.Last().GetEndTime(); 

        // Перерва 1 (10:30 - 10:45)
        dailySchedule.Add(new BreakTime(currentTime, 15));
        currentTime = dailySchedule.Last().GetEndTime();

        // Урок 2 (10:45 - 12:15)
        dailySchedule.Add(new LessonTime(currentTime, "Історія"));
        currentTime = dailySchedule.Last().GetEndTime();

        // Велика перерва 2 (12:15 - 13:00)
        dailySchedule.Add(new BreakTime(currentTime, 45));
        currentTime = dailySchedule.Last().GetEndTime();

        // Урок 3 (13:00 - 14:30)
        dailySchedule.Add(new LessonTime(currentTime, "Програмування"));
        
        
        // 2. Відображення розкладу та обчислення
        Console.WriteLine("\n--- Розклад дня ---");
        
        // Обчислення загальної тривалості занять і перерв
        TimeSpan totalLessonDuration = TimeSpan.Zero;
        TimeSpan totalBreakDuration = TimeSpan.Zero;

        foreach (var item in dailySchedule)
        {
            item.DisplayInfo();

            if (item is LessonTime)
            {
                totalLessonDuration += item.Duration;
            }
            else if (item is BreakTime)
            {
                totalBreakDuration += item.Duration;
            }
        }

        Console.WriteLine("\n--- Підсумки за день ---");
        Console.WriteLine($"Загальна тривалість занять: {totalLessonDuration.TotalMinutes} хв. ({totalLessonDuration:h\\:mm})");
        Console.WriteLine($"Загальна тривалість перерв: {totalBreakDuration.TotalMinutes} хв. ({totalBreakDuration:h\\:mm})");
        Console.WriteLine($"День завершився о: {dailySchedule.Last().GetEndTime():hh\\:mm}");
    }
}