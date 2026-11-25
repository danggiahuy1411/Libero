using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Libero.Views;
public partial class LoginSignupView : UserControl
{
    public LoginSignupView()
    {
        InitializeComponent();
    }

    private void RootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        const double defaultRightMinWidth = 420;
        double minCombinedWidth = LeftPanel.MinWidth + defaultRightMinWidth;

        if (e.NewSize.Width < minCombinedWidth)
        {
            LeftPanel.Visibility = Visibility.Collapsed;
            LeftColumn.Width = new GridLength(0);
            RightColumn.Width = new GridLength(1, GridUnitType.Star);
            RightPanel.MinWidth = 0;
        }
        else
        {
            LeftPanel.Visibility = Visibility.Visible;
            LeftColumn.Width = new GridLength(5, GridUnitType.Star);
            RightColumn.Width = new GridLength(6, GridUnitType.Star);
            RightPanel.MinWidth = defaultRightMinWidth;
        }
    }
}