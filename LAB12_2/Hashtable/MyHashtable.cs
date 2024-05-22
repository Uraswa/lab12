using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace LAB12_2.Hashtable;

/// <summary>
///  Класс, реализующий хеш таблицу методом цепочек.
/// </summary>
/// <typeparam name="TKey">Тип ключа</typeparam>
/// <typeparam name="TValue">Тип значения</typeparam>
public class MyHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    /// <summary>
    /// Тип цепочка
    /// </summary>
    private class Chain
    {
        /// <summary>
        /// Ключ и хранимое значение, типы данных такие же как в родительском классе.
        /// </summary>
        public KeyValuePair<TKey, TValue> KeyValue;

        /// <summary>
        ///  Ссылка на следующий элемент в цепочке
        /// </summary>
        public Chain? Next;

        public Chain(KeyValuePair<TKey, TValue> keyValue)
        {
            KeyValue = keyValue;
            Next = null;
        }
    }

    /// <summary>
    ///  Хеш таблица в своей основе использует массив фиксированной длины, по которой вычисляется индекс.
    /// после деления по модулю на длину массива значения хеш функции.
    /// </summary>
    private readonly Chain[] _chains;

    /// <summary>
    ///  Вместительность массива цепочек. Чем больше, тем меньше вероятность коллизии, но тем больше пустого
    /// места простаивает просто так.
    /// </summary>
    private readonly int _capacity;

    /// <summary>
    /// Количество элелементов во всех цепочках
    /// </summary>
    public int Count { get; private set; }

    public MyHashTable(int capacity = 10)
    {
        if (capacity < 2)
        {
            throw new ArgumentOutOfRangeException(
                "Вместимость хеш таблицы должна быть больше 1! Иначе использование хеш таблицы не имеет смысла!");
        }

        _capacity = capacity;
        _chains = new Chain[_capacity];
    }

    /// <summary>
    ///  Получение индекса цепочки в массиве Buckets.
    /// </summary>
    /// <param name="key">ключ от которого нужно посчитать хеш функцию.</param>
    /// <returns>Индекс цепочки, в которой находится ключ key</returns>
    private int GetIndex([NotNull] TKey key)
    {
        int hash = key.GetHashCode();
        return Math.Abs(hash % _chains.Length);
    }

    /// <summary>
    /// Установка элемента по ключу key. В случае нахождения дубликата, значение по ключу key будет перезаписано.
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="value">Значение</param>
    public void Set(TKey key, TValue value)
    {
        //считаем индекс цепочки.
        int chainIndex = GetIndex(key);
        
        //цепочки по индексу не сущ.
        if (_chains[chainIndex] == null)
        {
            //инициализация цепочки сразу с первым элементом, поэтому увеличиваем Count.
            _chains[chainIndex] = new Chain(new KeyValuePair<TKey, TValue>(key, value));
            Count++;
        }
        //цепочка уже существует
        else
        {
            //начинаем итерацию по цепочке, пока либо не дойдем до конца,
            // либо не найдем элемент с ключом key и не произведем замену
            var current = _chains[chainIndex];

            while (true)
            {
                if (current.KeyValue.Key.Equals(key))
                {
                    //элемент с таким ключом уже есть в цепочке, нужно заменить значение.
                    var newKeyValue = new KeyValuePair<TKey, TValue>(key, value);
                    current.KeyValue = newKeyValue;
                    return;
                }
                if (current.Next == null) break;
                current = current.Next;
            }

            //если не нашли, добавляем в конец цепочки и увеличиваем счетчик
            var newHashChain = new Chain(new KeyValuePair<TKey, TValue>(key, value));
            current.Next = newHashChain;
            Count++;
        }
    }

    /// <summary>
    /// Пробует получить значение по ключу.
    /// </summary>
    /// <param name="key">Ключ по которому нужно искать элемент</param>
    /// <param name="value">Сюда будет записано значение элемента, если он был найден</param>
    /// <returns>True, если элемент найден, иначе - false</returns>
    public bool TryGetValue(TKey key, out TValue value)
    {
        //получаем индекс цепочки
        int chainIndex = GetIndex(key);
        if (_chains[chainIndex] != null)
        {
            //итерируемся по цепочке
            var current = _chains[chainIndex];
            while (current != null)
            {
                //сравнимаем ключи
                if (current.KeyValue.Key.Equals(key))
                {
                    // нашли элемент, возвращаем
                    value = current.KeyValue.Value;
                    return true;
                }

                current = current.Next;
            }
        }

        //Не нашли, возвращаем default значение типа значения.
        value = default(TValue);
        return false;
    }

    /// <summary>
    /// Вывод элементов хеш таблицы.
    /// </summary>
    public void Print()
    {
        if (Count == 0)
        {
            Console.WriteLine("Хеш таблица пуста.");
            return;
        }
        //вызываем метод IEnumerable, коим является this.
        foreach (var keyValuePair in this)
        {
            Console.WriteLine($"Ключ: {keyValuePair.Key.ToString()}; Значение: {keyValuePair.Value.ToString()}");
        }
    }

    /// <summary>
    ///  Удаление элемента из хеш таблицы по ключу.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool Remove(TKey key)
    {
        //получаем индекс цепочки
        int chainIndex = GetIndex(key);
        if (_chains[chainIndex] != null)
        {
            //нужно хранить предыдущий, если prev null, значит current - первый элемент
            Chain prev = null;
            var current = _chains[chainIndex];
            while (current != null)
            {
                //сравниваем ключи
                if (current.KeyValue.Key.Equals(key))
                {
                    //значит первый элемент, нужно сделать первым следующий элемент
                    if (prev == null)
                    {
                        _chains[chainIndex] = current.Next;
                    }
                    else
                    {
                        //общий случай, убираем из цепочки текущий.
                        prev.Next = current.Next;
                    }

                    Count--;
                    return true;
                }

                prev = current;
                current = current.Next;
            }
        }

        //ничего не нашли.
        return false;
    }

    /// <summary>
    ///  Энумератор, который используется в циклах foreach для обхода элементов таблицы.
    ///  Элементы выподятся не по порядку. Порядок в цепочках также не гарантируется,
    /// так как метод Set может перезаписать элемент, и, не смотря на то, что ключ остался тот же, значение элемента
    /// // может быть другим
    /// </summary>
    /// <returns>Итератор по элементам таблицы</returns>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < _capacity; i++)
        {
            if (_chains[i] != null)
            {
                //цепочка не null, можно пройтись.
                var current = _chains[i];
                while (current != null)
                {
                    //функции генераторы, коим является перечислитель, могут
                    //возращать несколько значений последовательно
                    // после yield return начнет выполняться внешний код, как только внешний код запросит следующий элемент
                    // код в данном методе продолжит выполнятся со строчки, следующей за yield return.
                    // yield return может быть хоть сколько
                    // чтобы завершить выполнение, можно вызвать yield return break
                    yield return current.KeyValue;
                    current = current.Next; // следующий элемент
                }
            }
        }
    }

    [ExcludeFromCodeCoverage]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}