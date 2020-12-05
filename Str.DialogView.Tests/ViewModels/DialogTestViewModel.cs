using System;
using System.Windows;

using Str.MvvmCommon.Core;


namespace Str.DialogView.Tests.ViewModels {

  public class DialogTestViewModel : ObservableObject {

    #region Private Fieldss

    private RelayCommandAsync<EventArgs> initialized;
    private RelayCommandAsync<RoutedEventArgs> loaded;

    private RelayCommandAsync errorDialog;

    private RelayCommandAsync inputBoxDialog;

    private RelayCommandAsync messageBoxDialog1;
    private RelayCommandAsync messageBoxDialog2;

    #endregion Private Fields

    #region Properties

    public RelayCommandAsync<EventArgs> Initialized {
      get => initialized;
      set => SetField(ref initialized, value, () => Initialized);
    }

    public RelayCommandAsync<RoutedEventArgs> Loaded {
      get => loaded;
      set => SetField(ref loaded, value, () => Loaded);
    }

    public RelayCommandAsync ErrorDialog {
      get => errorDialog;
      set => SetField(ref errorDialog, value, () => ErrorDialog);
    }

    public RelayCommandAsync InputBoxDialog {
      get => inputBoxDialog;
      set => SetField(ref inputBoxDialog, value, () => InputBoxDialog);
    }

    public RelayCommandAsync MessageBoxDialog1 {
      get => messageBoxDialog1;
      set => SetField(ref messageBoxDialog1, value, () => MessageBoxDialog1);
    }

    public RelayCommandAsync MessageBoxDialog2 {
      get => messageBoxDialog2;
      set => SetField(ref messageBoxDialog2, value, () => MessageBoxDialog2);
    }

    #endregion Properties

  }

}
