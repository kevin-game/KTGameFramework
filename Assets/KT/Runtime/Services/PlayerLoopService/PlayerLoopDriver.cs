using System;
using System.Collections.Generic;

namespace KT
{
    public class PlayerLoopDriver : UnityEngine.MonoBehaviour
    {
        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;

        public void Update()
        {
            OnUpdate?.Invoke();
        }

        public void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        public void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
    }
}