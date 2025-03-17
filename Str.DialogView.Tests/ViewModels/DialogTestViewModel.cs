using System;
using System.Windows;

using JetBrains.Annotations;

using Str.MvvmCommon.Core;
using Str.MvvmCommon.Helpers;


namespace Str.DialogView.Tests.ViewModels;


[UsedImplicitly]
public class DialogTestViewModel : ObservableObject {

    #region Private Fieldss

    private RelayCommandAsync<EventArgs> initialized = RelayCommandHelper.EmptyRelayCommandT<EventArgs>();
    private RelayCommandAsync<RoutedEventArgs> loaded = RelayCommandHelper.EmptyRelayCommandT<RoutedEventArgs>();

    private RelayCommandAsync errorDialog = RelayCommandHelper.EmptyRelayCommand;

    private RelayCommandAsync inputBoxDialog = RelayCommandHelper.EmptyRelayCommand;

    private RelayCommandAsync messageBoxDialog1 = RelayCommandHelper.EmptyRelayCommand;
    private RelayCommandAsync messageBoxDialog2 = RelayCommandHelper.EmptyRelayCommand;

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
