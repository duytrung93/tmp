using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Quanlyphuongtien
{
    public class EntryForm : StackLayout
    {
        public View entry { get; set; }

        private StackLayout imageEntry;
        private StackLayout icon;
        private StackLayout vLabel;

        public EntryForm(string placeholder = "", string imgSrc = null, Label label = null, double labelWidth = 100, bool isLabelFirst = true, bool isPassword = false, float iCornerRadius = 0, View iView = null)
        {
            if (iView == null)
            {
                iView = new BorderlessEntry
                {
                    Margin = 0,
                    Placeholder = placeholder,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    IsPassword = isPassword
                };
            }
            entry = iView;

            imageEntry = new StackLayout
            {
                Padding = 0,
                Spacing = 0,
                Orientation = StackOrientation.Horizontal,
                GestureRecognizers = {
                        new TapGestureRecognizer{
                            Command = new Command(()=>{ entry.Focus(); })
                        }
                    },
            };

            if (imgSrc != null)
            {
                icon = new StackLayout
                {

                    Padding = new Thickness(10, 10, 5, 10),
                    Children = {
                     new Image
                     {
                         Source =imgSrc,
                         WidthRequest=20,
                         HorizontalOptions = LayoutOptions.CenterAndExpand,
                         VerticalOptions = LayoutOptions.CenterAndExpand
                     },
                }
                };
                imageEntry.Children.Add(icon);
            }
            if (label != null)
            {
                vLabel = new StackLayout
                {
                    Padding = new Thickness(10, 10, 5, 10),
                    WidthRequest = labelWidth,
                    Children =
                    {
                       label
                    }
                };
                if (isLabelFirst)
                    imageEntry.Children.Insert(0, vLabel);
                else
                    imageEntry.Children.Add(vLabel);
            }

            Children.Add(new Frame
            {
                CornerRadius = iCornerRadius,
                Padding = 1,
                BackgroundColor = Color.FromHex("#DDD"),
                Margin = 0,
                Content = new Frame
                {
                    CornerRadius = iCornerRadius,
                    Padding = 0,
                    BackgroundColor = Color.White,
                    Margin = 0,
                    Content = new StackLayout
                    {
                        Spacing = 0,
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            imageEntry,
                            entry

                         }
                    }
                }
            });
        }

    }
}
