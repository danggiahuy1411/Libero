using Libero.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libero.Services;

public interface INavigationService
{
    BaseViewModel? CurrentViewModel { get; }

    event Action? CurrentViewModelChanged;

    void NavigateTo<T>() where T : BaseViewModel;
}