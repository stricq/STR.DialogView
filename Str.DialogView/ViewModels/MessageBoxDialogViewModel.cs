using System.Windows;

using Str.DialogView.Contracts;

using Str.MvvmCommon.Core;
using Str.MvvmCommon.Helpers;


namespace Str.DialogView.ViewModels;


public class MessageBoxDialogViewModel : ObservableObject, IDialogViewModel {

    #region Private Fields

    private string header = String.Empty;

    private string message = String.Empty;

    private string okText = String.Empty;

    private string cancelText = String.Empty;

    private Visibility isCancelVisible;

    private RelayCommandAsync ok = RelayCommandHelper.EmptyRelayCommand;

    private RelayCommandAsync cancel = RelayCommandHelper.EmptyRelayCommand;

    #endregion Private Fields

    #region Properties

    public string Header {
        get => header;
        set => SetField(ref header, value, () => Header);
    }

    public string Message {
        get => message;
        set => SetField(ref message, value, () => Message);
    }

    public string OkText {
        get => okText;
        set => SetField(ref okText, value, () => OkText);
    }

    public string CancelText {
        get => cancelText;
        set => SetField(ref cancelText, value, () => CancelText);
    }

    public Visibility IsCancelVisible {
        get => isCancelVisible;
        set => SetField(ref isCancelVisible, value, () => IsCancelVisible);
    }

    public RelayCommandAsync Ok {
        get => ok;
        set => SetField(ref ok, value, () => Ok);
    }

    public RelayCommandAsync Cancel {
        get => cancel;
        set => SetField(ref cancel, value, () => Cancel);
    }

    #endregion Properties

}
