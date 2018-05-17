using Android.App;
using Android.OS;
using Android.Content.PM;
// New Xlabs
using XLabs.Ioc; // Using for SimpleContainer
using XLabs.Platform.Services.Geolocation; // Using for Geolocation
using XLabs.Platform.Device; // Using for Display
// End new Xlabs
namespace Quanlyphuongtien.Droid
{
    [Activity(Label = "Quản lý phương tiện",
        Icon = "@drawable/logo",
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize,
        MainLauncher = false)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);
            // New Xlabs
            var container = new SimpleContainer();
            container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
            container.Register<IGeolocator, Geolocator>();
            Resolver.ResetResolver();
            Resolver.SetResolver(container.GetResolver()); // Resolving the services
                                                           // End new Xlabs

            

            Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsGoogleMaps.Init(this, bundle);
            XamEffects.Droid.Effects.Init();
            LoadApplication(new App());
        }
    }
}

