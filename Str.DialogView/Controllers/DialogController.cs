using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

using Str.Common.Extensions;

using Str.DialogView.Contracts;
using Str.DialogView.Messages;
using Str.DialogView.ViewModels;

using Str.MvvmCommon.Contracts;


[assembly: XmlnsDefinition("http://schemas.stricq.com/dialogview", "Str.DialogView.Views")]

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]


namespace Str.DialogView.Controllers;


public class DialogController : IController, IMessageReceiver {

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

    public int InitializePriority => 100;

    public Task InitializeAsync() {
        viewModel.DialogViews.AddRange(dialogViews);

        RegisterMessages();

        return Task.CompletedTask;
    }

    #endregion IController Implementation

    #region Messages

    private void RegisterMessages() {
        messenger.Register<OpenDialogMessage>(this, OnOpenDialogAsync);

        messenger.Register<CloseDialogMessage>(this, OnCloseDialogAsync);
    }

    private async Task OnOpenDialogAsync(OpenDialogMessage message) {
        if (dialogViews.All(dv => dv.GetType() != message.DialogViewType)) return;

        IDialogViewModel model = (dialogViews.First(dv => dv.GetType() == message.DialogViewType).DataContext as IDialogViewModel)!;

        await messenger.SendAsync(new DialogVisibilityChangedMessage { IsVisible = true }).Fire();

        viewModel.DialogContent = model;

        viewModel.DialogBorderColor = message.IsError ? Brushes.Red : Brushes.BlueViolet;

        viewModel.Visibility = Visibility.Visible;
    }

    private async Task OnCloseDialogAsync(CloseDialogMessage message) {
        viewModel.Visibility = Visibility.Collapsed;

        viewModel.DialogContent = null;

        await messenger.SendAsync(new DialogVisibilityChangedMessage { IsVisible = false }).Fire();
    }

    #endregion Messages

}
