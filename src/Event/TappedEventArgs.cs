using MauiNavigationBar.Themes.Units;

namespace MauiNavigationBar.Event
{
    public class TappedEventArgs
    {
        public TappedEventArgs(MagicBarItem item)
        {
            Item = item;
        }

        public MagicBarItem Item { get; set; }
    }
}
