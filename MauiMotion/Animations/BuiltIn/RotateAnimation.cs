namespace MauiMotion.Animations.BuiltIn;

internal class RotateAnimation: AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        // We ensure it is visible, but we don't force it to 0.
        // This allows the rotation to start from wherever it currently is.
        if (view.Opacity < 1) view.Opacity = 1;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // This logic supports BOTH one-time spins and infinite loops.
        // view.Rotation + 360 ensures the spin always moves forward.
        await view.RotateToAsync(view.Rotation + 360, (uint)durationMs, easing);
            
        // Clean up the value to stay within 0-360 range for stability
        view.Rotation %= 360;
    }
}