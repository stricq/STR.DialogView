using System.ComponentModel.Composition;

using Str.MvvmCommon.Core;


namespace Str.DialogView.Tests.ViewModels {

  [Export]
  [ViewModel("DialogTestViewModel")]
  public class DialogTestViewModel : ObservableObject {

    #region Private Fieldss

    private RelayCommand errorDialog;

    #endregion Private Fields

    #region Properties

    public RelayCommand ErrorDialog {
      get => errorDialog;
      set => SetField(ref errorDialog, value, () => ErrorDialog);
    }

    #endregion Properties

  }

}
