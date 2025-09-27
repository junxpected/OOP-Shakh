using System;

public class TimeSpanBase
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan Duration { get; protected set; }

    public TimeSpanBase(TimeSpanBase startTime)
    {
        StartTime = startTime;
    }


    public abstract TimeSpanBase GetDuration();


    public TimeSpanBase GetEndTime()
    {
        return StartTime.Add(Duration);
    }


    public virtual string GetIntervalType()
    {
        return "Інтервал часу";
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"\n{GetIntervalType()}:");
        Console.WriteLine($"  Початок: {StartTime:hh\\:mm}");
        Console.WriteLine($"  Тривалість: {Duration.TotalMinutes} хв.");
        Console.WriteLine($"  Завершення: {GetEndTime():hh\\:mm}");
    }


    public class LessonTime : TimeSpanBase
    {
        private const int DefaultLessonDurationMinutes = 90;

        public string Subject { get; set; }

        public LessonTime(TimeSpan startTime, string subject) : base(startTime)
        {
            Subject - subject;

            Duration = GetDuration;
        }

        public override global::System.String GetIntervalType()
        {
            return $"Урок ({Subject})";
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
            currentTime = dailySchedule.Last().GetEndTime(); // Наступний початок - це час завершення попереднього

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
}