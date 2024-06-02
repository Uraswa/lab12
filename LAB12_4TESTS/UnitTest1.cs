using l10;
using LAB12_4.AvlTreeNET;
using Laba10;

namespace LAB12_4TESTS;

public class Tests
{
    [Test]
    public void TestEmptyConstructor()
    {
        var avl = new AvlTreeNet<Game>();
        
        Assert.IsTrue(avl.Count == 0);
    }

    [Test] 
    public void TestSizeConstructorLessZero()
    {
        Assert.Catch<ArgumentException>(() => new AvlTreeNet<Game>(-1));
    }
    
    [Test] 
    public void TestSizeConstructorZero()
    {
        var avl = new AvlTreeNet<Game>(0);
        Assert.IsTrue(avl.Count == 0);
    }
    
    [Test] 
    public void TestSizeConstructorOnePlus()
    {
        var avl = new AvlTreeNet<Game>(3);
        Assert.IsTrue(avl.Count == 3);
    }

    [Test]
    public void TestCloneEmpty()
    {
        var avl = new AvlTreeNet<Game>();
        var avl2 = new AvlTreeNet<Game>(avl);
        
        Assert.IsTrue(avl2.Count == 0);
    }
    
    [Test]
    public void TestCloneNotEmpty()
    {
        var avl = new AvlTreeNet<Game>(3);
        var avl2 = new AvlTreeNet<Game>(avl);

        var values1 = avl.GetValues().ToArray();
        var values2 = avl2.GetValues().ToArray();
        
        Assert.IsTrue(avl2.Count == avl.Count);

        for (int i = 0; i < avl.Count; i++)
        {
            Assert.IsTrue(values1[i].Equals(values2[i]));
            Assert.IsFalse(ReferenceEquals(values1[i], values2[i]));
        }
    }

    /// <summary>
    /// Проверка с производными классами
    /// </summary>
    [Test]
    public void TestContainsFalseDerivativeClass()
    {
        var avl = new AvlTreeNet<Game>(3);

        var first = avl.GetValues().ToArray()[0];
        var videoGame = new VideoGame(first.Name, first.MinimumPlayers, first.MaximumPlayers, first.Id, Device.Console,
            10);
        
        Assert.IsFalse(avl.Contains(videoGame));
    }
    
    /// <summary>
    /// Проверка с текущим классом поиск false
    /// </summary>
    [Test]
    public void TestContainsFalseCurrentClass()
    {
        var avl = new AvlTreeNet<Game>(3);

        var first = avl.GetValues().ToArray()[0];
        var game = new Game(first.Name + "test", first.MinimumPlayers, first.MaximumPlayers, first.Id);
        
        Assert.IsFalse(avl.Contains(game));
    }
    
    /// <summary>
    /// Проверка с текущим классом нахождение = true
    /// </summary>
    [Test]
    public void TestContainsTrueCurrentClass()
    {
        var avl = new AvlTreeNet<Game>(3);
        var first = avl.GetValues().ToArray()[0];
        Assert.IsTrue(avl.Contains(first));
    }
    
    /// <summary>
    /// Проверка с текущим классом, не первый элемент
    /// </summary>
    [Test]
    public void TestContainsTrueCurrentClassNotFirst()
    {
        var avl = new AvlTreeNet<Game>(3);
        var second = avl.GetValues().ToArray()[1];
        Assert.IsTrue(avl.Contains(second));
    }

    [Test]
    public void TestEnumerator()
    {
        var avl = new AvlTreeNet<Game>(3);
        var testedValues = avl.GetValues().ToArray();

        int index = 0;
        foreach (var game in avl)
        {
            Assert.IsTrue(game.Equals(testedValues[index]));
            index++;
        }
    }

    [Test]
    public void TestIsReadonly()
    {
        var avl = new AvlTreeNet<Game>();
        Assert.IsFalse(avl.IsReadOnly);
    }

    [Test]
    public void TestCopyToNull()
    {
        var avl = new AvlTreeNet<Game>(3);
        Assert.Catch<ArgumentException>(() => avl.CopyTo(null, 0));
    }
    
    [Test]
    public void TestCopyToNegativeIndexStart()
    {
        var avl = new AvlTreeNet<Game>(3);
        var arr = new Game[3];
        Assert.Catch<ArgumentException>(() => avl.CopyTo(arr, -1));
    }
    
    [Test]
    public void TestCopyToArraySizeLessThanTreeSize()
    {
        var avl = new AvlTreeNet<Game>(3);
        var arr = new Game[2];
        Assert.Catch<ArgumentException>(() => avl.CopyTo(arr, 0));
    }
    
    [Test]
    public void TestCopyToArrayFine()
    {
        var avl = new AvlTreeNet<Game>(3);
        var arr = new Game[3];
        avl.CopyTo(arr, 0);

        int i = 0;
        foreach (var game in avl)
        {
            Assert.IsTrue(game.Equals(arr[i]));
            i++;
        }
    }
}