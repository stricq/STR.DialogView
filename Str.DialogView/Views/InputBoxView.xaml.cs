using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

using Str.DialogView.Contracts;


namespace Str.DialogView.Views {

  [SuppressMessage("ReSharper", "RedundantExtendsListEntry", Justification = "Resharper is wrong, the base class is needed.")]
  public partial class InputBoxView : UserControl, IDialogViewLocator {

    public InputBoxView() {
      InitializeComponent();
    }

  }

}
