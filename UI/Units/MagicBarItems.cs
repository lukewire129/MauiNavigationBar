using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MauiNavigationBar.UI.Units;

public class MagicBarItems : Element, IList<MagicBarItem>, INotifyCollectionChanged
{
    readonly ObservableCollection<MagicBarItem> _treeViewNodes;

    public MagicBarItems(IEnumerable<MagicBarItem> treeViewNodes)
    {
        _treeViewNodes = new ObservableCollection<MagicBarItem> (treeViewNodes) ?? throw new ArgumentNullException (nameof (treeViewNodes));
        _treeViewNodes.CollectionChanged += OnTreeViewNodesChanged;
    }
    public MagicBarItems() : this (Enumerable.Empty<MagicBarItem> ())
    {

    }

    public event NotifyCollectionChangedEventHandler CollectionChanged
    {
        add { _treeViewNodes.CollectionChanged += value; }
        remove { _treeViewNodes.CollectionChanged -= value; }
    }

    public MagicBarItem this[int index]
    {
        get => _treeViewNodes.Count > index ? _treeViewNodes[index] : null;
        set => _treeViewNodes[index] = value;
    }

    public int Count => _treeViewNodes.Count;

    public bool IsReadOnly => false;

    public void Add(MagicBarItem item)
    {
        _treeViewNodes.Add (item);
    }

    public void Clear()
    {
        _treeViewNodes.Clear ();
    }

    public bool Contains(MagicBarItem item)
    {
        return _treeViewNodes.Contains (item);
    }

    public void CopyTo(MagicBarItem[] array, int arrayIndex)
    {
        _treeViewNodes.CopyTo (array, arrayIndex);
    }

    public IEnumerator<MagicBarItem> GetEnumerator()
    {
        return _treeViewNodes.GetEnumerator ();
    }

    public int IndexOf(MagicBarItem item)
    {
        return _treeViewNodes.IndexOf (item);
    }

    public void Insert(int index, MagicBarItem item)
    {
        _treeViewNodes.Insert (index, item);
    }

    public bool Remove(MagicBarItem item)
    {
        return _treeViewNodes.Remove (item);
    }

    public void RemoveAt(int index)
    {
        _treeViewNodes.RemoveAt (index);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged ();

        object bc = BindingContext;

        foreach (BindableObject item in _treeViewNodes)
            SetInheritedBindingContext (item, bc);
    }

    void OnTreeViewNodesChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
    {
        if (notifyCollectionChangedEventArgs.NewItems == null)
            return;

        object bc = BindingContext;

        foreach (BindableObject item in notifyCollectionChangedEventArgs.NewItems)
            SetInheritedBindingContext (item, bc);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _treeViewNodes.GetEnumerator ();
    }
}
