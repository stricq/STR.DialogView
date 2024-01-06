using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Str.Common.Extensions;
using Str.Common.Messages;

using Str.DialogView.Messages;
using Str.DialogView.ViewModels;
using Str.DialogView.Views;

using Str.MvvmCommon.Contracts;
using Str.MvvmCommon.Core;


namespace Str.DialogView.Controllers;


[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
public class ErrorDialogController(ErrorDialogViewModel viewModel, IMessenger messenger) : IController, IMessageReceiver {

  #region Private Fields

  private int index;

  private readonly ErrorDialogViewModel viewModel = viewModel;

  private readonly List<ApplicationErrorMessage> errors = [];

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
    messenger.Register<ApplicationErrorMessage>(this, true, OnApplicationErrorAsync);
  }

  private async Task OnApplicationErrorAsync(ApplicationErrorMessage message) {
    lock(errors) errors.Add(message);

    await TaskHelper.RunOnUiThreadAsync(async () => {
      RefreshErrors(); // This probably doesn't need to explicitly be on the ui thread...

      if (message.OpenErrorWindow) await messenger.SendAsync(new OpenDialogMessage { DialogViewType = typeof(ErrorDialogView), IsError = true }).Fire();
    }).Fire();
  }

  #endregion Messages

  #region Commands

  private void RegisterCommands() {
    viewModel.Clear = new RelayCommandAsync(OnClearExecuteAsync, CanClearExecute);

    viewModel.Previous = new RelayCommandAsync(OnPreviousExecuteAsync, CanPreviousExecute);

    viewModel.Next = new RelayCommandAsync(OnNextExecuteAsync, CanNextExecute);

    viewModel.Ok = new RelayCommandAsync(OnOkExecuteAsync);

    viewModel.ClearAll = new RelayCommandAsync(OnClearAllExecuteAsync, CanClearAllExecute);
  }

  #region Clear Command

  private Task OnClearExecuteAsync() {
    if (errors.Count == 0) return Task.CompletedTask;

    lock(errors) {
      errors.RemoveAt(index);

      if (index >= errors.Count) index = errors.Count - 1;

      if (errors.Count == 0) {
        index = 0;

        messenger.SendAsync(new CloseDialogMessage()).FireAndWait();
      }
      else RefreshErrors();
    }

    return Task.CompletedTask;
  }

  private bool CanClearExecute() {
    return errors.Count > 0;
  }

  #endregion Clear Command

  #region Previous Command

  private Task OnPreviousExecuteAsync() {
    if (errors.Count == 0) return Task.CompletedTask;

    --index;

    RefreshErrors();

    return Task.CompletedTask;
  }

  private bool CanPreviousExecute() {
    return errors.Count > 0 && index > 0;
  }

  #endregion Previous Command

  #region Next Command

  private Task OnNextExecuteAsync() {
    if (errors.Count == 0) return Task.CompletedTask;

    ++index;

    RefreshErrors();

    return Task.CompletedTask;
  }

  private bool CanNextExecute() {
    return errors.Count > 0 && index < (errors.Count - 1);
  }

  #endregion Next Command

  #region Ok Command

  private async Task OnOkExecuteAsync() {
    await messenger.SendAsync(new CloseDialogMessage()).Fire();
  }

  #endregion Ok Command

  #region ClearAll Command

  private Task OnClearAllExecuteAsync() {
    lock(errors) {
      errors.Clear();

      index = 0;

      messenger.SendAsync(new CloseDialogMessage()).FireAndWait();
    }

    return Task.CompletedTask;
  }

  private bool CanClearAllExecute() {
    return errors.Count > 0;
  }

  #endregion ClearAll Command

  #endregion Commands

  #region Private Methods

  [SuppressMessage("ReSharper", "RemoveRedundantBraces")]
  private void RefreshErrors() {
    lock(errors) {
      if (errors.Count > 0) {
        viewModel.Index       = $"Error {index + 1} of {errors.Count}";
        viewModel.HeaderText  = errors[index].HeaderText ?? "Application Error";

        if (errors[index].Exception != null) UnwindException(errors[index].Exception!);
        else {
          viewModel.MessageText = errors[index].ErrorMessage ?? String.Empty;
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

  private void UnwindException(Exception exception) {
    viewModel.MessageText = (String.IsNullOrEmpty(errors[index].ErrorMessage) ? exception.Message : errors[index].ErrorMessage) ?? String.Empty;
    viewModel.Visibility  = Visibility.Visible;

    StringBuilder stackTrace = new();

    Exception? ex = exception;

    do {
      if (ex != exception) stackTrace.Append("\n\n---------- Inner Exception ----------\n\n");

      stackTrace.Append($"{ex.Message}\n\n{ex.StackTrace}");

      ex = ex.InnerException;
    } while(ex != null);

    viewModel.StackTrace = stackTrace.ToString();
  }

  #endregion Private Methods

}
