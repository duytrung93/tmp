using System;
using Android.Text;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Quanlyphuongtien;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderlessDatePickerRenderer))]
[assembly: ExportRenderer(typeof(BorderlessTimePicker), typeof(BorderlessTimePickerRenderer))]
namespace Quanlyphuongtien
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class BorderlessEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (e == null)
            {
                throw new System.ArgumentNullException(nameof(e));
            }

            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                //Control.TextChanged += OnTextChange;
            }

        }

        //private void OnTextChange(object sender, Android.Text.TextChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
#pragma warning restore CS0618 // Type or member is obsolete

#pragma warning disable CS0618 // Type or member is obsolete
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            if (e == null)
            {
                throw new System.ArgumentNullException(nameof(e));
            }

            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                //Control.TextChanged += OnTextChange;
            }

        }

        //private void OnTextChange(object sender, Android.Text.TextChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
#pragma warning restore CS0618 // Type or member is obsolete

#pragma warning disable CS0618 // Type or member is obsolete
    public class BorderlessTimePickerRenderer : TimePickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            if (e == null)
            {
                throw new System.ArgumentNullException(nameof(e));
            }

            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                //Control.TextChanged += OnTextChange;
            }

        }

        //private void OnTextChange(object sender, Android.Text.TextChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
#pragma warning restore CS0618 // Type or member is obsolete


}