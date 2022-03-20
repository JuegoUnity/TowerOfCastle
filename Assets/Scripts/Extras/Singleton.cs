using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class DamageTextManager : Singleton<DamageTextManager>
public class Singleton<T> : MonoBehaviour where T : Component
{
/// <summary>
/// Creamos una clase Singleton que nos permita obtener herencia en nuestros scripts 
/// </summary>

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject newInstance = new GameObject();
                    instance = newInstance.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    public void Awake()
    {
        instance = this as T;
    }
}