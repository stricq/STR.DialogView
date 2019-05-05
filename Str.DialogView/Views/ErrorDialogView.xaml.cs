using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

    #region Dependency Properties

    #region Foreground Property

    public new static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(DialogView), new PropertyMetadata(Brushes.Black, OnForegroundPropertyChanged));

    private static void OnForegroundPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
      if (!(sender is ErrorDialogView edv) || !(e.NewValue is Brush brush)) return;

      edv.HeaderTextBlock.Foreground = brush;
      edv.StackTextBlock.Foreground  = brush;
    }

    public new Brush Foreground {
      get => GetValue(ForegroundProperty) as Brush;
      set => SetValue(ForegroundProperty, value);
    }

    #endregion Foreground Property

    #endregion Dependency Properties

  }

}
