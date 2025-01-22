﻿namespace KT
{
    public class LogService : ILogService
    {
        public void Info(string message)
        {
            UnityEngine.Debug.Log(message);
        }
        
        public void Warning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }
        
        public void Error(string message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}