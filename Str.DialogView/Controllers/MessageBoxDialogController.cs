using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows;

using Str.DialogView.Constants;
using Str.DialogView.Messages;
using Str.DialogView.ViewModels;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Controllers {

  [Export(typeof(IController))]
  public class MessageBoxDialogController : IController {

    #region Private Fields

    private MessageBoxDialogMessage message;

    private readonly IMessenger messenger;

    private readonly MessageBoxDialogViewModel viewModel;

    #endregion Private Fields

    #region Constructor

    [ImportingConstructor]
    public MessageBoxDialogController(MessageBoxDialogViewModel viewModel, IMessenger messenger) {
      this.viewModel = viewModel;

      this.messenger = messenger;
    }

    #endregion Constructor

    #region IController Implementation

    public int InitializePriority { get; } = 90;

    public async Task InitializeAsync() {
      RegisterMessages();

      RegisterCommands();

      await Task.CompletedTask;
    }

    #endregion IController Implementation

    #region Messages

    private void RegisterMessages() {
      messenger.Register<MessageBoxDialogMessage>(this, true, OnDialogMessage);
    }

    private void OnDialogMessage(MessageBoxDialogMessage dialogMessage) {
      if (dialogMessage == null) return;

      message = dialogMessage;

      RefreshMessage();

      messenger.Send(new OpenDialogMessage { Name = DialogNames.MessageBoxDialog });
    }

    #endregion Messages

    #region Commands

    private void RegisterCommands() {
      viewModel.Ok = new RelayCommandAsync(OnOkExecute);

      viewModel.Cancel = new RelayCommandAsync(OnCancelExecute);
    }

    private async Task OnOkExecute() {
      messenger.Send(new CloseDialogMessage());

      message.IsCancel = false;

      if (message.Callback != null) message.Callback(message);
      else if (message.CallbackAsync != null) await message.CallbackAsync(message);
    }

    private async Task OnCancelExecute() {
      messenger.Send(new CloseDialogMessage());

      message.IsCancel = true;

      if (message.Callback != null) message.Callback(message);
      else if (message.CallbackAsync != null) await message.CallbackAsync(message);
    }

    #endregion Commands

    #region Private Methods

    private void RefreshMessage() {
      viewModel.Header  = message.Header;
      viewModel.Message = message.Message;

      viewModel.OkText     = message.OkText     ?? "Ok";
      viewModel.CancelText = message.CancelText ?? "Cancel";

      viewModel.IsCancelVisible = message.HasCancel ? Visibility.Visible : Visibility.Collapsed;
    }

    #endregion Private Methods

  }

}
