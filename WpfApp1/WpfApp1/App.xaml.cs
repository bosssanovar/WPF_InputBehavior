using EntityMonitor;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.Windows;
using Usecase;
using WpfApp1.MainWindow;
using WpfApp1.Setup;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // DI
            var services = new ServiceCollection();
            services.AddSingleton<IXXRepository, InMemoryXXRepository>();
            services.AddTransient<SaveLoadUsecase>();
            services.AddTransient<InitUsecase>();

            services.AddTransient<MainWindowModel>();
            services.AddTransient<MainWindowView>();

            services.AddTransient<EntityMonitorView>();

            services.AddTransient<SetupModel>();
            services.AddTransient<SetupView>();

            var provider = services.BuildServiceProvider();

            var setupWindow = provider.GetRequiredService<SetupView>();
            var mainWindow = provider.GetRequiredService<MainWindowView>();
            MainWindow = mainWindow;
            var entityMonitor = provider.GetRequiredService<EntityMonitorView>();

            entityMonitor.Show();
            setupWindow.ShowDialog();

            mainWindow.Show();
        }
    }
}