using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Libero.ViewModels;
using MaterialDesignThemes.Wpf;
using Libero.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Threading;

namespace Libero.ViewModels;

public partial class AccountSettingViewModel : BaseViewModel
{
    // --- Phần Hồ sơ ---
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    private string fullName = "Lê Minh Anh";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    private string email = "anh.lm@student.uit.edu.vn";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    private string phoneNumber = "0901 234 567";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Vui lòng nhập Thẻ SV / CCCD")]
    private string identityCard = "22520001";

    // --- Phần Đổi mật khẩu ---
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nhập mật khẩu hiện tại")]
    private string currentPassword = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nhập mật khẩu mới")]
    private string newPassword = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nhập lại mật khẩu mới")]
    private string confirmNewPassword = string.Empty;

    [RelayCommand]
    private void SaveProfile()
    {
        ClearErrors(nameof(CurrentPassword));
        ClearErrors(nameof(NewPassword));
        ClearErrors(nameof(ConfirmNewPassword));

        ValidateProperty(FullName, nameof(FullName));
        ValidateProperty(Email, nameof(Email));
        ValidateProperty(PhoneNumber, nameof(PhoneNumber));
        ValidateProperty(IdentityCard, nameof(IdentityCard));

        if (HasErrors) return;

        // Xử lý lưu thông tin hồ sơ xuống database/service...
    }

    [RelayCommand]
    private void ChangePassword()
    {
        ClearErrors(nameof(FullName));
        ClearErrors(nameof(Email));
        ClearErrors(nameof(PhoneNumber));
        ClearErrors(nameof(IdentityCard));

        ValidateProperty(CurrentPassword, nameof(CurrentPassword));
        ValidateProperty(NewPassword, nameof(NewPassword));
        ValidateProperty(ConfirmNewPassword, nameof(ConfirmNewPassword));

        if (HasErrors || NewPassword != ConfirmNewPassword) return;

        // Xử lý đổi mật khẩu...
        CurrentPassword = string.Empty;
        NewPassword = string.Empty;
        ConfirmNewPassword = string.Empty;
    }
}