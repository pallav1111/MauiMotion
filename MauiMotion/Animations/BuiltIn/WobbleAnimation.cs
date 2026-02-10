namespace MauiMotion.Animations.BuiltIn;

internal class WobbleAnimation: AnimationBase
{
    protected override void Prepare(VisualElement view) { view.Rotation = 0; }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        // Tilt Left -> Tilt Right -> Center
        var step = (uint)(durationMs / 4);
            
        await view.RotateToAsync(-15, step, Easing.Linear);
        await view.RotateToAsync(15, step, Easing.Linear);
        await view.RotateToAsync(-10, step, Easing.Linear);
        await view.RotateToAsync(0, step, Easing.SpringOut);
    }
}