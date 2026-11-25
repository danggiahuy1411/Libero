using Libero;
using Libero.Services;
using Libero.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Libero;

public partial class App : Application
{
    private ServiceProvider _serviceProvider;
    public App()
    {
        ServiceCollection services = new();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = _serviceProvider
            .GetRequiredService<MainWindow>();

        mainWindow.DataContext = _serviceProvider
            .GetRequiredService<MainViewModel>();

        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider.Dispose();
        base.OnExit(e);
    }
}