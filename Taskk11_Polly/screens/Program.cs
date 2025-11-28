using System;
using System.Net.Http;
using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System.Threading;

public class Program
{
    // ----------- Сценарій 1: API тимчасово недоступний (Retry) -----------

    static int _apiFails = 0;

    static string CallApi()
    {
        _apiFails++;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] API attempt {_apiFails}");

        if (_apiFails <= 2) // перші 2 рази імітуємо збій
            throw new HttpRequestException("API недоступне");

        return "API-дані успішно отримано";
    }


    // ----------- Сценарій 2: База даних з перебоями (Retry + Circuit Breaker) -----------

    static int _dbFails = 0;

    static string QueryDatabase()
    {
        _dbFails++;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] DB attempt {_dbFails}");

        if (_dbFails <= 3)
            throw new Exception("Помилка підключення до БД");

        return "Результат з БД";
    }


    // ----------- Сценарій 3: Довга операція (Timeout + Fallback) -----------

    static string LongOperation()
    {
        Console.WriteLine("Починаю довгу операцію...");
        Thread.Sleep(5000); // 5 сек – спеціально довго
        return "Операція виконана";
    }


    // ================= MAIN =================

    public static void Main(string[] args)
    {
        Console.WriteLine("=== Самостійна робота №11 — Polly / Retry ===\n");

        // ================= Сценарій 1 =================
        Console.WriteLine("----- Сценарій 1: API + Retry -----");

        var retryApiPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetry(3,
                r => TimeSpan.FromSeconds(1 * r),
                (ex, time, retry, ctx) =>
                {
                    Console.WriteLine($"Retry {retry} після {time.TotalSeconds}s: {ex.Message}");
                });

        try
        {
            string result = retryApiPolicy.Execute(() => CallApi());
            Console.WriteLine("Результат: " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Фатальна помилка API: " + ex.Message);
        }

        Console.WriteLine();


        // ================= Сценарій 2 =================
        Console.WriteLine("----- Сценарій 2: DB + Retry + Circuit Breaker -----");

        var retryDb = Policy
            .Handle<Exception>()
            .WaitAndRetry(2, i => TimeSpan.FromMilliseconds(500));

        var circuitBreaker = Policy
            .Handle<Exception>()
            .CircuitBreaker(2, TimeSpan.FromSeconds(5),
            onBreak: (ex, time) =>
            {
                Console.WriteLine($"Circuit BREAK: {ex.Message}, пауза {time.TotalSeconds}s");
            },
            onReset: () => Console.WriteLine("Circuit RESET"),
            onHalfOpen: () => Console.WriteLine("Circuit HALF-OPEN"));

        var dbPolicy = Policy.Wrap(retryDb, circuitBreaker);

        try
        {
            string dbResult = dbPolicy.Execute(() => QueryDatabase());
            Console.WriteLine("DB => " + dbResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine("БД недоступна: " + ex.Message);
        }

        Console.WriteLine();


        // ================= Сценарій 3 =================
        Console.WriteLine("----- Сценарій 3: Long Operation + Timeout + Fallback -----");

        var timeout = Policy.Timeout(2, TimeoutStrategy.Pessimistic,
            onTimeout: (ctx, time, task) =>
            {
                Console.WriteLine($"Операція довше {time.TotalSeconds} секунд — Timeout");
            });

        var fallback = Policy<string>
            .Handle<TimeoutRejectedException>()
            .Fallback(() => "Fallback: повертаю запасний результат");

        var longOpPolicy = fallback.Wrap(timeout);

        string longResult = longOpPolicy.Execute(() => LongOperation());
        Console.WriteLine("Результат: " + longResult);


        Console.WriteLine("\n=== Кінець роботи ===");
    }
}
