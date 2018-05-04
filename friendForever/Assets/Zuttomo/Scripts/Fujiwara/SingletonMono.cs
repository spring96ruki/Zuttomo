using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {
	private static T instance;
	public static T Instance {
		get {
			if(instance == null) {
				System.Type t = typeof(T);
				instance = (T)FindObjectOfType(t);
			}
			return instance;
		}
	}
	virtual protected void Awake() {
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
		DontDestroyOnLoad (this.gameObject);
	}
}

public abstract class AnotherMono<T> : MonoBehaviour where T : MonoBehaviour{
	static T m_instance;
	protected static T instance {
		get {
			if (m_instance == null) {
				System.Type t = typeof(T);
				m_instance = FindObjectOfType<T> ();
				if (m_instance == null)
					m_instance = new GameObject (t.Name).AddComponent<T> ();
			}
			return m_instance;
		}
	}
	virtual protected void Awake() {
		if (this != instance) {
			Destroy (this.gameObject);
			return;
		}
		DontDestroyOnLoad (this.gameObject);
	}
}