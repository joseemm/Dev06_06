namespace AAD;

public class LnkList<T> where T : notnull
{
    public static LnkList<T> From(params T[] values)
    {
        var ll = new LnkList<T>();
        foreach (var value in values)
            ll.Add(value);
        return ll;
    }

    private LnkNode<T>? _head;

    private LnkNode<T>? _last;

    private int _count;

    public LnkList() : this(head: null, last: null)
    {
    }

    private LnkList(LnkNode<T>? head, LnkNode<T>? last)
    {
        _head = head;
        _last = last;
        _count = 0;
    }

    public T this[int index] => Get(index);

    public T Get(int index)
    {
        if (_head == null || index >= _count)
            throw new IndexOutOfRangeException();

        var currentIndex = 0;
        var current = _head;
        while (current != null)
        {
            if (currentIndex == index)
                break;
            
            currentIndex++;
            current = current.Next;
        }

        return current.Value;
    }
    
    public void Prepend(T value)
    {
        _head = new LnkNode<T>(value, _head);
        _count++;
    }

    /// <summary>
    /// O(1)
    /// </summary>
    /// <param name="element"></param>
    public void Add(T element)
    {
        var newNode = new LnkNode<T>(element);

        // O(1)
        if (_head == null)
            _head = _last = newNode;
        else // O(1)
        {
            _last.Next = newNode;
            _last = newNode;
        }

        _count++;
    }

    /// <summary>
    /// O(n)
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public void Insert(int index, T value)
    {
        // O(1)
        if (_count == 0)
            return;

        // O(1)
        if (index == 0)
        {
            Prepend(value);
            return;
        }

        var currentIndex = 0;
        var current = _head;
        while (current != null)
        {
            if (currentIndex == index - 1)
            {
                current.Next = new LnkNode<T>(value, current.Next);
                return;
            }

            current = current.Next;
            currentIndex++;
        }
    }

    /// <summary>
    /// O(1)
    /// </summary>
    /// <returns></returns>
    public int Count() =>
        _count;


    public bool Remove(T value)
    {
        if (_head == null)
            return false;

        if (_head.ValueEquals(value))
        {
            _head = _head.Next;
            _count -= 1;
            return true;
        }

        var currentNode = _head;
        while (currentNode != null)
        {
            var nextNode = currentNode.Next;

            if (nextNode != null && nextNode.ValueEquals(value))
            {
                currentNode.Next = nextNode.Next;
                _count -= 1;
                return true;
            }

            currentNode = currentNode.Next;
        }

        return false;
    }

    public bool RemoveAt(int index)
    {
        if (_head == null)
            throw new IndexOutOfRangeException();

        if (index < 0 || index >= _count)
            throw new IndexOutOfRangeException();

        if (_head.Next == null)
        {
            _head = _head.Next;
            _count--;
            return true;
        }

        var currentIndex = 0;
        var currentNode = _head;
        while (currentNode != null)
        {
            if (currentIndex == index - 1)
            {
                var nextNode = currentNode.Next;
                currentNode.Next = nextNode?.Next;
                _count--;
                return true;
            }

            currentIndex++;
            currentNode = currentNode.Next;
        }
        return false;
    }

    /// <summary>
    /// O(n)
    /// </summary>
    /// <returns></returns>
    public T[] ToArray()
    {
        if (_head == null)
            return Array.Empty<T>();

        var result = new List<T>();

        var current = _head;
        while (current != null)
        {
            result.Add(current.Value);
            current = current.Next;
        }

        return result.ToArray();
    }
}