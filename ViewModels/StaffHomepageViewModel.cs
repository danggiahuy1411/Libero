using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Libero.Services;
using Libero.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace Libero.ViewModels;

public partial class StaffHomepageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    public StaffHomepageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void SignOut()
    {
        _navigationService.NavigateTo<LoginSignupViewModel>();
    }
}