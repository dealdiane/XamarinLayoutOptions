using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Frame), typeof(LayoutOptionsSample.WinPhone.FrameRenderer))]

namespace LayoutOptionsSample.WinPhone
{
    public class FrameRenderer : Xamarin.Forms.Platform.WinPhone.FrameRenderer
    {
        public static readonly BindableProperty _visualElementImplicitStyleProperty = typeof(VisualElement).GetField("ImplicitStyleProperty", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null) as BindableProperty;

        protected override void OnElementChanged(Xamarin.Forms.Platform.WinPhone.ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            var newElement = e.NewElement;

            if (newElement == null)
            {
                return;
            }

            // Workaround for https://bugzilla.xamarin.com/show_bug.cgi?id=30765
            var paddingStyle = GetPaddingFromStyle(newElement.Style)
                                    ?? GetPaddingFromStyle(newElement.GetValue(_visualElementImplicitStyleProperty) as Style);

            if (paddingStyle != null)
            {
                //newElement.SetValue(paddingStyle.Property, paddingStyle.Value);
                // or
                newElement.Padding = (Thickness)paddingStyle.Value;
            }
        }

        private static Setter GetPaddingFromStyle(Style style)
        {
            if (style == null || style.Setters == null)
            {
                return null;
            }

            return style.Setters.FirstOrDefault(s => s.Property == Frame.PaddingProperty);
        }
    }
}