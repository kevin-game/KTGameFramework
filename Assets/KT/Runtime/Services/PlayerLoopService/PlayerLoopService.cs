using System;
using System.Collections.Generic;

namespace KT
{
    public class PlayerLoopService : IPlayerLoopService
    {
        private readonly PlayerLoopDriver _playerLoopDriver = GlobalGameObject.Instance.GetOrAddComponent<PlayerLoopDriver>();

        public void RegisterOnUpdate(Action action)
        {
            _playerLoopDriver.OnUpdate += action;
        }

        public void UnRegisterOnUpdate(Action action)
        {
            _playerLoopDriver.OnUpdate -= action;
        }

        public void RegisterOnLateUpdate(Action action)
        {
            _playerLoopDriver.OnLateUpdate += action;
        }

        public void UnRegisterOnLateUpdate(Action action)
        {
            _playerLoopDriver.OnLateUpdate -= action;
        }

        public void RegisterOnFixedUpdate(Action action)
        {
            _playerLoopDriver.OnFixedUpdate += action;
        }

        public void UnRegisterOnFixedUpdate(Action action)
        {
            _playerLoopDriver.OnFixedUpdate -= action;
        }
    }
}