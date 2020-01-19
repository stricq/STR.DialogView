using System;
using System.Threading.Tasks;
using System.Windows;

using Str.Common.Messages;

using Str.DialogView.Messages;
using Str.DialogView.Tests.ViewModels;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Tests.Controllers {

  public class DialogTestController : IController {

    #region Private Fields

    private readonly DialogTestViewModel viewModel;

    private readonly IMessenger messenger;

    #endregion Private Fields

    #region Constructor

    public DialogTestController(DialogTestViewModel viewModel, IMessenger messenger) {
      this.viewModel = viewModel;

      this.messenger = messenger;
    }

    #endregion Constructor

    #region IController Implementation

    public int InitializePriority { get; } = 1000;

    public Task InitializeAsync() {
      RegisterCommands();

      return Task.CompletedTask;
    }

    #endregion IController Implementation

    #region Commands

    private void RegisterCommands() {
      viewModel.Initialized = new RelayCommandAsync<EventArgs>(OnInitializedAsync);
      viewModel.Loaded      = new RelayCommandAsync<RoutedEventArgs>(OnLoadedAsync);

      viewModel.ErrorDialog = new RelayCommand(OnErrorDialogExecute);

      viewModel.InputBoxDialog = new RelayCommand(OnInputBoxDialogExecute);

      viewModel.MessageBoxDialog1 = new RelayCommand(OnMessageBoxDialog1Execute);

      viewModel.MessageBoxDialog2 = new RelayCommand(OnMessageBoxDialog2Execute);
    }

    private Task OnInitializedAsync(EventArgs ea) {
//    MessageBox.Show("OnInitialized");

      return Task.CompletedTask;
    }

    private Task OnLoadedAsync(RoutedEventArgs rea) {
//    MessageBox.Show("OnLoaded");

      return Task.CompletedTask;
    }

    private void OnErrorDialogExecute() {
      try {
        try {
          throw new Exception("Throwing an inner exception.");
        }
        catch(Exception ex) {
          throw new Exception("Caught an exception and re-throwing.", ex);
        }
      }
      catch(Exception ex) {
        messenger.Send(new ApplicationErrorMessage { HeaderText = "This is the Header", ErrorMessage = "This is the error message.", Exception = ex });

        messenger.Send(new ApplicationErrorMessage { Exception = ex });
      }
    }

    private void OnInputBoxDialogExecute() {
      messenger.Send(new InputBoxDialogMessage { Header = "Input A String", DefaultInput = "Some default text.", Message = "Please be so kind as to enter some text.", Callback = OnInputBoxDialogCallback });
    }

    private void OnMessageBoxDialog1Execute() {
      messenger.Send(new MessageBoxDialogMessage { Header = "Message Box Test 1", Message = "Testing MessageBox Dialog with Cancel.", CancelText = "Cancel Boo!", HasCancel = true, Callback = OnMessageBoxDialog1Callback});
    }

    private void OnMessageBoxDialog2Execute() {
      messenger.Send(new MessageBoxDialogMessage { Header = "Message Box Test 2", Message = "Testing MessageBox Dialog without Cancel.", OkText = "Ok Yay!" });
    }

    #endregion Commands

    #region Private Methods

    private void OnInputBoxDialogCallback(InputBoxDialogMessage callbackMessage) {
      string message = $"Your Input: {callbackMessage.Input}\n\nYour Action: {(callbackMessage.IsCancel ? "Cancel!" : "Ok!")}";

      messenger.Send(new MessageBoxDialogMessage { Header = "InputBoxDialog Response", Message = message });
    }

    private void OnMessageBoxDialog1Callback(MessageBoxDialogMessage callbackMessage) {
      messenger.Send(new MessageBoxDialogMessage { Header = "MessageBoxDialog1 Response", Message = callbackMessage.IsCancel ? "You Canceled!" : "Ok! Good to go!" });
    }

    #endregion Private Methods

  }

}
