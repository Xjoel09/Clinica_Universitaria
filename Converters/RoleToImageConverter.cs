using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MedicalUTP.Converters
{
    public class RoleToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string role)
            {
                return role switch
                {
                    "Administrativo" => "icon3.png",
                    "Estudiante" => "icon2.png",
                    "Docente" => "icon3.png",
                    _ => "icon.png"
                };
            }
            return "icon.png"; // imagen por defecto
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
