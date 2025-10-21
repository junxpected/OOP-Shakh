public interface IFileOperation
{
    // Метод, що виконує операцію.
    bool Execute(string fileName);
    
    // Властивість для отримання назви операції.
    string OperationName { get; }
}


/// Абстрактний базовий клас, що містить спільну логіку та загальний лічильник.

public abstract class FileOperationBase : IFileOperation
{
    // Приховане статичне поле для підрахунку ВСІХ успішних операцій.
    private static int _totalOperationCounter = 0;

    // Публічний доступ до загального лічильника.
    public static int TotalOperationsCount => _totalOperationCounter;

    // Абстрактна властивість: має бути реалізована нащадками.
    public abstract string OperationName { get; }

    // Абстрактний метод: має бути реалізований нащадками.
    public abstract bool Execute(string fileName);

    /// Спільна логіка виконання: імітація операції та збільшення загального лічильника.

    protected bool PerformOperation(string fileName, string actionType)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            _totalOperationCounter++; // Збільшуємо лічильник
            Console.WriteLine($"-> {OperationName} успішно виконано для файлу '{fileName}'.");
            return true;
        }
        Console.WriteLine($"-> Помилка: Неможливо виконати {OperationName} з порожньою назвою файлу.");
        return false;
    }
}