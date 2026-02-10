using MauiMotion.Core.Interfaces;
using MauiMotion.Engine;

namespace MauiMotion;

public static class AppBuilderExtensions
{
    /// <summary>
    /// Initializes the MauiMotion library.
    /// Call this in your MauiProgram.cs: builder.UseMauiMotion();
    /// </summary>
    public static MauiAppBuilder UseMauiMotion(this MauiAppBuilder builder)
    {
        // 1. Register the MotionFactory (The Engine)
        // We register it as a Singleton so we can inject it if needed later, 
        // though our attached properties currently use a static instance.
        builder.Services.AddSingleton<IMotionFactory, MotionFactory>();

        // 2. Future: If we add specific Handlers or Effects later,
        // they would be registered here (e.g., builder.ConfigureMauiHandlers...)

        return builder;
    }
}