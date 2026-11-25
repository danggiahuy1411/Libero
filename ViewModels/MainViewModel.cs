using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Libero.Services;
using Libero.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libero.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IThemeService _themeService;

    [ObservableProperty]
    private bool isDarkMode;

    [ObservableProperty]
    private BaseViewModel? currentViewModel;

    public MainViewModel(INavigationService navigationService, IThemeService themeService)
    {
        _navigationService = navigationService;
        _themeService = themeService;
        _navigationService.CurrentViewModelChanged +=
            OnCurrentViewModelChanged;
    }

    [RelayCommand]
    private void ToggleTheme()
    {
        _themeService.ToggleTheme();

        IsDarkMode = _themeService.IsDarkMode;
    }
    private void OnCurrentViewModelChanged()
    {
        CurrentViewModel = _navigationService.CurrentViewModel;
    }
}