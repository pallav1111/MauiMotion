namespace MauiMotion.Animations.BuiltIn;

internal class BounceAnimation : AnimationBase
{
    protected override void Prepare(VisualElement view) { view.TranslationY = 0; }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // A physical jump: Up -> Down -> Settles
        var step = (uint)(durationMs / 3);
        await view.TranslateToAsync(0, -20, step, Easing.CubicOut); // Jump up
        await view.TranslateToAsync(0, 0, step, Easing.BounceOut);  // Fall down with bounce
    }
}