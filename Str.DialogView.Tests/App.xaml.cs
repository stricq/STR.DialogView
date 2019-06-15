using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

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

      container.Initialize(() => new AggregateCatalog(new DirectoryCatalog(Directory.GetCurrentDirectory(), "Str.*.dll")));
    }

    #endregion Constructor

    #region Overrides

    protected override void OnStartup(StartupEventArgs args) {
      base.OnStartup(args);

      try {
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
