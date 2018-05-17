using System;
using Xamarin.Forms;
using Quanlyphuongtien;
using Xamarin.Forms.Platform.iOS;

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
                //Control.TextChanged += OnTextChange;
                var newElement = e.NewElement as MultiLineLabel;

                if (newElement != null)
                    Control.Lines = newElement.MaxLine;
                else
                    Control.Lines = 1;

            }
        }

        //private void OnTextChange(object sender, Android.Text.TextChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
#pragma warning restore CS0618 // Type or member is obsolete
}