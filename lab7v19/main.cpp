#include <iostream>
#include <vector>
#include <string>
#include <stdexcept>
#include <functional>
#include <thread>
#include <chrono>

using namespace std;

// ------------ FileProcessor ------------
class FileProcessor {
    int fails = 0;
public:
    vector<string> ReadDatabaseDump(const string& path) {
        if (fails < 2) {
            fails++;
            throw runtime_error("File not found");
        }
        return { "user1", "user2", "user3" };
    }
};

// ------------ NetworkClient ------------
class NetworkClient {
    int fails = 0;
public:
    vector<string> QueryDatabase(const string& conn, const string& q) {
        if (fails < 3) {
            fails++;
            throw runtime_error("Network error");
        }
        return { "remote1", "remote2" };
    }
};

// ------------ RetryHelper ------------
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

                cout << "Спроба " << i + 1 << " помилка: " << ex.what() << endl;

                this_thread::sleep_for(chrono::milliseconds(delayMs));
                delayMs *= 2; 
            }
        }
        throw runtime_error("Всі спроби вичерпано");
    }
};

// ------------ main() ------------
int main() {
    FileProcessor fp;
    NetworkClient nc;

    auto shouldRetry = [](const exception& e) {
        string m = e.what();
        return m == "File not found" || m == "Network error";
    };

    try {
        auto local = RetryHelper::Execute<vector<string>>(
            [&]() { return fp.ReadDatabaseDump("dump.txt"); },
            4, 300, shouldRetry
        );
        cout << "Local OK (" << local.size() << " rows)" << endl;

        auto remote = RetryHelper::Execute<vector<string>>(
            [&]() { return nc.QueryDatabase("conn", "select"); },
            5, 300, shouldRetry
        );
        cout << "Remote OK (" << remote.size() << " rows)" << endl;
    }
    catch (const exception& e) {
        cout << "Фатальна помилка: " << e.what() << endl;
    }

    return 0;
}
