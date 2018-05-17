using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quanlyphuongtien
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private StackLayout stackLayout;
        private DataAccess dataAccess;

        public UserLogin ActiveUser { get; }

        private RelativeLayout MainContainer;
        private LoadingWait loading;
        private EntryForm txtUserName;
        private EntryForm txtFullName;
        private EntryForm txtOldPassword;
        private EntryForm txtNewPass;
        private EntryForm txtReNewPass;

        public AccountPage(UserLogin iUserLogin)
        {
            ActiveUser = iUserLogin;

            MainContainer = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            Content = MainContainer;
            dataAccess = new DataAccess();
            InitLayout();
            InitLoading();
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
            InitHead();
            InitPage();
        }

        private void InitPage()
        {
            var formlayout = new StackLayout
            {
                Padding = 10,
                Spacing = 10,
                VerticalOptions = LayoutOptions.FillAndExpand

            };
            stackLayout.Children.Add(formlayout);

            txtUserName = new EntryForm(placeholder: "", label: new Label
            {
                Text = i18n.AccountPageTxtUserName,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10);

            txtFullName = new EntryForm(placeholder: "", label: new Label
            {
                Text = i18n.AccountPageTxtFullName,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10);

            txtOldPassword = new EntryForm(placeholder: "", isPassword: true, label: new Label
            {
                Text = i18n.AccountPageTxtOldPassword,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10);
            txtNewPass = new EntryForm(placeholder: "", isPassword: true, label: new Label
            {
                Text = i18n.AccountPageTxtNewPass,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10);
            txtReNewPass = new EntryForm(placeholder: "", isPassword: true, label: new Label
            {
                Text = i18n.AccountPageTxtReNewPass,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10);
            ((BorderlessEntry)txtUserName.entry).Text = ActiveUser.UserName;
            ((BorderlessEntry)txtUserName.entry).IsEnabled = false;
            ((BorderlessEntry)txtFullName.entry).Text = ActiveUser.FullName;
            ((BorderlessEntry)txtFullName.entry).IsEnabled = false;

            formlayout.Children.Add(txtUserName);
            formlayout.Children.Add(txtFullName);
            formlayout.Children.Add(txtOldPassword);
            formlayout.Children.Add(txtNewPass);
            formlayout.Children.Add(txtReNewPass);
            formlayout.Children.Add(new StackLayout
            {
                Children =
                {
                    new ButtonBorder(
                    text: i18n.AccountPagebtnSubmit,
                    iTextColor: "#fff",
                    iBackgroundColor: "#4a89dc",
                    iCornerRadius: 10,
                    iCommand: new Command(async() =>
                    {
                        await GetVehicleLogAsync();
                    }))
                }
            });
        }

        private async Task GetVehicleLogAsync()
        {

            BorderlessEntry pass = ((BorderlessEntry)txtOldPassword.entry);
            BorderlessEntry newPass = ((BorderlessEntry)txtNewPass.entry);
            BorderlessEntry reNewPass = ((BorderlessEntry)txtReNewPass.entry);
            if ((pass.Text ?? "").Length == 0)
            {
                pass.Focus();
                return;
            }
            if ((newPass.Text ?? "").Length == 0)
            {
                newPass.Focus();
                return;
            }
            if ((reNewPass.Text ?? "").Length == 0)
            {
                reNewPass.Focus();
                return;
            }

            if (!reNewPass.Text.Equals(newPass.Text))
            {
                await DisplayAlert(i18n.MessageAlert, i18n.AccountPageNewPassNotEqualRePass, i18n.MessageClose);
                return;
            }


            //await DisplayAlert("abc", "OK", "OK");
            loading.ShowLoading(i18n.MessageSystemProcessing);
            GetAPIResponse response = await GetDataAPI.GetAPI(API.ChangePassword, new
            {
                iPasswordOld = pass.Text,
                iPasswordNew = reNewPass.Text,
                iTockenkey = ActiveUser.Tockenkey
            });
            if (response.Status == HttpStatusCode.OK)
            {
                loading.OffLoading();
                await DisplayAlert(i18n.MessageAlert, i18n.AccountPageChangePassOK, i18n.MessageClose);
                await Navigation.PopModalAsync();
                var mainPage = App.Current.MainPage as MainPage;
                await mainPage.LogoutAsync();
            }
            else
            {
                await DisplayAlert(i18n.MessageCallAPIFail, response.Result, i18n.MessageClose);
            }
        }

        private void InitHead()
        {
            StackLayout btnBack = new StackLayout
            {
                Padding = 5,
                HeightRequest = 50,
                Children ={
                            new Image
                            {
                                Source = "arrow_left.png",
                                HeightRequest=25,
                                VerticalOptions = LayoutOptions.CenterAndExpand
                            },
                        }

            };
            Utility.SetEffect(btnBack, colorEffect: "#fff", command: new Command(async () =>
            {

                await loading.ShowLoading(i18n.MessageSystemProcessing);
                await Navigation.PopModalAsync();

            }));
            stackLayout.Children.Add(new StackLayout
            {
                HeightRequest = 50,
                BackgroundColor = Color.FromHex("#3a5795"),
                Spacing = 5,
                Orientation = StackOrientation.Horizontal,
                Children = {
                    btnBack,

                    new Label
                    {
                        TranslationX=-25,
                        Text = i18n.AccountPageTitle.ToUpper(),
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    }
                }
            });
        }
    }
}