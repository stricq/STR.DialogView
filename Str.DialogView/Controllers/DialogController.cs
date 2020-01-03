using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
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

  [Export(typeof(IController))]
  public class DialogController : IController {

    #region Private Fields

    private readonly DialogViewModel viewModel;

    private readonly Dictionary<IViewTagMetadata, IDialogViewLocator> allViews;

    private readonly IMessenger messenger;

    #endregion Private Fields

    #region Constructor

    [ImportingConstructor]
    public DialogController(DialogViewModel viewModel, IMessenger messenger, [ImportMany] IEnumerable<Lazy<IDialogViewLocator, IViewTagMetadata>> dialogViews) {
      this.viewModel = viewModel;

      this.messenger = messenger;

      allViews = dialogViews.ToDictionary(v => v.Metadata, v => v.Value);
    }

    #endregion Constructor

    #region IController Implementation

    public int InitializePriority { get; } = 100;

    public Task InitializeAsync() {
      viewModel.Visibility = Visibility.Collapsed;

      viewModel.DialogBorderColor = Brushes.BlueViolet;

      viewModel.DialogViews = new ObservableCollection<IDialogViewLocator>(allViews.Select(dv => dv.Value));

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
      IDialogViewModel model = allViews.Where(kvp => kvp.Key.Name == message.Name).Select(kvp => kvp.Value.DataContext as IDialogViewModel).FirstOrDefault();

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
