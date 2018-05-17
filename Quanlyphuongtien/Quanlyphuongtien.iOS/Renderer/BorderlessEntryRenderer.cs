using System;
using Xamarin.Forms;
using Quanlyphuongtien;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(CustomFrameRenderer))]
[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderlessDatePickerRenderer))]
[assembly: ExportRenderer(typeof(BorderlessTimePicker), typeof(BorderlessTimePickerRenderer))]
namespace Quanlyphuongtien
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class CustomFrameRenderer : FrameRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            if (e == null)
            {
                throw new System.ArgumentNullException(nameof(e));
            }

            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                e.NewElement.HasShadow = false;

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
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
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
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
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
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
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