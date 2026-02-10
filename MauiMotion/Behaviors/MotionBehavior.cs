using System.Reflection;
using MauiMotion.API;
using MauiMotion.Core.Enums;

namespace MauiMotion.Behaviors;

/// <summary>
/// A behavior that triggers an animation when a specific event (e.g., Clicked) fires.
/// </summary>
public class MotionBehavior : Behavior<VisualElement>
{
    // 1. Configuration Properties
    public string EventName { get; set; } = "Clicked"; // Default to Clicked
    public AnimationType Animation { get; set; }
    public int Duration { get; set; } = 500;

    // 2. State Management
    private VisualElement? _associatedObject;
    private EventInfo? _eventInfo;
    private Delegate? _handler;

    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);
        _associatedObject = bindable;

        // Setup the Event Listener using Reflection (Universal approach)
        SetupEvent();
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        // Cleanup to prevent memory leaks
        if (_eventInfo != null && _handler != null)
        {
            _eventInfo.RemoveEventHandler(_associatedObject, _handler);
        }

        base.OnDetachingFrom(bindable);
        _associatedObject = null;
    }

    private void SetupEvent()
    {
        if (_associatedObject == null || string.IsNullOrEmpty(EventName)) return;

        // Find the event on the control (e.g., "Clicked" on Button)
        _eventInfo = _associatedObject.GetType().GetRuntimeEvent(EventName);

        if (_eventInfo != null)
        {
            // Create a dynamic handler that points to OnEventTriggered
            var methodInfo = typeof(MotionBehavior).GetTypeInfo().GetDeclaredMethod(nameof(OnEventTriggered));
            if (_eventInfo.EventHandlerType != null)
                _handler = methodInfo?.CreateDelegate(_eventInfo.EventHandlerType, this);
            _eventInfo.AddEventHandler(_associatedObject, _handler);
        }
    }

    // 3. The Trigger: When the event fires, we call the API
    private async void OnEventTriggered(object sender, EventArgs e)
    {
        if (Animation == AnimationType.None) return;

        // Reuse the same logic from our API (Hybrid consistency)
        if (_associatedObject != null)
            await Motion.AnimateAsync(_associatedObject, Animation, Duration);
    }
}