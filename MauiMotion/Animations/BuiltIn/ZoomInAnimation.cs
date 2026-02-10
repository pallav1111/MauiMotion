namespace MauiMotion.Animations.BuiltIn;

internal class ZoomInAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        view.Scale = 0.5; // Start slightly visible but small
        view.Opacity = 0;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // We use SpringOut to give it that "Zoom Pop" feel
        await Task.WhenAll(
            view.ScaleToAsync(1, (uint)durationMs, Easing.SpringOut),
            view.FadeToAsync(1, (uint)durationMs, Easing.CubicOut)
        );
    }
}