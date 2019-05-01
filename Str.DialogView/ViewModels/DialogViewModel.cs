using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;

using Str.DialogView.Contracts;

using Str.MvvmCommon.Core;


namespace Str.DialogView.ViewModels {

  [Export]
  [ViewModel("Str.DialogView.DialogViewModel")]
  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "This is a library.")]
  public class DialogViewModel : ObservableObject {

    #region Private Fields

    private ObservableCollection<IDialogViewLocator> dialogViews;

    private SolidColorBrush dialogBorderColor;

    private Visibility visibility;

    private IDialogViewModel dialogContent;

    #endregion Private Fields

    #region Properties

    public ObservableCollection<IDialogViewLocator> DialogViews {
      get => dialogViews;
      set => SetField(ref dialogViews, value, () => DialogViews);
    }

    public SolidColorBrush DialogBorderColor {
      get => dialogBorderColor;
      set => SetField(ref dialogBorderColor, value, () => DialogBorderColor);
    }

    public Visibility Visibility {
      get => visibility;
      set => SetField(ref visibility, value, () => Visibility);
    }

    public IDialogViewModel DialogContent {
      get => dialogContent;
      set => SetField(ref dialogContent, value, () => DialogContent);
    }

    #endregion Properties

  }

}
