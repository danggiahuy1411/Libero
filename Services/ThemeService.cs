using System;
using System.Linq;
using System.Windows;

namespace Libero.Services;

public class ThemeService : IThemeService
{
    public bool IsDarkMode { get; private set; }

    public void ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;

        ApplyTheme();
    }

    private void ApplyTheme()
    {
        string themePath = IsDarkMode
            ? "Themes/DarkTheme.xaml"
            : "Themes/LightTheme.xaml";

        var newTheme = new ResourceDictionary
        {
            Source = new Uri(themePath, UriKind.Relative)
        };

        var dictionaries =
            Application.Current.Resources.MergedDictionaries;

        var oldTheme = dictionaries.FirstOrDefault(d =>
            d.Source != null &&
            (d.Source.OriginalString.EndsWith(
                "LightTheme.xaml", StringComparison.OrdinalIgnoreCase) ||
             d.Source.OriginalString.EndsWith(
                "DarkTheme.xaml", StringComparison.OrdinalIgnoreCase)));

        if (oldTheme != null)
        {
            int index = dictionaries.IndexOf(oldTheme);
            dictionaries[index] = newTheme;
        }
        else
        {
            dictionaries.Add(newTheme);
        }
    }
}
