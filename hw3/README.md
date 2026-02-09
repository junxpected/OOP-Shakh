# Принципи ISP та DIP (SOLID)

## 1. Interface Segregation Principle (ISP)

**ISP (Принцип розділення інтерфейсів)** стверджує, що *клієнти не повинні залежати від методів, які вони не використовують*. Іншими словами, краще мати кілька **вузьких (малих) інтерфейсів**, ніж один великий «жирний».

### Приклад порушення ISP

```csharp
public interface IMultifunctionDevice
{
    void Print();
    void Scan();
    void Fax();
}

public class OldPrinter : IMultifunctionDevice
{
    public void Print() { /* ok */ }
    public void Scan() { throw new NotImplementedException(); }
    public void Fax() { throw new NotImplementedException(); }
}
```

**Проблема:** `OldPrinter` змушений реалізовувати методи, які йому не потрібні.

### Вирішення (дотримання ISP)

```csharp
public interface IPrinter
{
    void Print();
}

public interface IScanner
{
    void Scan();
}

public interface IFax
{
    void Fax();
}

public class OldPrinter : IPrinter
{
    public void Print() { /* ok */ }
}
```

Тепер кожен клас реалізує лише потрібний інтерфейс.

---

## 2. Dependency Inversion Principle (DIP)

**DIP (Принцип інверсії залежностей)** говорить:

* модулі верхнього рівня не повинні залежати від модулів нижнього рівня;
* обидва мають залежати від **абстракцій**;
* абстракції не повинні залежати від деталей реалізації.

### Приклад без DIP (жорстка залежність)

```csharp
public class EmailService
{
    public void Send(string message) { }
}

public class Notification
{
    private EmailService _email = new EmailService();

    public void Notify(string msg)
    {
        _email.Send(msg);
    }
}
```

**Мінуси:** складно замінити Email на SMS, складно тестувати.

### Застосування DIP через Dependency Injection

```csharp
public interface IMessageService
{
    void Send(string message);
}

public class EmailService : IMessageService
{
    public void Send(string message) { }
}

public class Notification
{
    private readonly IMessageService _service;

    public Notification(IMessageService service)
    {
        _service = service;
    }

    public void Notify(string msg)
    {
        _service.Send(msg);
    }
}
```

Тепер залежність передається ззовні (**Dependency Injection**).

### Переваги DIP + DI

* легко міняти реалізації (Email → SMS → Push);
* код менш зв’язаний;
* просте модульне тестування (можна підставити mock).

---

## 3. Як ISP допомагає DI та тестуванню

**Вузькі інтерфейси (ISP):**

* простіше інжектити залежності (менше методів);
* легше створювати mock/stub для тестів;
* менше побічних залежностей у класах.

### Приклад для тестування

```csharp
public class FakeMessageService : IMessageService
{
    public string LastMessage;
    public void Send(string message)
    {
        LastMessage = message;
    }
}
```

Малий інтерфейс → простий fake → зрозумілий тест.

---

## Висновок

* **ISP** робить інтерфейси простими та спеціалізованими.
* **DIP** зменшує залежності між модулями та підвищує гнучкість.
* Разом вони значно покращують **Dependency Injection**, **тестованість** та **масштабованість** коду.
