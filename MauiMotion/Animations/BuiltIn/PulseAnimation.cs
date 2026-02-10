namespace MauiMotion.Animations.BuiltIn;

internal class PulseAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view) { /* No reset needed */ }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // Grows slightly and then shrinks back
        var halfWay = (uint)(durationMs / 2);
        await view.ScaleToAsync(1.1, halfWay, Easing.CubicIn);
        await view.ScaleToAsync(1.0, halfWay, Easing.CubicOut);
    }
}