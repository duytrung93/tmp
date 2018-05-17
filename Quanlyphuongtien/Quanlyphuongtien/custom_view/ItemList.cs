using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Quanlyphuongtien
{
    public class ItemList : StackLayout
    {
        public ItemList(View view, int padding = 10)
        {
            Padding = 0;
            Spacing = 0;
            Children.Add(new StackLayout
            {
                Padding = padding,
                Spacing = 0,
                Children =
                {
                     view
                }
            });
            Children.Add(new BoxView
            {
                HeightRequest = 1,
                Margin = 0,
                BackgroundColor = Color.FromHex("#eaeff2"),
                HorizontalOptions = LayoutOptions.FillAndExpand
            });
        }
    }
}
