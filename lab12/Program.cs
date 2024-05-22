using System.Diagnostics.CodeAnalysis;
using l10;
using lab12.List;

namespace lab12;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main()
    {
     Task1();
    }

    /// <summary>
    /// Задание 1 лабораторной работы 12
    /// </summary>
    public static void Task1()
    {
        var list = new MyList<Game>();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("0) Выйти");
            Console.WriteLine("1) Добавить K элементов в конец списка");
            Console.WriteLine("2) Добавить случайный элемент в начало списка");
            Console.WriteLine("3) Добавить случайный элемент в конец списка");
            Console.WriteLine("4) Удалить последний элемент по имени");
            Console.WriteLine("5) Клонировать список");
            Console.WriteLine("6) Вывести список");
            Console.WriteLine("7) Удалить старый список из памяти");

            uint decision = Helpers.Helpers.EnterUInt("Пункт меню", 1, 7);

            switch (decision)
            {
                case 0:
                    return;
                case 1:
                    AddKElementsToListEndWrapper(list);
                    break;
                case 2:
                    var game = new Game();
                    game.RandomInit();
                    list.AddItemStart(game);
                    list.Print();
                    Console.ReadKey();
                    break;
                case 3:
                    var gam = new Game();
                    gam.RandomInit();
                    list.AddItemEnd(gam);
                    list.Print();
                    Console.ReadKey();
                    break;
                case 4:
                    if (list.Count == 0)
                    {
                        Console.WriteLine("Список пуст!");
                        continue;
                    }
                    DeleteLastElementWithNameWrapper(list);
                    Console.ReadKey();
                    break;
                case 5:
                    var cloned = (MyList<Game>)list.Clone();
                    cloned.Print();
                    Console.ReadKey();
                    break;
                case 6:
                    list.Print();
                    Console.ReadKey();
                    break;
                case 7:
                    list.Clear();
                    list = new MyList<Game>();
                    list.Print();
                    Console.ReadKey();
                    break;
            }
        }
    }

    /// <summary>
    /// Пользовательская обёртка над DeleteLastElementWithName
    /// </summary>
    /// <param name="gamesList"></param>
    public static void DeleteLastElementWithNameWrapper(MyList<Game> gamesList)
    {
        Console.WriteLine("Введите название, игру с которым желаете удалить");
        var name2delete = Console.ReadLine();
        if (DeleteLastElementWithName(gamesList, name2delete))
        {
            Console.WriteLine($"Успешно. Был удален последний элемент с именем = {name2delete}, новый список:");
            gamesList.Print();
        }
        else
        {
            Console.WriteLine($"Элемент с именем = {name2delete} не был найден!");
        }
        Console.ReadKey();
    }

    /// <summary>
    /// Удаляет последний элемент с значением поля Name = name
    /// </summary>
    /// <param name="gamesList"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool DeleteLastElementWithName(MyList<Game> gamesList, string name)
    {
        return gamesList.RemoveItem((item) => item.Name == name, true);
    }

    /// <summary>
    /// Пользовательская обёртка над AddKElementsToListEnd
    /// </summary>
    /// <param name="games"></param>
    public static void AddKElementsToListEndWrapper(MyList<Game> games)
    {
        var k = Helpers.Helpers.EnterUInt("Количество элементов для добавления", 1);
        AddKElementsToListEnd(games, k);
        
        Console.WriteLine("Элементы были успешно добавлены. Вот получившийся список:");
        games.Print();
        Console.ReadKey();
    }

    /// <summary>
    /// Запрос, добавляющий к элементов в конец списка
    /// </summary>
    /// <param name="gamesList"></param>
    /// <param name="k"></param>
    public static void AddKElementsToListEnd(MyList<Game> gamesList, uint k)
    {
        for (int i = 0; i < k; i++)
        {
            var game = new Game();
            game.RandomInit();
            gamesList.AddItemEnd(game);
        }
    }
}