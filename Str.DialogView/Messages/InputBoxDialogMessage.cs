using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Str.Common.Messages;


namespace Str.DialogView.Messages {

  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "This is a library.")]
  public class InputBoxDialogMessage : MessageBase {

    public bool IsCancel { get; set; }

    public string Header { get; set; }

    public string Message { get; set; }

    public string DefaultInput { get; set; }

    public string Input { get; set; }

    public string OkText { get; set; }

    public string CancelText { get; set; }

    public Action<InputBoxDialogMessage> Callback { get; set; }

    public Func<InputBoxDialogMessage, Task> CallbackAsync { get; set; }

  }


  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  public class InputBoxDialogMessage<T> : InputBoxDialogMessage {

    public T State { get; set; }

  }

}
