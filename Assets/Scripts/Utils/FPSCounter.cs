using UnityEngine;

public class FPSCounter : MonoBehaviour {
    private static FPSCounter instance;

    [SerializeField] private int updateRate = 1;
    private int frameCount;

    private float timer;
    private float nextUpdate;
    private int fps;

    void Awake() {
        nextUpdate = Time.time;
    }
    void OnDestroy() {
        if (instance == this)
            instance = null;
    }

    public static int FPS { get { Init(); return instance.fps; } }

    private static void Init() {
        if (instance == null)
            instance = new GameObject("FPSCounter").AddComponent<FPSCounter>();
    }

    void Update () {
        ++frameCount;

	    if (Time.time >= nextUpdate) {
	        nextUpdate = Time.time + 1.0f / updateRate;
	        fps = frameCount * updateRate;
	        frameCount = 0;

			MacroSystem.SetMacroValue("FPS_VALUE", fps);
	    }
	}
}
