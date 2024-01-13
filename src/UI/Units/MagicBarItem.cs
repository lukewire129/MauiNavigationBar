using AlohaKit.Animations;
using MauiNavigationBar.Animations;
using MauiNavigationBar.Helper;

namespace MauiNavigationBar.UI.Units;
public enum Type
{
    Microsoft,
    Apple,
    Google,
    Facebook,
    Instagram,
    None,
}

public class MagicBarItem :TemplatedView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create (nameof (Text), typeof (string), typeof (MagicBarItem), default (string));

    public string Text
    {
        get => (string)GetValue (TextProperty);
        set => SetValue (TextProperty, value);
    }
    public static readonly BindableProperty TypeProperty = BindableProperty.Create (nameof (Type),
                                                            typeof (Type),
                                                            typeof (MagicBarItem),
                                                            default (Type));

    public int index { get; set; }
    public Type Type
    {
        get => (Type)GetValue (TypeProperty);
        set => SetValue (TypeProperty, value);
    }

    public static readonly BindableProperty IsSelectedProperty =
          BindableProperty.Create (nameof (IsSelected), typeof (bool), typeof (MagicBarItem), false,
              propertyChanged: CurrentChanged);

    static void CurrentChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(oldValue == newValue) return;
        var item = bindable as MagicBarItem;
        item?.UpdateCurrent ();
       
    }

    public bool IsSelected
    {
        get => (bool)GetValue (IsSelectedProperty);
        set { SetValue (IsSelectedProperty, value); }
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate ();

        if (_magicBar == null)
            _magicBar = VisualTreeHelper.GetParent<MagicBar> (this);

        if (_magicBar != null)
        {
            _magicBar.SendItemTapped (new TappedEventArgs (this));
        }

        _icon = GetTemplateChild ("PART_Icon") as Microsoft.Maui.Controls.Shapes.Path;
        _text = GetTemplateChild ("PART_Text") as Label;
        IconSelectedSB ();
        IconUnSelectedSB ();
        TextSelectedSB ();
        TextUnSelectedSB ();
    }

    StoryBoard iconSelectedSB;
    StoryBoard iconUnSelectedSB;
    StoryBoard textSelectedSB;
    StoryBoard textUnSelectedSB;
    MagicBar _magicBar;
    Microsoft.Maui.Controls.Shapes.Path _icon;
    Label _text;
    public MagicBarItem()
    {
        var textGesturedTap = new TapGestureRecognizer ();
        textGesturedTap.Tapped += OnTextTapped;
        this.GestureRecognizers.Add (textGesturedTap);
        this.Loaded += MagicBarItem_Loaded;
    }

    private void MagicBarItem_Loaded(object? sender, EventArgs e)
    {
        if (IsSelected)
        {
            Select ();
            UpdateCurrent ();
        }
    }

    void OnTextTapped(object sender, EventArgs e)
    {
        if (IsSelected == true)
            return;
        IsSelected = !IsSelected;
    }
    public void ChangeSelected(bool select)
    {
        this.IsSelected = select;

        if (IsSelected)
        {
            Select ();
        }
        else
        {
            UnSelect ();
        }
    }
    void UpdateCurrent()
    {
        if (_magicBar == null)
            _magicBar = VisualTreeHelper.GetParent<MagicBar> (this);

        _magicBar?.UpdateSelectedItem (this, IsSelected);
    }

    

    private void Select()
    {
        iconSelectedSB.Begin ();
        textSelectedSB.Begin ();

    }
    private void UnSelect()
    {
        iconUnSelectedSB.Begin ();
        textUnSelectedSB.Begin ();
    }
    private void IconSelectedSB()
    {
        MarginAnimation MarginAnimation = new MarginAnimation ();
        MarginAnimation.Target = _icon;
        MarginAnimation.To = new Thickness(15, -65, 0, 0);
        MarginAnimation.Duration = "500";

        FillAnimation animation = new FillAnimation ();
        animation.Target = _icon;
        animation.ToColor = new SolidColorBrush (Color.FromArgb ("#333333"));
        animation.Duration = "500";

        iconSelectedSB = new StoryBoard ();
        iconSelectedSB.Target = _icon;
        iconSelectedSB.Animations.Add (animation);
        iconSelectedSB.Animations.Add (MarginAnimation);
    }

    private void IconUnSelectedSB()
    {
        MarginAnimation MarginAnimation = new MarginAnimation ();
        MarginAnimation.Target = _icon;
        MarginAnimation.To = new Thickness (15, 0, 0, 0);
        MarginAnimation.Duration = "500";

        FillAnimation animation = new FillAnimation ();
        animation.Target = _icon;
        animation.ToColor = new SolidColorBrush (Color.FromArgb ("#44333333"));
        animation.Duration = "500";

        iconUnSelectedSB = new StoryBoard ();
        iconUnSelectedSB.Target = _icon;
        iconUnSelectedSB.Animations.Add (animation);
        iconUnSelectedSB.Animations.Add (MarginAnimation);
    }

    private void TextSelectedSB()
    {
        MarginAnimation MarginAnimation = new MarginAnimation ();
        MarginAnimation.Target = _text;

        MarginAnimation.To = new Thickness (0, 65, 0, 0);
        MarginAnimation.Duration = "500";

        TextColorAnimation animation = new TextColorAnimation ();
        animation.Target = _text;
        animation.ToColor =Color.FromArgb ("#333333");
        animation.Duration = "500";

        textSelectedSB = new StoryBoard ();
        textSelectedSB.Target = _text;
        textSelectedSB.Animations.Add (animation);
        textSelectedSB.Animations.Add (MarginAnimation);
    }

    private void TextUnSelectedSB()
    {
        MarginAnimation MarginAnimation = new MarginAnimation ();
        MarginAnimation.Target = _text;
        MarginAnimation.To = new Thickness (0, 80, 0, 0);
        MarginAnimation.Duration = "500";

        TextColorAnimation animation = new TextColorAnimation ();
        animation.Target = _text;
        animation.ToColor = Color.FromArgb ("#00000000");
        animation.Duration = "500";

        textUnSelectedSB = new StoryBoard ();
        textUnSelectedSB.Target = _text;
        textUnSelectedSB.Animations.Add (animation);
        textUnSelectedSB.Animations.Add (MarginAnimation);
    }
}
