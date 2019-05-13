using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

using Str.DialogView.Tests.Views;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Tests {

  public partial class App : Application {

    #region Private Fields

    private readonly IMvvmContainer container;

    #endregion Private Fields

    #region Constructor

    public App() {
      container = new MvvmContainer();

      container.Initialize(() => new AggregateCatalog(new DirectoryCatalog(Directory.GetCurrentDirectory(), "Str.*.dll")));
    }

    #endregion Constructor

    #region Overrides

    private void Application_Startup(object sender, StartupEventArgs e) {
      DialogTestView window = new DialogTestView();

      window.Show();
      window.Hide();

      window = new DialogTestView();

      // Don't use InitializeComponent() in MainWindow.xaml.cs to avoid double initialization
      window.InitializeComponent();

      window.Show();
    }

    protected override void OnStartup(StartupEventArgs args) {
      base.OnStartup(args);

      try {
        //IEnumerable<IAutoMapperConfiguration> configurations = container.GetAll<IAutoMapperConfiguration>();

        //MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => configurations.ForEach(configuration => configuration.RegisterMappings(cfg)));

        //try {
        //  mapperConfiguration.AssertConfigurationIsValid();
        //}
        //catch(Exception ex) {
        //  MessageBox.Show($"{ex.Message}\n\n{ex.GetType().FullName}", "Mapping Validation Error");
        //}

        //container.RegisterInstance(mapperConfiguration.CreateMapper());

        container.InitializeControllers();
      }
      catch(Exception ex) {
        while(ex.InnerException != null) ex = ex.InnerException;

        if (ex is ReflectionTypeLoadException rtle) {
          MessageBox.Show(String.Join("\n", rtle.LoaderExceptions.Select(le => le.Message)), "MEF Composition Error - Reflection Type Load Exception");
        }
        else MessageBox.Show($"{ex.Message}\n\n{ex.GetType().FullName}", "MEF Composition Error");
      }
    }

    #endregion Overrides

  }

}
