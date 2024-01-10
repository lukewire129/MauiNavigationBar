namespace MauiNavigationBar.Animations.Extention;

public static class CustomAnimationHelper
{
    public static Brush GetBrushValue(Brush from, Brush to, double animationProgress)
    {
        Color _fromColor = ((SolidColorBrush)from).Color;
        Color _toColor = ((SolidColorBrush)to).Color;
        double num = (double)(_toColor.Red - _fromColor.Red) * animationProgress;
        double num2 = (double)(_toColor.Green - _fromColor.Green) * animationProgress;
        double num3 = (double)(_toColor.Blue - _fromColor.Blue) * animationProgress;
        return new SolidColorBrush(Color.FromRgb ((double)_fromColor.Red + num, (double)_fromColor.Green + num2, (double)_fromColor.Blue + num3));
    }
}
