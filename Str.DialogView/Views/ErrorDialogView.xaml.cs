using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

using Str.DialogView.Constants;
using Str.DialogView.Contracts;

using Str.MvvmCommon.Core;


namespace Str.DialogView.Views {

  [Export(typeof(IDialogViewLocator))]
  [ViewTag(Name = DialogNames.ErrorDialog)]
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
