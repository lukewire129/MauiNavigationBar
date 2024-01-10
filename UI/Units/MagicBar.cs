using Microsoft.UI.Xaml.Controls.Primitives;
using System.ComponentModel;

namespace MauiNavigationBar.UI.Units;

[ContentProperty(nameof (Items))]
public class MagicBar : TemplatedView
{
	public static readonly BindableProperty ItemsProperty = BindableProperty.Create (nameof (Items), typeof (MagicBarItems), typeof (MagicBar), default (MagicBarItems), propertyChanged: OnRootNodesChanged);

    static void OnRootNodesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as MagicBar)?.UpdatetNodes ();
    }
    public MagicBarItems Items
	{
		get => (MagicBarItems)GetValue (ItemsProperty);
		set => SetValue (ItemsProperty, value);
	}

    public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create (nameof (SelectedItem), typeof (MagicBarItem), typeof (MagicBar), null);

    public MagicBarItem SelectedItem
    {
        get => (MagicBarItem)GetValue (SelectedItemProperty);
        set { SetValue (SelectedItemProperty, value); }
    }

    Grid _container;
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate ();

		_container = GetTemplateChild ("PART_Grd") as Grid;
    }

    void UpdatetNodes()
    {
        if (Items == null || Items.Count == 0)
            return;

        List<ColumnDefinition> def = new List<ColumnDefinition> ();
        for (int i=0; i<Items.Count; i++)
        {
            def.Add (new ColumnDefinition (GridLength.Star));
        }
        this._container.ColumnDefinitions = new ColumnDefinitionCollection (def.ToArray());
        int j = 0;
        foreach (var childNode in Items)
        {
            if (!_container.Children.Contains (childNode))
            {
                Grid.SetColumn (childNode, j++);
                UpdateTreeViewNodes (childNode);
                _container.Children.Add (childNode);
            }
        }
    }

    void UpdateTreeViewNodes(MagicBarItem treeViewNode)
    {
        UpdateTreeViewNode (treeViewNode);
    }

    void UpdateTreeViewNode(MagicBarItem treeViewNode)
    {
        if (treeViewNode == null)
            return;
    }

    [EditorBrowsable (EditorBrowsableState.Never)]
    internal void SendItemTapped(TappedEventArgs args)
    {
    }

    [EditorBrowsable (EditorBrowsableState.Never)]
    internal void UpdateSelectedItem(MagicBarItem selectedItem, bool isSelected)
    {
        if (SelectedItem == selectedItem)
            return;

        UnSelectItems (Items);
        selectedItem.ChangeSelected(isSelected);

        SelectedItem = selectedItem;
    }

    void UnSelectItems(MagicBarItems treeViewNodes)
    {
        foreach (var childNode in treeViewNodes)
        {
            childNode.ChangeSelected (false);
        }
    }
}
