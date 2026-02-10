using MauiMotion.Animations.BuiltIn;
using MauiMotion.Core.Enums;
using MauiMotion.Core.Interfaces;

namespace MauiMotion.Engine;

internal class MotionFactory : IMotionFactory
{
    private static readonly Dictionary<AnimationType, IMotionAnimation> Animations = new()
    {
        // --- Opacity ---
        { AnimationType.FadeIn, new FadeInAnimation() },
        { AnimationType.FadeOut, new FadeOutAnimation() },

        // --- Scaling ---
        { AnimationType.ScaleIn, new ScaleInAnimation() },
        { AnimationType.ScaleOut, new ScaleOutAnimation() },

        // --- Sliding ---
        { AnimationType.SlideInLeft, new SlideInLeftAnimation() },
        { AnimationType.SlideInRight, new SlideInRightAnimation() },
        { AnimationType.SlideInTop, new SlideInTopAnimation() },
        { AnimationType.SlideInBottom, new SlideInBottomAnimation() },

        // --- Attention ---
        { AnimationType.Shake, new ShakeAnimation() },

        // --- 3D ---
        { AnimationType.FlipInX, new FlipInXAnimation() },
        { AnimationType.FlipInY, new FlipInYAnimation() },
        
        // --- Zoom (Popups/Modals) ---
        { AnimationType.ZoomIn, new ZoomInAnimation() },
        { AnimationType.ZoomOut, new ZoomOutAnimation() },

        // --- Micro-Interactions ---
        { AnimationType.Pulse, new PulseAnimation() },
        { AnimationType.Bounce, new BounceAnimation() },
        
        { AnimationType.Rotate, new RotateAnimation() },
        { AnimationType.Wobble, new WobbleAnimation() },
        { AnimationType.Skeleton, new SkeletonAnimation() }
    };

    public IMotionAnimation? GetAnimation(AnimationType type)
    {
        return Animations.GetValueOrDefault(type); // Or a 'None' animation strategy
    }
}