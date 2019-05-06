using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

using Str.DialogView.Constants;
using Str.DialogView.Contracts;

using Str.MvvmCommon.Core;


namespace Str.DialogView.Views {

  [Export(typeof(IDialogViewLocator))]
  [ViewTag(Name = DialogNames.InputBoxDialog)]
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry", Justification = "Resharper is wrong, the base class is needed.")]
  public partial class InputBoxView : UserControl, IDialogViewLocator {

    public InputBoxView() {
      InitializeComponent();
    }

  }

}
