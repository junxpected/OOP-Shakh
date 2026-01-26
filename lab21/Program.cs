using System;

namespace lab21
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            TaxiCalculator calculator = new TaxiCalculator();

            Console.WriteLine("=== Розрахунок Таксі (OCP) ===");
            Console.Write("Тариф (Economy, Standard, Premium, Night): ");
            string choice = Console.ReadLine() ?? "";

            Console.Write("Відстань (км): ");
            decimal distance = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Очікування (хв): ");
            int minutes = int.Parse(Console.ReadLine() ?? "0");

            ITaxiStrategy strategy = TaxiFactory.CreateTaxi(choice);

            if (strategy != null)
            {
                decimal total = calculator.GetTotal(distance, minutes, strategy);
                Console.WriteLine("----------------------------------");
                Console.WriteLine("До сплати: " + total + " грн.");
            }
            else
            {
                Console.WriteLine("Помилка: Тариф не знайдено.");
            }
        }
    }
}