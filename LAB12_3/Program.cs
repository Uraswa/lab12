using System.Diagnostics.CodeAnalysis;
using l10;
using LAB12_3.AVL_TREE;
using LAB12_3.ISD;
using Laba10;

namespace LAB12_3;

[ExcludeFromCodeCoverage]
public class Program
{
    private static IsdTree<Game> ISDTree;
    private static AvlTree<Game> AVLTree;

    public static void Main()
    {
        ISDTree = new IsdTree<Game>(null);
        AVLTree = new AvlTree<Game>();

        while (true)
        {
            PrintMenu();

            uint treeType = Helpers.Helpers.EnterUInt("действие", 1, 3);

            switch (treeType)
            {
                case 1:
                    ISDLoop();
                    break;
                case 2:
                    AVLLoop();
                    break;
                case 3:
                    return;
            }
        }
    }

    private static void ISDLoop()
    {
        while (true)
        {
            PrintMenuISD();

            uint action = Helpers.Helpers.EnterUInt("пункт меню", 1, 6);
            switch (action)
            {
                case 1:
                    FormTreeWithRandomValues();
                    break;
                case 2:
                    FormTreeWithUserValues();
                    break;
                case 3:
                    ISDTree.PrintTree();
                    break;
                case 4:
                    ISDFindElementsWithName();
                    break;
                case 5:
                    if (ISDTree.Count != 0) ISDTree.Clear();
                    else Console.WriteLine("Дерево пустое!");
                    break;
                case 6:
                    return;
            }

            Console.WriteLine("Нажмите для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void AVLLoop()
    {
        while (true)
        {
            PrintAVLMenu();
            var action = Helpers.Helpers.EnterUInt("пункт меню", 1, 8);

            switch (action)
            {
                case 1:
                    InitAVLRandomElements();
                    break;
                case 2:
                    InitAVLUserElements();
                    break;
                case 3:
                    CreateAVLFromISD();
                    break;
                case 4:
                    AVLTree.PrintTree();
                    break;
                case 5:
                    AVLFindElementsWithName();
                    break;
                case 6:
                    AVLRemoveByValue();
                    break;
                case 7:
                    AVLTree.Clear();
                    break;
                case 8:
                    return;
                    break;
            }
            
            Console.WriteLine("Нажмите для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void PrintMenuISD()
    {
        Console.WriteLine("1. Создать ИДС со случайными значениями");
        Console.WriteLine("2. Создать ИДС с пользовательскими значениями");
        Console.WriteLine("3. Вывести ИДС");
        Console.WriteLine("4. Найти элементы по названию в ИДС");
        Console.WriteLine("5. Очистить ИДС");
        Console.WriteLine("6. Назад");
    }

    private static void PrintMenu()
    {
        Console.WriteLine("Выберите тип дерева, с которым хотите работать, либо выход:");
        Console.WriteLine("1. ISD");
        Console.WriteLine("2. AVL");
        Console.WriteLine("3. Выход");
    }

    private static void PrintAVLMenu()
    {
        Console.WriteLine("1. Создать AVL дерево со случаными значениями");
        Console.WriteLine("2. Создать AVL дерево с пользовательскими значениями");
        Console.WriteLine("3. Преобразовать ISD дерево в AVL");
        Console.WriteLine("4. Вывести AVL дерево");
        Console.WriteLine("5. Найти элементы по названию в AVL");
        Console.WriteLine("6. Удалить элемент из AVL дерева");
        Console.WriteLine("7. Удалить AVL дерево");
        Console.WriteLine("8. Назад");
    }

    private static Game GetRandomGame()
    {
        var rnd = Helpers.Helpers.GetOrCreateRandom();
        
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
        return game;
    }

    private static Game GetUserGame()
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
        return game;
    }

    private static void FormTreeWithRandomValues()
    {
        uint count = Helpers.Helpers.EnterUInt("количество элементов в дереве", 1);

        Game[] games = new Game[count];
        for (int i = 0; i < count; i++)
        {
            games[i] = GetRandomGame();
        }

        ISDTree = new IsdTree<Game>(games);
        Console.WriteLine("Дерево успешно создано!");
    }

    private static void FormTreeWithUserValues()
    {
        uint count = Helpers.Helpers.EnterUInt("количество элементов в дереве", 1);

        Game[] games = new Game[count];
        for (int i = 0; i < count; i++)
        {
            games[i] = GetUserGame();
        }

        ISDTree = new IsdTree<Game>(games);
        Console.WriteLine("Дерево успешно создано!");
    }

    private static void ISDFindElementsWithName()
    {
        if (ISDTree.Count == 0)
        {
            Console.WriteLine("Дерево пусто!");
            return;
        }

        Console.WriteLine("Введите имя элемента(ов) для поиска");
        var name = Console.ReadLine();

        int foundElements = 0;
        foreach (var game in ISDTree.FindElements(g => g != null && g.Name == name))
        {
            Console.WriteLine("----Элемент #" + (foundElements + 1) + "----");
            game.ShowVirtual();
            foundElements++;
        }

        if (foundElements == 0)
        {
            Console.WriteLine("Элементов с названием = " + name + " не найдено!");
        }
    }

    private static void InitAVLRandomElements()
    {
        AVLTree = new AvlTree<Game>();
        uint count = Helpers.Helpers.EnterUInt("количество элементов в дереве", 1);

        for (int i = 0; i < count; i++)
        {
            AVLTree.Insert(GetRandomGame());
        }
        Console.WriteLine("Дерево успешно создано!");
    }
    
    private static void InitAVLUserElements()
    {
        AVLTree = new AvlTree<Game>();
        uint count = Helpers.Helpers.EnterUInt("количество элементов в дереве", 1);

        for (int i = 0; i < count; i++)
        {
            AVLTree.Insert(GetUserGame());
        }
        Console.WriteLine("Дерево успешно создано!");
    }

    private static void CreateAVLFromISD()
    {
        if (ISDTree.Count == 0)
        {
            Console.WriteLine("ISD дерево пустое!");
            return;
        }
        AVLTree = new AvlTree<Game>();
        foreach (var game in ISDTree.FindElements(e => true))
        {
            AVLTree.Insert(game);
        }
        Console.WriteLine("ISD дерево успешно преобразовано в AVL!");
    }
    
    private static void AVLFindElementsWithName()
    {
        if (AVLTree.Count == 0)
        {
            Console.WriteLine("Дерево пусто!");
            return;
        }

        Console.WriteLine("Введите имя элемента(ов) для поиска");
        var name = Console.ReadLine();

        int foundElements = 0;
        foreach (var game in AVLTree.GetValues())
        {
            if (game == null || game.Name != name) continue;
            Console.WriteLine("----Элемент #" + (foundElements + 1) + "----");
            game.ShowVirtual();
            foundElements++;
        }

        if (foundElements == 0)
        {
            Console.WriteLine("Элементов с названием = " + name + " не найдено!");
        }
        
    }
    
    private static void AVLRemoveByValue()
    {
        if (AVLTree.Count == 0)
        {
            Console.WriteLine("Дерево пустое!");
            return;
        }

        Game game2Remove = null;
        uint gameId = Helpers.Helpers.EnterUInt("id игры");
        foreach (var value in AVLTree.GetValues())
        {
            if (value.Id.Number == gameId)
            {
                game2Remove = value;
                break;
            }
        }

        if (game2Remove == null)
        {
            Console.WriteLine("Элемент не был найден!");
            return;
        }

        if (AVLTree.Remove(game2Remove))
        {
            Console.WriteLine("Элемент успешно удален!");
        }
        else
        {
            Console.WriteLine("Элемент не был найден!");
        }
    }
}