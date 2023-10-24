using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;

public class MyList<T> : IEnumerable<T>
{
    private T[] items;
    private int count;

    public int Length
    {
        get { return count; }
    }

    public MyList()
    {
        items = new T[0];
        count = 0;
    }

    public MyList(int size)
    {
        if (size < 0)
            throw new ArgumentException("Size cannot be negative.");
        items = new T[size];
        count = 0;
    }

    public MyList(MyList<T> other)
    {
        items = new T[other.Length];
        count = other.Length;
        for (int i = 0; i < count; i++)
        {
            items[i] = other[i];
        }
    }

    public void Add(T element)
    {
        if (count == items.Length)
        {
            Array.Resize(ref items, count+1);
        }
        items[count] = element;
        count++;
    }

    public void Delete(T element)
    {
        int index = IndexOf(element);
        if (index != -1)
        {
            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }
            count--;
        }
    }

    public void Clear()
    {
        items = new T[0];
        count = 0;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index is out of range.");
            return items[index];
        }
    }

    public int IndexOf(T element)
    {
        for (int i = 0; i < count; i++)
        {
            if (object.Equals(items[i], element))
            {
                return i;
            }
        }
        return -1;
    }

    public void Insert(T element, int index)
    {
        if (index < 0 || index > count)
            throw new IndexOutOfRangeException("Index is out of range.");

        if (count == items.Length)
        {
            Array.Resize(ref items, count == 0 ? 4 : count * 2);
        }

        for (int i = count; i > index; i--)
        {
            items[i] = items[i - 1];
        }

        items[index] = element;
        count++;
    }

    public void Reverse()
    {
        for (int i = 0; i < count / 2; i++)
        {
            T temp = items[i];
            items[i] = items[count - 1 - i];
            items[count - 1 - i] = temp;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main()
    {
        MyList<int> list = new MyList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);

        Debug.Assert(list.Length == 3);
        Debug.Assert(list[0] == 1);
        Debug.Assert(list[1] == 2);
        Debug.Assert(list[2] == 3);

        list.Delete(2);
        Debug.Assert(list.Length == 2);
        Debug.Assert(list[0] == 1);
        Debug.Assert(list[1] == 3);

        list.Insert(2, 1);
        Debug.Assert(list.Length == 3);
        Debug.Assert(list[0] == 1);
        Debug.Assert(list[1] == 2);
        Debug.Assert(list[2] == 3);

        list.Reverse();
        Debug.Assert(list.Length == 3);
        Debug.Assert(list[0] == 3);
        Debug.Assert(list[1] == 2);
        Debug.Assert(list[2] == 1);
    }
}
