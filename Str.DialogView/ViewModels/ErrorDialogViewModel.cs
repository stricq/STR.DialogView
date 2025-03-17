using System;
using System.Windows;

using Str.DialogView.Contracts;

using Str.MvvmCommon.Core;
using Str.MvvmCommon.Helpers;


namespace Str.DialogView.ViewModels;


public class ErrorDialogViewModel : ObservableObject, IDialogViewModel {

    #region Private Fields

    private string headerText = String.Empty;

    private string index = String.Empty;

    private string messageText = String.Empty;

    private string stackTrace = String.Empty;

    private Visibility visibility;

    private RelayCommandAsync clear = RelayCommandHelper.EmptyRelayCommand;

    private RelayCommandAsync clearAll = RelayCommandHelper.EmptyRelayCommand;

    private RelayCommandAsync next = RelayCommandHelper.EmptyRelayCommand;

    private RelayCommandAsync ok = RelayCommandHelper.EmptyRelayCommand;

    private RelayCommandAsync previous = RelayCommandHelper.EmptyRelayCommand;

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

    public RelayCommandAsync Clear {
        get => clear;
        set => SetField(ref clear, value, () => Clear);
    }

    public RelayCommandAsync ClearAll {
        get => clearAll;
        set => SetField(ref clearAll, value, () => ClearAll);
    }

    public RelayCommandAsync Next {
        get => next;
        set => SetField(ref next, value, () => Next);
    }

    public RelayCommandAsync Ok {
        get => ok;
        set => SetField(ref ok, value, () => Ok);
    }

    public RelayCommandAsync Previous {
        get => previous;
        set => SetField(ref previous, value, () => Previous);
    }

    #endregion Properties

}
