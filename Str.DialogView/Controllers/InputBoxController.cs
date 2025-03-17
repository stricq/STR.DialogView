using System.Diagnostics.CodeAnalysis;

using Str.Common.Extensions;

using Str.DialogView.Messages;
using Str.DialogView.ViewModels;
using Str.DialogView.Views;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Controllers;


[SuppressMessage("ReSharper", "AsyncConverter.CanBeUseAsyncMethodHighlighting", Justification = "Cannot use async as this is the main thread.")]
public class InputBoxController(InputBoxViewModel viewModel, IMessenger messenger) : IController, IMessageReceiver {

    #region Private Fields

    private InputBoxDialogMessage? message;

    private readonly InputBoxViewModel viewModel = viewModel;

    private readonly IMessenger messenger = messenger;

    #endregion Private Fields

    #region IController Implementation

    public int InitializePriority => 90;

    public Task InitializeAsync() {
        RegisterMessages();

        RegisterCommands();

        return Task.CompletedTask;
    }

    #endregion IController Implementation

    #region Messages

    private void RegisterMessages() {
        messenger.Register<InputBoxDialogMessage>(this, true, OnInputBoxExecuteAsync);
    }

    private Task OnInputBoxExecuteAsync(InputBoxDialogMessage dialogMessage) {
        message = dialogMessage;

        RefreshMessage();

        return messenger.SendAsync(new OpenDialogMessage { DialogViewType = typeof(InputBoxView) });
    }

    #endregion Messages

    #region Commands

    private void RegisterCommands() {
        viewModel.Ok     = new RelayCommandAsync(OnOkExecuteAsync);
        viewModel.Cancel = new RelayCommandAsync(OnCancelExecuteAsync);
    }

    private async Task OnOkExecuteAsync() {
        await messenger.SendAsync(new CloseDialogMessage()).Fire();

        message!.Input    = viewModel.InputText;
        message!.IsCancel = false;

        if (message!.Callback != null) message!.Callback(message);
        else if (message!.CallbackAsync != null) await message!.CallbackAsync(message).Fire();
    }

    private async Task OnCancelExecuteAsync() {
        await messenger.SendAsync(new CloseDialogMessage()).Fire();

        message!.Input    = viewModel.InputText;
        message!.IsCancel = true;

        if (message!.Callback != null) message!.Callback(message);
        else if (message!.CallbackAsync != null) await message!.CallbackAsync(message).Fire();
    }

    #endregion Commands

    #region Private Methods

    private void RefreshMessage() {
        viewModel.HeaderText  = message!.Header;
        viewModel.MessageText = message!.Message;
        viewModel.InputText   = message!.DefaultInput;

        viewModel.OkText     = message!.OkText ?? "Ok";
        viewModel.CancelText = message!.CancelText ?? "Cancel";
    }

    #endregion Private Methods

}
