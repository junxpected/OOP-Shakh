#include <iostream>
#include <vector>
#include <algorithm>
#include <numeric>   // accumulate
#include <functional> // std::function

using namespace std;

// Клас Car
class Car {
public:
    string model;
    int mileage;
    double fuelConsumption;

    Car(string m, int ml, double fc)
        : model(m), mileage(ml), fuelConsumption(fc) {}

    // Зручний вивід інформації
    void print() const {
        cout << model << " | Пробіг: " << mileage
             << " км | Витрата: " << fuelConsumption << " л/100км\n";
    }
};

int main() {

    // Створюємо вектор машин
    vector<Car> cars = {
        Car("BMW 320d", 120000, 6.5),
        Car("Audi A4", 90000, 7.2),
        Car("Toyota Corolla", 150000, 6.0),
        Car("Skoda Octavia", 200000, 5.8),
        Car("Honda Civic", 80000, 6.8)
    };

    cout << "~~~~ Автомобілі в базі ~~~~\n";
    for (const auto& c : cars) c.print();
    cout << endl;

    // 1) Лямбда + std::function → делегат для середньої витрати пального
    std::function<double(const vector<Car>&)> calcAvgConsumption =
        [](const vector<Car>& list) {
            if (list.empty()) return 0.0;

            double sum = accumulate(
                list.begin(), list.end(), 0.0,
                [](double acc, const Car& c) {
                    return acc + c.fuelConsumption;
                }
            );
            return sum / list.size();
        };

    double avg = calcAvgConsumption(cars);
    cout << "Середня витрата пального: " << avg << " л/100км\n\n";

    // 2) Пошук машини з мінімальною витратою (найекономнішої)
    auto minCar = min_element(
        cars.begin(), cars.end(),
        [](const Car& a, const Car& b) {
            return a.fuelConsumption < b.fuelConsumption;
        }
    );

    cout << "Машина з мінімальною витратою:\n";
    minCar->print();
    cout << endl;

    // 3) Predicate → машини з пробігом > 100000 км
    auto highMileage = [](const Car& c) { return c.mileage > 100000; };

    cout << "Машини з пробігом > 100000 км:\n";
    for (const auto& c : cars)
        if (highMileage(c)) c.print();
    cout << endl;

    // 4) Action → вивід кожної машини
    std::function<void(const Car&)> printCar =
        [](const Car& c) {
            cout << "[Модель: " << c.model
                 << "] Пробіг: " << c.mileage
                 << " км | Витрата: " << c.fuelConsumption << " л/100км\n";
        };

    cout << "Вивід машин через Action (std::function<void>):\n";
    for (const auto& c : cars) printCar(c);
    cout << endl;

    // 5) Func → шукаємо машину з максимальною витратою
    std::function<const Car&(const vector<Car>&)> maxConsumption =
        [](const vector<Car>& list) -> const Car& {
            return *max_element(
                list.begin(), list.end(),
                [](const Car& a, const Car& b) {
                    return a.fuelConsumption < b.fuelConsumption;
                }
            );
        };

    const Car& maxCar = maxConsumption(cars);
    cout << "Машина з максимальною витратою:\n";
    maxCar.print();
    cout << endl;

    // 6) Анонімна функція (аналог delegate у С#)
    auto isEconomy = [](const Car& c) {
        return c.fuelConsumption < 6.5;
    };

    cout << "Економні машини (витрата < 6.5):\n";
    for (const auto& c : cars)
        if (isEconomy(c)) c.print();

    return 0;
}
