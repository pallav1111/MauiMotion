Contributing to MauiMotion
==========================

First off, thank you for considering contributing to MauiMotion! It's people like you who make the .NET community great.

We welcome contributions of all forms: bug fixes, new animations, documentation improvements, or just feedback.

Getting Started
-------------------

### Prerequisites

* **Visual Studio 2022** (17.12 or later) or **JetBrains Rider**.
* **.NET 10 SDK**
* Workloads for MAUI (`maui-android`, `maui-ios`).

### Building the Project

1.  Clone the repository.

        git clone https://github.com/pallav1111/MauiMotion.git

2.  Open `MauiMotion.sln`.
3.  Set **MauiMotion.Sample** as the Startup Project.
4.  Build and Run on your simulator/emulator of choice.

How to Add a New Animation
-----------------------------

MauiMotion is designed to be extensible. To add a new animation type (e.g., `BounceIn`):

### Step 1: Create the Class

Create a new file in `MauiMotion/Animations/BuiltIn/BounceInAnimation.cs`. Inherit from `AnimationBase`.

    using MauiMotion.Animations;
    
    namespace MauiMotion.Animations.BuiltIn;
    
    public class BounceInAnimation : AnimationBase
    {
        // 1. PREPARE: Set the initial state (Before animation starts)
        protected override void Prepare(VisualElement view)
        {
            view.Scale = 0;
            view.Opacity = 0;
        }
    
        // 2. EXECUTE: Run the animation logic
        protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
        {
            // Use Task.WhenAll for parallel animations
            await Task.WhenAll(
                view.FadeTo(1, (uint)durationMs, easing),
                view.ScaleTo(1, (uint)durationMs, Easing.BounceOut) // Override easing if needed
            );
        }
    }

### Step 2: Register the Enum

Add your new type to the `AnimationType` enum in `MauiMotion.Core/Enums/AnimationType.cs`.

    public enum AnimationType
    {
        None = 0,
        // ... existing ...
        BounceIn, // <--- Add this
    }

### Step 3: Register in Factory

Map the enum to your class in `MauiMotion/Engine/MotionFactory.cs`.

    private static readonly Dictionary<AnimationType, IMotionAnimation> _animations = new()
    {
        // ... existing ...
        { AnimationType.BounceIn, new BounceInAnimation() },
    };

### Step 4: Test It

1.  Open `MauiMotion.Sample/ShowcasePage.xaml`.
2.  Add a temporary button or box to test your new animation.
3.  Run the sample app and verify it looks smooth.

Coding Standards
-------------------

* **Clean Code**: We follow standard C# coding conventions.
* **No "Magic Numbers"**: Try to avoid hardcoding duration values inside the animation class unless necessary. Use the `durationMs` parameter passed to `ExecuteAsync`.
* **Performance**: Avoid complex calculations or allocating large objects inside `ExecuteAsync`. This code runs on the UI thread during scrolling.
* **Null Safety**: Always use nullable reference types (`VisualElement? view`).

Reporting Bugs
-----------------

If you find a bug, please create an issue that includes:

1.  The version of MauiMotion you are using.
2.  The platform (Android/iOS/Mac) and OS version.
3.  A code snippet or reproduction steps.

License
----------

By contributing, you agree that your contributions will be licensed under its MIT License.