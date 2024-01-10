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
        //IconSelectedSB ();
        //IconUnSelectedSB ();
        //TextSelectedSB ();
        //TextUnSelectedSB ();
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

        //if (IsSelected)
        //{
        //    Select ();
        //}
        //else
        //{
        //    UnSelect ();
        //}
    }
    void UpdateCurrent()
    {
        if (_magicBar == null)
            _magicBar = VisualTreeHelper.GetParent<MagicBar> (this);

        _magicBar?.UpdateSelectedItem (this, IsSelected);
    }

    private void IconSelectedSB()
    {
        FillAnimation animation = new FillAnimation ();
        animation.ToColor = new SolidColorBrush(Color.FromArgb ("#333333"));
        animation.Duration = "500";

        iconSelectedSB = new StoryBoard ();
        iconSelectedSB.Target = _icon;
        iconSelectedSB.Animations.Add (animation);
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
    private void IconUnSelectedSB()
    {
        FillAnimation animation = new FillAnimation ();
        animation.ToColor = new SolidColorBrush (Color.FromArgb ("#44333333"));
        animation.Duration = "500";

        iconUnSelectedSB = new StoryBoard ();
        iconUnSelectedSB.Target = _icon;
        iconUnSelectedSB.Animations.Add (animation);
    }

    private void TextSelectedSB()
    {
        ColorAnimation animation = new ColorAnimation ();
        animation.ToColor =Color.FromArgb ("#333333");
        animation.Duration = "500";

        textSelectedSB = new StoryBoard ();
        textSelectedSB.Target = _text;
        textSelectedSB.Animations.Add (animation);
    }

    private void TextUnSelectedSB()
    {
        ColorAnimation animation = new ColorAnimation ();
        animation.ToColor = Color.FromArgb ("#00000000");
        
        animation.Duration = "500";

        textUnSelectedSB = new StoryBoard ();
        textUnSelectedSB.Target = _text;        
        textUnSelectedSB.Animations.Add (animation);
    }
}
