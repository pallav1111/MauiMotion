namespace MauiMotion.Animations.BuiltIn;

internal class ZoomOutAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        // Ensure we start from a visible, full-scale state
        view.Scale = 1;
        view.Opacity = 1;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // Shrinks down to 50% size while fading out
        // We use CubicIn so it starts slow and accelerates as it disappears
        await Task.WhenAll(
            view.ScaleToAsync(0.5, (uint)durationMs, Easing.CubicIn),
            view.FadeToAsync(0, (uint)durationMs, Easing.CubicIn)
        );
    }
}