using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Fedandburk.Common.Extensions.Tests;

public sealed class ObservableRangeCollection<T> : ObservableCollection<T>
{
    private readonly struct SuppressEventsDisposable : IDisposable
    {
        private readonly ObservableRangeCollection<T> _collection;

        public SuppressEventsDisposable(ObservableRangeCollection<T> collection)
        {
            _collection = collection;
            ++collection._suppressEvents;
        }

        public void Dispose()
        {
            --_collection._suppressEvents;
        }
    }

    private int _suppressEvents;

    public ObservableRangeCollection()
    {
    }

    public ObservableRangeCollection(IEnumerable<T> items) : base(items)
    {
    }

    private SuppressEventsDisposable SuppressEvents()
    {
        return new SuppressEventsDisposable(this);
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (_suppressEvents != 0)
        {
            return;
        }

        base.OnCollectionChanged(e);
    }

    public void AddRange(IEnumerable<T> items)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        var startingIndex = Items.Count;
        var itemsList = items.ToList();
        
        using (SuppressEvents())
        {
            foreach (var item in itemsList)
            {
                Add(item);
            }
        }

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itemsList, startingIndex));
    }

    public void ReplaceWith(IEnumerable<T> items)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        using (SuppressEvents())
        {
            Clear();
            AddRange(items);
        }

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public void RemoveRange(int start, int count)
    {
        if (start < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        var end = start + count - 1;

        if (end < 0 || end > Count)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        var removedItems = new List<T>(count);

        for (var i = start; i <= end; i++)
        {
            removedItems.Add(this[i]);
        }

        using (SuppressEvents())
        {
            for (var i = end; i >= start; i--)
            {
                RemoveAt(i);
            }
        }

        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems, start));
    }
}