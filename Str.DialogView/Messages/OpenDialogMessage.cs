using System;
using System.Diagnostics.CodeAnalysis;

using Str.Common.Messages;


namespace Str.DialogView.Messages {

  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "This is a library.")]
  public class OpenDialogMessage : MessageBase {

    public Type DialogType { get; set; }

    public bool IsError { get; set; }

  }

}
