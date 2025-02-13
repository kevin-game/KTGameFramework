using System.Collections;
using UnityEngine;

namespace MG
{
    public interface ICoroutine
    {
        //TODO: use unity Coroutine temporarly. Create my own CoroutineMgr late
        public Coroutine StartCoroutine(IEnumerator corutinue);
        public void StopCoroutine(IEnumerator corutinue);
    }
}
