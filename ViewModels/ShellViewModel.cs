using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Libero.Models;
using Libero.Services;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace Libero.ViewModels;

public partial class ShellViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private bool isNavCollapsed;

    [ObservableProperty]
    private BaseViewModel? currentPageViewModel;

    [ObservableProperty]
    private string currentPageKey = "Homepage";

    [ObservableProperty]
    private ObservableCollection<NavItemModel> navigationItems = new();

    [ObservableProperty]
    private string userName = "Lê Minh Anh";

    [ObservableProperty]
    private string userInitials = "AL";

    [ObservableProperty]
    private string userRoleDisplay = "Khách hàng";

    public ShellViewModel(INavigationService navigationService, IServiceProvider serviceProvider, IUserSessionService userSession)
    {
        _navigationService = navigationService;
        _serviceProvider = serviceProvider;

        UserName = userSession.UserName;
        UserInitials = userSession.UserInitials;

        // Phải truyền userSession.CurrentRole vào đây thay vì chữ "Customer" cứng
        SetRole(userSession.CurrentRole);
    }

    public void SetRole(string role)
    {
        UserRoleDisplay = role switch
        {
            "Customer" => "Khách hàng",
            "Staff" => "Nhân viên",
            "Admin" => "Quản trị viên",
            _ => "Khách hàng"
        };

        NavigationItems = role switch
        {
            "Customer" => new ObservableCollection<NavItemModel>
            {
                new() { PageKey = "Homepage", Title = "Trang chủ", Icon = PackIconKind.ViewGridOutline },
                new() { PageKey = "SearchBooks", Title = "Tra cứu sách", Icon = PackIconKind.BookSearchOutline },
                new() { PageKey = "AccountSetting", Title = "Tài khoản", Icon = PackIconKind.AccountOutline, SectionHeader = "Cá nhân" }
            },
            "Staff" => new ObservableCollection<NavItemModel>
            {
                new() { PageKey = "StaffDashboard", Title = "Tổng quan", Icon = PackIconKind.ViewDashboardOutline },
                new() { PageKey = "BorrowReturn", Title = "Kho sách", Icon = PackIconKind.BookMultipleOutline },
                new() { PageKey = "AccountSetting", Title = "Tài khoản", Icon = PackIconKind.AccountOutline, SectionHeader = "Cá nhân" }
            },
            "Admin" => new ObservableCollection<NavItemModel>
            {
                new() { PageKey = "AdminDashboard", Title = "Thống kê", Icon = PackIconKind.ChartBoxOutline },
                new() { PageKey = "ManageUsers", Title = "Người dùng", Icon = PackIconKind.AccountGroupOutline },
                new() { PageKey = "AccountSetting", Title = "Tài khoản", Icon = PackIconKind.AccountOutline, SectionHeader = "Cá nhân" }
            },
            _ => new ObservableCollection<NavItemModel>()
        };

        if (NavigationItems.Count > 0)
            Navigate(NavigationItems[0].PageKey);
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
            "StaffDashboard" => _serviceProvider.GetRequiredService<StaffHomepageViewModel>(),
            "AdminDashboard" => _serviceProvider.GetRequiredService<AdminHomepageViewModel>(),
            _ => CurrentPageViewModel
        };
    }

    [RelayCommand]
    private void OpenProfile()
    {
        Navigate("AccountSetting");
    }

    [RelayCommand]
    private void SwitchRole(string role)
    {
        SetRole(role);
    }

    [RelayCommand]
    private void Logout()
    {
        _navigationService.NavigateTo<LoginSignupViewModel>();
    }
}