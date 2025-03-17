using System.Threading.Tasks;
using System.Windows;

using Str.Common.Extensions;

using Str.DialogView.Messages;
using Str.DialogView.ViewModels;
using Str.DialogView.Views;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Controllers;


public class MessageBoxDialogController(MessageBoxDialogViewModel viewModel, IMessenger messenger) : IController, IMessageReceiver {

    #region Private Fields

    private MessageBoxDialogMessage? message;

    private readonly IMessenger messenger = messenger;

    private readonly MessageBoxDialogViewModel viewModel = viewModel;

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
        messenger.Register<MessageBoxDialogMessage>(this, true, OnDialogMessageAsync);
    }

    private async Task OnDialogMessageAsync(MessageBoxDialogMessage dialogMessage) {
        message = dialogMessage;

        RefreshMessage();

        await messenger.SendAsync(new OpenDialogMessage { DialogViewType = typeof(MessageBoxDialogView) }).Fire();
    }

    #endregion Messages

    #region Commands

    private void RegisterCommands() {
        viewModel.Ok = new RelayCommandAsync(OnOkExecuteAsync);

        viewModel.Cancel = new RelayCommandAsync(OnCancelExecuteAsync);
    }

    private async Task OnOkExecuteAsync() {
        await messenger.SendAsync(new CloseDialogMessage()).Fire();

        message!.IsCancel = false;

        if (message!.Callback != null) message!.Callback(message);
        else if (message!.CallbackAsync != null) await message!.CallbackAsync(message).Fire();
    }

    private async Task OnCancelExecuteAsync() {
        await messenger.SendAsync(new CloseDialogMessage()).Fire();

        message!.IsCancel = true;

        if (message!.Callback != null) message!.Callback(message);
        else if (message!.CallbackAsync != null) await message!.CallbackAsync(message).Fire();
    }

    #endregion Commands

    #region Private Methods

    private void RefreshMessage() {
        viewModel.Header  = message!.Header;
        viewModel.Message = message!.Message;

        viewModel.OkText     = message!.OkText ?? "Ok";
        viewModel.CancelText = message!.CancelText ?? "Cancel";

        viewModel.IsCancelVisible = message!.HasCancel ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion Private Methods

}
