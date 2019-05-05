using System.ComponentModel.Composition;
using System.Windows;

using Str.DialogView.Contracts;

using Str.MvvmCommon.Core;


namespace Str.DialogView.ViewModels {

  [Export]
  [ViewModel("Str.DialogView.ErrorDialogViewModel")]
  public class ErrorDialogViewModel : ObservableObject, IDialogViewModel {

    #region Private Fields

    private string headerText;

    private string index;

    private string messageText;

    private string stackTrace;

    private Visibility visibility;

    private RelayCommand clear;

    private RelayCommand clearAll;

    private RelayCommand next;

    private RelayCommand ok;

    private RelayCommand previous;

    #endregion Private Fields

    #region Properties

    public string HeaderText {
      get => headerText;
      set => SetField(ref headerText, value, () => HeaderText);
    }

    public string Index {
      get => index;
      set => SetField(ref index, value, () => Index);
    }

    public string MessageText {
      get => messageText;
      set => SetField(ref messageText, value, () => MessageText);
    }

    public string StackTrace {
      get => stackTrace;
      set => SetField(ref stackTrace, value, () => StackTrace);
    }

    public Visibility Visibility {
      get => visibility;
      set => SetField(ref visibility, value, () => Visibility);
    }

    public RelayCommand Clear {
      get => clear;
      set => SetField(ref clear, value, () => Clear);
    }

    public RelayCommand ClearAll {
      get => clearAll;
      set => SetField(ref clearAll, value, () => ClearAll);
    }

    public RelayCommand Next {
      get => next;
      set => SetField(ref next, value, () => Next);
    }

    public RelayCommand Ok {
      get => ok;
      set => SetField(ref ok, value, () => Ok);
    }

    public RelayCommand Previous {
      get => previous;
      set => SetField(ref previous, value, () => Previous);
    }

    #endregion Properties

  }

}
