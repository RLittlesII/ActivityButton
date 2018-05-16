using System;
using System.Linq;
using Xamarin.Forms;

namespace ActivityButton
{
    public class CustomButton : Button
    {
        public CustomButton()
        {
            Style = App.Current.Resources["ActivityControlStyle"] as Style;
            HeightRequest = 48;
        }
    }
}