public class FileSave : FileOperationBase
{
    // Спеціалізований лічильник тільки для операцій збереження.
    private static int _saveCount = 0;
    
    // Публічний доступ до лічильника збережень.
    public static int SaveCount => _saveCount;

    // Реалізація абстрактної властивості.
    public override string OperationName => "Збереження файлу";

    /// Виконання операції збереження.
    public override bool Execute(string fileName)
    {
        // Викликаємо базовий метод для спільної логіки.
        if (PerformOperation(fileName, "зберегти"))
        {
            _saveCount++; // Збільшуємо власний лічильник.
            return true;
        }
        return false;
    }
}