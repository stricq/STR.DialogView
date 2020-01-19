using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Str.Common.Extensions;

using Str.DialogView.Messages;
using Str.DialogView.ViewModels;
using Str.DialogView.Views;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Controllers {

  [SuppressMessage("ReSharper", "AsyncConverter.CanBeUseAsyncMethodHighlighting", Justification = "Cannot use async as this is the main thread.")]
  public class InputBoxController : IController {

    #region Private Fields

    private InputBoxDialogMessage message;

    private readonly InputBoxViewModel viewModel;

    private readonly IMessenger messenger;

    #endregion Private Fields

    #region Constructor

    public InputBoxController(InputBoxViewModel viewModel, IMessenger messenger) {
      this.viewModel = viewModel;

      this.messenger = messenger;
    }

    #endregion Constructor

    #region IController Implementation

    public int InitializePriority { get; } = 90;

    public Task InitializeAsync() {
      RegisterMessages();

      RegisterCommands();

      return Task.CompletedTask;
    }

    #endregion IController Implementation

    #region Messages

    private void RegisterMessages() {
      messenger.Register<InputBoxDialogMessage>(this, true, OnInputBoxExecute);
    }

    private void OnInputBoxExecute(InputBoxDialogMessage dialogMessage) {
      if (dialogMessage == null) return;

      message = dialogMessage;

      RefreshMessage();

      messenger.Send(new OpenDialogMessage { DialogViewType = typeof(InputBoxView)});
    }

    #endregion Messages

    #region Commands

    private void RegisterCommands() {
      viewModel.Ok     = new RelayCommandAsync(OnOkExecuteAsync);
      viewModel.Cancel = new RelayCommandAsync(OnCancelExecuteAsync);
    }

    private async Task OnOkExecuteAsync() {
      messenger.Send(new CloseDialogMessage());

      message.Input    = viewModel.InputText;
      message.IsCancel = false;

      if (message.Callback != null) message.Callback(message);
      else if (message.CallbackAsync != null) await message.CallbackAsync(message).Fire();
    }

    private async Task OnCancelExecuteAsync() {
      messenger.Send(new CloseDialogMessage());

      message.Input    = viewModel.InputText;
      message.IsCancel = true;

      if (message.Callback != null) message.Callback(message);
      else if (message.CallbackAsync != null) await message.CallbackAsync(message).Fire();
    }

    #endregion Commands

    #region Private Methods

    private void RefreshMessage() {
      viewModel.HeaderText  = message.Header;
      viewModel.MessageText = message.Message;
      viewModel.InputText   = message.DefaultInput;

      viewModel.OkText     = message.OkText     ?? "Ok";
      viewModel.CancelText = message.CancelText ?? "Cancel";
    }

    #endregion Private Methods

  }

}
