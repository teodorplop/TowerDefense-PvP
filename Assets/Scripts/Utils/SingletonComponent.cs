using UnityEngine;

public class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour {
    protected static T _instance;

    public static T Instance {
        get {
        	if (ReferenceEquals(_instance, null)) {
				_instance = FindObjectOfType<T>();
                if (ReferenceEquals(_instance,  null)) {
                	var singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(singleton)" + typeof(T).ToString();
                }
			}
            return _instance;
        }
    }
    protected void Awake() {
        if (_instance == null) {
			_instance = gameObject.GetComponent<T> ();
		} else {
			Destroy (gameObject);
		}
    }
    protected void OnDestroy() {
        if (_instance == gameObject.GetComponent<T> ()) {
			_instance = null;
		}
    }
}