using System;
using VContainer.Diagnostics;


public static class ActionExtension
{
    public static void Register(this Action action, Action other, UnityEngine.GameObject unregisterOnDestroyGameObject=null)
    {
        if (action is null)
            action = other;
        else 
            action += other;

        if (unregisterOnDestroyGameObject is null)
            return;

        var unregister = unregisterOnDestroyGameObject.GetOrAddComponent<UnregisterOnDestroyComponent>();
        unregister.SetUnregisterAction(() => action -= other);
    }
    public static void Register<T>(this Action<T> action, Action<T> other, UnityEngine.GameObject unregisterOnDestroyGameObject=null)
    {
        if (action is null)
            action = other;
        else 
            action += other;

        if (unregisterOnDestroyGameObject is null)
            return;

        var unregister = unregisterOnDestroyGameObject.GetOrAddComponent<UnregisterOnDestroyComponent>();
        unregister.SetUnregisterAction(() => action -= other);
    }
    public static void Register<T1, T2>(this Action<T1, T2> action, Action<T1, T2> other, UnityEngine.GameObject unregisterOnDestroyGameObject=null)
    {
        if (action is null)
            action = other;
        else 
            action += other;

        if (unregisterOnDestroyGameObject is null)
            return;

        var unregister = unregisterOnDestroyGameObject.GetOrAddComponent<UnregisterOnDestroyComponent>();
        unregister.SetUnregisterAction(() => action -= other);
    }
    
}
