namespace MauiMotion.Animations.BuiltIn;

internal class ShakeAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        // Reset position just in case
        view.TranslationX = 0;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // A simple shake effect: Left -> Right -> Center
        var step = (uint)(durationMs / 4);
            
        await view.TranslateToAsync(-10, 0, step, Easing.Linear);
        await view.TranslateToAsync(10, 0, step, Easing.Linear);
        await view.TranslateToAsync(-5, 0, step, Easing.Linear);
        await view.TranslateToAsync(0, 0, step, Easing.SpringOut);
    }
}