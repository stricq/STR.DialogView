using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Str.DialogView.Views;


[SuppressMessage("ReSharper", "RedundantExtendsListEntry", Justification = "Resharper is wrong, the base class must be specified.")]
[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
public partial class DialogView : UserControl {

  #region Constructor

  public DialogView() {
    InitializeComponent();
  }

  #endregion Constructor

  #region Dependency Properties

  #region Background Property

  public static readonly new DependencyProperty BackgroundProperty = DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(DialogView), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 227, 239, 255)), OnBackgroundPropertyChanged));

  private static void OnBackgroundPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
    if (sender is not DialogView dv || e.NewValue is not Brush brush) return;

    dv.InnerBorder.Background = brush;
  }

  public new Brush Background {
    get => (GetValue(BackgroundProperty) as Brush)!;
    set => SetValue(BackgroundProperty, value);
  }

  #endregion Background Property

  #region Foreground Property

  public static readonly new DependencyProperty ForegroundProperty = DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(DialogView), new PropertyMetadata(Brushes.Black, OnForegroundPropertyChanged));

  private static void OnForegroundPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
    if (sender is not DialogView dv || e.NewValue is not Brush brush) return;

    dv.ContentControl.Foreground = brush;
  }

  public new Brush Foreground {
    get => (GetValue(ForegroundProperty) as Brush)!;
    set => SetValue(ForegroundProperty, value);
  }

  #endregion Foreground Property

  #endregion Dependency Properties

}
