using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : Singleton<DamageTextManager>
{
    public ObjectPooler Pooler { get; set; } 

    
/// <summary>
/// Iniciamos nuestro Object Pooler
/// </summary>
    void Start()
    {
        Pooler = GetComponent<ObjectPooler>();
    }
    
}
