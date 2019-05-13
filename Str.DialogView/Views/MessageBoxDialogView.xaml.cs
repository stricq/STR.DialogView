using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

using Str.DialogView.Constants;
using Str.DialogView.Contracts;

using Str.MvvmCommon.Core;


namespace Str.DialogView.Views {

  [Export(typeof(IDialogViewLocator))]
  [ViewTag(Name = DialogNames.MessageBoxDialog)]
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry", Justification = "It is required.")]
  public partial class MessageBoxDialogView : UserControl, IDialogViewLocator {

    public MessageBoxDialogView() {
      InitializeComponent();
    }

  }

}
