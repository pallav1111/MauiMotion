namespace MauiMotion.Animations.BuiltIn;

internal class ScaleInAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        // Start invisible and tiny
        view.Opacity = 0;
        view.Scale = 0;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // We run Fade and Scale together for a smooth effect
        await Task.WhenAll(
            view.FadeToAsync(1, (uint)durationMs, easing),
            view.ScaleToAsync(1, (uint)durationMs, easing)
        );
    }
}