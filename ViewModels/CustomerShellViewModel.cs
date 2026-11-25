using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Libero.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Libero.ViewModels;

public partial class CustomerShellViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private bool isNavCollapsed;

    [ObservableProperty]
    private BaseViewModel? currentPageViewModel;

    [ObservableProperty]
    private string _currentPageKey = "Homepage";

    [ObservableProperty]
    private string userName = "Lê Minh Anh";

    [ObservableProperty]
    private string userInitials = "AL";

    [ObservableProperty]
    private string userRoleDisplay = "Khách hàng";

    public CustomerShellViewModel(INavigationService navigationService, IServiceProvider serviceProvider)
    {
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;

        // Mở mặc định trang chủ khách hàng khi vừa đăng nhập
        Navigate("Homepage");
    }

    [RelayCommand]
    private void Navigate(string pageKey)
    {
        if (string.IsNullOrWhiteSpace(pageKey)) return;

        CurrentPageKey = pageKey;

        CurrentPageViewModel = pageKey switch
        {
            "Homepage" => _serviceProvider.GetRequiredService<CustomerHomepageViewModel>(),
            "AccountSetting" => _serviceProvider.GetRequiredService<AccountSettingViewModel>(),
            "Logout" => _serviceProvider.GetRequiredService<LoginSignupViewModel>(),
            _ => CurrentPageViewModel
        };
    }

    [RelayCommand]
    private void OpenProfile()
    {
        // Bấm "Hồ sơ cá nhân" ở AccountMenu -> Mở trang AccountSettingView
        Navigate("AccountSetting");
    }

    [RelayCommand]
    private void SwitchRole(string role)
    {
        UserRoleDisplay = role switch
        {
            "Customer" => "Khách hàng",
            "Staff" => "Nhân viên",
            "Admin" => "Quản trị viên",
            _ => UserRoleDisplay
        };
    }

    [RelayCommand]
    private void Logout()
    {
        // Quay về màn hình LoginSignupView
        _navigationService.NavigateTo<LoginSignupViewModel>();
    }
}