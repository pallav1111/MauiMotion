using MauiMotion.Core.Interfaces;

namespace MauiMotion.Animations;

public abstract class AnimationBase : IMotionAnimation
{
    public async Task AnimateAsync(VisualElement? view, int durationMs, int delayMs, Easing easing)
    {
        if (view is null) return;

        // Step 1: Prepare (Set initial state e.g., Opacity = 0)
        Prepare(view);

        // Step 2: Delay
        if (delayMs > 0)
        {
            await Task.Delay(delayMs);
            
            if (view.Handler == null || !view.IsLoaded) 
                return;
        }

        // Step 3: Execute
        await ExecuteAsync(view, durationMs, easing);
    }

    protected abstract void Prepare(VisualElement view);
    protected abstract Task ExecuteAsync(VisualElement view, int durationMs, Easing easing);
}