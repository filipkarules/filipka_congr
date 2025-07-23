using Pozdravlyator;
using System;

class Program
{
    static void Main()
    {
        var manager = new BirthdayManager();

        Console.Clear();
        Console.WriteLine("🎉 Добро пожаловать в 'Поздравлятор' 🎉\n");
        ShowTodayAndUpcoming(manager);

        while (true)
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine("1. Показать все ДР");
            Console.WriteLine("2. Добавить ДР");
            Console.WriteLine("3. Удалить ДР");
            Console.WriteLine("4. Редактировать ДР");
            Console.WriteLine("5. Показать ближайшие ДР");
            Console.WriteLine("6. ❌ ВЫЙТИ ❌");
            Console.WriteLine("7. Загрузить из файла");
            Console.WriteLine("8. Сохранить в файл");
            Console.Write("Выберите опцию: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    ShowAll(manager);
                    break;
                case "2":
                    Console.Clear();
                    AddBirthday(manager);
                    break;
                case "3":
                    Console.Clear();
                    RemoveBirthday(manager);
                    break;
                case "4":
                    Console.Clear();
                    EditBirthday(manager);
                    break;
                case "5":
                    Console.Clear();
                    ShowTodayAndUpcoming(manager);
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("До свидания!");
                    return;

                case "7":
                    Console.Clear();
                    Console.Write("Введите имя файла для загрузки: ");
                    var loadFile = Console.ReadLine();
                    manager.LoadFromFile(loadFile);
                    Console.WriteLine($"✅ Загружено {manager.Entries.Count} записей из \"{loadFile}\".");
                    break;

                case "8":
                    Console.Clear();
                    Console.Write("Введите имя файла для сохранения: ");
                    var saveFile = Console.ReadLine();
                    manager.SaveToFile(saveFile);
                    Console.WriteLine($"✅ Список сохранён в файл \"{saveFile}\".");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("❌ Неверная команда!");
                    break;
            }
        }
    }

    static void ShowAll(BirthdayManager manager)
    {
        var all = manager.GetAll();
        if (all.Count == 0)
        {
            Console.WriteLine("Список пуст.");
            return;
        }

        Console.WriteLine("\n--- Все записи ---");
        for (int i = 0; i < all.Count; i++)
            Console.WriteLine($"{i + 1}. {all[i]}");
    }

    static void ShowTodayAndUpcoming(BirthdayManager manager)
    {
        var today = manager.GetTodayBirthdays();
        var upcoming = manager.GetUpcoming();

        Console.WriteLine("--- Сегодня ---");
        if (today.Count == 0)
            Console.WriteLine("Сегодня нет ДР.");
        else
            foreach (var entry in today)
                Console.WriteLine($"🎉 {entry}");

        Console.WriteLine("\n--- Ближайшие ---");
        if (upcoming.Count == 0)
            Console.WriteLine("В ближайшую неделю нет ДР.");
        else
            foreach (var entry in upcoming)
                Console.WriteLine(entry);
    }

    static void AddBirthday(BirthdayManager manager)
    {
        Console.Write("Введите имя: ");
        var name = Console.ReadLine();
        Console.Write("Введите дату рождения (дд.мм.гггг): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            manager.AddEntry(new BirthdayEntry { Name = name, DateOfBirth = date });
            Console.WriteLine("✅ Запись добавлена.");
        }
        else
            Console.WriteLine("❌ Неверный формат даты.");
    }

    static void RemoveBirthday(BirthdayManager manager)
    {
        ShowAll(manager);
        Console.Write("Введите номер записи для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            manager.RemoveEntry(index - 1);
            Console.WriteLine("✅ Запись удалена.");
        }
        else
            Console.WriteLine("❌ Неверный ввод.");
    }

    static void EditBirthday(BirthdayManager manager)
    {
        ShowAll(manager);
        Console.Write("Введите номер записи для редактирования: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            Console.Write("Новое имя: ");
            var name = Console.ReadLine();
            Console.Write("Новая дата рождения (дд.мм.гггг): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                manager.EditEntry(index - 1, new BirthdayEntry { Name = name, DateOfBirth = date });
                Console.WriteLine("✅ Запись обновлена.");
            }
            else
                Console.WriteLine("❌ Неверная дата.");
        }
        else
            Console.WriteLine("❌ Неверный номер.");
    }
}
