using l10;
using LAB12_3.AVL_TREE;
using LAB12_3.ISD;
using Laba10;

namespace LAB12_3;

public class Program
{
    private static IsdTree<Game> Tree;
    
    public static void Main()
    {
        Tree = new IsdTree<Game>(null);

        while (true)
        {
            PrintMenu();

            uint action = Helpers.Helpers.EnterUInt("пункт меню", 1, 5);
            switch (action)
            {
                case 1:
                    FormTreeWithRandomValues();
                    break;
                case 2:
                    FormTreeWithUserValues();
                    break;
                case 3:
                    Tree.PrintTree();
                    break;
                case 4:
                    FindElementsWithName();
                    break;
                case 5:
                    if (Tree.Count != 0)Tree.Clear();
                    else Console.WriteLine("Дерево пустое!");
                    break;
            }
            
            Console.WriteLine("Нажмите для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("1. Создать ИДС со случайными значениями");
        Console.WriteLine("2. Создать ИДС с пользовательскими значениями");
        Console.WriteLine("3. Вывести ИДС");
        Console.WriteLine("4. Найти элементы по названию в ИДС");
        Console.WriteLine("5. Очистить ИДС");
    }

    private static void FormTreeWithRandomValues()
    {
        uint count = Helpers.Helpers.EnterUInt("количество элементов в дереве", 1);
        var rnd = Helpers.Helpers.GetOrCreateRandom();

        Game[] games = new Game[count];
        for (int i = 0; i < count; i++)
        {
            var gameType = rnd.Next(1, 3);
            Game game = null;
            switch (gameType)
            {
                case 1:
                    game = new Game();
                    break;
                case 2:
                    game = new VideoGame();
                    break;
                case 3:
                    game = new VRGame();
                    break;
            }
            game.RandomInit();
            games[i] = game;
        }

        Tree = new IsdTree<Game>(games);
        Console.WriteLine("Дерево успешно создано!");
    }
    
    private static void FormTreeWithUserValues()
    {
        uint count = Helpers.Helpers.EnterUInt("количество элементов в дереве", 1);
        
        Game[] games = new Game[count];
        for (int i = 0; i < count; i++)
        {
            var gameType = Helpers.Helpers.EnterUInt("тип игры(1: игра, 2: видеоигра, 3: VR игра)", 1, 3);
            Game game = null;
            switch (gameType)
            {
                case 1:
                    game = new Game();
                    break;
                case 2:
                    game = new VideoGame();
                    break;
                case 3:
                    game = new VRGame();
                    break;
            }
            game.Init();
            games[i] = game;
        }

        Tree = new IsdTree<Game>(games);
        Console.WriteLine("Дерево успешно создано!");
    }

    private static void FindElementsWithName()
    {
        if (Tree.Count == 0)
        {
            Console.WriteLine("Дерево пусто!");
            return;
        }
        
        Console.WriteLine("Введите имя элемента(ов) для поиска");
        var name = Console.ReadLine();

        int foundElements = 0;
        foreach (var game in Tree.FindElements(g => g != null && g.Name == name))
        {
            Console.WriteLine("----Элемент #" + (foundElements + 1) + "----");
            game.Show();
            foundElements++;
        }

        if (foundElements == 0)
        {
            Console.WriteLine("Элементов с названием = " + name + " не найдено!");
        }
    }
}