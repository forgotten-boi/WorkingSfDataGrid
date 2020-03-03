using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TrialAppDemo.Models
{
    public class Dark : DataGridStyle
    {
        public Dark()
        {
        }

        public override Color GetHeaderBackgroundColor()
        {
            return Color.FromRgb(15, 15, 15);
        }

        public override Color GetHeaderForegroundColor()
        {
            return Color.FromRgb(255, 255, 255);
        }

        public override Color GetRecordBackgroundColor()
        {
            return Color.FromRgb(43, 43, 43);
        }

        public override Color GetRecordForegroundColor()
        {
            return Color.FromRgb(255, 255, 255);
        }

        public override Color GetSelectionBackgroundColor()
        {
            return Color.FromRgb(42, 159, 214);
        }

        public override Color GetSelectionForegroundColor()
        {
            return Color.FromRgb(255, 255, 255);
        }

        public override Color GetCaptionSummaryRowBackgroundColor()
        {
            return Color.FromRgb(02, 02, 02);
        }

        [Obsolete]
        public override Color GetCaptionSummaryRowForeGroundColor()
        {
            return Color.FromRgb(255, 255, 255);
        }

        public override Color GetBorderColor()
        {
            return Color.FromRgb(81, 83, 82);
        }

        public override Color GetLoadMoreViewBackgroundColor()
        {
            return Color.FromRgb(242, 242, 242);
        }

        public override Color GetLoadMoreViewForegroundColor()
        {
            return Color.FromRgb(34, 31, 31);
        }

        public override Color GetAlternatingRowBackgroundColor()
        {
            return Color.Yellow;
        }
    }

    public class CellStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToDouble(value) < 300)
                return Color.White;
            return Color.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
