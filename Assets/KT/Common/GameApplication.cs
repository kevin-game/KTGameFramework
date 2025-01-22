using System;
using KT;
using VContainer;
using VContainer.Unity;

namespace KT
{
    public class GameApplication : LifetimeScope
    {
        private static Action<IContainerBuilder> _builderFunc;
        private static GameApplication _gameApp;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ILogService, LogService>(Lifetime.Singleton);
            builder.Register<IConfigService, ConfigService>(Lifetime.Singleton);
            builder.Register<ISaveService, SaveService>(Lifetime.Singleton);
            
            _builderFunc?.Invoke(builder);
        }
    
        public static GameApplication Build(Action<IContainerBuilder> builderFunc)
        {
            if (_gameApp is not null) return _gameApp;
            _builderFunc = builderFunc;

            _gameApp = GlobalGameObject.Instance.AddComponent<GameApplication>();
            return _gameApp;
        }
    }
}
