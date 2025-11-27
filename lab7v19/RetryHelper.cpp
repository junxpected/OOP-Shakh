// шаблонний retry з експоннційною затримкою
class RetryHelper {
public:
    template<typename T>
    static T Execute(function<T()> op, int count = 3, int delayMs = 500,
                     function<bool(const exception&)> shouldRetry = nullptr)
    {
        for (int i = 0; i < count; i++) {
            try {
                return op();
            }
            catch (const exception& ex) {
                if (shouldRetry && !shouldRetry(ex))
                    throw;

                cout << "Спроба " << i+1 << " помилка: " << ex.what() << endl;
                this_thread::sleep_for(chrono::milliseconds(delayMs));
                delayMs *= 2;
            }
        }
        throw runtime_error("Всі спроби вичерпано");
    }
};
