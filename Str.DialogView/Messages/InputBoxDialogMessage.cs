using System;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Str.Common.Messages;


namespace Str.DialogView.Messages;


public class InputBoxDialogMessage : MessageBase {

    public bool IsCancel { get; set; }

    public required string Header { get; init; }

    public required string Message { get; init; }

    public string DefaultInput { get; [UsedImplicitly] set; } = String.Empty;

    public string Input { get; set; } = String.Empty;

    public string? OkText { get; [UsedImplicitly] init; }

    public string? CancelText { get; [UsedImplicitly] init; }

    public Action<InputBoxDialogMessage>? Callback { get; [UsedImplicitly] init; }

    public Func<InputBoxDialogMessage, Task>? CallbackAsync { get; init; }

}


[UsedImplicitly]
public class InputBoxDialogMessage<T> : InputBoxDialogMessage {

    [UsedImplicitly]
    public required T State { get; init; }

}
