public class FileLoad : FileOperationBase
{
    // Спеціалізований лічильник тільки для операцій завантаження.
    private static int _loadCount = 0;
    
    // Публічний доступ до лічильника завантажень.
    public static int LoadCount => _loadCount;

    // Реалізація абстрактної властивості.
    public override string OperationName => "Завантаження файлу";

    /// Виконання операції завантаження.
    public override bool Execute(string fileName)
    {
        // Викликаємо базовий метод для спільної логіки.
        if (PerformOperation(fileName, "завантажити"))
        {
            _loadCount++; // Збільшуємо власний лічильник.
            return true;
        }
        return false;
    }
}