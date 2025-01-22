using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using KT;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class HelloWorldService
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
        GameApplication.Build(builder =>
        {
            builder.Register<HelloWorldService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GamePresenter>();
        });
    }
    
    public HelloWorldService()
    {
        UnityEngine.Debug.Log("Hello world service created");
    }
    public void Hello()
    {
        UnityEngine.Debug.Log("Hello world");
    }
}
