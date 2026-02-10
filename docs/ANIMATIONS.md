Version 1.0

MauiMotion Documentation
========================

**MauiMotion** is a declarative animation engine for .NET MAUI. It is designed to let you build complex motion UIs entirely in XAML without writing code-behind or managing `ViewExtensions` tasks manually.

* [Setup & Namespace](#setup)
* [Core Concepts (Properties)](#core-concepts)
* [Built-in Animations](#built-in)
* [Common Patterns (Waterfalls, Skeletons)](#patterns)
* [Custom Animations](#custom)

* * *

1\. Setup & Namespace
---------------------

To use MauiMotion, you must register the namespace in your XAML file. Register the service in `MauiProgram.cs`.

### Register the Service

Go to your `MauiProgram.cs` file and add `.UseMauiMotion()` to the builder.

    using MauiMotion; // Add this using
    
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiMotion() // <--- Add this line
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
    
        return builder.Build();
    }

### XAML Namespace

Add the `xmlns:mm` namespace to the root element of your page (e.g., `ContentPage` or `App.xaml`).

    <ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                 xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                 
                 xmlns:mm="http://mauimotion.com"
                 
                 x:Class="MyApp.MainPage">

2\. Core Concepts
-----------------

MauiMotion uses **Attached Properties** to add animation capabilities to standard MAUI controls (Grid, Border, Label, Image, etc).

### Basic Syntax

    <Label Text="Hello" 
           mm:Motion.Animation="FadeIn" 
           mm:Motion.Duration="500" />

### Available Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Animation` | Enum | `None` | The type of animation to run (e.g., FadeIn, SlideInTop). |
| `Duration` | Int | `500` | How long the animation takes in milliseconds. |
| `Delay` | Int | `0` | Time to wait before starting the animation. |
| `RepeatCount` | Int | `1` | How many times to run. Set to `-1` for infinite looping. |
| `Easing` | Easing | `Linear` | The acceleration curve (e.g., `SpringOut`, `CubicIn`). |

3\. Built-in Animations
-----------------------

### Entrance Animations

Best used when a page loads or an item appears.

* `FadeIn` / `FadeOut`
* `ScaleIn` / `ScaleOut`
* `ZoomIn` / `ZoomOut` (Scale with overshoot)
* `SlideInTop` / `SlideInBottom`
* `SlideInLeft` / `SlideInRight`

### Attention Seekers

Best used for user feedback, validation errors, or notifications.

* `Shake`: Horizontal vibration (Error state).
* `Pulse`: Heartbeat scaling effect.
* `Bounce`: Physical gravity bounce.
* `Wobble`: Rotational shake.

### 3D Transitions

* `FlipInX`: Rotates around the X-axis.
* `FlipInY`: Rotates around the Y-axis.

### Continuous Loops

Best used with `RepeatCount="-1"`.

* `Rotate`: Spins 360 degrees continuously.
* `Skeleton`: Pulsates opacity (0.3 to 1.0) for loading placeholders.

4\. Common Patterns
-------------------

### A. The "Waterfall" List (Staggered Entrance)

When displaying a list of items, you usually want them to animate in one by one. Use the **`EntranceBehavior`** inside your `DataTemplate`.

**How it works:** The behavior automatically calculates a delay based on the order the items are loaded. Setting `Stagger="100"` means the 1st item waits 0ms, the 2nd waits 100ms, the 3rd waits 200ms, etc.

    <CollectionView ItemsSource="{Binding Items}">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <Border Padding="10">
                
                    <Border.Behaviors>
                        <mm:EntranceBehavior Animation="SlideInBottom" 
                                             Duration="600" 
                                             Stagger="100" />
                    </Border.Behaviors>
    
                    <Label Text="{Binding Name}" />
                </Border>
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>

### B. Interactive Buttons

Trigger an animation when a user interacts with a control (e.g., clicking a button).

    <Button Text="Save Changes" 
            BackgroundColor="#2196F3">
        <Button.Behaviors>
            <mm:MotionBehavior EventName="Clicked" 
                               Animation="Pulse" 
                               Duration="200" />
        </Button.Behaviors>
    </Button>

### C. Skeleton Loading Screen

Create a placeholder UI while your data is loading from the API.

    <StackLayout Spacing="10">
        <!-- Title Skeleton -->
        <BoxView HeightRequest="20" WidthRequest="150" Color="#E0E0E0"
                 mm:Motion.Animation="Skeleton" 
                 mm:Motion.RepeatCount="-1" />
    
        <!-- Content Skeleton -->
        <BoxView HeightRequest="100" Color="#E0E0E0"
                 mm:Motion.Animation="Skeleton" 
                 mm:Motion.RepeatCount="-1" />
    </StackLayout>

5\. Custom Animations
---------------------

If the built-in animations aren't enough, you can create your own.

### Step 1: Define the Animation (C#)

Create a class that inherits from `AnimationBase`.

    using MauiMotion.Animations;
    
    public class BlurAnimation : AnimationBase
    {
        // 1. Prepare: Set initial state
        protected override void Prepare(VisualElement view)
        {
            view.Opacity = 0;
            view.Scale = 0.8;
        }
    
        // 2. Execute: Run the animation logic
        protected override async Task ExecuteAsync(VisualElement view, int durationMs, Easing easing)
        {
            await Task.WhenAll(
                view.FadeTo(1, (uint)durationMs, easing),
                view.ScaleTo(1, (uint)durationMs, easing)
            );
        }
    }

### Step 2: Use it in XAML

Define the animation as a resource and attach it using `Motion.CustomAnimation`.

    <ContentPage xmlns:local="clr-namespace:MyApp.Animations">
    
        <ContentPage.Resources>
            <local:BlurAnimation x:Key="MyBlurAnim" />
        </ContentPage.Resources>
    
        <Border mm:Motion.CustomAnimation="{StaticResource MyBlurAnim}"
                mm:Motion.Duration="800">
            <Label Text="Custom Blur!" />
        </Border>
    
    </ContentPage>




© 2026 MauiMotion. Licensed under MIT.