namespace MauiMotion.Animations.BuiltIn;

internal class SkeletonAnimation: AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        view.Opacity = 0.5; // Start dimmed
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // Smoothly pulses opacity to simulate loading
        var half = (uint)(durationMs / 2);
        await view.FadeToAsync(1.0, half, Easing.CubicIn);
        await view.FadeToAsync(0.5, half, Easing.CubicOut);
    }
}