using System.Reflection;
using l10;
using lab12.List;

namespace TestProject1;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestDeleteFromEmpty()
    {
        var emptyList = new MyList<Game>();
        Assert.IsFalse(emptyList.RemoveItem((g) => g.Name == "123"));
    }
    
    [Test]
    public void TestDeleteNonExistingElement()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));
        Assert.IsFalse(list.RemoveItem((g) => g.Name == "fourth"));
    }
    
    [Test]
    public void TestDeleteFirstElement()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game());
        
        Assert.IsTrue(list.RemoveItem((g) => g.Name == "first"));
        Assert.IsTrue(list.Start.Data.Name == "second");
        Assert.IsTrue(list.Count == 2);
    }
    
    [Test]
    public void TestDeleteLastElement()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game());
        list.AddItemEnd(new Game("prelast", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("last", 2, 4, new Game.IdNumber()));
        
        Assert.IsTrue(list.RemoveItem((g) => g.Name == "last"));
        Assert.IsTrue(list.End.Data.Name == "prelast");
        Assert.IsTrue(list.Count == 2);
    }
    
    [Test]
    public void TestDeleteMiddleElement()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));
        
        Assert.IsTrue(list.RemoveItem((g) => g.Name == "second"));
        Assert.IsTrue(list.Count == 2);
        Assert.IsTrue(list.Start.Next.Data.Name == "third");
        Assert.IsTrue(list.End.Prev.Data.Name == "first");
    }
    
    
    [Test]
    public void AddToStartNull()
    {
        var list = new MyList<Game>();
        Assert.Throws<NullReferenceException>(() => list.AddItemStart(null));
    }
    
    [Test]
    public void AddToStartEmptyList()
    {
        var list = new MyList<Game>();
        list.AddItemStart(new Game("first", 2, 4, new Game.IdNumber()));
        
        Assert.IsTrue(list.Start.Data.Name == "first");
        Assert.IsTrue(list.End.Data.Name == "first");
    }
    
    [Test]
    public void AddToStartNotEmptyList()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemStart(new Game("first", 2, 4, new Game.IdNumber()));
        
        Assert.IsTrue(list.Start.Data.Name == "first");
        Assert.IsTrue(list.Start.Next.Data.Name == "second");
        Assert.IsTrue(list.End.Data.Name == "second");
    }
    
    [Test]
    public void AddToEndNull()
    {
        var list = new MyList<Game>();
        Assert.Throws<NullReferenceException>(() =>list.AddItemEnd(null));
    }
    
    [Test]
    public void AddToEndEmptyList()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        
        Assert.IsTrue(list.Start.Data.Name == "first");
        Assert.IsTrue(list.End.Data.Name == "first");
    }
    
    [Test]
    public void AddToEndNotEmptyList()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));

        Assert.IsTrue(list.Start.Next.Data.Name == "second");
        Assert.IsTrue(list.End.Data.Name == "second");
    }
    
    [Test]
    public void TestPrint()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));
        
        list.Print();
    }
    
    [Test]
    public void TestPrintElementNull()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));

        list.Start.Data = null;
        Assert.Throws<NullReferenceException>(() => list.Print());
    }
    
    [Test]
    public void TestConstructorCopyNull()
    {
        Assert.Throws<NullReferenceException>(() => new MyList<Game>(null));
    }
    
    [Test]
    public void TestConstructorCopyArray()
    {
        var array2copy = new Game[]
        {
            new Game("first", 2, 4, new Game.IdNumber()),
            new Game("second", 2, 4, new Game.IdNumber()),
            new Game("third", 2, 4, new Game.IdNumber()),
        };

        var list = new MyList<Game>(array2copy);
        
        Assert.IsTrue(list.Count == 3);
        Assert.IsTrue(list.Start.Data.Name == "first");
        Assert.IsTrue(list.Start.Next.Data.Name == "second");
        Assert.IsTrue(list.End.Prev.Data.Name == "second");
        Assert.IsTrue(list.End.Data.Name == "third");
    }
    
    [Test]
    public void TestEmptyConstructor()
    {
        var list = new MyList<Game>();
        
        Assert.IsTrue(list.Count == 0);
        Assert.IsTrue(list.Start == null);
        Assert.IsTrue(list.End == null);
    }
    
    [Test]
    public void TestFindNotExisting()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));
        
        Assert.IsNull(list.FindItem((g) => g.Name == "123"));
    }
    
    [Test]
    public void TestFindInEmpty()
    {
        var list = new MyList<Game>();
        Assert.IsNull(list.FindItem((g) => g.Name == "123"));
    }
    
    [Test]
    public void TestFindNullComparer()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));

        Assert.Throws<NullReferenceException>(() => list.FindItem(null));
    }
    
    [Test]
    public void TestFindElement()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));
        
        Assert.NotNull(list.FindItem((g) => g.Name == "second"));
    }

    /// <summary>
    /// Каким то образом данные были повреждены. Как код отреагирует на это.
    /// </summary>
    [Test]
    public void TestFindWhenDataSabotaged()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));

        list.Start.Next.Data = null;
        Assert.Throws<NullReferenceException>(() => list.FindItem((g) => g.Name == "third"));
    }
    
    [Test]
    public void TestClone()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));

        var cloned = (MyList<Game>)list.Clone();
        Assert.IsFalse(ReferenceEquals(list, cloned));
        
        Assert.IsFalse(ReferenceEquals(list.Start, cloned.Start));
        Assert.IsFalse(ReferenceEquals(list.End, cloned.End));
        Assert.IsFalse(ReferenceEquals(list.End.Prev, cloned.End.Prev));
        
        Assert.IsFalse(ReferenceEquals(list.Start.Data, cloned.Start.Data));
        Assert.IsFalse(ReferenceEquals(list.End.Data, cloned.End.Data));
        Assert.IsFalse(ReferenceEquals(list.End.Prev.Data, cloned.End.Prev.Data));
        
        Assert.IsTrue(list.Start.Data.Equals(cloned.Start.Data));
        Assert.IsTrue(list.End.Data.Equals(cloned.End.Data));
        Assert.IsTrue(list.End.Prev.Data.Equals(cloned.End.Prev.Data));
    }
    
    [Test]
    public void TestCloneElementNull()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));

        list.Start.Data = null;
        Assert.Throws<NullReferenceException>(() => list.Clone());
    }

    [Test]
    public void TestClear()
    {
        var list = new MyList<Game>();
        list.AddItemEnd(new Game("first", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("second", 2, 4, new Game.IdNumber()));
        list.AddItemEnd(new Game("third", 3, 4, new Game.IdNumber()));
        
        list.Clear();
        Assert.IsTrue(list.Count == 0);
        Assert.IsTrue(list.End == null);
        Assert.IsTrue(list.Start == null);
    }
}