using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Str.Common.Extensions;
using Str.Common.Messages;

using Str.DialogView.Constants;
using Str.DialogView.Messages;
using Str.DialogView.ViewModels;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Controllers {

  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  public class ErrorDialogViewController : IController {

    #region Private Fields

    private int index;

    private readonly ErrorDialogViewModel viewModel;

    private readonly List<ApplicationErrorMessage> errors;

    private readonly IMessenger messenger;

    #endregion Private Fields

    #region Constructor

    public ErrorDialogViewController(ErrorDialogViewModel viewModel, IMessenger messenger) {
      this.viewModel = viewModel;

      this.messenger = messenger;

      errors = new List<ApplicationErrorMessage>();
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
      messenger.Register<ApplicationErrorMessage>(this, OnApplicationError);
    }

    private void OnApplicationError(ApplicationErrorMessage message) {
      lock(errors) errors.Add(message);

      TaskHelper.RunOnUiThread(() => {
        RefreshErrors(); // This probably doesn't need to explicitly be on the ui thread...

        if (message.OpenErrorWindow) messenger.Send(new OpenDialogMessage { Name = DialogNames.ErrorDialog, IsError = true });
      }).FireAndForget();
    }

    #endregion Messages

    #region Commands

    private void RegisterCommands() {
      viewModel.Clear = new RelayCommand(OnClearExecute, CanClearExecute);

      viewModel.Previous = new RelayCommand(OnPreviousExecute, CanPreviousExecute);

      viewModel.Next = new RelayCommand(OnNextExecute, CanNextExecute);

      viewModel.Ok = new RelayCommand(OnOkExecute);

      viewModel.ClearAll = new RelayCommand(OnClearAllExecute, CanClearAllExecute);
    }

    #region Clear Command

    private void OnClearExecute() {
      if (errors.Count == 0) return;

      lock(errors) {
        errors.RemoveAt(index);

        if (index >= errors.Count) index = errors.Count - 1;

        if (errors.Count == 0) {
          index = 0;

          messenger.Send(new CloseDialogMessage());
        }
        else RefreshErrors();
      }
    }

    private bool CanClearExecute() {
      return errors.Count > 0;
    }

    #endregion Clear Command

    #region Previous Command

    private void OnPreviousExecute() {
      if (errors.Count == 0) return;

      --index;

      RefreshErrors();
    }

    private bool CanPreviousExecute() {
      return errors.Count > 0 && index > 0;
    }

    #endregion Previous Command

    #region Next Command

    private void OnNextExecute() {
      if (errors.Count == 0) return;

      ++index;

      RefreshErrors();
    }

    private bool CanNextExecute() {
      return errors.Count > 0 && index < (errors.Count - 1);
    }

    #endregion Next Command

    #region Ok Command

    private void OnOkExecute() {
      messenger.Send(new CloseDialogMessage());
    }

    #endregion Ok Command

    #region ClearAll Command

    private void OnClearAllExecute() {
      lock(errors) {
        errors.Clear();

        index = 0;

        messenger.Send(new CloseDialogMessage());
      }
    }

    private bool CanClearAllExecute() {
      return errors.Count > 0;
    }

    #endregion ClearAll Command

    #endregion Commands

    #region Private Methods

    private void RefreshErrors() {
      lock(errors) {
        if (errors.Count > 0) {
          viewModel.Index       = $"Error {index + 1} of {errors.Count}";
          viewModel.HeaderText  = errors[index].HeaderText ?? "Application Error";

          if (errors[index].Exception != null) {
            viewModel.MessageText = $"{errors[index].ErrorMessage}{(String.IsNullOrEmpty(errors[index].ErrorMessage) ? String.Empty : "\n\n")}{errors[index].Exception.Message}";
            viewModel.Visibility  = Visibility.Visible;

            StringBuilder str = new StringBuilder();

            Exception ex = errors[index].Exception;

            do {
              if (ex != errors[index].Exception) str.Append("\n\n---------- Inner Exception ----------\n\n");

              str.AppendFormat("{0}\n\n{1}", ex.Message, ex.StackTrace);

              ex = ex.InnerException;
            } while(ex != null);

            viewModel.StackTrace = str.ToString();
          }
          else {
            viewModel.MessageText = errors[index].ErrorMessage;
            viewModel.Visibility  = Visibility.Collapsed;
            viewModel.StackTrace  = String.Empty;
          }
        }
        else {
          viewModel.Index       = "No Errors";
          viewModel.HeaderText  = String.Empty;
          viewModel.MessageText = String.Empty;
          viewModel.Visibility  = Visibility.Collapsed;
          viewModel.StackTrace  = String.Empty;
        }
      }
    }

    #endregion Private Methods

  }

}
