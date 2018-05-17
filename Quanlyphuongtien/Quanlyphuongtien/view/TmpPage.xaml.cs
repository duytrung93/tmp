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
    public partial class TmpPage : ContentPage
    {
        private StackLayout stackLayout;
        private DataAccess dataAccess;
        private RelativeLayout MainContainer;
        private LoadingWait loading;

        public TmpPage()
        {
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
            MainContainer.Children.Add(
               new ScrollView { Content = stackLayout },
               Constraint.RelativeToParent(p => 0),
               Constraint.RelativeToParent(p => 0),
               Constraint.RelativeToParent(p => p.Width),
               Constraint.RelativeToParent(p => p.Height)
               );
            InitHead();
            InitPage();
        }

        private void InitPage()
        {
            var formlayout = new StackLayout
            {
                Padding = 10,
                Spacing = 10
            };
            stackLayout.Children.Add(formlayout);

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
                                HeightRequest=40
                            },
                        }

            };
            Utility.SetEffect(btnBack, colorEffect: "#fff", command: new Command(async () =>
            {
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
                        Text = i18n.TrackingPageTitle.ToUpper(),
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    }
                }
            });
        }
    }
}