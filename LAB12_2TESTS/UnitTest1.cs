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
        var table = new MyHashTable<int, int>();
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
            var hashtable = new MyHashTable<int, int>(1);
        });
    }
    
    /// <summary>
    ///  Тестирование конструктора с нормальным значением capacity
    /// </summary>
    [Test]
    public void TestHashtableConstructorFineCapacity()
    {
        var table = new MyHashTable<int, int>(15);
        Assert.IsTrue(table.Count == 0);
    }
    
    /// <summary>
    /// Добавление отрицательного ключа  в hashtable
    /// </summary>
    [Test]
    public void TestAddNehativeKeyElementToHashtable()
    {
        var table = new MyHashTable<int, int>();
        table.Set(-1, 1);
        Assert.IsTrue(table.Count == 1);
    }

    /// <summary>
    /// Добавление элемента в hashtable
    /// </summary>
    [Test]
    public void TestAddElementToHashtable()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        Assert.IsTrue(table.Count == 1);
    }
    
    /// <summary>
    /// Добавление дубликата в hashtable
    /// </summary>
    [Test]
    public void TestAddDuplicateToHashtable()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(0, 1);
        Assert.IsTrue(table.Count == 1);
    }
    
    /// <summary>
    /// Добавление другого элемента с тем же хеш кодом
    /// </summary>
    [Test]
    public void TestAddDifferentElementWithSameHashcode()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(10, 2);
        Assert.IsTrue(table.Count == 2);
    }
    
    /// <summary>
    /// Добавление другого элемента с другим хеш кодом
    /// </summary>
    [Test]
    public void TestAddDifferentElementWithDifferentHashcode()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(1, 2);
        Assert.IsTrue(table.Count == 2);
    }
    
    /// <summary>
    /// Удаление единственного элемента в цепочке.
    /// </summary>
    [Test]
    public void TestDeleteOnlyElementInChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Remove(0);
        Assert.IsTrue(table.Count == 0);
    }
    
    /// <summary>
    /// Удаление среднего элемента в цепочке
    /// </summary>
    [Test]
    public void TestRemoveMiddleElementInChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(10, 2);
        table.Set(20, 3);
        table.Remove(10);
        Assert.IsTrue(table.Count == 2);
    }
    
    /// <summary>
    /// Удаление первого элемента в цепочке
    /// </summary>
    [Test]
    public void TestRemoveFirstElementInChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(10, 2);
        table.Set(20, 3);
        table.Remove(0);
        Assert.IsTrue(table.Count == 2);
    }
    
    /// <summary>
    /// Удаление последнего элемента в цепочке
    /// </summary>
    [Test]
    public void TestRemoveLastElementInChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(10, 2);
        table.Set(20, 3);
        table.Remove(20);
        Assert.IsTrue(table.Count == 2);
    }
    
    /// <summary>
    /// Удаление несуществующего элемента в существующем цепочке
    /// </summary>
    [Test]
    public void TestRemoveNotExistingElementInExistingChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(10, 2);
        table.Set(20, 3);
        table.Remove(30);
        Assert.IsTrue(table.Count == 3);
    }
    
    /// <summary>
    /// Удаление несуществующего элемента в несуществующем цепочке
    /// </summary>
    [Test]
    public void TestRemoveNotExistingElementInNotExistingChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(10, 2);
        table.Set(20, 3);
        table.Remove(31);
        Assert.IsTrue(table.Count == 3);
    }
    
    /// <summary>
    /// Попробовать получить несуществующий элемент в существующем цепочке
    /// </summary>
    [Test]
    public void TestGetNotExistingElementInExistingChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        Assert.IsFalse(table.TryGetValue(10, out var _));
    }
    
    /// <summary>
    /// Попробовать получить несуществующий элемент в несуществующем цепочке
    /// </summary>
    [Test]
    public void TestGetNotExistingElementInNotExistingChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        Assert.IsFalse(table.TryGetValue(11, out var _));
    }
    
    /// <summary>
    /// Попробовать получить существующий элемент в существующем цепочке
    /// </summary>
    [Test]
    public void TestGetExistingElementInExistingChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        Assert.IsTrue(table.TryGetValue(0, out var _value));
        Assert.IsTrue(_value == 1);
    }
    
    /// <summary>
    /// Тестирование энумератора, когда все цепочкы пусты
    /// </summary>
    [Test]
    public void TestEnumeratorEmptyHashtable()
    {
        var table = new MyHashTable<int, int>();
        int iterations = 0;
        foreach (KeyValuePair<int,int> pair in table)
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
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        int iterations = 0;
        foreach (KeyValuePair<int,int> pair in table)
        {
            iterations++;
        }
        Assert.IsTrue(iterations == 1);
    }
    
    /// <summary>
    /// Тестирование энумератора, когда есть много цепочков
    /// </summary>
    [Test]
    public void TestEnumeratorManyChains()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(1, 1);
        int iterations = 0;
        foreach (KeyValuePair<int,int> pair in table)
        {
            iterations++;
        }
        Assert.IsTrue(iterations == 2);
    }
    
    /// <summary>
    /// Тестирование энумератора, когда в цепочке много элементов
    /// </summary>
    [Test]
    public void TestEnumeratorManyItemsInChain()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Set(10, 2);
        int iterations = 0;
        foreach (KeyValuePair<int,int> pair in table)
        {
            iterations++;
        }
        Assert.IsTrue(iterations == 2);
    }
    
    /// <summary>
    /// Тестирование вывода пустой таблицы
    /// </summary>
    [Test]
    public void TestPrintEmptyHashtable()
    {
        var table = new MyHashTable<int, int>();
        table.Print();
    }
    
    /// <summary>
    /// Тестирование вывода непустой таблицы
    /// </summary>
    [Test]
    public void TestPrintNotEmptyHashtable()
    {
        var table = new MyHashTable<int, int>();
        table.Set(0, 1);
        table.Print();
    }
    
    
}