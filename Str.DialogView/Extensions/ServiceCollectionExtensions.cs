using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;

using Str.DialogView.Contracts;
using Str.DialogView.Controllers;
using Str.DialogView.ViewModels;
using Str.DialogView.Views;

using Str.MvvmCommon.Contracts;


namespace Str.DialogView.Extensions;


[SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
public static class ServiceCollectionExtensions {

    public static void AddStrDialogView(this IServiceCollection services) {

        services.AddSingleton<IController, DialogController>();
        services.AddSingleton<DialogViewModel>();

        services.AddSingleton<IDialogViewLocator, ErrorDialogView>();
        services.AddSingleton<IController, ErrorDialogController>();
        services.AddSingleton<ErrorDialogViewModel>();

        services.AddSingleton<IDialogViewLocator, InputBoxView>();
        services.AddSingleton<IController, InputBoxController>();
        services.AddSingleton<InputBoxViewModel>();

        services.AddSingleton<IDialogViewLocator, MessageBoxDialogView>();
        services.AddSingleton<IController, MessageBoxDialogController>();
        services.AddSingleton<MessageBoxDialogViewModel>();

    }

}
