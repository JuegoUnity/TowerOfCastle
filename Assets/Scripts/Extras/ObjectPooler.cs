using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> _pool;
    private GameObject _poolContainer;

/// <summary>
/// Cramos el metodo que añade la nueva instancia del prefab en la lista
/// </summary>
    private void Awake()
    {
        _pool = new List<GameObject>();
        _poolContainer = new GameObject($"Pool - {prefab.name}");
        
        CreatePooler();
    }
/// <summary>
/// Creamos nuestro object pool con un tamaño de poolsize de 10 que le hemos pasado por variable
/// </summary>
    private void CreatePooler()
    {
        for (int i = 0; i < poolSize; i++)
        {
            _pool.Add(CreateInstance());
        }
    }
/// <summary>
/// Cramos la funcion para recoger los prefabs y solo activarlos cuando la clase los necesite
/// </summary>
    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(prefab);
        newInstance.transform.SetParent(_poolContainer.transform);
        newInstance.SetActive(false);
        return newInstance;
    }
/// <summary>
/// Funcion que nos permite recoger la informacion de nuestra object pool
/// </summary>
    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }
        
        return CreateInstance();
    }
/// <summary>
/// Funcion que devuelve el objeto al object pool
/// </summary>
    public static void ReturnToPool(GameObject instance)
    {
       instance.SetActive(false); 
    }
/// <summary>
/// Funcion que nos permite esperar para que no cargue tanto al programa
/// </summary>
    public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
    }
}
