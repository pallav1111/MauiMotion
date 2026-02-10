namespace MauiMotion.Animations.BuiltIn;

internal class ScaleOutAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        view.Scale = 1;
        view.Opacity = 1;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // Shrink and Fade simultaneously
        await Task.WhenAll(
            view.ScaleToAsync(0, (uint)durationMs, easing),
            view.FadeToAsync(0, (uint)durationMs, easing)
        );
    }
}