using System;
using System.Windows;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Str.DialogView.Extensions;

using Str.DialogView.Tests.Views;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Tests {

  public partial class App {

    #region Private Fields

    private readonly IMvvmContainer container;

    #endregion Private Fields

    #region Constructor

    public App() {
      container = new MvvmContainer();

      container.Initialize(ConfigureServices);
    }

    #endregion Constructor

    #region Overrides

    protected override void OnStartup(StartupEventArgs args) {
      container.OnStartupAsync();

      try {
        container.InitializeControllersAsync();
      }
      catch(Exception ex) {
        while(ex.InnerException != null) ex = ex.InnerException;

        MessageBox.Show($"{ex.Message}\n\n{ex.GetType().FullName}", "Dependency Injection Error");
      }

      container.Get<DialogTestView>().Show();

      base.OnStartup(args);
    }

    protected override void OnExit(ExitEventArgs args) {
      container.OnExitAsync();

      base.OnExit(args);
    }

    #endregion Overrides

    #region Private Methods

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration) {
      services.AddStrDialogView();
    }

    #endregion Private Methods

  }

}
