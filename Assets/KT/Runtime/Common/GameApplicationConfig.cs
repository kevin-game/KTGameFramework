using UnityEngine;
using UnityEngine.Serialization;

namespace KT
{
    [CreateAssetMenu(menuName = "KT/Create GameApplicationConfig", fileName = "GameApplicationConfig")]
    public class GameApplicationConfig : ScriptableObject
    {
        public YooAsset.EPlayMode playMode = YooAsset.EPlayMode.EditorSimulateMode;
        public string dbFileName = "default.db";
        public string cdnDefaultHost= "http://127.0.0.1";// 安卓模拟器地址 "http://10.0.2.2"
        public string cdnFallbackHost = "http://127.0.0.1";
        public string cdnAndroidDir = "CDN/Android/v1.0";
        public string cdnIPhoneDir = "CDN/IPhone/v1.0";
        public string cdnWebGLDir = "CDN/WebGL/v1.0";
        public string cdnPCDir = "CDN/PC/v1.0";
    }
}