namespace KT
{
    public class GameApplicationConfig : UnityEngine.ScriptableObject
    {
        public YooAsset.EPlayMode PlayMode { get; set; } = YooAsset.EPlayMode.EditorSimulateMode;
        public string DbFileName { get; set; } = "default.db";
    }
}