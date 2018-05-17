using System;
using Android.Text;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Quanlyphuongtien;

[assembly: ExportRenderer(typeof(MultiLineLabel), typeof(MultiLineLabelRenderer))]
namespace Quanlyphuongtien
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class MultiLineLabelRenderer : LabelRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            if (e == null)
            {
                throw new System.ArgumentNullException(nameof(e));
            }

            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.SetSingleLine(false);
                //Control.TextChanged += OnTextChange;
                var newElement = e.NewElement as MultiLineLabel;

                if (newElement != null)
                    Control.SetMaxLines(newElement.MaxLine);
                else
                    Control.SetLines(1);

            }
        }

        //private void OnTextChange(object sender, Android.Text.TextChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
#pragma warning restore CS0618 // Type or member is obsolete
}