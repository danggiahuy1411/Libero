using System;
using System.Collections.Generic;
using System.Text;

namespace Libero.Services;

public interface IThemeService
{
    bool IsDarkMode { get; }
    void ToggleTheme();
}