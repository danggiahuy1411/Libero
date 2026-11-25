using System;
using System.Globalization;
using System.Windows.Data;

namespace Libero.Converters;

public class StringEqualsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return string.Equals(value?.ToString(), parameter?.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Chỉ trả về tên trang khi RadioButton được chọn (true)
        if (value is bool isChecked && isChecked && parameter != null)
            return parameter.ToString()!;

        return Binding.DoNothing;
    }
}