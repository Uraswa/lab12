using l10;
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
}