using l10;

namespace lab12.List;

/// <summary>
/// Собственная реализация связного списка
/// </summary>
/// <typeparam name="T"></typeparam>
public class MyList<T> : ICloneable where T : ICloneable
{
    /// <summary>
    /// Голова, указывающая на начало списка
    /// </summary>
    public ListNode<T>? Start { get; private set; }
    
    /// <summary>
    /// Голово, указывающая на конец списка
    /// </summary>
   public ListNode<T>? End { get; private set; }

    /// <summary>
    /// Текущее количество элементов в списке
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public MyList()
    {
        Count = 0;
        Start = null;
        End = null;
    }

    
    /// <summary>
    /// Создать список, скопировав элементы из коллекции (глубокое копирование всех элементов)
    /// </summary>
    /// <param name="collection2Copy">Коллекция для клонирования</param>
    public MyList(IEnumerable<T> collection2Copy)
    {
        if (collection2Copy == null)
        {
            throw new NullReferenceException("Ошибка. Клонируемая коллекция = null!");
        }
        
        foreach (var item2Copy in collection2Copy)
        {
            AddItemEnd(item2Copy);
        }
    }

    /// <summary>
    /// Добавляет новый элемент в начало списка, данные элемента клонируются
    /// </summary>
    /// <param name="item">Добавляемый элемент</param>
    public void AddItemStart(T item)
    {
        if (item == null)
        {
            throw new NullReferenceException("Значение добавляемого элемента не может быть null!");
        }
        var clonedItem = (T) item.Clone();
        var node = new ListNode<T>(clonedItem);

        //список пуст, вставляем единственный элемент
        if (Start == null)
        {
            Start = node;
            End = node;
        }
        else
        {
            Start.Prev = node;
            node.Next = Start;
            Start = node;
        }
        
        Count++;
    }

    /// <summary>
    /// Добавляет новый элемент в конец списка, данные элемента клонируются
    /// </summary>
    /// <param name="item">Добавляемый элемент</param>
    public void AddItemEnd(T item)
    {
        if (item == null)
        {
            throw new NullReferenceException("Значение добавляемого элемента не может быть null!");
        }
        var clonedItem = (T)item.Clone();
        var node = new ListNode<T>(clonedItem);

        //значит список пуст
        if (End == null)
        {
            Start = node;
            End = node;
        }
        else
        {
            End.Next = node;
            node.Prev = End;
            End = node;
        }
        
        Count++;
    }

    /// <summary>
    /// Поиск элемента в списке
    /// </summary>
    /// <param name="comparer">Компаратор для поиска двух элементов</param>
    /// <param name="reversed">Выполнить поиск с конца</param>
    /// <returns>null, если не нашли. Ноду списка - если нашли</returns>
    /// <exception cref="NullReferenceException">Если вдруг элемент списка равен null</exception>
    public ListNode<T> FindItem(Func<T, bool> comparer, bool reversed = false)
    {
        if (comparer == null)
        {
            throw new NullReferenceException("Функция компаратор не может быть null!");
        }
        
        ListNode<T> current = reversed ? End : Start;
        while (current != null)
        {
            if (current.Data == null)
            {
                throw new NullReferenceException("Данные элемента списка равны null!");
            }
            if (comparer(current.Data)) return current;
            current = reversed ? current.Prev : current.Next;
        }
        
        //не нашли
        return null;
    }

    /// <summary>
    /// Удаляет элемент по значению из списка
    /// </summary>
    /// <param name="comparer">Компаратор для поиска двух элементов</param>
    /// <param name="reversed">Выполнить поиск с конца</param>
    /// <returns>True - если элемент был успешно удален</returns>
    public bool RemoveItem(Func<T, bool> comparer, bool reversed = false)
    {
        if (Start == null)
        {
            return false;
        }

        var foundItem = FindItem(comparer, reversed);
        if (foundItem == null) return false;

        Count--;
        
        //значит удаляем единственный
        if (ReferenceEquals(Start, End))
        {
            Start = End = null;
            return true;
        }

        //значит удаляем первый элемент
        if (foundItem.Prev == null)
        {
            Start = Start.Next;
            Start.Prev = null;
            return true;
        }

        //значит удаляем последний элемент
        if (foundItem.Next == null)
        {
            End = End.Prev;
            End.Next = null;
            return true;
        }

        //серединный случай
        var prev = foundItem.Prev;
        var next = foundItem.Next;

        prev.Next = next;
        next.Prev = prev;

        return true;
    }

    /// <summary>
    /// Вывод на печать элементы списка с начала
    /// </summary>
    public void Print()
    {
        if (Count == 0)
        {
            Console.Write("Список пуст.");
            return;
        }
        ListNode<T>? current = Start;
        while (current != null)
        {
            if (current.Data == null)
            {
                throw new NullReferenceException("Итерируемый элемент равен null!");
            }
            Console.Write(current.ToString());
            current = current.Next;
        }
    }

    /// <summary>
    /// Клонирует список, глубоко клонируя каждый из элементов
    /// </summary>
    /// <returns>возвращает новый список</returns>
    public object Clone()
    {
        var newList = new MyList<T>();
        ListNode<T>? current = Start;

        while (current != null)
        {
            if (current.Data == null)
            {
                throw new NullReferenceException("Итерируемый элемент при клонировании оказался равен null!");
            }
            newList.AddItemEnd(current.Data);
            current = current.Next;
        }

        return newList;
    }

    public void Clear()
    {
        Start = null;
        End = null;
        Count = 0;
    }
}