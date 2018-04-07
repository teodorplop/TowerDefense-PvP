using System.Collections;
using UnityEngine;

public class CoroutineUtils : MonoBehaviour {}

public static class CoroutineStarter {
    private static CoroutineUtils coroutineUtils;
    private static CoroutineUtils CoroutineUtils {
        get {
            if (coroutineUtils == null) {
                coroutineUtils = new GameObject("CoroutineUtils").AddComponent<CoroutineUtils>();
                Object.DontDestroyOnLoad(coroutineUtils);
            }
            return coroutineUtils;
        }
    }

    public static Coroutine CoroutineStart(IEnumerator routine) {
        return CoroutineUtils.StartCoroutine(routine);
    }
}
