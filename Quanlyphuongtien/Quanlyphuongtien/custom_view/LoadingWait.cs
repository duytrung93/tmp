using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Quanlyphuongtien
{
    public class LoadingWait
    {
        public StackLayout Container { get; internal set; }
        public Label WaitText { get; internal set; }
        public LoadingWait()
        {
            Image loadingImg = new Image
            {
                Source = "spinter1.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            WaitText = new Label
            {
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };
            Container = new StackLayout
            {
                IsVisible = false,
                BackgroundColor = Color.FromRgba(0, 0, 0, 0.5),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 30,
                Children =
                {
                    new StackLayout{
                        HorizontalOptions=LayoutOptions.CenterAndExpand,
                        VerticalOptions=LayoutOptions.CenterAndExpand,
                        Children={
                           loadingImg,
                           WaitText
                        }
                    }
                }
            };
            Task.Run(async () =>
            {
                while (true)
                {
                    await loadingImg.RotateTo(360 * 10, 1600 * 10, Easing.Linear);
                    await loadingImg.RotateTo(0, 0); // reset to initial position
                }

            });
        }
        public void ShowLoading()
        {
            ShowLoading(null);
        }
        public Task ShowLoading(string message)
        {
            return Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Container.IsVisible = true;
                    WaitText.Text = message ?? i18n.MessageSystemProcessing;
                    await Container.FadeTo(1, 250);
                });
            });


        }
        public async void OffLoading()
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Container.FadeTo(0, 250);
                    WaitText.Text = "";
                    Container.IsVisible = false;
                });
            });

        }

        internal void ShowLoading(object messageSystemProcessing)
        {
            throw new NotImplementedException();
        }
    }
}
