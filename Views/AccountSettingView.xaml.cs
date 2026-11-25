using System.Windows;
using System.Windows.Controls;

namespace Libero.Views
{
    public partial class AccountSettingView : UserControl
    {
        private bool _isStacked;

        public AccountSettingView()
        {
            InitializeComponent();
        }

        private void CardsGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            bool shouldStack = e.NewSize.Width < 780;
            if (shouldStack == _isStacked) return;

            _isStacked = shouldStack;

            if (shouldStack)
            {
                // Chuyển sang 1 cột, đẩy Đổi mật khẩu xuống hàng dưới
                GapColumn.Width = new GridLength(0);
                RightColumn.Width = new GridLength(0);
                GapRow.Height = new GridLength(24);

                Grid.SetRow(PasswordCard, 2);
                Grid.SetColumn(PasswordCard, 0);
            }
            else
            {
                // Trả về 2 cột song song
                GapColumn.Width = new GridLength(24);
                RightColumn.Width = new GridLength(1, GridUnitType.Star);
                GapRow.Height = new GridLength(0);

                Grid.SetRow(PasswordCard, 0);
                Grid.SetColumn(PasswordCard, 2);
            }
        }
    }
}