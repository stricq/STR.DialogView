using System;
using System.Threading.Tasks;

using Str.Common.Messages;


namespace Str.DialogView.Messages {

  public class MessageBoxDialogMessage : MessageBase {

    public bool IsCancel { get; set; }

    public string Header { get; set; }

    public string Message { get; set; }

    public string OkText { get; set; }

    public string CancelText { get; set; }

    public bool HasCancel { get; set; }

    public Action<MessageBoxDialogMessage> Callback { get; set; }

    public Func<MessageBoxDialogMessage, Task> CallbackAsync { get; set; }

  }

  public class MessageBoxDialogMessage<T> : MessageBoxDialogMessage {

    public T State { get; set; }

  }

}
