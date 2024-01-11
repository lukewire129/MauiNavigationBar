using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MauiNavigationBar.UI.Units;

public class MagicBarItems : Element, IList<MagicBarItem>, INotifyCollectionChanged
{
    readonly ObservableCollection<MagicBarItem> _items;

    public MagicBarItems(IEnumerable<MagicBarItem> items)
    {
        _items = new ObservableCollection<MagicBarItem> (items) ?? throw new ArgumentNullException (nameof (items));
        _items.CollectionChanged += OnItemsChanged;
    }
    public MagicBarItems() : this (Enumerable.Empty<MagicBarItem> ())
    {

    }

    public event NotifyCollectionChangedEventHandler CollectionChanged
    {
        add { _items.CollectionChanged += value; }
        remove { _items.CollectionChanged -= value; }
    }

    public MagicBarItem this[int index]
    {
        get => _items.Count > index ? _items[index] : null;
        set => _items[index] = value;
    }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(MagicBarItem item)
    {
        _items.Add (item);
    }

    public void Clear()
    {
        _items.Clear ();
    }

    public bool Contains(MagicBarItem item)
    {
        return _items.Contains (item);
    }

    public void CopyTo(MagicBarItem[] array, int arrayIndex)
    {
        _items.CopyTo (array, arrayIndex);
    }

    public IEnumerator<MagicBarItem> GetEnumerator()
    {
        return _items.GetEnumerator ();
    }

    public int IndexOf(MagicBarItem item)
    {
        return _items.IndexOf (item);
    }

    public void Insert(int index, MagicBarItem item)
    {
        _items.Insert (index, item);
    }

    public bool Remove(MagicBarItem item)
    {
        return _items.Remove (item);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt (index);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged ();

        object bc = BindingContext;

        foreach (BindableObject item in _items)
            SetInheritedBindingContext (item, bc);
    }

    void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
    {
        if (notifyCollectionChangedEventArgs.NewItems == null)
            return;

        object bc = BindingContext;

        foreach (BindableObject item in notifyCollectionChangedEventArgs.NewItems)
            SetInheritedBindingContext (item, bc);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator ();
    }
}
