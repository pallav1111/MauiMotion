namespace MauiMotion.Animations.BuiltIn;

internal class FlipInXAnimation: AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        view.RotationX = -90; // Start flipped on X axis
        view.Opacity = 0;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        await Task.WhenAll(
            view.RotateXToAsync(0, (uint)durationMs, easing),
            view.FadeToAsync(1, (uint)durationMs, easing)
        );
    }
}