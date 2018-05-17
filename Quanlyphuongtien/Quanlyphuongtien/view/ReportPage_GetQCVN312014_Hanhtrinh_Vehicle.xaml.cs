using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Quanlyphuongtien
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportPage_GetQCVN312014_Hanhtrinh_Vehicle : ContentPage
    {
        private StackLayout stackLayout;
        private DataAccess dataAccess;
        private double widthContent;
        private RelativeLayout MainContainer;
        private LoadingWait loading;
        private ButtonBorder btnFormInput;
        private ButtonBorder btnMap;
        private View tabLayout;
        private RelativeLayout MainLayout;
        private RelativeLayout mapLayout;
        private Map map;
        private StackLayout formLayout;
        private VehicleOnline selectedVehicle;
        private EntryForm txtVehicleId;
        private BorderlessTimePicker txtToTime;
        private BorderlessTimePicker txtFromTime;
        private BorderlessDatePicker txtToDate;
        private BorderlessDatePicker txtFromDate;
        private int indexRun = 0;
        private double runSpeed = 30;

        public UserLogin ActiveUser { get; }

        private BitmapDescriptor bitmapDescriptor;
        private LogData logData;
        private bool runable = true;
        private Label lblShowSpeed;
        private Slider speedRunSlider;

        public Pin Pin { get; private set; }
        public Pin PinStart { get; private set; }
        public Pin PinEnd { get; private set; }

        public ReportPage_GetQCVN312014_Hanhtrinh_Vehicle(UserLogin iUserLogin)
        {
            ActiveUser = iUserLogin;
            var a = (MainPage)App.Current.MainPage;
            widthContent = a.Width;
            MainContainer = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            Content = MainContainer;
            dataAccess = new DataAccess();
            InitLayout();
            InitListDetailViewLog();
            InitLoading();
        }

        private void InitListDetailViewLog()
        {
            listDetailViewLog = new ListDetailViewLog((isEnd) =>
            {

                if (isEnd)
                {
                    if (info.CurentPage <= info.TotalPage)
                        BinListDetail((ushort)(info.CurentPage + 1));
                }
            });
            MainContainer.Children.Add(
                listDetailViewLog.Container,
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

            btnFormInput = new ButtonBorder(i18n.ViewLogPageBtnFormInput, iBackgroundColor: "#3bafda", iPadding: 5, iTextColor: "#fff", iCommand: new Command(() =>
            {
                OpenFormInput();
            }));

            btnMap = new ButtonBorder(i18n.ViewLogPageBtnMap, iPadding: 5, iCommand: new Command(() =>
            {
                OpenMap();
            }));

            gridTab.Children.Add(btnFormInput, 0, 0);
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

        private void OpenFormInput()
        {
            btnFormInput.MainContainer.BackgroundColor = Color.FromHex("#3bafda");
            btnFormInput.label.TextColor = Color.FromHex("#fff");

            btnMap.MainContainer.BackgroundColor = Color.FromHex("#DDD");
            btnMap.label.TextColor = Color.FromHex("#000");
            Device.BeginInvokeOnMainThread(async () =>
            {
                await mapLayout.TranslateTo(MainLayout.Width, 0);

            });
            Device.BeginInvokeOnMainThread(async () =>
            {
                await formLayout.TranslateTo(0, 0);

            });
        }

        private void OpenMap()
        {
            if (logData != null)
            {
                btnMap.MainContainer.BackgroundColor = Color.FromHex("#3bafda");
                btnMap.label.TextColor = Color.FromHex("#fff");

                btnFormInput.MainContainer.BackgroundColor = Color.FromHex("#DDD");
                btnFormInput.label.TextColor = Color.FromHex("#000");

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await formLayout.TranslateTo(-MainLayout.Width, 0);
                });
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await mapLayout.TranslateTo(0, 0);
                });
            }
            else
            {
                DisplayAlert(i18n.MessageAlert, i18n.MessageApiNoData, i18n.MessageClose);
            }

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

            InitFormInput(MainLayout);



        }

        private void InitFormInput(RelativeLayout mainLayout)
        {
            formLayout = new StackLayout
            {
                BackgroundColor = Color.White,
                Padding = 10,
                Spacing = 10,
            };
            mainLayout.Children.Add(
             formLayout,
             Constraint.RelativeToParent(p => 0),
             Constraint.RelativeToParent(p => 0),
             Constraint.RelativeToParent(p => p.Width),
             Constraint.RelativeToParent(p => p.Height)
             );



            txtVehicleId = new EntryForm(placeholder: "Nhập biển số", label: new Label
            {
                Text = "Biển số",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10);
            ((BorderlessEntry)txtVehicleId.entry).Focused += TxtVehicleId_Focused;
            formLayout.Children.Add(txtVehicleId);
            txtFromDate = new BorderlessDatePicker
            {
                BackgroundColor = Color.Transparent,
                Format = "dd/MM/yyyy",
                Date = DateTime.Now,
            };
            formLayout.Children.Add(new EntryForm(placeholder: "Ngày bắt đầu", iView: txtFromDate, label: new Label
            {
                Text = "Từ ngày",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10));
            txtToDate = new BorderlessDatePicker
            {
                Format = "dd/MM/yyyy",
                Date = DateTime.Now,
            };
            formLayout.Children.Add(new EntryForm(placeholder: "Ngày kết thúc", iView: txtToDate, label: new Label
            {
                Text = "Đến ngày",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10));
            txtFromTime = new BorderlessTimePicker
            {
                Format = "HH:mm:ss",
                Time = new TimeSpan(0, 0, 0),
            };
            formLayout.Children.Add(new EntryForm(placeholder: "Giờ bắt đầu", iView: txtFromTime, label: new Label
            {
                Text = "Từ giờ",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10));

            txtToTime = new BorderlessTimePicker
            {
                Format = "HH:mm:ss",
                Time = new TimeSpan(23, 59, 59),
            };
            formLayout.Children.Add(new EntryForm(placeholder: "Giờ kết thúc", iView: txtToTime, label: new Label
            {
                Text = "Đến giờ",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            }, labelWidth: 100, iCornerRadius: 10));


            formLayout.Children.Add(new StackLayout
            {
                Children =
                {
                    new ButtonBorder(
                    text: i18n.ViewLogPageBtnGetLog,
                    iTextColor: "#fff",
                    iBackgroundColor: "#4a89dc",
                    iCornerRadius: 10,
                    iCommand: new Command(async() =>
                    {
                        await GetViewLogAsync();
                    }))
                }
            });
        }

        private async Task GetViewLogAsync()
        {

            BorderlessEntry Plate = ((BorderlessEntry)txtVehicleId.entry);
            if (selectedVehicle == null)
            {
                await DisplayAlert(i18n.MessageAlert, i18n.ViewLogPageValidatePlateRequied, "Đóng lại");
                TxtVehicleId_Focused(Plate, null);
                return;
            }

            //await DisplayAlert("abc", "OK", "OK");
            string fromdate = txtFromDate.Date.ToString("yyyy-MM-dd") + "T" + txtFromTime.Time.ToString();
            loading.ShowLoading(i18n.MessageSystemProcessing);
            GetAPIResponse response = await GetDataAPI.GetAPI(API.GetVehicleLog, new
            {
                FromDate = txtFromDate.Date.ToString("yyyy-MM-dd") + "T" + txtFromTime.Time.ToString(),
                ToDate = txtToDate.Date.ToString("yyyy-MM-dd") + "T" + txtToTime.Time.ToString(),
                VehicleId = selectedVehicle.Id,
                MaxSpeed = 80,
                Plate = selectedVehicle.Plate,
                isHC = true,
                TockenKey = ActiveUser.Tockenkey
            });

            if (response.Status == HttpStatusCode.OK)
            {
                logData = new LogData(response.Result);
                if (logData.Log.Count > 2)
                {
                    BindLogData();
                }
                else
                {
                    await DisplayAlert(i18n.MessageAlert, i18n.MessageApiNoData, i18n.MessageClose);
                }
                loading.OffLoading();
            }
            else
            {
                await DisplayAlert(i18n.MessageCallAPIFail, response.Result, i18n.MessageClose);
            }
        }
        private bool FirstTimeGetData = true;
        private void BindLogData()
        {
            loading.ShowLoading(i18n.MessageInitData);
            List<Position> positions = new List<Position>();
            Polyline polyline = new Polyline();
            polyline.StrokeColor = Color.FromHex("#008fe5");
            polyline.StrokeWidth = 5f;
            polyline.Tag = "POLYLINE"; // Can set any object
            listDetailViewLog.ViewLayout.Children.Clear();
            int count = BinListDetail(0);

            logData.Log.ForEach(item =>
            {

                Position position = new Position((double)item.Lat / 1000000, (double)item.Lon / 1000000);
                positions.Add(position);
                polyline.Positions.Add(position);
                count += 1;
            });
            //BinPolyline
            map.Polylines.Clear();
            map.Polylines.Add(polyline);
            map.MoveToRegion(MapSpan.FromPositions(polyline.Positions), false);
            Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(map.VisibleRegion.Center, Distance.FromKilometers(map.VisibleRegion.Radius.Kilometers * 1.7)));
                return false;
            });
            //Bin marker
            bitmapDescriptor = BitmapDescriptorFactory.FromBundle(String.Format("Vehicle_{0}.png", logData.Log[0].State));
            if (Pin != null && map.Pins.Contains(Pin))
                map.Pins.Remove(Pin);
            Pin = new Pin
            {
                Anchor = new Point(0.5, 0.5),
                Label = "",
                Rotation = logData.Log[0].Direction * 2,
                ZIndex = 10,
                Icon = bitmapDescriptor,
                Flat = true,
                Position = positions[0]
            };

            map.Pins.Add(Pin);

            if (PinStart != null && map.Pins.Contains(PinStart))
                map.Pins.Remove(PinStart);
            PinStart = new Pin
            {
                Label = i18n.ViewLogPagePointStart,
                Position = positions[0]
            };

            map.Pins.Add(PinStart);

            if (PinEnd != null && map.Pins.Contains(PinEnd))
                map.Pins.Remove(PinEnd);
            PinEnd = new Pin
            {
                Label = i18n.ViewLogPagePointEnd,
                Position = positions[positions.Count - 1]
            };

            map.Pins.Add(PinEnd);
            BinDetailRun();
            if (FirstTimeGetData)
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
                {
                    OpenMap();
                    loading.OffLoading();
                    return false;
                });
            }
            else
            {
                OpenMap();
                loading.OffLoading();
            }
        }
        private int BinListDetail(ushort iPage)
        {
            loading.ShowLoading(i18n.MessageSystemProcessing);
            info = new PageInfo(iPage, 50, logData.Log);
            var count = 0;
            List<VehicleLog> listTmp = (List<VehicleLog>)info.Data;
            listTmp.ForEach(item =>
            {
                listDetailViewLog.ViewLayout.Children.Add(new ItemList(new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 0,
                    Children =
                    {
                        new Label
                        {
                            LineBreakMode = LineBreakMode.TailTruncation,
                            Text = item.Dtime.Split(' ')[1],//logData.Log[indexRun].Dtime,
                            HorizontalOptions = LayoutOptions.Start,
                            WidthRequest = 70,
                            FontSize = 8,
                        },
                        new Label
                        {
                            LineBreakMode = LineBreakMode.TailTruncation,
                            Text = item.Speed,//logData.Log[indexRun].Speed,
                            HorizontalOptions = LayoutOptions.Start,
                            WidthRequest = 100,
                            FontSize = 8,
                        },
                                new Label
                        {
                            LineBreakMode = LineBreakMode.TailTruncation,
                            Text = item.Address,//logData.Log[indexRun].Address,
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            FontSize = 8,
                        }
                    }
                }));
            });
            loading.OffLoading();
            return count;
        }

        private void BinDetailRun()
        {
            //map.Pins.Remove(Pin);
            double direction = 0;
            VehicleLog vehicleLog = logData.Log[indexRun];
            Position position = new Position((double)vehicleLog.Lat / 1000000, (double)vehicleLog.Lon / 1000000);
            if (indexRun > 0)
            {
                VehicleLog vehicleLogOld = logData.Log[indexRun - 1];
                direction = Utility.CalculationAngle((double)vehicleLogOld.Lat / 1000000, (double)vehicleLogOld.Lon / 1000000, position.Latitude, position.Longitude);
                if (vehicleLog.State != vehicleLogOld.State)
                {
                    map.Pins.Remove(Pin);
                    bitmapDescriptor = BitmapDescriptorFactory.FromBundle(String.Format("Vehicle_{0}.png", vehicleLog.State));
                    Pin.Icon = bitmapDescriptor;
                    map.Pins.Add(Pin);
                    //PauseViewLog();

                }
            }
            else
            {
                direction = vehicleLog.Direction * 2;
            }
            Pin.Position = position;
            Pin.Rotation = (float)direction;
            lblShowSpeed.Text = runSpeed.ToString() + "Bản ghi/giây " + indexRun + "/" + logData.Log.Count.ToString();
            //BinLbldetail
            lblLogDataDTime.Text = logData.Log[indexRun].Dtime.Split(' ')[1];
            lblLogDataSpeed.Text = logData.Log[indexRun].Speed;
            lblLogDataAddress.Text = logData.Log[indexRun].Address;
        }

        private void TxtVehicleId_Focused(object sender, FocusEventArgs e)
        {
            BorderlessEntry a = (BorderlessEntry)sender;
            a.Unfocus();
            Navigation.PushModalAsync(new AutocompleteView(
                iApiName: API.GetVehicleOnlineByListId,
                iType: typeof(List<VehicleOnline>),
                param: new { iListVehicleId = "", Tockenkey = ActiveUser.Tockenkey },
                iCallBack: (item) =>
                {
                    //DisplayAlert("abc", item.Text, "OK");
                    ((BorderlessEntry)txtVehicleId.entry).Text = item.Text;
                    selectedVehicle = (VehicleOnline)item.Data;
                }));
        }


        private void InitMap(RelativeLayout mainLayout)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                mapLayout = new RelativeLayout
                {
                    BackgroundColor = Color.White,
                    TranslationX = widthContent,
                };
                mainLayout.Children.Add(
                 mapLayout,
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

                mapLayout.Children.Add(map,
                 Constraint.RelativeToParent(p => 0),
                 Constraint.RelativeToParent(p => 0),
                 Constraint.RelativeToParent(p => p.Width),
                 Constraint.RelativeToParent(p => p.Height - 50)
                 );

                lblShowSpeed = new Label
                {
                    FontSize = 10,
                    Text = i18n.ViewLogPageLblShowSpeed,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.Start,
                };
                speedRunSlider = new Slider
                {
                    TranslationX = -15,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Maximum = 100,
                    Minimum = 1,
                    Value = runSpeed
                };
                speedRunSlider.ValueChanged += SpeedRunSlider_ValueChanged;
                StackLayout actionBar = new StackLayout
                {
                    BackgroundColor = Color.White,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 5,
                    Children =
                    {
                        new ButtonBorder(
                            icon: "icons8_play_96.png",
                            iCornerRadius: 5,
                            iBackgroundColor: "#5bc24c",
                            iPadding: 0,
                            iCommand: new Command(()=>{
                                RunViewLog();
                            })
                        )
                        {
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HeightRequest = 40,
                        },
                        new ButtonBorder(icon: "icons8_pause_96.png", iCornerRadius: 5, iBackgroundColor: "#d3b118", iPadding: 0,
                            iCommand: new Command(()=>{
                                PauseViewLog();
                            }))
                        {
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HeightRequest = 40,
                        },
                        new ButtonBorder(icon: "icons8_stop_96.png", iCornerRadius: 5, iBackgroundColor: "#d33218", iPadding: 0,
                            iCommand: new Command(()=>{
                                StopViewLog();
                            }))
                        {
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HeightRequest = 40,
                        },
                        new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Children =
                            {
                                lblShowSpeed,
                                speedRunSlider
                            }
                        },
                    }
                };
                mapLayout.Children.Add(actionBar,
                 Constraint.RelativeToParent(p => 0),
                 Constraint.RelativeToParent(p => p.Height - 50),
                 Constraint.RelativeToParent(p => p.Width),
                 Constraint.RelativeToParent(p => 50)
                 );

                lblLogDataDTime = new Label
                {
                    LineBreakMode = LineBreakMode.TailTruncation,
                    Text = "",//logData.Log[indexRun].Dtime,
                    HorizontalOptions = LayoutOptions.Start,
                    WidthRequest = 70,
                    FontSize = 8,
                };
                lblLogDataSpeed = new Label
                {
                    LineBreakMode = LineBreakMode.TailTruncation,
                    Text = "",//logData.Log[indexRun].Speed,
                    HorizontalOptions = LayoutOptions.Start,
                    WidthRequest = 100,
                    FontSize = 8,
                };
                lblLogDataAddress = new Label
                {
                    LineBreakMode = LineBreakMode.TailTruncation,
                    Text = "",//logData.Log[indexRun].Address,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    FontSize = 8,
                };
                mapLayout.Children.Add(new Frame
                {
                    Margin = 0,
                    Padding = 5,
                    BackgroundColor = Color.FromRgba(255, 255, 255, 0.9),
                    CornerRadius = 10,
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Children =
                        {
                            lblLogDataDTime,
                            lblLogDataSpeed,
                            lblLogDataAddress

                        }
                    },
                    GestureRecognizers =
                    {
                        new TapGestureRecognizer
                        {
                            Command = new Command(async()=>{
                                await listDetailViewLog.Show();
                            })
                        }
                    }
                },
                Constraint.RelativeToParent(p => 5),
                Constraint.RelativeToParent(p => p.Height - 100),
                Constraint.RelativeToParent(p => p.Width - 10),
                Constraint.RelativeToParent(p => 40)
                );
                mapLayout.Children.Add(new Image
                {
                    Source = "arrow_left_black.png",
                    Rotation = 90,
                    Opacity = 0.7,
                },
                 Constraint.RelativeToParent(p => p.Width / 2 - 5),
                 Constraint.RelativeToParent(p => p.Height - 97),
                 Constraint.RelativeToParent(p => 10),
                 Constraint.RelativeToParent(p => 10)
               );
            });
        }

        private void SpeedRunSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            runSpeed = e.NewValue;
        }

        private int loopRun = 0;
        private Label lblLogDataDTime;
        private Label lblLogDataSpeed;
        private Label lblLogDataAddress;
        private ListDetailViewLog listDetailViewLog;
        private PageInfo info;

        private void PauseViewLog()
        {
            runable = false;
            Device.StartTimer(TimeSpan.FromMilliseconds((1000 / runSpeed) * 2), () =>
              {
                  indexRun += 1;
                  loopRun += 1;
                  runable = true;
                  return false;
              });
        }
        private void StopViewLog()
        {
            runable = false;
            Device.StartTimer(TimeSpan.FromMilliseconds((1000 / runSpeed) * 2), () =>
              {
                  indexRun = 0;
                  BinDetailRun();
                  runable = true;
                  return false;
              });
        }
        private void RunViewLog()
        {
            if (logData != null)
            {
                if (indexRun < logData.Log.Count)
                {

                    //map.Pins.Add(Pin);
                    BinDetailRun();
                    if (indexRun < logData.Log.Count && runable == true)
                        Device.StartTimer(TimeSpan.FromMilliseconds(1000 / runSpeed), () =>
                            {
                                indexRun += 1;
                                loopRun += 1;
                                RunViewLog();
                                return false;
                            });
                    else
                    {
                        loopRun = 0;
                    }
                }
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
                BackgroundColor = Color.FromHex(Contanst.PrimaryColor),
                Spacing = 5,
                Orientation = StackOrientation.Horizontal,
                Children = {
                    btnBack,

                    new Label
                    {
                        TranslationX=-25,
                        Text = i18n.ViewLogPageTitle.ToUpper(),
                        TextColor = Color.White,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                    }
                }
            });
        }
    }
}