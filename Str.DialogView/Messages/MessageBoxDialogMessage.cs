using System;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Str.Common.Messages;


namespace Str.DialogView.Messages;


public class MessageBoxDialogMessage : MessageBase {

  public bool IsCancel { get; set; }

  public required string Header { get; init; }

  public required string Message { get; init; }

  public string? OkText { get; init; }

  public string? CancelText { get; init; }

  public bool HasCancel { get; init; }

  public Action<MessageBoxDialogMessage>? Callback { get; [UsedImplicitly] init; }

  public Func<MessageBoxDialogMessage, Task>? CallbackAsync { get; init; }

}


[UsedImplicitly]
public class MessageBoxDialogMessage<T> : MessageBoxDialogMessage {

  [UsedImplicitly]
  public required T State { get; init; }

}
