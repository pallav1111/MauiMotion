namespace MauiMotion.Animations.BuiltIn;

internal class FadeInAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        view.Opacity = 0;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        await view.FadeToAsync(1, (uint)durationMs, easing);
    }   
}