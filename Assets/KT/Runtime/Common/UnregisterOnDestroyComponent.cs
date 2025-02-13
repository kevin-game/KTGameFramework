using System;

public class UnregisterOnDestroyComponent : UnityEngine.MonoBehaviour
{
    private Action _action;

    public void SetUnregisterAction(Action action)
    {
        _action = action;
    }
        
    private void OnDestroy()
    {
        _action?.Invoke();
    }
}
