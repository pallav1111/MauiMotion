using MauiMotion.Core.Enums;
using MauiMotion.Core.Interfaces;
using MauiMotion.Engine;

namespace MauiMotion.API;

public static class Motion
    {
        // Internal Engine Instance
        private static readonly MotionFactory Factory = new();

        #region Attached Properties

        // 1. Animation Type
        public static readonly BindableProperty AnimationProperty =
            BindableProperty.CreateAttached("Animation", typeof(AnimationType), typeof(Motion), AnimationType.None, propertyChanged: OnAnimationChanged);

        // 2. Duration (ms)
        public static readonly BindableProperty DurationProperty =
            BindableProperty.CreateAttached("Duration", typeof(int), typeof(Motion), 500);

        // 3. Base Delay (ms)
        public static readonly BindableProperty DelayProperty =
            BindableProperty.CreateAttached("Delay", typeof(int), typeof(Motion), 0);

        // 4. Stagger (ms) - NEW!
        public static readonly BindableProperty StaggerProperty =
            BindableProperty.CreateAttached("Stagger", typeof(int), typeof(Motion), 0);

        // 5. Easing
        public static readonly BindableProperty EasingProperty =
            BindableProperty.CreateAttached("Easing", typeof(Easing), typeof(Motion), Easing.Linear);
        
        // 6. Custom
        public static readonly BindableProperty CustomAnimationProperty =
            BindableProperty.CreateAttached("CustomAnimation", typeof(IMotionAnimation), typeof(Motion), null);
        
        // 7. RepeatCount
        public static readonly BindableProperty RepeatCountProperty =
            BindableProperty.CreateAttached("RepeatCount", typeof(int), typeof(Motion), 1);

        // --- Getters and Setters ---
        public static AnimationType GetAnimation(BindableObject view) => (AnimationType)view.GetValue(AnimationProperty);
        public static void SetAnimation(BindableObject view, AnimationType value) => view.SetValue(AnimationProperty, value);

        public static int GetDuration(BindableObject view) => (int)view.GetValue(DurationProperty);
        public static void SetDuration(BindableObject view, int value) => view.SetValue(DurationProperty, value);

        public static int GetDelay(BindableObject view) => (int)view.GetValue(DelayProperty);
        public static void SetDelay(BindableObject view, int value) => view.SetValue(DelayProperty, value);

        public static int GetStagger(BindableObject view) => (int)view.GetValue(StaggerProperty);
        public static void SetStagger(BindableObject view, int value) => view.SetValue(StaggerProperty, value);

        public static Easing GetEasing(BindableObject view) => (Easing)view.GetValue(EasingProperty);
        public static void SetEasing(BindableObject view, Easing value) => view.SetValue(EasingProperty, value);

        public static IMotionAnimation? GetCustomAnimation(BindableObject view) => (IMotionAnimation)view.GetValue(CustomAnimationProperty);
        public static void SetCustomAnimation(BindableObject view, IMotionAnimation? value) => view.SetValue(CustomAnimationProperty, value);
        
        public static int GetRepeatCount(BindableObject view) => (int)view.GetValue(RepeatCountProperty);
        public static void SetRepeatCount(BindableObject view, int value) => view.SetValue(RepeatCountProperty, value);
        #endregion

        private static void OnAnimationChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not VisualElement element) return;
            
            var animationType = (AnimationType)newValue;
            
            element.Loaded -= OnElementLoaded;

            // Hook into the Loaded event to trigger animation when the view appears
            if (animationType != AnimationType.None)
            {
                element.Loaded += OnElementLoaded;
            }
        }
        
        private static async void OnElementLoaded(object? sender, EventArgs e)
        {
            if (sender is not VisualElement element) 
                return;
            
            // Double check property value in case it changed fast
            var type = GetAnimation(element); 
            if (type != AnimationType.None)
            {
                await RunAnimation(element, type);
            }
        }

        private static async Task RunAnimation(VisualElement element, AnimationType type)
        {
            // Get the animation strategy
            var animationStrategy = type == AnimationType.Custom 
                ? GetCustomAnimation(element) 
                : Factory.GetAnimation(type);
            
            if (animationStrategy is null) return;
            
            // Gather configurations
            var duration = GetDuration(element);
            var baseDelay = GetDelay(element);
            var stagger = GetStagger(element);
            var easing = GetEasing(element);
            var repeat = GetRepeatCount(element);

            // Calculate Final Delay using the StaggerManager
            var finalDelay = baseDelay;
            if (stagger > 0)
            {
                finalDelay += StaggerManager.GetNextDelay(stagger);
            }

            while (repeat is -1 or > 0)
            {
                // Safety: Stop if the view was removed from screen!
                if (!element.IsLoaded) break; 

                // Execute via Strategy
                await animationStrategy.AnimateAsync(element, duration, finalDelay, easing);

                // Decrement count if not infinite
                if (repeat > 0) repeat--;
        
                // Optional: Small pause between loops?
                // await Task.Delay(50); 
            }
        }
        
        /// <summary>
        /// Manually triggers an animation on a visual element (e.g., on button click).
        /// </summary>
        internal static async Task AnimateAsync(VisualElement view, AnimationType animationType, int durationMs = 500, int delayMs = 0)
        {
            // 1. Get the strategy from our existing factory
            var animation = Factory.GetAnimation(animationType);
    
            if (animation != null)
            {
                // 2. Execute it immediately
                await animation.AnimateAsync(view, durationMs, delayMs, Easing.Linear);
            }
        }
    }