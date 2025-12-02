using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

class Program
{
    //перевірка чи число просте
    static bool IsPrime(int n)
    {
        if (n < 2) return false;
        for (int i = 2; i * i <= n; i++)
            if (n % i == 0) return false;
        return true;
    }
//NEXT
    static void Main()
    {
        Console.WriteLine("IndependentWork12 — PLINQ");

        int size = 2_000_000; // 2 million
        var rnd = new Random();
        var data = new List<int>(size);

        for (int i = 0; i < size; i++)
            data.Add(rnd.Next(1, 2_000_000));

        // замір часу для LINQ

        var sw = Stopwatch.StartNew();
        var normal = data
            .Where(x => x % 2 == 0)
            .Select(x => IsPrime(x))
            .ToList();
        sw.Stop();
        Console.WriteLine("LINQ time: " + sw.ElapsedMilliseconds + " ms");

        // замір часу для PLINQ

        sw.Restart();
        var parallel = data
            .AsParallel()
            .Where(x => x % 2 == 0)
            .Select(x => IsPrime(x))
            .ToList();
        sw.Stop();
        Console.WriteLine("PLINQ time: " + sw.ElapsedMilliseconds + " ms");

        // сценарій з побічними ефектами

        int badCounter = 0;

        // тут результат буде неправильний, бо багато потоків лізуть у змінну одночасно

        data.AsParallel().ForAll(x =>
        {
            if (x % 2 == 0) badCounter++;
        });


        Console.WriteLine("Bad counter (unsafe): " + badCounter);



        // виправлення через lock
        int goodCounter = 0;
        object locker = new object();


        data.AsParallel().ForAll(x =>
        {
            if (x % 2 == 0)
            {
                lock (locker)
                {
                    goodCounter++;
                }
            }
        });

        Console.WriteLine("Good counter (safe): " + goodCounter);

    }
}