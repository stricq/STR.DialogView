﻿using System.Diagnostics.CodeAnalysis;

using Str.Common.Messages;


namespace Str.DialogView.Messages;


[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "This is a library.")]
public class DialogVisibilityChangedMessage : MessageBase {

    public bool IsVisible { get; set; }

}
