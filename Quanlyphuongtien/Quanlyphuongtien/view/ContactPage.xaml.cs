using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Quanlyphuongtien
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        private StackLayout stackLayout;
        private DataAccess dataAccess;

        public UserLogin ActiveUser { get; }

        private RelativeLayout MainContainer;
        private LoadingWait loading;

        public ContactPage(UserLogin iUserLogin)
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

            /*
             Trụ sở: Số 19, Tổ 29, Khu X2A, Yên Sở, Hoàng Mai, Hà Nội

Chi nhánh tại Miền Nam: Số 279/006C Âu Cơ, Phường 5, Quận 11, TP. Hồ Chí Minh

Tổng đài CSKH: 1900.6735
             */
            formlayout.Children.Add(new MultiLineLabel
            {
                MaxLine = 2,
                FormattedText = new FormattedString
                {

                    Spans =
                    {
                        new Span
                        {
                            Text = i18n.ContactPagelblCompanyName,
                            FontAttributes = FontAttributes.Bold
                        },

                    }
                }
            });

            formlayout.Children.Add(new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label
                    {
                        Text = i18n.ContactPagelblAddress,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        WidthRequest=65,
                        LineBreakMode = LineBreakMode.NoWrap,
                    },
                    new MultiLineLabel
                    {
                        MaxLine = 2,
                        Text = i18n.ContactPageValueAddress,

                        HorizontalOptions = LayoutOptions.FillAndExpand,

                    },
                }
            });
            formlayout.Children.Add(new MultiLineLabel
            {
                MaxLine = 2,
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span
                        {
                            Text = i18n.ContactPagelblEmail,
                            FontAttributes = FontAttributes.Bold,
                        },
                         new Span
                        {
                            Text = i18n.ContactPageValueEmail,
                        },
                    }
                }
            });
            formlayout.Children.Add(new MultiLineLabel
            {
                MaxLine = 2,
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span
                        {
                            Text = i18n.ContactPagelblphone,
                            FontAttributes = FontAttributes.Bold
                        },
                         new Span
                        {
                            Text = i18n.ContactPageValuePhone,
                        },
                    }
                }
            });

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
                        Text = i18n.ContactPageTitle.ToUpper(),
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    }
                }
            });
        }
    }
}