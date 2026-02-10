namespace MauiMotion.Core.Enums;

public enum AnimationType
{
    None = 0,
        
    // Opacity
    FadeIn,
    FadeOut,
        
    // Scaling
    ScaleIn,
    ScaleOut,
        
    // Sliding (Translation)
    SlideInLeft,
    SlideInRight,
    SlideInTop,
    SlideInBottom,
    
    // Attention
    Shake,
        
    // 3D
    FlipInX,
    FlipInY,
    
    // Zoom (Popups/Modals)
    ZoomIn,
    ZoomOut,
    Pulse,
    Bounce,
    
    Rotate,
    Wobble,
    Skeleton,
    
    Custom
}