using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Quanlyphuongtien
{
    public class ButtonBorder : StackLayout
    {
        public Label label { get; set; }
        public StackLayout MainContainer { get; private set; }

        public ButtonBorder(string text = "", string icon = "", double iconWidth = 40, string iTextColor = "#000", string iBackgroundColor = null, string borderColor = null, float iBorderWidth = 0, int iPadding = 10, Command iCommand = null, float iCornerRadius = 0)
        {
            borderColor = borderColor ?? "#DDD";
            iBackgroundColor = iBackgroundColor ?? "#DDD";
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;

            //BackgroundColor = Color.FromHex(borderColor);
            Padding = 0;//new Thickness(0, 1, 1, 1),
            Spacing = 0;
            if (text != "")
            {
                label = new Label
                {
                    Text = text,
                    TextColor = Color.FromHex(iTextColor),
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
            }
            else
            {
                label = new Label { Text = "" };
            }


            MainContainer = new StackLayout
            {
                Padding = iPadding,
                //BackgroundColor = Color.FromHex(iBackgroundColor),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Children =
                {
                    label
                }
            };
            if (icon != "")
            {
                Image image = new Image
                {
                    Source = icon,
                    WidthRequest = iconWidth,
                    HeightRequest = iconWidth
                };
                MainContainer.Children.Add(image);
            }
            Children.Add(new Frame
            {
                CornerRadius = iCornerRadius,
                Padding = 1,
                //BackgroundColor = Color.FromHex("#DDD"),
                Margin = 0,
                Content = new Frame
                {
                    CornerRadius = iCornerRadius,
                    Padding = 0,
                    BackgroundColor = Color.FromHex(iBackgroundColor),
                    Margin = 0,
                    Content = new StackLayout
                    {
                        Spacing = 0,
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            MainContainer
                         }
                    }
                }
            });
            Utility.SetEffect(this, colorEffect: iBackgroundColor, command: iCommand);

        }


    }
}
