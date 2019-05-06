using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

using Str.Common.Messages;

using Str.DialogView.Messages;
using Str.DialogView.Tests.ViewModels;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Tests.Controllers {

  [Export(typeof(IController))]
  public class DialogTestController : IController {

    #region Private Fields

    private readonly DialogTestViewModel viewModel;

    private readonly IMessenger messenger;

    #endregion Private Fields

    #region Constructor

    [ImportingConstructor]
    public DialogTestController(DialogTestViewModel viewModel, IMessenger messenger) {
      this.viewModel = viewModel;

      this.messenger = messenger;
    }

    #endregion Constructor

    #region IController Implementation

    public int InitializePriority { get; } = 1000;

    public async Task InitializeAsync() {
      RegisterCommands();

      await Task.CompletedTask;
    }

    #endregion IController Implementation

    #region Commands

    private void RegisterCommands() {
      viewModel.ErrorDialog = new RelayCommand(OnErrorDialogExecute);

      viewModel.InputBoxDialog = new RelayCommand(OnInputBoxDialogExecute);
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
      messenger.Send(new InputBoxDialogMessage { Header = "Input A String", DefaultInput = "Some default text.", Message = "Please be so kind as to enter some text." });
    }

    #endregion Commands

  }

}
