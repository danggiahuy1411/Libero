using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Libero.Services;
using Libero.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Windows.Threading;

namespace Libero.ViewModels;

public partial class LoginSignupViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    public LoginSignupViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        CheckOperatingStatus();
        _operatingTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(10)
        };
        _operatingTimer.Tick += (_, _) => CheckOperatingStatus();
        _operatingTimer.Start();
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nhập Tên đăng nhập / Số điện thoại")]
    private string _account = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nhập mật khẩu")]
    private string _password = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nhập Tên đăng nhập")]
    private string _regAccount = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nhập số điện thoại")]
    private string _regPhone = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nhập mật khẩu")]
    private string _regPassword = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Nhập lại mật khẩu")]
    private string _confirmPassword = string.Empty;

    // Dùng để điều khiển tab Đăng nhập / Đăng ký từ ViewModel (nếu muốn tự chuyển tab)
    [ObservableProperty]
    private bool _isLoginTabSelected = true;

    [ObservableProperty]
    private bool _isRegisterTabSelected = false;

    public SnackbarMessageQueue ToastQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));

    [RelayCommand]
    private void Login()
    {
        ClearErrors(nameof(RegAccount));
        ClearErrors(nameof(RegPhone));
        ClearErrors(nameof(RegPassword));
        ClearErrors(nameof(ConfirmPassword));
        ValidateProperty(Account, nameof(Account));
        ValidateProperty(Password, nameof(Password));

        if (HasErrors)
        {
            return;
        }

        if (Account == "admin" && Password == "admin")
        {
            //_navigationService.NavigateTo<>();
        }
        else if (Account == "user" && Password == "user")
        {
            //_navigationService.NavigateTo<>();
        }
        else
        {
            ToastQueue.Enqueue("❌ Tài khoản hoặc mật khẩu không đúng!");
        }
    }

    [RelayCommand]
    private void Register()
    {
        ClearErrors(nameof(Account));
        ClearErrors(nameof(Password));
        ValidateProperty(RegAccount, nameof(RegAccount));
        ValidateProperty(RegPhone, nameof(RegPhone));
        ValidateProperty(RegPassword, nameof(RegPassword));
        ValidateProperty(ConfirmPassword, nameof(ConfirmPassword));

        // Kiểm tra thêm mật khẩu nhập lại có khớp không
        if (RegPassword != ConfirmPassword)
        {
            ToastQueue.Enqueue("⚠️ Mật khẩu nhập lại chưa khớp!");
            return;
        }

        if (HasErrors)
        {
            return;
        }

        // 2. Điền sẵn tài khoản vừa đăng ký sang form Đăng nhập & xóa trắng form Đăng ký
        Account = RegAccount;
        Password = string.Empty;
        RegAccount = string.Empty;
        RegPhone = string.Empty;
        RegPassword = string.Empty;
        ConfirmPassword = string.Empty;

        // Tự động chuyển tab sang Đăng nhập
        IsLoginTabSelected = true;
        IsRegisterTabSelected = false;

        // 3. Hiển thị thông báo ngay trên trang hiện tại
        ToastQueue.Enqueue("🎉 Đăng ký tài khoản Libero thành công!");
    }

    private readonly DispatcherTimer _operatingTimer;

    [ObservableProperty]
    private string _openHours = "07:30";

    [ObservableProperty]
    private string _closeHours = "20:00";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StatusText))]
    private bool _isOperating;

    public string StatusText => IsOperating ? "Active" : "Inactive";

    partial void OnOpenHoursChanged(string value) => CheckOperatingStatus();
    partial void OnCloseHoursChanged(string value) => CheckOperatingStatus();

    private void CheckOperatingStatus()
    {
        DateTime now = DateTime.Now;

        if (now.DayOfWeek == DayOfWeek.Sunday)
        {
            IsOperating = false;
            return;
        }

        if (TimeSpan.TryParse(OpenHours, out TimeSpan start) &&
            TimeSpan.TryParse(CloseHours, out TimeSpan end))
        {
            TimeSpan current = now.TimeOfDay;
            IsOperating = current >= start && current < end;
        }
        else
        {
            IsOperating = false;
        }
    }
}