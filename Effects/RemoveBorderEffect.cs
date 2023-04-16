// The effect (RemoveBorderEffect) in this file was mostly copied from https://www.nuget.org/packages/Xamarin.CommunityToolkit/ 
using Microsoft.Maui.Controls.Platform;

namespace ShoppingList.Effects
{
    internal class RemoveBorderEffect : RoutingEffect
    {
    }

#if ANDROID
internal class RemoveBorderPlatformEffect : PlatformEffect
{
        Android.Graphics.Drawables.Drawable? originalBackground;

    protected override void OnAttached()
    {
        originalBackground = Control.Background;

        var shape = new Android.Graphics.Drawables.ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
        if (shape.Paint != null)
        {
            shape.Paint.Color = global::Android.Graphics.Color.Transparent;
            shape.Paint.StrokeWidth = 0;
            shape.Paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
        }

        Control.Background = shape;
    }

    protected override void OnDetached()
    {
            Control.Background = originalBackground;
    }
}
#elif IOS
internal class RemoveBorderPlatformEffect : PlatformEffect
{
        UIKit.UITextBorderStyle? oldBorderStyle;

        UIKit.UITextField? TextField => Control as UIKit.UITextField;

    protected override void OnAttached()
    {
        oldBorderStyle = TextField?.BorderStyle;
        SetBorderStyle(UIKit.UITextBorderStyle.None);
    }

    protected override void OnDetached()
    {
            SetBorderStyle(oldBorderStyle);
    }

        void SetBorderStyle(UIKit.UITextBorderStyle? borderStyle)
    {
        if (TextField != null && borderStyle.HasValue)
            TextField.BorderStyle = borderStyle.Value;
    }
}
#elif WINDOWS
internal class RemoveBorderPlatformEffect : PlatformEffect
{
        Microsoft.UI.Xaml.Thickness oldBorderThickness;

        protected override void OnAttached()
        {
            if (Control is Microsoft.UI.Xaml.Controls.Control uwpControl)
            {
                oldBorderThickness = uwpControl.BorderThickness;
                uwpControl.BorderThickness = new Microsoft.UI.Xaml.Thickness(0.0);
            }
        }

        protected override void OnDetached()
        {
            if (Control is Microsoft.UI.Xaml.Controls.Control uwpControl)
            {
                uwpControl.BorderThickness = oldBorderThickness;
            }
        }
    }
#elif MACCATALYST
    internal class RemoveBorderPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            throw new NotImplementedException();
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }
    }
#endif
}
