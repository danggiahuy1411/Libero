using Libero.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Libero.Controls;

public partial class SharedBackground : UserControl
{
    public ObservableCollection<BackgroundIconModel> BackgroundIcons { get; }
        = new();

    private readonly Random _random = new();

    private const double CellSize = 160.0;
    private const double BaseIconSize = 70.0;

    private double _lastWidth;
    private double _lastHeight;

    public SharedBackground()
    {
        InitializeComponent();

        Loaded += OnBackgroundLoaded;
        SizeChanged += OnBackgroundSizeChanged;
    }

    private void OnBackgroundLoaded(object sender, RoutedEventArgs e)
    {
        GenerateIcons(ActualWidth, ActualHeight);
    }

    private void OnBackgroundSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (Math.Abs(e.NewSize.Width - _lastWidth) > 50 ||
            Math.Abs(e.NewSize.Height - _lastHeight) > 50)
        {
            GenerateIcons(e.NewSize.Width, e.NewSize.Height);
        }
    }

    private void GenerateIcons(double width, double height)
    {
        if (width <= 0 || height <= 0)
            return;

        _lastWidth = width;
        _lastHeight = height;

        BackgroundIcons.Clear();

        int cols = (int)Math.Ceiling(width / CellSize);
        int rows = (int)Math.Ceiling(height / CellSize);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                // Khoảng 10% ô sẽ không có icon
                if (_random.NextDouble() < 0.10)
                    continue;

                double scale = 0.7 + _random.NextDouble() * 0.8;

                double actualSize = BaseIconSize * scale;

                double maxOffsetX = Math.Max(0, CellSize - actualSize);
                double maxOffsetY = Math.Max(0, CellSize - actualSize);

                double x = c * CellSize +
                           _random.NextDouble() * maxOffsetX;

                double y = r * CellSize +
                           _random.NextDouble() * maxOffsetY;

                // Không để icon bị cắt ở cạnh cửa sổ
                if (x + actualSize > width ||
                    y + actualSize > height)
                    continue;

                BackgroundIcons.Add(new BackgroundIconModel
                {
                    X = x,
                    Y = y,

                    BaseSize = BaseIconSize,
                    Scale = scale,

                    Angle = _random.Next(-25, 26),

                    Opacity = 0.12 + _random.NextDouble() * 0.16,

                    IconType = GetRandomIconType()
                });
            }
        }
    }

    private BackgroundIconType GetRandomIconType()
    {
        // Tăng tỷ lệ xuất hiện của sách
        int random = _random.Next(100);

        if (random < 25)
            return BackgroundIconType.Apple;

        if (random < 50)
            return BackgroundIconType.OpenBook;

        if (random < 75)
            return BackgroundIconType.Book;

        if (random < 88)
            return BackgroundIconType.Bookmark;

        return BackgroundIconType.Pencil;
    }
}
