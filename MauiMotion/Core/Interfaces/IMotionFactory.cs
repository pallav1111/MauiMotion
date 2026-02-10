using MauiMotion.Core.Enums;

namespace MauiMotion.Core.Interfaces;

internal interface IMotionFactory
{
    /// <summary>
    /// Retrieves the correct animation strategy based on the enum type.
    /// </summary>
    IMotionAnimation? GetAnimation(AnimationType type);
}