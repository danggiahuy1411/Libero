using System;
using System.Collections.Generic;
using System.Text;
using MaterialDesignThemes.Wpf;

namespace Libero.Models;

public class NavItemModel
{
    public string PageKey { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public PackIconKind Icon { get; set; }
    public string? SectionHeader { get; set; }
}