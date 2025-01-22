namespace KT
{
    public class GameApplicationConfig : UnityEngine.ScriptableObject
    {
        public YooAsset.EPlayMode PlayMode { get; set; } = YooAsset.EPlayMode.EditorSimulateMode;
    }
}