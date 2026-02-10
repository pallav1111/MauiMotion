namespace MauiMotion.Animations.BuiltIn;

internal class SlideInLeftAnimation(double offset = -100) : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        view.TranslationX = offset; // Start shifted Left
        view.Opacity = 0;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        await Task.WhenAll(
            view.TranslateToAsync(0, 0, (uint)durationMs, easing),
            view.FadeToAsync(1, (uint)durationMs, easing)
        );
    }
}