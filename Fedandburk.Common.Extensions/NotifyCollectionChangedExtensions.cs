using System;
using System.Collections.Specialized;

namespace Fedandburk.Common.Extensions;

public static class NotifyCollectionChangedExtensions
{
    /// <summary>
    /// Creates a weak subscription to listen for the CollectionChanged event.
    /// </summary>
    /// <param name="notifyCollectionChanged"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IDisposable WeakSubscribe(
        this INotifyCollectionChanged notifyCollectionChanged,
        NotifyCollectionChangedEventHandler handler
    )
    {
        if (notifyCollectionChanged == null)
        {
            throw new ArgumentNullException(nameof(notifyCollectionChanged));
        }

        if (handler == null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return new WeakCollectionSubscription(notifyCollectionChanged, handler);
    }
}