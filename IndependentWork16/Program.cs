using System;
using System.Collections.Generic;

namespace IndependentWork16
{
    // ПОРУШЕННЯ SRP
    public class BadOrderProcessor
    {
        public void Process(string orderName, int amount)
        {
            // 1. Валідація
            if (string.IsNullOrEmpty(orderName)) 
            {
                Console.WriteLine("Помилка: Назва замовлення порожня!");
                return;
            }

            // 2. Збереження в БД (ні)
            Console.WriteLine($"Збереження замовлення '{orderName}' на суму {amount} грн в базу даних...");

            // 3. Відправка Email
            Console.WriteLine($"Відправка Email: Замовлення '{orderName}' успішно оброблено!");
        }
    }

    // RЕФАКТОРИНГ

    // Interfaces
    public interface IOrderValidator
    {
        bool Validate(string name, int amount);
    }

    public interface IOrderRepository
    {
        void Save(string name, int amount);
    }

    public interface IEmailService
    {
        void SendConfirmation(string name);
    }

    // Реалізації інтерфейсів
    public class OrderValidator : IOrderValidator
    {
        public bool Validate(string name, int amount) => !string.IsNullOrEmpty(name) && amount > 0;
    }

    public class SqlOrderRepository : IOrderRepository
    {
        public void Save(string name, int amount) => 
            Console.WriteLine($"[DB] Замовлення {name} ({amount} грн) збережено.");
    }

    public class GmailService : IEmailService
    {
        public void SendConfirmation(string name) => 
            Console.WriteLine($"[Email] Повідомлення про замовлення {name} надіслано.");
    }

    public class OrderService
    {
        private readonly IOrderValidator _validator;
        private readonly IOrderRepository _repository;
        private readonly IEmailService _emailService;

        public OrderService(IOrderValidator validator, IOrderRepository repository, IEmailService emailService)
        {
            _validator = validator;
            _repository = repository;
            _emailService = emailService;
        }

        public void CreateOrder(string name, int amount)
        {
            if (_validator.Validate(name, amount))
            {
                _repository.Save(name, amount);
                _emailService.SendConfirmation(name);
                Console.WriteLine("Обробка завершена успішно.");
            }
            else
            {
                Console.WriteLine("Помилка валідації!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Старий (поганий) підхід");
            var badProcessor = new BadOrderProcessor();
            badProcessor.Process("Ноутбук", 25000);

            Console.WriteLine("\n Новий підхід (SRP)");
            var service = new OrderService(
                new OrderValidator(),
                new SqlOrderRepository(),
                new GmailService()
            );

            service.CreateOrder("Смартфон", 12000);

            Console.ReadKey();
        }
    }
}