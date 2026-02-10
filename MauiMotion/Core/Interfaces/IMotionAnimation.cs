namespace MauiMotion.Core.Interfaces;

/// <summary>
/// The contract that all animation strategies must implement.
/// This allows us to create new animations without changing the API.
/// </summary>
public interface IMotionAnimation
{
    /// <summary>
    /// Executes the specific animation logic.
    /// </summary>
    /// <param name="view">The UI control to animate (Button, Label, CollectionView Item, etc.)</param>
    /// <param name="durationMs">How long the animation takes in milliseconds.</param>
    /// <param name="delayMs">How long to wait before starting.</param>
    /// <param name="easing">The acceleration curve (Linear, Bounce, Spring, etc.).</param>
    Task AnimateAsync(VisualElement? view, int durationMs, int delayMs, Easing easing);
}