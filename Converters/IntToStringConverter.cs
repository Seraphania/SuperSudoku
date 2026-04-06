//using System;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SuperSudoku.Converters
//{
//    internal class IntToStringConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            if (value == null)
//                return "";
//            return value.ToString();
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            if (string.IsNullOrWhiteSpace(value as string))
//                return null;

//            if (int.TryParse((string)value, out int result))
//                return result;

//            return null;
//        }
//    }
//}
