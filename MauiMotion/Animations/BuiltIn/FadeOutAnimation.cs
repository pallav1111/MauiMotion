namespace MauiMotion.Animations.BuiltIn;

internal class FadeOutAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        // Start fully visible
        view.Opacity = 1;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        await view.FadeToAsync(0, (uint)durationMs, easing);
    }
}