using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using Quanlyphuongtien;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneService))]
namespace Quanlyphuongtien
{
    class PhoneService : iService
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public void OpenDialer(string phoneNumber)
        {
            //var uri = Android.Net.Uri.Parse(String.Format("tel:{0}", phoneNumber));
            //var intent = new Intent(Intent.ActionDial, uri);
            //Android.App.Application.Context.StartActivity(intent);



        }
    }
}