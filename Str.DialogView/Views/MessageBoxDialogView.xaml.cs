using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

using Str.DialogView.Contracts;


namespace Str.DialogView.Views {

  [SuppressMessage("ReSharper", "RedundantExtendsListEntry", Justification = "It is required.")]
  public partial class MessageBoxDialogView : UserControl, IDialogViewLocator {

    public MessageBoxDialogView() {
      InitializeComponent();
    }

  }

}
