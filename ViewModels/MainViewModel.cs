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

    [ObservableProperty]
    private BaseViewModel? currentViewModel;

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _navigationService.CurrentViewModelChanged +=
            OnCurrentViewModelChanged;
    }
    private void OnCurrentViewModelChanged()
    {
        CurrentViewModel = _navigationService.CurrentViewModel;
    }
}