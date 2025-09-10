using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;

    private static bool isQuitting = false;



    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    if (isQuitting)
                    {
                        Debug.Log("���� �߿� ����");
                        return null;
                    }
                    GameObject go = new GameObject(typeof(T).ToString() + " (Singleton)");
                    _instance = go.AddComponent<T>();
                    
                    if (!Application.isBatchMode)
                    {
                        if (Application.isPlaying)
                            DontDestroyOnLoad(go);
                    }
                }
            }
            return _instance;
        }
    }

    public static bool IsCreatedInstance()
    {
        return (_instance != null);
    }

    public void Release()
    {
        if (IsCreatedInstance())
        {
            Destroy(_instance.gameObject);
            _instance = null;
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void Init()
    {
    }
}