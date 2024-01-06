using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;

using Str.DialogView.Contracts;

using Str.MvvmCommon.Core;


namespace Str.DialogView.ViewModels {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "This is a library.")]
  public class DialogViewModel : ObservableObject {

    #region Private Fields

    private ObservableCollection<IDialogViewLocator> dialogViews = new ObservableCollection<IDialogViewLocator>();

    private SolidColorBrush dialogBorderColor = Brushes.BlueViolet;

    private Visibility visibility = Visibility.Collapsed;

    private IDialogViewModel? dialogContent;

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

    public IDialogViewModel? DialogContent {
      get => dialogContent;
      set => SetField(ref dialogContent, value, () => DialogContent);
    }

    #endregion Properties

  }

}
