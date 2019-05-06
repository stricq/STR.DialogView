using System.ComponentModel.Composition;

using Str.MvvmCommon.Core;


namespace Str.DialogView.Tests.ViewModels {

  [Export]
  [ViewModel("DialogTestViewModel")]
  public class DialogTestViewModel : ObservableObject {

    #region Private Fieldss

    private RelayCommand errorDialog;

    private RelayCommand inputBoxDialog;

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

    #endregion Properties

  }

}
