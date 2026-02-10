namespace MauiMotion.Animations.BuiltIn;

internal class SlideInBottomAnimation(double offset = 100) : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        // Start transparent and shifted down
        view.Opacity = 0;
        view.TranslationY = offset;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // Slide up to original position (0) and fade in
        await Task.WhenAll(
            view.FadeToAsync(1, (uint)durationMs, easing),
            view.TranslateToAsync(0, 0, (uint)durationMs, easing)
        );
    }
}