using UnityEngine;

public static class GameObjectExtension
{
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T: MonoBehaviour
    {
        T c = gameObject.GetComponent<T>();
        if (c is null)
        {
            c = gameObject.AddComponent<T>();
        }

        return c;
    }
}
