using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Libero.Controls;

public partial class ToggleTheme : UserControl
{
    public ToggleTheme()
    {
        InitializeComponent();

        Loaded += (s, e) => ApplyTheme(false);
    }

    public static readonly DependencyProperty IsDarkModeProperty =
        DependencyProperty.Register(
            nameof(IsDarkMode),
            typeof(bool),
            typeof(ToggleTheme),
            new PropertyMetadata(false, OnThemeChanged));

    public bool IsDarkMode
    {
        get => (bool)GetValue(IsDarkModeProperty);
        set => SetValue(IsDarkModeProperty, value);
    }

    public static readonly DependencyProperty ToggleCommandProperty =
        DependencyProperty.Register(
            nameof(ToggleCommand),
            typeof(ICommand),
            typeof(ToggleTheme));

    public ICommand ToggleCommand
    {
        get => (ICommand)GetValue(ToggleCommandProperty);
        set => SetValue(ToggleCommandProperty, value);
    }

    private static void OnThemeChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        if (d is ToggleTheme control)
        {
            control.ApplyTheme(true);
        }
    }

    private void ApplyTheme(bool animate)
    {
        if (ToggleThumb == null)
            return;

        // Vùng bên trong Margin="3" và BorderThickness="1" rộng 76px -> mỗi cột rộng đúng 38px
        double columnWidth = ActualWidth > 0 ? (ActualWidth - 8) / 2.0 : 38.0;
        double targetX = IsDarkMode ? columnWidth : 0;

        if (animate)
        {
            var animation = new DoubleAnimation
            {
                To = targetX,
                Duration = TimeSpan.FromMilliseconds(250),
                EasingFunction = new CubicEase
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            ThumbTransform.BeginAnimation(
                TranslateTransform.XProperty,
                animation);
        }
        else
        {
            ThumbTransform.BeginAnimation(
                TranslateTransform.XProperty, null);

            ThumbTransform.X = targetX;
        }

        // Icon đang nằm trên ToggleThumb dùng SurfaceBrush, icon còn lại dùng TextMutedBrush
        string sunKey = IsDarkMode ? "TextMutedBrush" : "SurfaceBrush";
        string moonKey = IsDarkMode ? "SurfaceBrush" : "TextMutedBrush";

        SunCircle.SetResourceReference(Shape.StrokeProperty, sunKey);
        SunRays.SetResourceReference(Shape.StrokeProperty, sunKey);
        MoonIcon.SetResourceReference(Shape.StrokeProperty, moonKey);
    }
}