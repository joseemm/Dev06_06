namespace AAD;

public class LnkNode<T> where T : notnull
{
    public T Value { get; }
    public LnkNode<T>? Next { get; private set; }

    public LnkNode(T value) : this(value, null) => 
        Value = value;

    public LnkNode(T value, LnkNode<T>? next)
    {
        Value = value;
        Next = next;
    }

    public bool IsLast => Next == null;

    public bool ValueEquals(T value) =>
        Value.Equals(value);

    public void Link(LnkNode<T> nextNode) => Next = nextNode;
}