class Figure:
    def __init__(self, name, area):
        self.__name = name      
        self._area = area       
        print(f"Фігура {name} створена.")

    @property
    def area(self):
        return self._area

    def get_figure(self):
        return f"Назва: {self.__name}, Площа: {self._area}"

    def __del__(self):
        print(f"Фігура {self.__name} видалена.")


f1 = Figure("Квадрат", 25)
f2 = Figure("Коло", 78.5)

print(f1.get_figure())
print(f2.get_figure())
