using System;

using Str.DialogView.Contracts;

using Str.MvvmCommon.Core;
using Str.MvvmCommon.Helpers;


namespace Str.DialogView.ViewModels;


public class InputBoxViewModel : ObservableObject, IDialogViewModel {

    #region Private Fields

    private string headerText = String.Empty;
    private string messageText = String.Empty;
    private string inputText = String.Empty;

    private string okText = String.Empty;
    private string cancelText = String.Empty;

    private RelayCommandAsync ok = RelayCommandHelper.EmptyRelayCommand;
    private RelayCommandAsync cancel = RelayCommandHelper.EmptyRelayCommand;

    #endregion Private Fields

    #region Properties

    public string HeaderText {
        get => headerText;
        set => SetField(ref headerText, value, () => HeaderText);
    }

    public string MessageText {
        get => messageText;
        set => SetField(ref messageText, value, () => MessageText);
    }

    public string InputText {
        get => inputText;
        set => SetField(ref inputText, value, () => InputText);
    }

    public string OkText {
        get => okText;
        set => SetField(ref okText, value, () => OkText);
    }

    public string CancelText {
        get => cancelText;
        set => SetField(ref cancelText, value, () => CancelText);
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
