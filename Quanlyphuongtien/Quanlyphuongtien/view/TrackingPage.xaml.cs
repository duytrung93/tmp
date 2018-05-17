using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Ioc;
using Xamarin.Forms.GoogleMaps;
using System.Threading;

namespace Quanlyphuongtien
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackingPage : ContentPage
    {
        private StackLayout stackLayout;
        private DataAccess dataAccess;
        private RelativeLayout MainContainer;
        private LoadingWait loading;
        private UserLogin ActiveUser;
        private EntryForm txtSearchVehicleOnline;
        private List<VehicleOnline> vehicleOnlines;
        private StackLayout listVehicleView;
        private double widthContent;
        private Map map;
        private List<Pin> pins;
        private VehicleOnline selectedVehicle;
        private VehicleTooltip vehicleTooltip = new VehicleTooltip();
        private Pin selectedVehicleIdPin;
        private RelativeLayout mapPage;
        private StackLayout vehiclePage;
        private ButtonBorder btnListVehicle;
        private ButtonBorder btnMap;
        private StackLayout tabLayout;
        private double timeItv = 10 * 1000;//Millisecond
        private StackLayout infoBoxLayout;
        private bool isShowAllMarker = true;
        private bool allowOffForm = true;

        private bool IsMapFirstShow { get; set; }

        public RelativeLayout MainLayout { get; private set; }
        public Label TrackingPageInfoBoxPlate { get; private set; }
        public Span TrackingPageInfoBoxLblAddress { get; private set; }
        public Span TrackingPageInfoBoxLblDtime { get; private set; }
        public Span TrackingPageInfoBoxLblOil { get; private set; }
        public Span TrackingPageInfoBoxLblDoorState { get; private set; }
        public Span TrackingPageInfoBoxLblAir { get; private set; }
        public Span TrackingPageInfoBoxLblTotalTimeRun { get; private set; }
        public Span TrackingPageInfoBoxLblTotalTimePauseOn { get; private set; }
        public Span TrackingPageInfoBoxLblTotalKM { get; private set; }
        public Span TrackingPageInfoBoxLblSpeed { get; private set; }
        public Span TrackingPageInfoBoxLblStatus { get; private set; }

        public TrackingPage(UserLogin iUserLogin)
        {
            IsMapFirstShow = false;
            ActiveUser = iUserLogin;
            if (ActiveUser == null)
            {
                Navigation.PushModalAsync(new LoginPage((loginResult) =>
                {
                    ActiveUser = loginResult;
                    Init();

                }));
            }
            else
            {
                Init();
            }
        }

        private void Init()
        {

            var a = (MainPage)App.Current.MainPage;
            widthContent = a.Width;

            MainContainer = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
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
                VerticalOptions = LayoutOptions.FillAndExpand,
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
            InitTab();
            InitPage();
        }

        private void InitTab()
        {


            Grid gridTab = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 0,
                RowSpacing = 0,
                ColumnDefinitions =
                        {
                            new ColumnDefinition  { Width=GridLength.Star},
                            new ColumnDefinition  { Width=GridLength.Star},
                        },
                RowDefinitions =
                        {
                            new RowDefinition{  Height = GridLength.Star}
                        }
            };

            btnListVehicle = new ButtonBorder(i18n.TrackingpageBtnListVehicle, iBackgroundColor: "#3bafda", iPadding: 5, iTextColor: "#fff", iCommand: new Command(() =>
            {
                OpenListVehicle();
            }));

            btnMap = new ButtonBorder(i18n.TrackingpageBtnMap, iPadding: 5, iCommand: new Command(() =>
            {
                OpenMap();
            }));

            gridTab.Children.Add(btnListVehicle, 0, 0);
            gridTab.Children.Add(btnMap, 1, 0);
            tabLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                Children =
                {
                    gridTab

                }
            };
            stackLayout.Children.Add(tabLayout);

        }

        private void OpenListVehicle()
        {
            btnListVehicle.MainContainer.BackgroundColor = Color.FromHex("#3bafda");
            btnListVehicle.label.TextColor = Color.FromHex("#fff");

            btnMap.MainContainer.BackgroundColor = Color.FromHex("#DDD");
            btnMap.label.TextColor = Color.FromHex("#000");
            Device.BeginInvokeOnMainThread(async () =>
            {
                await mapPage.TranslateTo(MainLayout.Width, 0);

            });
            Device.BeginInvokeOnMainThread(async () =>
            {
                await vehiclePage.TranslateTo(0, 0);

            });
        }

        private void OpenMap()
        {





            IsMapFirstShow = true;
            btnMap.MainContainer.BackgroundColor = Color.FromHex("#3bafda");
            btnMap.label.TextColor = Color.FromHex("#fff");

            btnListVehicle.MainContainer.BackgroundColor = Color.FromHex("#DDD");
            btnListVehicle.label.TextColor = Color.FromHex("#000");

            Device.BeginInvokeOnMainThread(async () =>
            {
                await vehiclePage.TranslateTo(-MainLayout.Width, 0);
            });
            Device.BeginInvokeOnMainThread(async () =>
            {
                await mapPage.TranslateTo(0, 0);
            });
            DrawPinToMap();



        }

        private void InitPage()
        {
            MainLayout = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = MainContainer.Height - 50
                //BackgroundColor = Color.Red,
            };


            stackLayout.Children.Add(MainLayout);
            InitMap(MainLayout);

            InitListVehiclePage(MainLayout);



        }

        private void InitMap(RelativeLayout mainLayout)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                mapPage = new RelativeLayout
                {
                    BackgroundColor = Color.White,
                    TranslationX = widthContent,
                };
                mainLayout.Children.Add(
                 mapPage,
                 Constraint.RelativeToParent(p => 0),
                 Constraint.RelativeToParent(p => 0),
                 Constraint.RelativeToParent(p => p.Width),
                 Constraint.RelativeToParent(p => p.Height)
                 );
                map = new Map()
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    MapType = MapType.Street,
                    MyLocationEnabled = true
                };
                map.UiSettings.RotateGesturesEnabled = false;
                map.UiSettings.ZoomControlsEnabled = false;
                map.UiSettings.MyLocationButtonEnabled = true;

                mapPage.Children.Add(map,
                 Constraint.RelativeToParent(p => 0),
                 Constraint.RelativeToParent(p => 0),
                 Constraint.RelativeToParent(p => p.Width),
                 Constraint.RelativeToParent(p => p.Height - 30)
                 );


                //var myLocation = await geolocator.GetPositionAsync(10000);
                //if (myLocation != null)
                //    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(myLocation.Latitude, myLocation.Longitude), Distance.FromKilometers(5)), false);

                infoBoxLayout = new StackLayout
                {
                    BackgroundColor = Color.White,
                    Children =
                    {

                    }
                };

                Label btnTogleInfoBox = new Label
                {
                    Text = i18n.TrackingPageInfoBoxBtnShow,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    TextColor = Color.White
                };
                StackLayout btnToggleInfoBox = new StackLayout
                {
                    Padding = new Thickness(10, 0),
                    BackgroundColor = Color.Red,
                    HorizontalOptions = LayoutOptions.End,
                    Children =
                    {
                        btnTogleInfoBox
                    }
                };

                Utility.SetEffect(btnToggleInfoBox, command: new Command(() =>
                {
                    if (infoBoxLayout.TranslationY == 0)
                    {
                        btnTogleInfoBox.Text = i18n.TrackingPageInfoBoxBtnHide;
                        infoBoxLayout.TranslateTo(0, -infoBoxLayout.Height + 30);
                    }
                    else
                    {
                        btnTogleInfoBox.Text = i18n.TrackingPageInfoBoxBtnShow;
                        infoBoxLayout.TranslateTo(0, 0);
                    }

                }));

                TrackingPageInfoBoxPlate = new Label
                {
                    Text = i18n.TrackingPageInfoBoxPlate,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = Color.White,
                };
                TrackingPageInfoBoxLblAddress = new Span
                {
                    Text = "",

                };

                TrackingPageInfoBoxLblDtime = new Span
                {
                    Text = "",

                };
                TrackingPageInfoBoxLblStatus = new Span
                {
                    Text = "",

                };
                TrackingPageInfoBoxLblTotalKM = new Span
                {
                    Text = "",

                };
                TrackingPageInfoBoxLblSpeed = new Span
                {
                    Text = "",
                };

                TrackingPageInfoBoxLblOil = new Span
                {
                    Text = "",

                };
                TrackingPageInfoBoxLblDoorState = new Span
                {
                    Text = "",

                };
                TrackingPageInfoBoxLblAir = new Span
                {
                    Text = "",

                };
                TrackingPageInfoBoxLblTotalTimeRun = new Span
                {
                    Text = "",

                };
                TrackingPageInfoBoxLblTotalTimePauseOn = new Span
                {
                    Text = "",

                };


                infoBoxLayout.Children.Add(new StackLayout
                {
                    Spacing = 0,
                    Children =
                {
                    new StackLayout{
                        HeightRequest = 30,
                        BackgroundColor = Color.FromHex("#3bafda"),
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            TrackingPageInfoBoxPlate,
                            btnToggleInfoBox,
                        }
                    },
                    new StackLayout
                    {
                        Padding=new Thickness(10,0),
                        Spacing=0,
                        Children =
                        {
                            new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        LineBreakMode=LineBreakMode.TailTruncation,
                                        FormattedText= new FormattedString{
                                            Spans =
                                            {
                                                new Span
                                                {
                                                    Text = i18n.TrackingPageInfoBoxLblAddress,
                                                    FontAttributes= FontAttributes.Bold,
                                                },
                                                TrackingPageInfoBoxLblAddress
                                            }
                                        }
                                    }
                                }
                            },
                            new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        FormattedText= new FormattedString{
                                            Spans =
                                            {
                                                new Span
                                                {
                                                    Text = i18n.TrackingPageInfoBoxLblDtime,
                                                    FontAttributes= FontAttributes.Bold,
                                                },
                                                TrackingPageInfoBoxLblDtime
                                            }
                                        }
                                    }
                                }
                            },
                            new StackLayout
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
                                                FormattedText= new FormattedString{
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageInfoBoxLblStatus,
                                                            FontAttributes= FontAttributes.Bold,
                                                        },
                                                        TrackingPageInfoBoxLblStatus
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    new StackLayout
                                    {
                                        IsVisible = false,
                                        HorizontalOptions = LayoutOptions.End,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FormattedText= new FormattedString{
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageInfoBoxLblSpeed,
                                                            FontAttributes= FontAttributes.Bold,
                                                        },
                                                        TrackingPageInfoBoxLblSpeed
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            },
                            new StackLayout
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
                                                FormattedText= new FormattedString{
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageInfoBoxLblTotalKM,
                                                            FontAttributes= FontAttributes.Bold,
                                                        },
                                                        TrackingPageInfoBoxLblTotalKM
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    new StackLayout
                                    {
                                        HorizontalOptions = LayoutOptions.End,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FormattedText= new FormattedString{
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageInfoBoxLblOil,
                                                            FontAttributes= FontAttributes.Bold,
                                                        },
                                                        TrackingPageInfoBoxLblOil
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            },
                            new StackLayout
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
                                                FormattedText= new FormattedString{
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageInfoBoxLblDoorState,
                                                            FontAttributes= FontAttributes.Bold,
                                                        },
                                                        TrackingPageInfoBoxLblDoorState
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    new StackLayout
                                    {
                                        HorizontalOptions = LayoutOptions.End,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FormattedText= new FormattedString{
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageInfoBoxLblAir,
                                                            FontAttributes= FontAttributes.Bold,
                                                        },
                                                        TrackingPageInfoBoxLblAir
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            },
                            new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {
                                     new StackLayout
                                    {
                                        Children =
                                        {
                                            new Label
                                            {
                                                FormattedText= new FormattedString{
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageInfoBoxLblTotalTimeRun,
                                                            FontAttributes= FontAttributes.Bold,
                                                        },
                                                        TrackingPageInfoBoxLblTotalTimeRun
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    new StackLayout
                                    {
                                        Children =
                                        {
                                            new Label
                                            {
                                                FormattedText= new FormattedString{
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageInfoBoxLblTotalTimePauseOn,
                                                            FontAttributes= FontAttributes.Bold,
                                                        },
                                                        TrackingPageInfoBoxLblTotalTimePauseOn
                                                    }
                                                }
                                            }
                                        }
                                    },

                                }
                            },


                        }
                    },



                }
                });
                LoadVehicleTooltip();




                mapPage.Children.Add(infoBoxLayout,
                 Constraint.RelativeToParent(p => 0),
                 Constraint.RelativeToParent(p => p.Height - 30),
                 Constraint.RelativeToParent(p => p.Width)
                 );


                pins = new List<Pin>();



            });

        }

        private void LoadVehicleTooltip()
        {
            VehicleTooltip item = vehicleTooltip;
            TrackingPageInfoBoxPlate.Text = ((selectedVehicle ?? new VehicleOnline()).Plate ?? i18n.TrackingPageInfoBoxPlate);
            TrackingPageInfoBoxLblAddress.Text = item.Address ?? "";
            TrackingPageInfoBoxLblDtime.Text = item.DateUpdate ?? "";
            TrackingPageInfoBoxLblOil.Text = item.Oil ?? "";
            TrackingPageInfoBoxLblDoorState.Text = item.DoorState ?? "";
            TrackingPageInfoBoxLblAir.Text = item.Air ?? "";
            TrackingPageInfoBoxLblTotalTimeRun.Text = item.TotalTimeRun ?? "";
            TrackingPageInfoBoxLblTotalTimePauseOn.Text = item.TotalTimePauseOn ?? "";
            TrackingPageInfoBoxLblTotalKM.Text = item.TotalKM ?? "";
            TrackingPageInfoBoxLblSpeed.Text = item.Speed ?? "";
            TrackingPageInfoBoxLblStatus.Text = item.Status ?? "";

        }

        private void InitListVehiclePage(RelativeLayout mainLayout)
        {
            vehiclePage = new StackLayout
            {
                BackgroundColor = Color.White,
                Spacing = 0,
            };
            mainLayout.Children.Add(
             vehiclePage,
             Constraint.RelativeToParent(p => 0),
             Constraint.RelativeToParent(p => 0),
             Constraint.RelativeToParent(p => p.Width),
             Constraint.RelativeToParent(p => p.Height)
             );
            InitSearchBox(vehiclePage);
            InitListVehicle(vehiclePage);
        }

        private void InitSearchBox(StackLayout layout)
        {
            txtSearchVehicleOnline = new EntryForm(i18n.TrackingPageTxtSearchVehicleOnline, iCornerRadius: 5)
            {
                Padding = 10
            };
            ((BorderlessEntry)txtSearchVehicleOnline.entry).Completed += Entry_Completed;
            ((BorderlessEntry)txtSearchVehicleOnline.entry).TextChanged += Entry_TextChanged;
            layout.Children.Add(txtSearchVehicleOnline);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry input = (Entry)sender;

            if ((input.Text ?? "").Length > 0)
            {
                List<VehicleOnline> listSearched = vehicleOnlines.FindAll(p => p.Plate.ToLower().IndexOf(input.Text.ToLower()) > -1);
                DrawListVehicleOnline(listSearched);
            }
            else
            {
                DrawListVehicleOnline(vehicleOnlines);
            }
        }

        private void Entry_Completed(object sender, EventArgs e)
        {

        }

        private void InitListVehicle(StackLayout layout)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                loading.ShowLoading(i18n.MessageApiCalling);
                GetAPIResponse response = await GetDataAPI.GetAPI(API.GetLeftMenuOnline, new { Tockenkey = ActiveUser.Tockenkey });
                loading.OffLoading();
                if (response.Status == HttpStatusCode.OK)
                {
                    GetLeftMenuOnline activeUser = new GetLeftMenuOnline(response.Result);

                    LeftMenuOnline leftMenuOnline = (activeUser.ListLeftMenuOnline ?? new List<LeftMenuOnline>()).FirstOrDefault();
                    if (leftMenuOnline != null)
                    {
                        listVehicleView = new StackLayout
                        {
                            Spacing = 0,
                        };
                        layout.Children.Add(new ScrollView
                        {
                            Content = listVehicleView,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = MainContainer.Height - txtSearchVehicleOnline.Height - tabLayout.Height - 50,
                        });
                        vehicleOnlines = (leftMenuOnline.Data ?? new List<VehicleOnline>());


                        DrawListVehicleOnline(vehicleOnlines);

                        await GetRealTimeOnlineAsync();

                    }
                }
                else
                {
                    await DisplayAlert(i18n.MessageCallAPIFail, response.Result, i18n.MessageClose);
                }
            });
        }

        private async Task GetRealTimeOnlineAsync()
        {

            var response = await GetDataAPI.GetAPI(API.GetVehicleOnlineByListId, new { iListVehicleId = "", Tockenkey = ActiveUser.Tockenkey });

            if (response.Status == HttpStatusCode.OK)
            {
                vehicleOnlines = VehicleOnline.GetListVehicleOnline(response.Result);
                if (vehicleOnlines != null)
                {
                    DrawListVehicleOnline(vehicleOnlines);
                    Entry_TextChanged(txtSearchVehicleOnline.entry, null);
                    //if (IsMapFirstShow)
                    //{
                    //    await DisplayAlert("abc", IsMapFirstShow.ToString(), "OK");
                    //}
                    if (IsMapFirstShow)
                    {
                        DrawPinToMap();

                    }

                    //LoadInfoBox
                    GetInfoboxInfo();


                    Device.StartTimer(TimeSpan.FromMilliseconds(timeItv), () =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await GetRealTimeOnlineAsync();
                        });
                        return false;
                    });
                }
            }
            else
            {
                await DisplayAlert(i18n.MessageCallAPIFail, response.Result, i18n.MessageClose);
            }




        }

        private void GetInfoboxInfo()
        {
            if (selectedVehicle != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var GetVehicleOnlineByListIdResponse = await GetDataAPI.GetAPI(API.GetVehicleTooltip, new { iVehicle_id = selectedVehicle.Id, Tockenkey = ActiveUser.Tockenkey });
                    if (GetVehicleOnlineByListIdResponse.Status == HttpStatusCode.OK)
                    {
                        vehicleTooltip = new VehicleTooltip(GetVehicleOnlineByListIdResponse.Result);
                        LoadVehicleTooltip();
                    }
                    else
                    {
                        await DisplayAlert(i18n.MessageCallAPIFail, GetVehicleOnlineByListIdResponse.Result, i18n.MessageClose);
                    }
                });

            }
        }

        private Pin DrawMarker(BitmapDescriptor bitmapDescriptor, object tag, Position iPosition)
        {
            return new Pin
            {
                Label = "",
                Icon = bitmapDescriptor,
                Tag = tag,//ActiveMarker
                Position = iPosition,
                Anchor = new Point(0.5, 0.5),
                Flat = true
            };
        }
        private void DrawPinToMap()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (selectedVehicle != null)
                {
                    var activeVehicle = vehicleOnlines.Find(p => p.Id == selectedVehicle.Id);
                    if (activeVehicle != null)
                    {
                        selectedVehicle = activeVehicle;
                        BitmapDescriptor bitmapDescriptor = BitmapDescriptorFactory.FromBundle(String.Format("VehicleActive_{0}.gif", selectedVehicle.State));

                        Position position = new Position(activeVehicle.Latitude, activeVehicle.Longitude);
                        if (selectedVehicleIdPin == null)
                        {
                            selectedVehicleIdPin = new Pin
                            {
                                Label = "",
                                Icon = bitmapDescriptor,
                                Position = position,
                                Anchor = new Point(0.5, 0.5),
                                ZIndex = 0,
                                Flat = true
                            };



                        }
                        else
                        {
                            if (map.Pins.Contains(selectedVehicleIdPin))
                                map.Pins.Remove(selectedVehicleIdPin);
                            selectedVehicleIdPin.Position = position;
                            selectedVehicleIdPin.Icon = bitmapDescriptor;
                        }
                        map.Pins.Add(selectedVehicleIdPin);
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(5)), true);


                    }
                }

                vehicleOnlines.ForEach(item =>
                {
                    Pin pin = pins.Find(p => Convert.ToUInt32(p.Tag) == item.Id);
                    //BitmapDescriptor bitmapDescriptor = VehicleOnline.GetStateIcon((VehicleOnloineState)item.State);
                    BitmapDescriptor bitmapDescriptor = BitmapDescriptorFactory.FromBundle(String.Format("Vehicle_{0}.png", item.State));

                    Position position = new Position(item.Latitude, item.Longitude);
                    double Direction = 0;
                    double oldRotation = 0;
                    if (pin == null)
                    {
                        pin = new Pin
                        {
                            Label = "",
                            Icon = bitmapDescriptor,
                            Tag = item.Id,
                            Position = position,
                            Anchor = new Point(0.5, 0.5),
                            ZIndex = 10,
                            Flat = true,
                            Rotation = (float)item.Direction
                        };
                        pins.Add(pin);
                        Direction = item.Direction * 2;

                        map.PinClicked += Map_PinClicked;

                    }
                    else
                    {
                        oldRotation = pin.Rotation;
                        if (map.Pins.Contains(pin))
                            map.Pins.Remove(pin);
                        var oldPos = pin.Position;
                        pin.Position = position;
                        Direction = Utility.CalculationAngle(oldPos.Latitude, oldPos.Longitude, position.Latitude, position.Longitude);
                        if (Direction < 0)
                            Direction = oldRotation;
                        pin.Icon = bitmapDescriptor;
                    }
                    map.Pins.Add(pin);
                    Utility.MarkerRotation(pin, Direction, 250);

                });
                if (selectedVehicle == null && pins.Count > 0)
                {
                    if (isShowAllMarker == true)
                    {
                        map.MoveToRegion(MapSpan.FromPositions(pins.Select(p => p.Position)), false);
                        Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                        {
                            map.MoveToRegion(MapSpan.FromCenterAndRadius(map.VisibleRegion.Center, Distance.FromKilometers(map.VisibleRegion.Radius.Kilometers * 1.2)));
                            isShowAllMarker = false;
                            return false;
                        });
                    }

                }
            });
        }

        private void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            uint tag = (uint)e.Pin.Tag;
            Pin pin = pins.Find(p => Convert.ToUInt32(p.Tag) == tag);
            if (pin != null)
            {
                selectedVehicle = vehicleOnlines.Find(p => p.Id == (uint)pin.Tag);
                DrawPinToMap();
                GetInfoboxInfo();
            }
        }

        private void Pin_Clicked(object sender, EventArgs e)
        {
        }

        private void DrawListVehicleOnline(List<VehicleOnline> iListVehicleOnline)
        {
            var count = 0;
            listVehicleView.Children.Clear();
            iListVehicleOnline.ForEach(item =>
            {
                count = count + 1;
                ItemList itemList = new ItemList(new StackLayout
                {
                    Spacing = 0,
                    Padding = new Thickness(0, 0, 0, 5),
                    Children =
                                {
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        BackgroundColor= Color.FromHex("#ccf0ff"),
                                        Padding= new Thickness(10,0),
                                        Children =
                                        {
                                            new Label
                                            {
                                                Text = String.Format("{0}. {1}",count,item.Plate),
                                                TextColor = Color.FromHex("#000"),
                                                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
                                                HorizontalOptions = LayoutOptions.StartAndExpand,
                                                FontAttributes = FontAttributes.Bold,
                                            },
                                            new Label
                                            {
                                                Text = item.DateUpdate,//.ToString("dd/MM/yyyy hh:mm:ss"),
                                                TextColor = Color.FromHex(VehicleOnline.GetStateColor((VehicleOnloineState)item.State)),
                                                HorizontalOptions = LayoutOptions.End,

                                            }
                                        }
                                    },
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Padding= new Thickness(10,0),
                                        Spacing=0,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
                                                HorizontalOptions = LayoutOptions.Start,
                                                FormattedText = new FormattedString {
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageLblVehicleOnlineSpeed,
                                                            ForegroundColor = Color.FromHex("#006da6"),
                                                        },
                                                        new Span
                                                        {
                                                            Text = item.Speed,
                                                            ForegroundColor = Color.FromHex(VehicleOnline.GetStateColor((VehicleOnloineState)item.State)),
                                                        }
                                                    }
                                                }

                                            },
                                        }
                                    },
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Padding= new Thickness(10,0),
                                        Spacing=0,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
                                                HorizontalOptions = LayoutOptions.Start,
                                                FormattedText = new FormattedString {
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageLblVehicleOnlineAddress,
                                                            ForegroundColor = Color.FromHex("#006da6"),
                                                        },
                                                        new Span
                                                        {
                                                            Text = item.Address,
                                                        }
                                                    }
                                                }
                                            },
                                        }
                                    },
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Padding= new Thickness(10,0),
                                        Spacing=0,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
                                                HorizontalOptions = LayoutOptions.Start,
                                                 FormattedText = new FormattedString {
                                                    Spans =
                                                    {
                                                        new Span
                                                        {
                                                            Text = i18n.TrackingPageLblVehicleOnlineTotalKM,
                                                            ForegroundColor = Color.FromHex("#006da6"),
                                                        },
                                                        new Span
                                                        {
                                                            Text =  (item.TotalKM>0?item.TotalKM:0).ToString(),
                                                        }
                                                    }
                                                }
                                            },
                                        }
                                    },
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Padding= new Thickness(10,0),
                                        Spacing=0,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
                                                HorizontalOptions = LayoutOptions.Start,
                                                FormattedText = new FormattedString {
                                                   Spans =
                                                   {
                                                       new Span
                                                       {
                                                           Text = i18n.TrackingPageLblVehicleOnlineTotalTimeRun,
                                                           ForegroundColor = Color.FromHex("#006da6"),
                                                       },
                                                       new Span
                                                       {
                                                           Text = item.TotalTimeRun
                                                       }
                                                   }
                                                }
                                            },
                                        }
                                    },
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Horizontal,
                                        Padding= new Thickness(10,0),
                                        Spacing=0,
                                        Children =
                                        {
                                            new Label
                                            {
                                                FontSize = Device.GetNamedSize(NamedSize.Small,typeof(Label)),
                                                HorizontalOptions = LayoutOptions.Start,
                                                FormattedText = new FormattedString {
                                                   Spans =
                                                   {
                                                       new Span
                                                       {
                                                           Text = i18n.TrackingPageLblVehicleOnlineTotalTimePauseOn,
                                                           ForegroundColor = Color.FromHex("#006da6"),
                                                       },
                                                       new Span
                                                       {
                                                           Text = item.TotalTimePauseOn
                                                       }
                                                   }
                                                }
                                            }
                                        }
                                    }
                                }
                }, 0);
                Utility.SetEffect(itemList, command: new Command(() =>
                {
                    selectedVehicle = item;
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(item.Latitude, item.Longitude), Distance.FromKilometers(2)), false);
                    GetInfoboxInfo();
                    OpenMap();

                }));
                listVehicleView.Children.Add(
                    itemList);
            });

            if (iListVehicleOnline.Count == 0)
            {
                listVehicleView.Children.Add(new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = vehicleOnlines.Count == 0 ? i18n.TrackingPageLblNotHaveVehicle : i18n.TrackingPageLblCantFindVehicle
                });
            }


        }

        private void InitHead()
        {
            StackLayout btnBack = new StackLayout
            {
                Padding = 5,
                HeightRequest = 50,
                Children =
                {
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
                VerticalOptions = LayoutOptions.Start,
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
                        //GestureRecognizers =
                        //{
                        //    new TapGestureRecognizer
                        //    {
                        //        Command = new Command(async()=>{
                        //            await  mapPage.LayoutTo(new Rectangle(0,0,MainLayout.Width,MainLayout.Height));
                        //            await vehiclePage.LayoutTo(new Rectangle(-MainLayout.Width,0,MainLayout.Width,MainLayout.Height));
                        //        })
                        //    }
                        //}
                    }


                }
            });
        }
    }
}