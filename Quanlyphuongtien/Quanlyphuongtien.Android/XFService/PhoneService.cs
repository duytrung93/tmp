using Android.App;
using Android.Content;
using Android.Telephony;
using Android.Runtime;
using Quanlyphuongtien.Droid;
using System.Linq;
using Xamarin.Forms;
using System;
using System.IO;

[assembly: Dependency(typeof(PhoneService))]
namespace Quanlyphuongtien.Droid
{
    public class PhoneService : iService
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }



        public void OpenDialer(string phoneNumber)
        {

            var uri = Android.Net.Uri.Parse(String.Format("tel:{0}", phoneNumber));
            var intent = new Intent(Intent.ActionDial, uri);
            Android.App.Application.Context.StartActivity(intent);
        }
    }
}