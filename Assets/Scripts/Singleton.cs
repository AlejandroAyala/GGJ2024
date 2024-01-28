using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;


    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject g = new GameObject(typeof(T).Name);
                g.AddComponent<T>();
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if (!_instance)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}