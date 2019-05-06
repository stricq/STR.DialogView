using System.ComponentModel.Composition;
using System.Threading.Tasks;

using Str.DialogView.Constants;
using Str.DialogView.Messages;
using Str.DialogView.ViewModels;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Controllers {

  [Export(typeof(IController))]
  public class InputBoxController : IController {

    #region Private Fields

    private InputBoxDialogMessage message;

    private readonly InputBoxViewModel viewModel;

    private readonly IMessenger messenger;

    #endregion Private Fields

    #region Constructor

    [ImportingConstructor]
    public InputBoxController(InputBoxViewModel viewModel, IMessenger messenger) {
      this.viewModel = viewModel;

      this.messenger = messenger;
    }

    #endregion Constructor

    #region IController Implementation

    public int InitializePriority { get; } = 110;

    public async Task InitializeAsync() {
      RegisterMessages();

      RegisterCommands();

      await Task.CompletedTask;
    }

    #endregion IController Implementation

    #region Messages

    private void RegisterMessages() {
      messenger.Register<InputBoxDialogMessage>(this, true, OnInputBoxExecute);
    }

    private void OnInputBoxExecute(InputBoxDialogMessage dialogMessage) {
      message = dialogMessage;

      RefreshMessage();

      messenger.Send(new OpenDialogMessage { Name = DialogNames.InputBoxDialog });
    }

    #endregion Messages

    #region Commands

    private void RegisterCommands() {
      viewModel.Ok     = new RelayCommandAsync(OnOkExecute);
      viewModel.Cancel = new RelayCommandAsync(OnCancelExecute);
    }

    private async Task OnOkExecute() {
      messenger.Send(new CloseDialogMessage());

      message.Input    = viewModel.InputText;
      message.IsCancel = false;

      if (message.Callback != null) message.Callback(message);
      else if (message.CallbackAsync != null) await message.CallbackAsync(message);
    }

    private async Task OnCancelExecute() {
      messenger.Send(new CloseDialogMessage());

      message.Input    = viewModel.InputText;
      message.IsCancel = true;

      if (message.Callback != null) message.Callback(message);
      else if (message.CallbackAsync != null) await message.CallbackAsync(message);
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
