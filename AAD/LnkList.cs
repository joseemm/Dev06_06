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
        if (_head == null)
            throw new IndexOutOfRangeException();

        if (index < 0 || index >= _count)
            throw new IndexOutOfRangeException();

        var currentNode = _head;
        var currentIndex = 0;

        while (currentNode != null)
        {
            if (currentIndex == index)
                break;

            currentIndex++;
            currentNode = currentNode.Next;
        }

        return currentNode!.Value;
    }

    // O(1)
    public void Prepend(T value)
    {
        if (_head == null)
        {
            _head = new LnkNode<T>(value);
            return;
        }

        _head = new LnkNode<T>(value, next: _head);
    }


    // O(1)

   public void Add(T element)
{
    var newNode = new LnkNode<T>(element);

    // O(1)
    if (_head == null)
        _head = _last = newNode;
    else // O(1)
    {
        _last!.Next = newNode;
        _last = newNode;
    }

    _count++;

    // usar un método auxiliar para actualizar el tamaño de la lista
    UpdateSize();
}

// O(1)
private void UpdateSize()
{
    // incrementar el tamaño si el último nodo no es nulo
    if (_last != null)
        _count++;
}


    // O(n)

   public void Insert(int index, T value)
{
    // O(1)
    if (_count == 0)
        return;

    // O(1)
    if (index == 0)
    {
        Prepend(value); // usar el método Prepend en lugar de crear un nuevo nodo
        return;
    }

    // O(1)
    if (index == _count)
    {
        Append(value); // usar el método Append en lugar de recorrer la lista
        return;
    }

    var currentIndex = 0;
    var current = _head;
    while (current != null)
    {
        if (currentIndex == index - 1)
        {
            var newNode = new LnkNode<T>(value, current.Next);
            current.Next = newNode; // no es necesario asignar newNode.Next
            return;
        }

        current = current.Next;
        currentIndex++;
    }
}

    private void Append(T value)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T value)
{
    if (_head == null)
        return false;

    // usar una variable para guardar el nodo anterior
    var previousNode = _head;
    var currentNode = _head.Next;

    // verificar el primer nodo
    if (_head.ValueEquals(value))
    {
        _head = _head.Next;
        return true;
    }

    // recorrer la lista desde el segundo nodo
    while (currentNode != null)
    {
        // si el nodo actual tiene el valor buscado
        if (currentNode.ValueEquals(value))
        {
            // saltar el nodo actual y enlazar el anterior con el siguiente
            previousNode.Next = currentNode.Next;
            return true;
        }

        // avanzar los nodos
        previousNode = currentNode;
        currentNode = currentNode.Next;
    }

    return false;
}

    public void RemoveAt(int index)
    {
        if (_head == null)
            throw new IndexOutOfRangeException();

        if (index < 0 || index >= _count)
            throw new IndexOutOfRangeException();

        if (index == 0)
        {
            _head = _head.Next;
            return;
        }
        
        var currentIndex = 0;
        var currentNode = _head;
        
        while (currentNode != null)
        {
            if (currentIndex == index - 1)
            {
                currentNode.Next = currentNode.Next!.Next;
                return;
            }
            
            currentIndex++;
            currentNode = currentNode.Next;
        }
    }

    // O(1)
    public int Count() =>
        _count;

    // O(n)
   
public T[] ToArray()
{
    if (_head == null)
        return Array.Empty<T>();

    var result = new T[_count]; // usar el tamaño de la lista enlazada

    var current = _head;
    var i = 0; // usar un índice para recorrer el arreglo
    while (current != null)
    {
        result[i] = current.Value; // asignar el valor al arreglo
        current = current.Next;
        i++; // incrementar el índice
    }

    return result;
}

    
}
