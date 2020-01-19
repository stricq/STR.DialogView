using System;
using System.Windows;

using Str.MvvmCommon.Core;


namespace Str.DialogView.Tests.ViewModels {

  public class DialogTestViewModel : ObservableObject {

    #region Private Fieldss

    private RelayCommandAsync<EventArgs> initialized;
    private RelayCommandAsync<RoutedEventArgs> loaded;

    private RelayCommand errorDialog;

    private RelayCommand inputBoxDialog;

    private RelayCommand messageBoxDialog1;
    private RelayCommand messageBoxDialog2;

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

    public RelayCommand ErrorDialog {
      get => errorDialog;
      set => SetField(ref errorDialog, value, () => ErrorDialog);
    }

    public RelayCommand InputBoxDialog {
      get => inputBoxDialog;
      set => SetField(ref inputBoxDialog, value, () => InputBoxDialog);
    }

    public RelayCommand MessageBoxDialog1 {
      get => messageBoxDialog1;
      set => SetField(ref messageBoxDialog1, value, () => MessageBoxDialog1);
    }

    public RelayCommand MessageBoxDialog2 {
      get => messageBoxDialog2;
      set => SetField(ref messageBoxDialog2, value, () => MessageBoxDialog2);
    }

    #endregion Properties

  }

}
