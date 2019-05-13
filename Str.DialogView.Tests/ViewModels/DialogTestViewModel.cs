using System.ComponentModel.Composition;

using Str.MvvmCommon.Core;


namespace Str.DialogView.Tests.ViewModels {

  [Export]
  [ViewModel("DialogTestViewModel")]
  public class DialogTestViewModel : ObservableObject {

    #region Private Fieldss

    private RelayCommand errorDialog;

    private RelayCommand inputBoxDialog;

    private RelayCommand messageBoxDialog1;
    private RelayCommand messageBoxDialog2;

    #endregion Private Fields

    #region Properties

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
