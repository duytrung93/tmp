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

namespace Quanlyphuongtien
{
    public partial class TestPage : ContentPage
    {
        Label l;
        public TestPage()
        {
            l = new Label
            {
                Text = "waiting"
            };

            Content = new StackLayout
            {
                Children =
                {
                    l,
                    new ExtendedEntry
                    {
                        Placeholder="TEst",
                        HasBorder=true,

                    }
                }
            };
            var oGeolocator = Resolver.Resolve<IGeolocator>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                Position a = await oGeolocator.GetPositionAsync(10000);
                if (a != null)
                {
                    l.Text = a.Latitude + "," + a.Longitude;
                }
            });

            oGeolocator.PositionChanged += OGeolocator_PositionChanged;
            oGeolocator.StartListening(10000, 0);
            var device = Resolver.Resolve<IDevice>();
            var oDisplay = device.Display; // create display-interface
                                           //
            var iBreite = oDisplay.Width; // Returns EG: 2560
            var iHoehe = oDisplay.Height; // Returns EG: 1600
            var iXdpi = oDisplay.Xdpi; // Returns EG: 248.182998657227
            var iYdpi = oDisplay.Ydpi; // Returns EG: 247.804000854492
                                       //
            var HW = device.HardwareVersion; // Returns EG: "universal5420"
            var FirmWareVersion = device.FirmwareVersion; // Returns EG: "4.4.2"
            var Manufacturer = device.Manufacturer; // Returns EG: "samsung"
            var ID = device.Id; // Returns EG: "5205ce9c4b88215b"
            var DeviceOrientation = device.Orientation; // Returns EG: XLabs.Enums.Orientation.Portrait
            var Memory = device.TotalMemory; // Returns EG: 2910535680
            var DeviceName = device.Name; // Returns EG: "SM-T900"
            var cTimeZone = device.TimeZone; // Returns EG: "Europe/Zurich"
            var cLanguageCode = device.LanguageCode; // Returns EG: "de"
            var oNetwork = device.Network; // Create Interface to Network-functions
            var xx = oNetwork.InternetConnectionStatus();// Returns EG: XLabs.Platform.Services.NetworkStatus.ReachableViaWiFiNetwork
            TimeSpan TSTimeOut = new TimeSpan(1000);
            var NetworkAvailable = oNetwork.IsReachable("172.2.13.33", TSTimeOut);
            // Returns EG: Id = 53, Status = Running
            //Further properties / methods / events:
            //oNetwork.IsReachableByWifi()
            //oNetwork.ReachabilityChanged event
            var oBlueTooth = device.BluetoothHub;
            // Create Interface to BluetoothHub
            //Further properties / methods / events:
            //oBlueTooth.Enabled property
            //oBlueTooth.GetPairedDevices()
            //oBlueTooth.GetType()
            //oBlueTooth.OpenSettings()
            var oMicroPhone = device.Microphone;
            // Create Interface to Microphone
            //Further properties / methods / events:
            //oMicroPhone.BitsPerSample property
            //oMicroPhone.SampleRate property
            //oMicroPhone.SupportedSampleRates property
            //oMicroPhone.ChannelCount property
            //oMicroPhone.OnBroadcast event
            //oMicroPhone.Start() method to start recording
            //oMicroPhone.Stop()method to stop recording
            var oAccelometer = device.Accelerometer;
            // Create Interface to Accelerometer
            //Further properties / methods / events:
            //oAccelometer.Interval property
            //oAccelometer.LatestReading property
            //oAccelometer.ReadingAvailable event
            var oBattery = device.Battery;
            // Create Interface to Battery
            //Further properties / methods / events:
            //oBattery.Charging property
            //oBattery.Level property
            //oBattery.OnChargerStatusChanged event
            //oBattery.OnLevelChange event
            var oFileManager = device.FileManager;
            // Create Interface to FileManager
            //Further properties / methods / events:
            //oFileManager.CreateDirectory() method
            //oFileManager.DirectoryExists() method
            //oFileManager.FileExists() method
            //oFileManager.GetLastWriteTime() method
            //oFileManager.OpenFile() method
            var oGyroscope = device.Gyroscope;
            //Further properties / methods / events:
            //oGyroscope.Interval property
            //oGyroscope.LatestReading property
            //oGyroscope.ReadingAvailable event
            var oMediaPicker = device.MediaPicker;
            //Further properties / methods / events:
            //oMediaPicker.IsCameraAvailable property
            //oMediaPicker.IsPhotosSupported property
            //oMediaPicker.OnError event Gets or sets the error
            //oMediaPicker.OnMediaSelected event
            //oMediaPicker.SelectPhotoAsync() // method, that select an image from library
            //oMediaPicker.SelectVideoAsync() // method, that select a video from library
            //oMediaPicker.TakePhotoAsync() // method, that takes a Photo
            //oMediaPicker.TakeVideoAsync() // method, that takes a Video
            var oPhoneServices = device.PhoneService;
            //oPhoneServices.CanSendSMS property
            //oPhoneServices.CellularProvider property
            //oPhoneServices.DialNumber() method
            //oPhoneServices.ICC property gets the ISO-Country-code
            //oPhoneServices.IsCellularDataEnabled property
            //oPhoneServices.IsCellularDataRoamingEnabled property
            //oPhoneServices.IsNetworkAvailable property
            //oPhoneServices.MCC property: Gets the Mobile country-code
            //oPhoneServices.MNC property: gets the mobile network-code
            //oPhoneServices.SendSMS() method to send a SMS

        }

        private void OGeolocator_PositionChanged(object sender, PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Position a = e.Position;
                if (a != null)
                {
                    l.Text = a.Latitude + "," + a.Longitude;
                }
            });
        }
    }
}
