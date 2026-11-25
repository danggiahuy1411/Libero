using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Libero.Models;

public enum BackgroundIconType
{
    Apple,
    OpenBook,
    Book,
    Bookmark,
    Pencil
}

public partial class BackgroundIconModel : ObservableObject
{
    [ObservableProperty]
    private double x;

    [ObservableProperty]
    private double y;

    [ObservableProperty]
    private double baseSize = 40.0;

    [ObservableProperty]
    private double scale = 1.0;

    [ObservableProperty]
    private double angle;

    [ObservableProperty]
    private double opacity;

    [ObservableProperty]
    private BackgroundIconType iconType;

    public double Size => BaseSize * Scale;

    partial void OnScaleChanged(double value)
    {
        OnPropertyChanged(nameof(Size));
    }

    partial void OnBaseSizeChanged(double value)
    {
        OnPropertyChanged(nameof(Size));
    }
}