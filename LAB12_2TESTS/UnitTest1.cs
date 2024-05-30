using l10;
using LAB12_2.Hashtable;
namespace LAB12_2TESTS;

public class Tests
{
    /// <summary>
    ///  Тестирование конструктора по умолчанию
    /// </summary>
    [Test]
    public void TestHashtableConstructorEmpty()
    {
        var table = new MyHashTable<Game, Game>();
        Assert.IsTrue(table.Count == 0);
    }
    
    /// <summary>
    ///  Тестирование неверной capacity
    /// </summary>
    [Test]
    public void TestHashtableConstructorBadCapacity()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var hashtable = new MyHashTable<Game, Game>(1);
        });
    }
    
    /// <summary>
    ///  Тестирование конструктора с нормальным значением capacity
    /// </summary>
    [Test]
    public void TestHashtableConstructorFineCapacity()
    {
        var table = new MyHashTable<Game, Game>(15);
        Assert.IsTrue(table.Count == 0);
    }
    
    /// <summary>
    /// Добавление элемента в hashtable
    /// </summary>
    [Test]
    public void TestAddElementToHashtable()
    {
        var table = new MyHashTable<Game, Game>();
        var game = new Game();
        game.RandomInit();
        table.Set(game, game);
        
        Assert.IsTrue(table.Count == 1);

        foreach (var keyValuePair in table)
        {
            Assert.IsTrue(keyValuePair.Key.Equals(game));
            Assert.IsTrue(keyValuePair.Value.Equals(game));
        }
    }
    
    /// <summary>
    /// Добавление дубликата в hashtable
    /// </summary>
    [Test]
    public void TestAddDuplicateToHashtable()
    {
        var table = new MyHashTable<Game, Game>();
        
        var game = new Game();
        game.RandomInit();
        
        table.Set(game, game);
        table.Set(game, game);
        
        Assert.IsTrue(table.Count == 1);

        foreach (var keyValuePair in table)
        {
            Assert.IsTrue(keyValuePair.Key.Equals(game));
            Assert.IsTrue(keyValuePair.Value.Equals(game));
        }
    }
    
    /// <summary>
    /// Добавление другого элемента с тем же хеш кодом
    /// </summary>
    [Test]
    public void TestAddDifferentElementWithSameHashcode()
    {
        var table = new MyHashTable<Game,Game>();

        var games = new Game[]
        {
            new Game("1", 1, 1, new Game.IdNumber(0)),
            new Game("1", 1, 1, new Game.IdNumber(10))
        };
        
        table.Set(games[0], games[0]);
        table.Set(games[1], games[1]);
        
        Assert.IsTrue(table.Count == 2);

        int index = 0;
        foreach (var pair in table)
        {
            Assert.IsTrue(pair.Key.Equals(games[index]));
            Assert.IsTrue(pair.Value.Equals(games[index]));
            index++;
        }
    }
    
    /// <summary>
    /// Добавление другого элемента с другим хеш кодом
    /// </summary>
    [Test]
    public void TestAddDifferentElementWithDifferentHashcode()
    {
        var table = new MyHashTable<Game,Game>();

        var games = new Game[]
        {
            new Game("1", 1, 1, new Game.IdNumber(0)),
            new Game("2", 1, 1, new Game.IdNumber(11))
        };
        
        table.Set(games[0], games[0]);
        table.Set(games[1], games[1]);
        
        Assert.IsTrue(table.Count == 2);

        int index = 0;
        foreach (var pair in table)
        {
            Assert.IsTrue(pair.Key.Equals(games[index]));
            Assert.IsTrue(pair.Value.Equals(games[index]));
            index++;
        }
    }
    
    /// <summary>
    /// Удаление единственного элемента в цепочке.
    /// </summary>
    [Test]
    public void TestDeleteOnlyElementInChain()
    {
        var table = new MyHashTable<Game, Game>();
        var game = new Game("1", 1, 1, new Game.IdNumber(0));
        table.Set(game, game);
        table.Remove(game);
        Assert.IsTrue(table.Count == 0);

        int c = 0;
        foreach (var pair in table)
        {
            c++;
        }
        Assert.IsTrue(c == 0);
    }
    
    
    /// <summary>
    /// Удаление среднего элемента в цепочке
    /// </summary>
    [Test]
    public void TestRemoveMiddleElementInChain()
    {
        var table = new MyHashTable<Game, Game>();
        
        var games = new Game[]
        {
            new Game("1", 1, 1, new Game.IdNumber(0)),
            new Game("2", 1, 1, new Game.IdNumber(10)),
            new Game("3", 1, 1, new Game.IdNumber(20))
        };
        
        table.Set(games[0], games[0]);
        table.Set(games[1], games[1]);
        table.Set(games[2], games[2]);
        
        Assert.IsTrue(table.Remove(games[1]));
        Assert.IsTrue(table.Count == 2);

        foreach (var pair in table)
        {
            Assert.IsFalse(pair.Key.Equals(games[1]));
            Assert.IsFalse(pair.Value.Equals(games[1]));
        }
    }
    
    /// <summary>
    /// Удаление первого элемента в цепочке
    /// </summary>
    [Test]
    public void TestRemoveFirstElementInChain()
    {
        var table = new MyHashTable<Game, Game>();
        
        var games = new Game[]
        {
            new Game("1", 1, 1, new Game.IdNumber(0)),
            new Game("2", 1, 1, new Game.IdNumber(10)),
            new Game("3", 1, 1, new Game.IdNumber(20))
        };
        
        table.Set(games[0], games[0]);
        table.Set(games[1], games[1]);
        table.Set(games[2], games[2]);
        
        Assert.IsTrue(table.Remove(games[0]));
        Assert.IsTrue(table.Count == 2);

        foreach (var pair in table)
        {
            Assert.IsFalse(pair.Key.Equals(games[0]));
            Assert.IsFalse(pair.Value.Equals(games[0]));
        }
    }
    
    /// <summary>
    /// Удаление последнего элемента в цепочке
    /// </summary>
    [Test]
    public void TestRemoveLastElementInChain()
    {
        var table = new MyHashTable<Game, Game>();
        
        var games = new Game[]
        {
            new Game("1", 1, 1, new Game.IdNumber(0)),
            new Game("2", 1, 1, new Game.IdNumber(10)),
            new Game("3", 1, 1, new Game.IdNumber(20))
        };
        
        table.Set(games[0], games[0]);
        table.Set(games[1], games[1]);
        table.Set(games[2], games[2]);
        
        Assert.IsTrue(table.Remove(games[2]));
        Assert.IsTrue(table.Count == 2);

        foreach (var pair in table)
        {
            Assert.IsFalse(pair.Key.Equals(games[2]));
            Assert.IsFalse(pair.Value.Equals(games[2]));
        }
    }
    
    /// <summary>
    /// Удаление несуществующего элемента в существующем цепочке
    /// </summary>
    [Test]
    public void TestRemoveNotExistingElementInExistingChain()
    {
        var table = new MyHashTable<Game, Game>();
        
        var games = new Game[]
        {
            new Game("1", 1, 1, new Game.IdNumber(0)),
            new Game("2", 1, 1, new Game.IdNumber(10)),
            new Game("3", 1, 1, new Game.IdNumber(20))
        };
        
        table.Set(games[0], games[0]);
        table.Set(games[1], games[1]);
        table.Set(games[2], games[2]);
        
        Assert.IsFalse(table.Remove(new Game("4", 1, 1, new Game.IdNumber(30))));
        Assert.IsTrue(table.Count == 3);

        int c = 0;
        foreach (var pair in table)
        {
            c++;
        }
        
        Assert.IsTrue(c == 3);
    }
    
    /// <summary>
    /// Удаление несуществующего элемента в несуществующем цепочке
    /// </summary>
    [Test]
    public void TestRemoveNotExistingElementInNotExistingChain()
    {
        var table = new MyHashTable<Game, Game>();
        
        var games = new Game[]
        {
            new Game("1", 1, 1, new Game.IdNumber(0)),
            new Game("2", 1, 1, new Game.IdNumber(10)),
            new Game("3", 1, 1, new Game.IdNumber(20))
        };
        
        table.Set(games[0], games[0]);
        table.Set(games[1], games[1]);
        table.Set(games[2], games[2]);
        
        Assert.IsFalse(table.Remove(new Game("4", 1, 1, new Game.IdNumber(31))));
        Assert.IsTrue(table.Count == 3);

        int c = 0;
        foreach (var pair in table)
        {
            c++;
        }
        
        Assert.IsTrue(c == 3);
    }
    
    /// <summary>
    /// Попробовать получить несуществующий элемент в существующем цепочке
    /// </summary>
    [Test]
    public void TestGetNotExistingElementInExistingChain()
    {
        var table = new MyHashTable<Game, Game>();
        var game = new Game("1", 1, 1, new Game.IdNumber(0));
        table.Set(game, game);
        Assert.IsFalse(table.TryGetValue(new Game("1", 1, 1, new Game.IdNumber(10)), out var _));
    }
    
    /// <summary>
    /// Попробовать получить несуществующий элемент в несуществующем цепочке
    /// </summary>
    [Test]
    public void TestGetNotExistingElementInNotExistingChain()
    {
        var table = new MyHashTable<Game, Game>();
        var game = new Game("1", 1, 1, new Game.IdNumber(0));
        table.Set(game, game);
        Assert.IsFalse(table.TryGetValue(new Game("1", 1, 1, new Game.IdNumber(11)), out var _));
    }
    
    /// <summary>
    /// Попробовать получить существующий элемент в существующем цепочке
    /// </summary>
    [Test]
    public void TestGetExistingElementInExistingChain()
    {
        var table = new MyHashTable<Game, Game>();
        var game = new Game("1", 1, 1, new Game.IdNumber(0));
        table.Set(game, game);
        Assert.IsTrue(table.TryGetValue(game, out var _value));
        Assert.IsTrue(_value.Equals(game));
    }
    
    /// <summary>
    /// Тестирование энумератора, когда все цепочкы пусты
    /// </summary>
    [Test]
    public void TestEnumeratorEmptyHashtable()
    {
        var table = new MyHashTable<Game, Game>();
        int iterations = 0;
        foreach (KeyValuePair<Game, Game> pair in table)
        {
            iterations++;
        }
        Assert.IsTrue(iterations == 0);
    }
    
    /// <summary>
    /// Тестирование энумератора, когда есть только один цепочк
    /// </summary>
    [Test]
    public void TestEnumeratorOneChain()
    {
        var table = new MyHashTable<Game, Game>();
        var game = new Game("1", 1, 1, new Game.IdNumber(0));
        table.Set(game, game);
        int iterations = 0;
        foreach (KeyValuePair<Game, Game> pair in table)
        {
            Assert.IsTrue(game.Equals(pair.Value));
            Assert.IsTrue(game.Equals(pair.Key));
            iterations++;
        }
        Assert.IsTrue(iterations == 1);
    }
    
    /// <summary>
    /// Тестирование энумератора, когда есть много цепочек
    /// </summary>
    [Test]
    public void TestEnumeratorManyChains()
    {
        var table = new MyHashTable<Game, Game>();
        
        var games = new Game[]
        {
            new Game("1", 1, 1, new Game.IdNumber(0)),
            new Game("2", 1, 1, new Game.IdNumber(11)),
            new Game("3", 1, 1, new Game.IdNumber(22))
        };
        
        table.Set(games[0], games[0]);
        table.Set(games[1], games[1]);
        table.Set(games[2], games[2]);
        int iterations = 0;
        foreach (KeyValuePair<Game, Game> pair in table)
        {
            Assert.IsTrue(games[iterations].Equals(pair.Value));
            Assert.IsTrue(games[iterations].Equals(pair.Key));
            iterations++;
        }
        Assert.IsTrue(iterations == 3);
    }
    
    /// <summary>
    /// Тестирование энумератора, когда в цепочке много элементов
    /// </summary>
    [Test]
    public void TestEnumeratorManyItemsInChain()
    {
        var table = new MyHashTable<Game, Game>();
        
        var games = new Game[]
        {
            new Game("1", 1, 1, new Game.IdNumber(0)),
            new Game("2", 1, 1, new Game.IdNumber(10)),
            new Game("3", 1, 1, new Game.IdNumber(20))
        };
        
        table.Set(games[0], games[0]);
        table.Set(games[1], games[1]);
        table.Set(games[2], games[2]);
        int iterations = 0;
        foreach (KeyValuePair<Game, Game> pair in table)
        {
            Assert.IsTrue(games[iterations].Equals(pair.Value));
            Assert.IsTrue(games[iterations].Equals(pair.Key));
            iterations++;
        }
        Assert.IsTrue(iterations == 3);
    }
    
    /// <summary>
    /// Тестирование вывода пустой таблицы
    /// </summary>
    [Test]
    public void TestPrintEmptyHashtable()
    {
        var table = new MyHashTable<Game, Game>();
        table.Print();
    }
    
    /// <summary>
    /// Тестирование вывода непустой таблицы
    /// </summary>
    [Test]
    public void TestPrintNotEmptyHashtable()
    {
        var table = new MyHashTable<Game, Game>();
        var game = new Game("1", 1, 1, new Game.IdNumber(0));
        table.Set(game, game);
        table.Print();
    }
    
    
}