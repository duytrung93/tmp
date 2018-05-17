using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace Quanlyphuongtien
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutocompleteView : ContentPage
    {
        public Action<ParaResult> callBack;
        public List<ParaResult> sourceItem;
        private StackLayout stacklayout;
        private EntryForm inputSearch;
        private RelativeLayout MainLayout;
        private StackLayout listLayout;
        private int timeFlag = 0;
        private double timeWait = 500;

        public AutocompleteView() { }
        public AutocompleteView(String iApiName, object param, Type iType, Action<ParaResult> iCallBack = null)
        {
            sourceItem = new List<ParaResult>();
            callBack = iCallBack;
            MinimumLentgh = 0;
            Placeholder = "Tìm kiếm...";
            InitLayout();
            Device.BeginInvokeOnMainThread(async () =>
            {
                GetAPIResponse response = await GetDataAPI.GetAPI(iApiName, param);

                if (response.Status == HttpStatusCode.OK)
                {

                    if (iType == typeof(List<VehicleOnline>))
                    {
                        List<VehicleOnline> vehicleOnlines = JsonConvert.DeserializeObject<List<VehicleOnline>>(response.Result) ?? new List<VehicleOnline>();
                        vehicleOnlines.ForEach(item =>
                        {
                            sourceItem.Add(new ParaResult
                            {
                                Text = item.Plate,
                                Data = item
                            });
                        });
                    }
                    Init();
                }
                else
                {
                    await DisplayAlert(i18n.MessageCallAPIFail, response.Result, i18n.MessageClose);
                }
            });
        }

        private void InitLayout()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                stacklayout = new StackLayout
                {
                    Padding = 0,
                    Spacing = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                if (Device.RuntimePlatform == Device.iOS)
                {
                    stacklayout.Padding = new Thickness(0, 20, 0, 0);
                }

                Content = stacklayout;


                inputSearch = new EntryForm(placeholder: Placeholder, label: iLabel)
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White
                };
                Entry e = ((Entry)inputSearch.entry);
                e.HeightRequest = 48;

                ((Entry)inputSearch.entry).TextChanged += AutocompleteView_TextChanged;
                stacklayout.Children.Add(new StackLayout
                {
                    BackgroundColor = Color.FromHex(Contanst.PrimaryColor),
                    HeightRequest = 50,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Fill,
                    Children =
                    {

                        new StackLayout{
                            HeightRequest=50,
                            WidthRequest=50,
                            GestureRecognizers = {
                                new TapGestureRecognizer{
                                    Command = new Command(async()=>{
                                        try
                                        {
                                            await Navigation.PopModalAsync();
                                        }
                                        catch (Exception)
                                        {

                                        }
                                    })
                                }
                            },
                            Children =
                            {
                                new Image
                                {
                                    Source="arrow_left.png",
                                    HeightRequest = 25,
                                    HorizontalOptions=LayoutOptions.CenterAndExpand,
                                    VerticalOptions=LayoutOptions.CenterAndExpand
                                },
                            }
                        },
                        inputSearch
                    }
                });

                MainLayout = new RelativeLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White,
                };
                stacklayout.Children.Add(MainLayout);

                //Content
                listLayout = new StackLayout
                {

                };
                MainLayout.Children.Add(new ScrollView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Content = listLayout
                },
                Constraint.RelativeToParent(p => 0),
                Constraint.RelativeToParent(p => 0),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height)
                );
            });
        }

        public string Placeholder { get; private set; }
        public Label iLabel { get; private set; }
        public int MinimumLentgh { get; private set; }

        public static AutocompleteView GetAutocomplete(String iApiName, object param, Action<ParaResult> iCallBack)
        {
            AutocompleteView autocompleteView = new AutocompleteView();
            return autocompleteView;
        }

        public static AutocompleteView GetAutocomplete(List<ParaResult> iSource, Action<ParaResult> iCallBack)
        {
            AutocompleteView autocompleteView = new AutocompleteView();
            autocompleteView.callBack = iCallBack;
            autocompleteView.sourceItem = iSource;


            autocompleteView.Init();

            return autocompleteView;
        }

        private void Init()
        {

            sourceItem = sourceItem ?? new List<ParaResult>();
            DrawListItem(sourceItem);
        }

        private void AutocompleteView_TextChanged(object sender, TextChangedEventArgs e)
        {
            timeFlag += 1;
            var timeFlagAction = timeFlag;


            Device.StartTimer(TimeSpan.FromMilliseconds(timeWait), () =>
            {
                if (timeFlagAction == timeFlag)
                {
                    BorderlessEntry input = (BorderlessEntry)sender;
                    if (input.Text.Length < MinimumLentgh)
                        return false;
                    var text = input.Text;
                    if ((input.Text ?? "").Length > 0)
                    {
                        List<ParaResult> listSearched = sourceItem.FindAll(p => p.Text.ToLower().IndexOf(input.Text.ToLower()) > -1);
                        DrawListItem(listSearched);
                    }
                    else
                    {
                        DrawListItem(sourceItem);
                    }

                    return false;
                }
                else
                    return false;
                {
                }
            });

        }

        private void DrawListItem(List<ParaResult> iListItem)
        {
            listLayout.Children.Clear();
            iListItem.ForEach(item =>
            {
                ItemList itemList = new ItemList(new Label
                {
                    Text = item.Text,
                });
                listLayout.Children.Add(itemList);
                Utility.SetEffect(itemList, command: new Command(async () =>
                {
                    itemList.IsEnabled = false;
                    await Navigation.PopModalAsync();
                    callBack?.Invoke(item);

                }));
            });
        }
    }
}