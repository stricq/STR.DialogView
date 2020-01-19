using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

using Str.DialogView.Contracts;


namespace Str.DialogView.Views {

  [SuppressMessage("ReSharper", "RedundantExtendsListEntry", Justification = "Resharper is wrong, the base class must be specified.")]
  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  public partial class ErrorDialogView : UserControl, IDialogViewLocator {

    #region Constructor

    public ErrorDialogView() {
      InitializeComponent();
    }

    #endregion Constructor

  }

}
