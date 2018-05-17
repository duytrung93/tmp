using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;
using XLabs.Platform.Services.Geolocation;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing.Impl;

namespace Quanlyphuongtien
{
    public partial class MainPage : ContentPage
    {
        private DataAccess dataAccess;
        private LoadingWait loading;
        private StackLayout stackLayout;

        public UserLogin ActiveUser { get; set; }
        public RelativeLayout MainContainer { get; private set; }

        public MainPage()
        {


        
            dataAccess = new DataAccess();
            Task.Run(async () =>
            {
                ActiveUser = await dataAccess.GetUserLogin();
                if (ActiveUser == null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PushModalAsync(new LoginPage((iUserLogin) =>
                       {
                           ActiveUser = iUserLogin;
                           Device.BeginInvokeOnMainThread(() =>
                           {
                               Init();
                           });
                       }), false);
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Init();
                    });

                }
            });


        }

        private void Init()
        {
            MainContainer = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
            };
            Content = MainContainer;
            InitLayout();
            InitLoading();

        }

        private void InitLayout()
        {
            stackLayout = new StackLayout()
            {
                Padding = 0,
                Spacing = 0,
            };
            if (Device.RuntimePlatform == Device.iOS)
            {
                MainContainer.Children.Add(
             new ScrollView { Content = stackLayout },
             Constraint.RelativeToParent(p => 0),
             Constraint.RelativeToParent(p => 20),
             Constraint.RelativeToParent(p => p.Width),
             Constraint.RelativeToParent(p => p.Height - 20)
             );
            }
            else
            {
                MainContainer.Children.Add(
             new ScrollView { Content = stackLayout },
             Constraint.RelativeToParent(p => 0),
             Constraint.RelativeToParent(p => 0),
             Constraint.RelativeToParent(p => p.Width),
             Constraint.RelativeToParent(p => p.Height)
             );
            }
            InitHeader();
            InitContent();
        }

        private void InitContent()
        {
            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 300,
                ColumnSpacing = 0,
                RowSpacing = 0,
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width=GridLength.Star },
                    new ColumnDefinition {Width=GridLength.Star },
                    new ColumnDefinition {Width=GridLength.Star },
                },
                RowDefinitions =
                {
                    new RowDefinition {Height=GridLength.Star },
                    new RowDefinition {Height=GridLength.Star },
                    new RowDefinition {Height=GridLength.Star },
                }
            };
            grid.Children.Add(new MainPageButton(i18n.MainPageBtnTracking, "global.png", new Command(async () =>
            {
                await loading.ShowLoading(i18n.MessageInitData);
                Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
                {
                    Navigation.PushModalAsync(new TrackingPage(ActiveUser));
                    loading.OffLoading();
                    return false;
                });

            })), 0, 0);
            grid.Children.Add(new MainPageButton(i18n.MainPageBtnRouter, "map.png", new Command(async () =>
            {
                await loading.ShowLoading(i18n.MessageInitData);
                Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
                {
                    Navigation.PushModalAsync(new ViewLogPage(ActiveUser));
                    loading.OffLoading();
                    return false;
                });
            })), 1, 0);
            grid.Children.Add(new MainPageButton(i18n.MainPageBtnReport, "report.png"), 2, 0);
            grid.Children.Add(new MainPageButton(i18n.MainPageBtnContact, "phone.png", new Command(async () =>
            {
                await loading.ShowLoading(i18n.MessageInitData);
                Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
                {
                    Navigation.PushModalAsync(new ContactPage(ActiveUser));
                    loading.OffLoading();
                    return false;
                });
            })), 0, 1);
            grid.Children.Add(new MainPageButton(i18n.MainPageBtnUser, "setting.png", new Command(async () =>
            {
                await loading.ShowLoading(i18n.MessageInitData);
                Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
                {
                    Navigation.PushModalAsync(new AccountPage(ActiveUser));
                    loading.OffLoading();
                    return false;
                });
            })), 1, 1);
            grid.Children.Add(new MainPageButton(i18n.MainPageBtnLogout, "exit.png", new Command(async () => { await LogoutAsync(); })), 2, 1);

            stackLayout.Children.Add(new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = 10,
                Children = {
                    grid
                }
            });

        }

        public async Task LogoutAsync()
        {
            loading.ShowLoading(i18n.MessageSystemProcessing);
            await dataAccess.DeleteUserLogin();
            loading.OffLoading();
            ActiveUser = null;
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    await Navigation.PushModalAsync(new LoginPage((userLogin) =>
                    {
                        ActiveUser = userLogin;
                    }));
                    //App.Current.MainPage = new MainPage();
                }
                catch (Exception ex)
                {
                    var a = ex.Message;
                    throw;
                }


            });
        }

        private void InitHeader()
        {
            stackLayout.Children.Add(new StackLayout
            {
                HeightRequest = 50,
                BackgroundColor = Color.FromHex("#3a5795"),
                Children = {
                    new Label
                    {
                        Text = i18n.MainPageTitle.ToUpper(),
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    }
                }
            });
        }

        private void InitLoading()
        {
            loading = new LoadingWait();
            MainContainer.Children.Add(
                loading.Container,
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
        }
    }
}
