using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonComponent : MonoBehaviour
{
    private static SingletonComponent _instance;
    public static SingletonComponent Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<SingletonComponent>();
                if(_instance == null)
                {
                    var go = new GameObject("SingletonComponent");
                    _instance = go.AddComponent<SingletonComponent>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if(Instance != this)
        {
            Debug.Log($"SingletonComponent Duplicate: deleting {this.name}", this);
            DestroyImmediate(this.gameObject);
        } else
        {
            Debug.Log($"SingletonComponent: {this.name}", this);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
#endif
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            Debug.Log("SingletonComponent OnDestroy: set instance to null");
            _instance = null;
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
#endif  
    }

#if UNITY_EDITOR
    private void EditorApplication_playModeStateChanged(UnityEditor.PlayModeStateChange obj)
    {
        switch(obj)
        {
            case UnityEditor.PlayModeStateChange.ExitingEditMode:
                Debug.Log("SingletonComponent PlayModeStateChange ExitingEditMode: set instance to null");
                _instance = null;
                break;
        }
    }
#endif
}
