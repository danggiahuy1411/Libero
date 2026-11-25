using Libero.Services;
using Libero.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libero.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    public BaseViewModel? CurrentViewModel { get; private set; }

    public event Action? CurrentViewModelChanged;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void NavigateTo<T>() where T : BaseViewModel
    {
        CurrentViewModel = _serviceProvider.GetRequiredService<T>();
        CurrentViewModelChanged?.Invoke();
    }
}