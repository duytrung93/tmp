using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace Quanlyphuongtien
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private RelativeLayout MainContainer;
        private LoadingWait loading;
        private StackLayout stackLayout;
        private EntryForm txtUsername;
        private EntryForm txtPassword;
        private DataAccess dataAccess;
        private Action<UserLogin> CallBack;

        public LoginPage(Action<UserLogin> iCallBack)
        {


            CallBack = iCallBack;
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
             Constraint.RelativeToParent(p => p.Height-20)
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

            InitForm();
        }

        private void InitForm()
        {
            var formlayout = new StackLayout
            {
                Padding = 10,
                Spacing = 10
            };
            stackLayout.Children.Add(formlayout);

            txtUsername = new EntryForm(i18n.LoginPageTxtUsername, "icons8_user.png");
            ((BorderlessEntry)txtUsername.entry).Completed += txtUsername_Entry_Completed;
            formlayout.Children.Add(txtUsername);
            txtPassword = new EntryForm(i18n.LoginPageTxtPassword, "icons8_password_1.png", isPassword: true);
            ((BorderlessEntry)txtPassword.entry).Completed += txtPasswordEntry_Completed;
            formlayout.Children.Add(txtPassword);

            //Button btnLoginn = new Button
            //{
            //    Text = i18n.LoginPageBtnLoginLabel,
            //    BackgroundColor = Color.FromHex("#4a89dc"),
            //    TextColor = Color.White,

            //};
            ButtonBorder buttonBorder = new ButtonBorder(text: i18n.LoginPageBtnLoginLabel, iTextColor: "#fff", iBackgroundColor: "#4a89dc", iBorderWidth: 1, iCornerRadius: 5, iCommand: new Command(() =>
              {
                  ProcessLogin();
              }));
            buttonBorder.HorizontalOptions = LayoutOptions.End;







            //btnLoginn.Clicked += BtnLoginn_Clicked;
            formlayout.Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children =
                        {
                            new Label
                            {
                                Text=i18n.LogInPageLblForgotPass,
                                TextColor = Color.FromHex("#4a89dc"),
                            },
                            new CheckBox
                            {
                                DefaultText=i18n.LoginPageCbxAutoLogin,
                                Checked=true,
                            }
                        }
                    },
                    buttonBorder
                }
            });
            formlayout.Children.Add(new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            });
            formlayout.Children.Add(new Label
            {
                Text = i18n.LoginPageLblHotLine,
                TextColor = Color.FromHex("#626262"),
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.End
            });


        }

        private void txtPasswordEntry_Completed(object sender, EventArgs e)
        {
            ProcessLogin();
        }

        private void txtUsername_Entry_Completed(object sender, EventArgs e)
        {
            ProcessLogin();
        }

        private void ProcessLogin()
        {

            if ((((BorderlessEntry)txtUsername.entry).Text ?? "").Length == 0)
            {
                txtUsername.entry.Focus();
                return;
            }
            if ((((BorderlessEntry)txtPassword.entry).Text ?? "").Length == 0)
            {
                txtPassword.entry.Focus();
                return;
            }
            var username = ((BorderlessEntry)txtUsername.entry).Text;
            var password = ((BorderlessEntry)txtPassword.entry).Text;
            loading.ShowLoading(i18n.MessageSystemProcessing);
            Device.BeginInvokeOnMainThread(async () =>
            {
                GetAPIResponse response = await GetDataAPI.GetAPI(API.Login, new { iUserName = username, iPassword = password });
                loading.OffLoading();
                if (response.Status == HttpStatusCode.OK)
                {
                    UserLogin activeUser = new UserLogin(response.Result);

                    await dataAccess.SaveUserLogin(activeUser);
                    await Navigation.PopModalAsync();
                    CallBack?.Invoke(activeUser);

                }
                else
                {
                    await DisplayAlert(i18n.MessageAlert, response.Result, i18n.MessageClose);
                }

            });

        }

        private void BtnLoginn_Clicked(object sender, EventArgs e)
        {
            ProcessLogin();

        }

        private void InitHead()
        {
            stackLayout.Children.Add(new StackLayout
            {
                HeightRequest = 50,
                BackgroundColor = Color.FromHex("#3a5795"),
                Children = {
                    new Label
                    {
                        Text = i18n.LoginPageTitle.ToUpper(),
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    }
                }
            });
            Image logoImage = new Image
            {
                HeightRequest = 120,
                Source = "logo.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            StackLayout logoContain = new StackLayout
            {
                HeightRequest = 120,
                Children = {
                    logoImage
                }
            };
            stackLayout.Children.Add(logoContain);
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }

}