using System.Diagnostics.CodeAnalysis;
using l10;
using LAB12_2.Hashtable;

namespace LAB12_2;

[ExcludeFromCodeCoverage]
class Program
{
    public static void Main()
    {
        var table = new MyHashTable<string, Game>();
        while (true)
        {
            PrintMenu();
            //выбранный пользователем пункт меню.
            var choice = Helpers.Helpers.EnterUInt("пункт меню", 1, 7);

            switch (choice)
            {
                //
                case 1:
                    //Создать новую хеш таблицу с новым размером
                    //newSize - capacity новой таблицы
                    var newSize = Helpers.Helpers.EnterUInt("размер хеш таблицы", 2, 65536);
                    table = new MyHashTable<string, Game>((int) newSize);
                    Console.WriteLine("Таблица с размером " + newSize + " успешно создана");
                    break;
                case 2:
                    //вывод таблицы
                    table.Print();
                    break;
                case 3:
                    //вставка randomElementsCount случайных элементов в таблицу
                    var randomElementsCount = Helpers.Helpers.EnterUInt("количество случайных элементов", 1, 65536);
                    for (int i = 0; i < randomElementsCount; i++)
                    {
                        var game = new Game();
                        game.RandomInit();
                        table.Set(game.Name, game);
                    }

                    Console.WriteLine("Успешно добавлены " + randomElementsCount + " случайных элементов");
                    break;
                case 4:
                    //вставка userEnterElementsCount элементов в таблицу
                    var userEnterElementsCount = Helpers.Helpers.EnterUInt("количество элементов для ввода", 1, 65536);
                    for (int i = 0; i < userEnterElementsCount; i++)
                    {
                        Console.WriteLine($"-----{i + 1}-ый элемент-----");
                        var game = new Game();
                        game.Init(); //пользовательский ввод полей объекта
                        
                        Console.WriteLine("Введите ключ элемента:");
                        string key = Console.ReadLine();
                        if (key == null) key = "";
                        //провека, может элемент с таким ключом уже есть в таблице
                        if (table.TryGetValue(key, out var alreadyPresentValue))
                        {
                            Console.WriteLine("Похоже, элемент с ключом = " + key + " уже есть в таблице, заменить его?");
                            //спрашиваем у пользователя, что делать, если элемент с ключом key уже есть в таблице
                            var replaceDecision = Helpers.Helpers.EnterUInt("0 - нет/ 1 - да", 0, 1);
                            if (replaceDecision == 0) continue; //пользователь решил не заменять существующий
                        }
                        table.Set(key, game);
                    }

                    Console.WriteLine("Элементы успешно добавлены");
                    break;
                case 5:
                    //удаление элемента
                    if (table.Count == 0)
                    {
                        Console.WriteLine("Таблица пуста!");
                    }
                    else
                    {
                        Console.WriteLine("Введите ключ элемента, который Вы хотите удалить:");
                        string key = Console.ReadLine();
                        if (key == null) key = "";

                        if (table.Remove(key))
                        {
                            Console.WriteLine("Элемент успешно удален!");
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден!");
                        }
                    }

                    break;
                case 6:
                    //получить и вывести элемент по ключу
                    if (table.Count == 0)
                    {
                        Console.WriteLine("Таблица пуста!");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Введите ключ элемента, который Вы хотите получить:");
                        string key = Console.ReadLine();
                        if (key == null) key = "";

                        if (table.TryGetValue(key, out var gotValue))
                        {
                            gotValue.ShowVirtual();
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден!");
                        }
                    }

                    break;
                case 7:
                    //выход
                    return;
            }

            Console.WriteLine("Нажмите для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    /// <summary>
    /// Вывод меню
    /// </summary>
    private static void PrintMenu()
    {
        Console.WriteLine("1. Создать новую хеш таблицу с новым размером");
        Console.WriteLine("2. Вывести хеш таблицу.");
        Console.WriteLine("3. Добавить n случайных элементов в таблицу.");
        Console.WriteLine("4. Вручную добавить n элементов в таблицу.");
        Console.WriteLine("5. Удалить элемент из таблицы.");
        Console.WriteLine("6. Получить элемент из хеш таблицы по ключу.");
        Console.WriteLine("7. Выход.");
    }
}