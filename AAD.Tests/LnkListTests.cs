namespace AAD.Tests;

public class LnkListTests
{
    [Fact]
    public void Prepend_Empty()
    {
        var ll = Empty<int>();

        ll.Prepend(100);

        AssertEqual(new[] { 100 }, ll);
    }

    [Fact]
    public void Prepend_Many()
    {
        var ll = From(2, 3, 4);

        ll.Prepend(1);

        AssertEqual(new[] { 1, 2, 3, 4 }, ll);
    }

    [Fact]
    public void GetByIndex_Empty()
    {
        var ll = Empty<int>();

        Assert.Throws<IndexOutOfRangeException>(() => ll[1]);
    }

    [Fact]
    public void GetByIndex_Many()
    {
        var ll = LnkList<string>.From("Hello", "World", "Yes");

        var value = ll[1];

        Assert.Equal("World", value);
    }

    [Fact]
    public void GetByIndex_LastOfMany()
    {
        var ll = LnkList<string>.From("Hello", "World", "Yes");

        var value = ll[2];

        Assert.Equal("Yes", value);
    }

    [Fact]
    public void GetByIndex_IndexOutOfRange()
    {
        var ll = LnkList<string>.From("Hello", "World", "Yes");

        Assert.Throws<IndexOutOfRangeException>(() => ll[50]);
    }

    [Fact]
    public void Add_Empty()
    {
        var ll = Empty<string>();

        ll.Add("One");

        AssertEqual(new[] { "One" }, ll);
    }

    [Fact]
    public void Add_Many()
    {
        var ll = From(1, 2, 3);

        ll.Add(4);

        AssertEqual(new[] { 1, 2, 3, 4 }, ll);
    }

    [Fact]
    public void Insert_Empty()
    {
        var ll = Empty<string>();

        Assert.Throws<IndexOutOfRangeException>(() => ll.Insert(0, "Juan"));
    }

    [Fact]
    public void Insert_OneElement()
    {
        var ll = LnkList<string>.From("Pablo");

        ll.Insert(0, "Juan");

        AssertEqual(new[] { "Juan", "Pablo" }, ll);
    }

    [Fact]
    public void Insert_Many()
    {
        var ll = LnkList<string>.From("Juan", "Duarte");

        ll.Insert(1, "Pablo");

        AssertEqual(new[] { "Juan", "Pablo", "Duarte" }, ll);
    }

    [Fact]
    public void Remove_EmptyList()
    {
        var ll = Empty<string>();

        Assert.False(ll.Remove("Hello"));
    }

    [Fact]
    public void Remove_One()
    {
        var ll = LnkList<string>.From("Hello");

        var result = ll.Remove("Hello");

        Assert.True(result);

        AssertEqual(Array.Empty<string>(), ll);
    }

    [Fact]
    public void Remove_LastOfTwo()
    {
        var ll = LnkList<string>.From("A", "B");

        var result = ll.Remove("B");

        Assert.True(result);

        AssertEqual(new[] { "A" }, ll);
    }

    [Fact]
    public void Remove_Many()
    {
        var ll = LnkList<string>.From("Bread", "Ham", "Butter");

        var result = ll.Remove("Ham");

        Assert.True(result);

        AssertEqual(new[] { "Bread", "Butter" }, ll);
    }

    [Fact]
    public void Remove_DoesNotExists_Empty()
    {
        var ll = Empty<string>();

        var result = ll.Remove("Ghost");

        Assert.False(result);
        Assert.Equal(0, ll.Count());
    }

    [Fact]
    public void Remove_DoesNotExists_One()
    {
        var ll = LnkList<string>.From("A");

        var result = ll.Remove("B");

        Assert.False(result);
        Assert.Equal(1, ll.Count());
    }

    [Fact]
    public void Remove_DoesNotExists_Many()
    {
        var ll = LnkList<string>.From("A", "B", "C");

        var result = ll.Remove("D");

        Assert.False(result);
        Assert.Equal(3, ll.Count());
    }

    [Fact]
    public void RemoveAt_Empty()
    {
        var ll = Empty<string>();

        Assert.Throws<IndexOutOfRangeException>(
            () => ll.RemoveAt(10));
    }

    [Fact]
    public void RemoveAt_OneElementList()
    {
        var ll = LnkList<string>.From("A");

        Assert.True(ll.RemoveAt(0));

        AssertEqual(Array.Empty<string>(), ll);
    }

    [Fact]
    public void RemoveAt_LastOfManyElements()
    {
        var ll = LnkList<string>.From("A", "B", "C");

        Assert.True(ll.RemoveAt(2));

        AssertEqual(new[] { "A", "B" }, ll);
    }

    [Fact]
    public void RemoveAt_MiddleOfManyElements()
    {
        var ll = LnkList<string>.From("A", "B", "C");

        Assert.True(ll.RemoveAt(1));

        AssertEqual(new[] { "A", "C" }, ll);
    }

    [Fact]
    public void Count_Empty()
    {
        var ll = Empty<int>();

        Assert.Equal(0, ll.Count());
    }

    [Fact]
    public void Count_Many()
    {
        var ll = From(1, 2, 3, 4);

        Assert.Equal(4, ll.Count());
    }

    [Fact]
    public void Last()
    {
        var ll = From(1, 2, 3);

        Assert.Equal(3, ll.Last());
    }

    [Fact]
    public void Last_Empty()
    {
        var ll = Empty<string>();

        Assert.Throws<InvalidOperationException>(() => ll.Last());
    }

    [Fact]
    public void FromTest()
    {
        var ll = From(1, 2, 3);

        AssertEqual(new[] { 1, 2, 3 }, ll);
    }

    [Fact]
    public void ToArray_Empty()
    {
        var ll = Empty<int>();

        AssertEqual(Array.Empty<int>(), ll);
    }

    [Fact]
    public void ToArray_OneElement()
    {
        var ll = From(1);
        ;

        AssertEqual(new[] { 1 }, ll);
    }

    [Fact]
    public void ToArray_TwoElements()
    {
        var ll = From(1, 2);
        ;

        AssertEqual(new[] { 1, 2 }, ll);
    }

    [Fact]
    public void ToArray_Many()
    {
        var ll = From(1, 2, 3, 4);

        AssertEqual(new[] { 1, 2, 3, 4 }, ll);
    }

    protected virtual void AssertEqual<T>(T[] values, LnkList<T> ll) where T : notnull
    {
        Assert.Equal(values, ll.ToArray());
        Assert.Equal(values.Length, ll.Count());
        if (values.Length > 0)
            Assert.Equal(values.Last(), ll.Last());
    }

    protected virtual LnkList<T> Empty<T>() where T : notnull => new();
    
    protected virtual LnkList<T> From<T>(params T[] values) where T : notnull => LnkList<T>.From(values);
}