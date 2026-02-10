using MauiMotion.Animations;

namespace MauiMotion.Sample.CustomAnimations;

public class Spin360Animation : AnimationBase
{
    protected override void Prepare(VisualElement view)
    {
        view.Rotation = 0;
        view.Opacity = 0;
    }

    protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
    {
        view.Opacity = 1;
        await view.RotateToAsync(360, (uint)durationMs, easing);
    }
}