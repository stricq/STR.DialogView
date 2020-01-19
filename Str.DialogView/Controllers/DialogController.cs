using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

using Str.DialogView.Contracts;
using Str.DialogView.Messages;
using Str.DialogView.ViewModels;

using Str.MvvmCommon.Contracts;


[assembly: XmlnsDefinition("http://schemas.stricq.com/dialogview", "Str.DialogView.Views")]


namespace Str.DialogView.Controllers {

  public class DialogController : IController {

    #region Private Fields

    private readonly DialogViewModel viewModel;

    private readonly IEnumerable<IDialogViewLocator> dialogViews;

    private readonly IMessenger messenger;

    #endregion Private Fields

    #region Constructor

    public DialogController(DialogViewModel viewModel, IMessenger messenger, IEnumerable<IDialogViewLocator> dialogViews) {
      this.viewModel = viewModel;

      this.messenger = messenger;

      this.dialogViews = dialogViews;
    }

    #endregion Constructor

    #region IController Implementation

    public int InitializePriority { get; } = 100;

    public Task InitializeAsync() {
      viewModel.Visibility = Visibility.Collapsed;

      viewModel.DialogBorderColor = Brushes.BlueViolet;

      viewModel.DialogViews = new ObservableCollection<IDialogViewLocator>(dialogViews);

      RegisterMessages();

      return Task.CompletedTask;
    }

    #endregion IController Implementation

    #region Messages

    private void RegisterMessages() {
      messenger.Register<OpenDialogMessage>(this, OnOpenDialog);

      messenger.Register<CloseDialogMessage>(this, OnCloseDialog);
    }

    private void OnOpenDialog(OpenDialogMessage message) {
      IDialogViewModel model = dialogViews.Where(dv => dv.GetType() == message.DialogViewType).Select(dv => dv.DataContext as IDialogViewModel).FirstOrDefault();

      if (model == null) return;

      messenger.Send(new DialogVisibilityChangedMessage { IsVisible = true });

      viewModel.DialogContent = model;

      viewModel.DialogBorderColor = message.IsError ? Brushes.Red : Brushes.BlueViolet;

      viewModel.Visibility = Visibility.Visible;
    }

    private void OnCloseDialog(CloseDialogMessage message) {
      viewModel.Visibility = Visibility.Collapsed;

      viewModel.DialogContent = null;

      messenger.Send(new DialogVisibilityChangedMessage { IsVisible = false });
    }

    #endregion Messages

  }

}
