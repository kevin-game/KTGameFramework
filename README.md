# KTGameFramework
KT Unity Game Framework

# Quick Start
```csharp
public class HelloWorld
{
    // only "AfterSceneLoa" work, other will make GameApplication build twice, because of VContainer build order
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
        GameApplication.Build(builder =>
        {
            builder.Register<HelloWorldService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GamePresenter>();
        });
    }
}
```