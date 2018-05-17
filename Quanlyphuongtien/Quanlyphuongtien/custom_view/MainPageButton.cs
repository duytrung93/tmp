using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Quanlyphuongtien
{
    public class MainPageButton : Frame
    {
        public MainPageButton(string iLabel, string img, Command command = null)
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;

            WidthRequest = 100;
            HeightRequest = 100;
            CornerRadius = 100;
            Padding = 0;
            Margin = 0;
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                    {
                        new Image
                        {
                            Source = img,
                            WidthRequest = 50,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                        },
                        new Label
                        {
                            Text = iLabel,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            TextColor = Color.FromHex(Contanst.PrimaryTextColor)
                        }
                    }
            };
            if (command != null)
                this.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = command
                });
            //Utility.SetEffect(this, command: command);

        }
    }
}
