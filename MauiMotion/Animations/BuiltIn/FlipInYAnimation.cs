namespace MauiMotion.Animations.BuiltIn;

internal class FlipInYAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        view.RotationY = -90; // Start flipped on Y axis
        view.Opacity = 0;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        await Task.WhenAll(
            view.RotateYToAsync(0, (uint)durationMs, easing),
            view.FadeToAsync(1, (uint)durationMs, easing)
        );
    }
}