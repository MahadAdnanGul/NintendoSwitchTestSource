using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingletonPersistent<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static bool Quitting { get; private set; }
		
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // Commented because using findobjectoftype in constructor causes an exception
					
                // instance = FindObjectOfType<T>();
                // if (instance == null)
                // {
                // 	var obj = new GameObject();
                // 	obj.name = typeof(T).Name;
                // 	obj.hideFlags = HideFlags.DontUnloadUnusedAsset;
                // 	instance = obj.AddComponent<T>();
                // }
					
                Debug.LogError("Instance not found");
            }

            return instance;
        }
    }
		
    #region  Methods
		
    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            // Debug.Log("DESTROYING = " + typeof(T).Name);
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        Quitting = true;
    }
    #endregion
}
