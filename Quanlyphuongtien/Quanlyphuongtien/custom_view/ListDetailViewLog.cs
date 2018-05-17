using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Quanlyphuongtien
{
    public class ListDetailViewLog
    {
        public StackLayout Container { get; internal set; }
        public StackLayout ViewLayout { get; set; }
        Action<bool> callback;
        private Label lblScrollHeight;
        private Label lblScrollY;
        private bool preventOff = false;

        public ListDetailViewLog(Action<bool> iCallback = null)
        {
            callback = iCallback;
            ViewLayout = new StackLayout
            {

            };
            //< Frame HorizontalOptions = "Fill"
            //          VerticalOptions = "FillAndExpand"
            //          Padding = "0" >
            //       < ScrollView >
            //           < StackLayout Spacing = "0" >

            //                < StackLayout Padding = "10"
            //                            BackgroundColor = "#eaeff2" >
            //                   < Label Text = "Chọn loại xe" />

            //                </ StackLayout >

            //                < StackLayout x: Name = "VehicleTypeSelectView"
            //                            Spacing = "0" >

            //               </ StackLayout >
            //           </ StackLayout >
            //       </ ScrollView >
            //   </ Frame >
            ScrollView scroll = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 0,
                    Padding = 0,
                    Children =
                    {
                        new StackLayout{
                            //BackgroundColor = Color.FromHex("#a7a7a7"),
                            //VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.FromHex("#eaeff2"),
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Padding = 10,
                            Children =
                            {
                                new Label
                                {
                                    Text = "Dữ liệu lộ trình",
                                    HorizontalOptions = LayoutOptions.Start,
                                    VerticalOptions = LayoutOptions.Center,
                                }
                            }
                        },
                        ViewLayout
                    },
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer
                        {
                            Command = new Command(()=>{
                                //preventOff = true;
                            })
                        }
                    }
                },
            };
            scroll.Scrolled += Scroll_Scrolled;
            Frame content = new Frame
            {
                CornerRadius = 0,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = 0,
                Content = scroll,

            };

            lblScrollHeight = new Label
            {
                Text = "",
            };
            lblScrollY = new Label
            {
                Text = "",
            };
            Container = new StackLayout
            {
                IsVisible = false,
                BackgroundColor = Color.FromRgba(0, 0, 0, 0.8),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 20, 10, 10),
                Children =
                {
                    new StackLayout{
                        HorizontalOptions=LayoutOptions.FillAndExpand,
                        VerticalOptions=LayoutOptions.FillAndExpand,
                        Children={
                           content,
                           //lblScrollHeight,
                           //lblScrollY,
                           new Image
                           {
                               Source = "Down.png",
                               HeightRequest=40,
                               WidthRequest=40,
                               VerticalOptions = LayoutOptions.End,
                               HorizontalOptions = LayoutOptions.CenterAndExpand,
                           },
                        //   new ButtonBorder(
                        //    icon: "Down.png",
                        //    iCornerRadius: 40,
                        //    iBackgroundColor: "#00000000",
                        //    iPadding: 0,
                        //    iCommand: new Command(async()=>{
                        //        await Off();
                        //    })
                        //)
                        //   {
                        //       VerticalOptions = LayoutOptions.End,
                        //       HorizontalOptions = LayoutOptions.CenterAndExpand,
                        //   }
                        }
                }

                },
                GestureRecognizers =
                {
                    new TapGestureRecognizer
                    {
                        Command = new Command(async()=>{
                            await Off();
                        })
                    }
                }

            };


        }

        private void Scroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            ScrollView a = (ScrollView)sender;

            var c = (uint)a.ScrollY + 1;
            var b = (uint)a.ContentSize.Height - (uint)a.Height;

            //lblScrollHeight.Text = "e.ScrollY: " + c.ToString();
            //lblScrollY.Text = "a.ContentSize.Height: " + b.ToString();

            callback?.Invoke(c >= b-200);

        }

        public async Task Show()
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Container.IsVisible = true;
                    await Container.FadeTo(1, 250);
                });
            });


        }
        public async Task Off()
        {
            if (!preventOff)
            {
                await Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Container.FadeTo(0, 250);
                        Container.IsVisible = false;
                    });
                });

            }
            else
            {
                preventOff = false;
            }


        }


    }
}
