class Phone:
    def __init__(self, brand, model, battery_level=100):
        self.__brand = brand       
        self.__model = model       
        self._battery_level = battery_level  
        print(f"Телефон {self.__brand} {self.__model} створений.")

    @property
    def battery_level(self):
        return self._battery_level

    def call(self, number):
        if self._battery_level > 0:
            self._battery_level -= 10  
            return f"📞 Дзвінок на {number}... Залишок батареї: {self._battery_level}%"
        else:
            return "❌ Батарея розряджена!"

    def __del__(self):
        print(f"Телефон {self.__brand} {self.__model} вимкнено.")


p1 = Phone("Apple", "iPhone 13")
p2 = Phone("Samsung", "Galaxy S22", battery_level=50)

print(p1.call("+380971112233"))
print(p1.call("+380501234567"))

print(p2.call("+380631234567"))
