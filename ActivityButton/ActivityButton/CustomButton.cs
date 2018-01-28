using System;
using System.Linq;
using Xamarin.Forms;

namespace ActivityButton
{
    public class CustomButton : Label
    {
        public CustomButton()
        {
            Style = App.Current.Resources["ActivityControlStyle"] as Style;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalTextAlignment = TextAlignment.Center;
        }
    }
}