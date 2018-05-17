using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Quanlyphuongtien
{
    public class MenuTab : RelativeLayout
    {
        public ScrollView ScrollContent { get; internal set; }
        public StackLayout MenuContent { get; internal set; }
        public Label WaitText { get; internal set; }
        public StackLayout BackgrounDismiss { get; }
        double MaxWidth = 0;
        private async void OnMenuDismissLayoutClicked(bool isDismiss)
        {

            if (isDismiss)
            {
                await ScrollContent.LayoutTo(new Rectangle(-MaxWidth, 0, MaxWidth, Height));
                await BackgrounDismiss.FadeTo(0, 100);
                IsVisible = false;
            }
            else
            {
                IsVisible = true;
                await BackgrounDismiss.FadeTo(0.7, 100);
                await ScrollContent.LayoutTo(new Rectangle(0, 0, MaxWidth, Height));
            }
        }
        public MenuTab(StackLayout menuContent, double maxWidth)
        {
            BackgroundColor = Color.Transparent;
            IsVisible = false;
            MaxWidth = maxWidth;
            MenuContent = menuContent;
            BackgrounDismiss = new StackLayout
            {
                BackgroundColor = Color.FromHex("#000000"),
                Opacity = 0.7,
            };
            BackgrounDismiss.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => { OnMenuDismissLayoutClicked(true); })
            });
            this.Children.Add(BackgrounDismiss,
                Constraint.RelativeToParent(p =>
                {//x
                    return 0;
                }),
                Constraint.RelativeToParent(p =>
                {//y
                    return 0;
                }),
                Constraint.RelativeToParent(p =>
                {//width
                    return p.Width;
                }),
                Constraint.RelativeToParent(p =>
                {//height
                    return p.Height;
                })
            );
            ScrollContent = new ScrollView
            {
                Content = MenuContent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            this.Children.Add(ScrollContent,
                Constraint.RelativeToParent(p =>
                {//x
                    return -maxWidth;
                }),
                Constraint.RelativeToParent(p =>
                {//y
                    return 0;
                }),
                Constraint.RelativeToParent(p =>
                {//width
                    return MaxWidth;
                }),
                Constraint.RelativeToParent(p =>
                {//height
                    return p.Height;
                })
            );



        }
        public void ShowMenu()
        {
            OnMenuDismissLayoutClicked(false);
        }
        public void OffMenu()
        {
            OnMenuDismissLayoutClicked(true);
        }
    }
}
