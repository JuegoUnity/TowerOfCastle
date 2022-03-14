using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObkectPooler : MonoBehaviour
{
  [SerializeField] private GameObject prefab;
  [SerializeField] private int poolSize = 10;

  private List<GameObject> _pool;

  private void Awake()
   {
      _pool = new List<GameObject>();
      CreatePooler();
  }

    private void CreatePooler()
    {
        for (int i = 0; i < poolSize; i++)
        {
            _pool.Add(CreateInstance());
            
        }
    }
  private GameObject CreateInstance()
  {
      GameObject newInstance = Instantiate(prefab);
      newInstance.SetActive(false);
      return newInstance;
  }
}
