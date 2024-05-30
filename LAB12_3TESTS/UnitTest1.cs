using l10;
using LAB12_3.AVL_TREE;
using LAB12_3.ISD;

namespace LAB12_3TESTS;

public class Tests
{
    [Test]
    public void TestISDTreeEmptyArrayConstructor1()
    {
        var tree = new IsdTree<Game>(null);
        Assert.IsTrue(tree.Count == 0);
    }
    
    [Test]
    public void TestISDTreeEmptyArrayConstructor2()
    {
        var tree = new IsdTree<Game>(new Game[0]);
        Assert.IsTrue(tree.Count == 0);
    }
    
    [Test]
    public void TestISDTreeConstructorOneElement()
    {
        var games = new Game[]
        {
            new("g1", 1, 2, new Game.IdNumber())
        };
        
        var tree = new IsdTree<Game>(games);
        Assert.IsTrue(tree.Count == 1);

        int i = 0;
        foreach (var g in tree.FindElements((e) => true))
        {
            Assert.That(g, Is.EqualTo(games[i]));
            i++;
        }
    }
    
    [Test]
    public void TestISDTreeConstructorTwoElements()
    {
        var games = new Game[]
        {
            new("g1", 1, 2, new Game.IdNumber()),
            new("g2", 1, 2, new Game.IdNumber())
        };
        
        var tree = new IsdTree<Game>(games);
        Assert.IsTrue(tree.Count == 2);

        var output = tree.FindElements((e) => true).ToArray();
        Assert.IsTrue(output[0].Equals(games[1]));
        Assert.IsTrue(output[1].Equals(games[0]));
        
    }
    
    [Test]
    public void TestISDTreeConstructorThreeElements()
    {
        var games = new Game[]
        {
            new("g1", 1, 2, new Game.IdNumber()),
            new("g2", 1, 2, new Game.IdNumber()),
            new("g3", 1, 2, new Game.IdNumber()),
        };
        
        var tree = new IsdTree<Game>(games);
        Assert.IsTrue(tree.Count == 3);

        var output = tree.FindElements((e) => true).ToArray();
        Assert.IsTrue(output[0].Equals(games[1]));
        Assert.IsTrue(output[1].Equals(games[0]));
        Assert.IsTrue(output[2].Equals(games[2]));
        
    }
    
    [Test]
    public void TestISDConstructorNullValue()
    {
        var games = new Game[]
        {
            null
        };
        
        var tree = new IsdTree<Game>(games);
        Assert.IsTrue(tree.Count == 1);

        var output = tree.FindElements((e) => true).ToArray();
        Assert.IsTrue(output[0] == null);
    }
    
    [Test]
    public void TestPrintEmpty()
    {
        var tree = new IsdTree<Game>(null);
        tree.PrintTree();
    }
    
    [Test]
    public void TestPrintOneLayer()
    {
        var games = new Game[]
        {
            new("g1", 1, 2, new Game.IdNumber()),
        };
        var tree = new IsdTree<Game>(games);
        tree.PrintTree();
    }
    
    [Test]
    public void TestPrintTwoLayers()
    {
        var games = new Game[]
        {
            new("g1", 1, 2, new Game.IdNumber()),
            new("g2", 1, 2, new Game.IdNumber()),
            new("g3", 1, 2, new Game.IdNumber()),
        };
        var tree = new IsdTree<Game>(games);
        tree.PrintTree();
    }

    [Test]
    public void TestEmptyFind()
    {
        var tree = new IsdTree<Game>(null);

        var output = tree.FindElements(e => true).ToArray();
        Assert.IsTrue(output.Length == 0);
    }

    [Test]
    public void TestClear()
    {
        var tree = new IsdTree<Game>(null);
        tree.Clear();
        Assert.IsTrue(tree.Count == 0);
    }

    [Test]
    public void TestCloning()
    {
        var games = new Game[]
        {
            new("g1", 1, 2, new Game.IdNumber())
        };

        var tree = new IsdTree<Game>(games);
        var output = tree.FindElements(g => true).ToArray();
        
        Assert.IsFalse(ReferenceEquals(games[0], output[0]));
        Assert.IsFalse(ReferenceEquals(games[0].Id, output[0].Id));
        Assert.IsTrue(games[0].Equals(output[0]));
    }
    
    // AVL TREE TESTS

    [Test]
    public void TestAVLPrintEmpty()
    {
        var tree = new AvlTree<Game>();
        tree.PrintTree();
    }
    
    [Test]
    public void TestAVLClear()
    {
        var tree = new AvlTree<Game>();
        tree.Clear();
    }

    [Test]
    public void TestAVLEnumerableEmpty()
    {
        var tree = new AvlTree<Game>();
        int iterationsCount = 0;
        foreach (var val in tree.GetValues())
        {
            iterationsCount++;
        }

        Assert.IsTrue(iterationsCount == 0);
    }

    [Test]
    public void TestAVLOneElement()
    {
        var tree = new AvlTree<Game>();

        var game0 = new Game();
        game0.RandomInit();
        
        tree.Insert(game0);
        
        Assert.IsTrue(tree.Count == 1);
        Assert.IsTrue(tree.GetValues().ToArray()[0].Equals(game0));
    }
    
    /// <summary>
    /// Проверка, что два элемента правильно позиционируются относительно
    /// друг друга 
    /// </summary>
    [Test]
    public void TestAVLTwoElementsSecondIsLower()
    {
        var tree = new AvlTree<Game>();

        var game0 = new Game();
        game0.RandomInit();

        var game1 = new Game();
        game1.RandomInit();
        game0.Name = "";
        
        tree.Insert(game0);
        tree.Insert(game1);
        
        Assert.IsTrue(tree.Count == 2);
        Assert.IsTrue(tree.GetValues().ToArray()[0].Equals(game0));
        Assert.IsTrue(tree.GetValues().ToArray()[1].Equals(game1));
    }
    
    /// <summary>
    /// Проверка, что два элемента правильно позиционируются относительно
    /// друг друга 
    /// </summary>
    [Test]
    public void TestAVLTwoElementsSecondIsBigger()
    {
        var tree = new AvlTree<Game>();

        var game0 = new Game();
        game0.RandomInit();
        game0.Name = "";

        var game1 = new Game();
        game1.RandomInit();
        
        tree.Insert(game0);
        tree.Insert(game1);
        
        Assert.IsTrue(tree.Count == 2);
        Assert.IsTrue(tree.GetValues().ToArray()[0].Equals(game0));
        Assert.IsTrue(tree.GetValues().ToArray()[1].Equals(game1));
    }

    [Test]
    public void TestAVLDuplicate()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber(1)),
            new Game("1", 1, 2, new Game.IdNumber(1)),
        };

        var tree = new AvlTree<Game>();
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        
        Assert.IsTrue(tree.Count == 1);
    }
    
    //ТЕСТИРОВАНИЕ ВРАЩЕНИЙ

    [Test]
    public void TestAVLRotationLeftLeft()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
            new Game("3", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);

        var output = tree.GetValues().ToArray();
        
        Assert.IsTrue(tree.Count == 3);
        Assert.IsTrue(output[0].Equals(games[1]));
        Assert.IsTrue(output[2].Equals(games[2]));
        Assert.IsTrue(output[1].Equals(games[0]));
    }
    
    [Test]
    public void TestAVLRotationRightRight()
    {
        var games = new Game[]
        {
            new Game("2", 1, 2, new Game.IdNumber()),
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("0", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);

        var output = tree.GetValues().ToArray();
        
        Assert.IsTrue(tree.Count == 3);
        Assert.IsTrue(output[0].Equals(games[1]));
        Assert.IsTrue(output[1].Equals(games[2]));
        Assert.IsTrue(output[2].Equals(games[0]));
    }

    [Test]
    public void TestAVLRotationRightLeft()
    {
        var games = new Game[]
        {
            new Game("4", 1, 2, new Game.IdNumber()),
            new Game("6", 1, 2, new Game.IdNumber()),
            new Game("5", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);

        var output = tree.GetValues().ToArray();
        
        Assert.IsTrue(tree.Count == 3);
        Assert.IsTrue(output[0].Equals(games[2]));
        Assert.IsTrue(output[1].Equals(games[0]));
        Assert.IsTrue(output[2].Equals(games[1]));
    }
    
    [Test]
    public void TestAVLRotationLeftRight()
    {
        var games = new Game[]
        {
            new Game("2", 1, 2, new Game.IdNumber()),
            new Game("0", 1, 2, new Game.IdNumber()),
            new Game("1", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);

        var output = tree.GetValues().ToArray();
        
        Assert.IsTrue(tree.Count == 3);
        Assert.IsTrue(output[0].Equals(games[2]));
        Assert.IsTrue(output[1].Equals(games[1]));
        Assert.IsTrue(output[2].Equals(games[0]));
    }

    [Test]
    public void TestAVLMultipleRotations()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
            new Game("3", 1, 2, new Game.IdNumber()),
            new Game("4", 1, 2, new Game.IdNumber()),
            new Game("5", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);
        tree.Insert(games[3]);
        tree.Insert(games[4]);

        var output = tree.GetValues().ToArray();
        
        Assert.IsTrue(tree.Count == 5);
        Assert.IsTrue(output[0].Equals(games[1]));
        Assert.IsTrue(output[1].Equals(games[0]));
        Assert.IsTrue(output[2].Equals(games[3]));
        Assert.IsTrue(output[3].Equals(games[2]));
        Assert.IsTrue(output[4].Equals(games[4]));
    }

    [Test]
    public void TestAVLPrintNotEmpty()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        
        tree.PrintTree();
    }

    [Test]
    public void TestAVLFindByValueNotFound()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);

        tree.FindByValue(new Game());
    }
    
    [Test]
    public void TestAVLFindByValueFound()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);

        tree.FindByValue(games[1]);
    }

    [Test]
    public void TestAVLRemoveByValueNotFound()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);

        Assert.IsFalse(tree.Remove(new Game()));
        Assert.IsTrue(tree.Count == 1);
    }
    
    [Test]
    public void TestAVLRemoveByValueFound()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);

        Assert.IsTrue(tree.Remove(games[0]));
        Assert.IsTrue(tree.Count == 0);
    }
    
    [Test]
    public void TestAVLRemoveByValueFoundFatherNodeLeftChildExist()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("0", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);

        Assert.IsTrue(tree.Remove(games[0]));
        Assert.IsTrue(tree.Count == 1);
    }
    
    [Test]
    public void TestAVLRemoveByValueFoundFatherNodeRightChildExist()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);

        Assert.IsTrue(tree.Remove(games[0]));
        Assert.IsTrue(tree.Count == 1);
    }
    
    [Test]
    public void TestAVLRemoveByValueFoundRightChild()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);

        Assert.IsTrue(tree.Remove(games[1]));
        Assert.IsTrue(tree.Count == 1);
    }
    
    [Test]
    public void TestAVLRemoveByValueFoundFatherNodeBothChildExist()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
            new Game("3", 1, 2, new Game.IdNumber()),
            new Game("4", 1, 2, new Game.IdNumber()),
            new Game("5", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);
        tree.Insert(games[3]);
        tree.Insert(games[4]);

        Assert.IsTrue(tree.Remove(games[1]));
        Assert.IsTrue(tree.Count == 4);
        
        var output = tree.GetValues().ToArray();
        Assert.IsTrue(output[0].Equals(games[2]));
    }
    
    [Test]
    public void TestAVLRemoveByValueFoundLeftLeftRotation()
    {
        var games = new Game[]
        {
            new Game("1", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
            new Game("3", 1, 2, new Game.IdNumber()),
            new Game("4", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);
        tree.Insert(games[3]);

        Assert.IsTrue(tree.Remove(games[0]));
        Assert.IsTrue(tree.Count == 3);
        
        var output = tree.GetValues().ToArray();
        Assert.IsTrue(output[0].Equals(games[2]));
    }
    
    [Test]
    public void TestAVLRemoveByValueFoundRightRightRotation()
    {
        var games = new Game[]
        {
            new Game("3", 1, 2, new Game.IdNumber()),
            new Game("5", 1, 2, new Game.IdNumber()),
            new Game("2", 1, 2, new Game.IdNumber()),
            new Game("1", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);
        tree.Insert(games[3]);

        Assert.IsTrue(tree.Remove(games[1]));
        Assert.IsTrue(tree.Count == 3);
        
        var output = tree.GetValues().ToArray();
        Assert.IsTrue(output[0].Equals(games[2]));
    }
    
    [Test]
    public void TestAVLRemoveByValueFoundLeftRightRotation()
    {
        var games = new Game[]
        {
            new Game("2", 1, 2, new Game.IdNumber()),
            new Game("3", 1, 2, new Game.IdNumber()),
            new Game("0", 1, 2, new Game.IdNumber()),
            new Game("1", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);
        tree.Insert(games[3]);

        Assert.IsTrue(tree.Remove(games[1]));
        Assert.IsTrue(tree.Count == 3);
        
        var output = tree.GetValues().ToArray();
        Assert.IsTrue(output[0].Equals(games[3]));
    }
    
    [Test]
    public void TestAVLRemoveByValueFoundRightLeftRotation()
    {
        var games = new Game[]
        {
            new Game("3", 1, 2, new Game.IdNumber()),
            new Game("0", 1, 2, new Game.IdNumber()),
            new Game("5", 1, 2, new Game.IdNumber()),
            new Game("4", 1, 2, new Game.IdNumber()),
        };

        var tree = new AvlTree<Game>();
        
        tree.Insert(games[0]);
        tree.Insert(games[1]);
        tree.Insert(games[2]);
        tree.Insert(games[3]);

        Assert.IsTrue(tree.Remove(games[1]));
        Assert.IsTrue(tree.Count == 3);
        
        var output = tree.GetValues().ToArray();
        Assert.IsTrue(output[0].Equals(games[3]));
    }
    
    
}