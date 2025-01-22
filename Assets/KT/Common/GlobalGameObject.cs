using UnityEngine;

namespace KT
{
    public class GlobalGameObject
    {
        private static GameObject _instance;

        public static GameObject Instance => _instance ?? Init();
        
        private static GameObject Init()
        {
            if (_instance is null)
            {
                _instance = new GameObject(nameof(GlobalGameObject));
                Object.DontDestroyOnLoad(_instance);
            }
            
            return _instance;
        }
    }
}