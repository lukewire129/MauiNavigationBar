using System.ComponentModel;

namespace MauiNavigationBar.UI.Units;

[ContentProperty(nameof (Items))]
public class MagicBar : TemplatedView
{
	public static readonly BindableProperty ItemsProperty = BindableProperty.Create (nameof (Items), typeof (MagicBarItems), typeof (MagicBar), default (MagicBarItems), propertyChanged: OnItemsChanged);

    static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as MagicBar)?.UpdateItems ();
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
    Grid _circle;
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate ();
        _circle = GetTemplateChild ("PART_Circle") as Grid;
        _container = GetTemplateChild ("PART_Grd") as Grid;

    }

    void UpdateItems()
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
        foreach (var child in Items)
        {
            if (!_container.Children.Contains (child))
            {
                child.index = j;
                Grid.SetColumn (child, j++);
                UpdateViewItem (child);
                _container.Children.Add (child);
            }
        }
    }

    void UpdateViewItem(MagicBarItem childern)
    {
        if (childern == null)
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


        _circle.TranslateTo (selectedItem.index * 80,0);
    }

    void UnSelectItems(MagicBarItems items)
    {
        foreach (var child in items)
        {
            child.ChangeSelected (false);
        }
    }
}
