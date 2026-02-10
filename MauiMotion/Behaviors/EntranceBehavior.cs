using MauiMotion.API;
using MauiMotion.Core.Enums;
using MauiMotion.Engine;

namespace MauiMotion.Behaviors;

public class EntranceBehavior : Behavior<VisualElement>
{
    // Properties configurable in XAML
    public AnimationType Animation { get; set; } = AnimationType.FadeIn;
    public int Duration { get; set; } = 500;
    public int Stagger { get; set; } // The waterfall delay
    public bool OneTime { get; set; } = true;

    private VisualElement? _associatedObject;
    private bool _hasAnimated; // State tracker

    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);
        _associatedObject = bindable;
            
        // Hook into Loaded event (when the item appears)
        _associatedObject.Loaded += OnViewLoaded;
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {   
        if (_associatedObject != null)
        {
            _associatedObject.Loaded -= OnViewLoaded;
        }
        base.OnDetachingFrom(bindable);
        _associatedObject = null;
    }

    private async void OnViewLoaded(object? sender, EventArgs e)
    {
        if (_associatedObject == null || Animation == AnimationType.None) return;

        // 1. CHECK: If OneTime is true and we already animated, STOP.
        if (OneTime && _hasAnimated) 
        {
            // Ensure it's visible just in case it was stuck hidden
            _associatedObject.Opacity = 1; 
            return;
        }
        
        // 2. Hide immediately to prevent flash
        _associatedObject.Opacity = 0;
        
        // 3. Calculate Delay
        var finalDelay = 0;
        if (Stagger > 0)
        {
            finalDelay = StaggerManager.GetNextDelay(Stagger);
        }

        // 4. Mark as animated immediately so we don't double-fire
        _hasAnimated = true;

        // 5. Animate
        await Motion.AnimateAsync(_associatedObject, Animation, Duration, finalDelay);
    }
}