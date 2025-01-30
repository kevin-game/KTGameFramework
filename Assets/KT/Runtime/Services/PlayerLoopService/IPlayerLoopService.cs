namespace KT
{
    public interface IPlayerLoopService
    {
        public void RegisterOnUpdate(System.Action action);
        public void UnRegisterOnUpdate(System.Action action);
        
        public void RegisterOnLateUpdate(System.Action action);
        public void UnRegisterOnLateUpdate(System.Action action);
        
        public void RegisterOnFixedUpdate(System.Action action);
        public void UnRegisterOnFixedUpdate(System.Action action);
    }
}