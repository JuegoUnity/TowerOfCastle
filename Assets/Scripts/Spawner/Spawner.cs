using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{

    public static Action OnWaveCompleted;
    //Creamos las variables para hacer los diferentes tipos random spawn de los enemigos.
    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;
    
    
    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;
    
    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header ("Poolers")]
    [SerializeField] private ObjectPooler enemyWave10Pooler;
    [SerializeField] private ObjectPooler enemyWave11To20Pooler;
    [SerializeField] private ObjectPooler enemyWave21To30Pooler;
    [SerializeField] private ObjectPooler enemyWave31To40Pooler;
    [SerializeField] private ObjectPooler enemyWave41To50Pooler;
    [SerializeField] private ObjectPooler enemyWave51To60Pooler;
    [SerializeField] private ObjectPooler enemyWave61To70Pooler;
    [SerializeField] private ObjectPooler enemyWave71To80Pooler;
    [SerializeField] private ObjectPooler enemyWave81To90Pooler;
    [SerializeField] private ObjectPooler enemyWave91To100Pooler;





    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRamaining;

   
    

    
    private Waypoint _waypoint;

    private void Start()
    {
        
        _waypoint = GetComponent<Waypoint>();
        _enemiesRamaining = enemyCount;

    }
    // Hacemos que Spawneen enemigos desde el contador. Hasta 10 enemigos. Agregando los segundos que nosotros queramos y aleatoriamente saldran los enemigos.
    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    


   


    //Creamos el metodo SpawnEnemy para poder instanciar nuestro testGO.
    private void SpawnEnemy()
    {
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
    }
     //Añadimos un metodo en el que podamos tener un Spawn respetando el contador hasta 10 pero eligiendo nosotros la velocidad a la que spawnearan los enemigos.
    private float GetSpawnDelay()
    {
        float delay = 0f;
        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }

        return delay;
    }
    

    //Creamos la funcion para que nos devuelva un tipo de randomTimer.
    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private ObjectPooler GetPooler()
    {
        int currentWave = LevelManager.Instance.CurrentWave;
        if (currentWave <= 10) // 1-10
        {
            return enemyWave10Pooler;  
        }

        if (currentWave > 10 && currentWave <= 20)// 11-20
        {
            return enemyWave11To20Pooler;
        }

        if (currentWave > 20 && currentWave <= 30)// 21-30
        {
            return enemyWave21To30Pooler;
        }

        if (currentWave > 30 && currentWave <= 40)// 31-40
        {
            return enemyWave31To40Pooler;
        }

        if (currentWave > 40 && currentWave <= 50)// 41-50
        {
            return enemyWave41To50Pooler;
        }

        if (currentWave >  51 && currentWave <= 60)// 51-60   
        {
            return enemyWave51To60Pooler;
        }

        if (currentWave > 61 && currentWave <= 70)// 61-70
        {
            return enemyWave61To70Pooler;
        }

        if (currentWave > 71 && currentWave <= 80)// 71-80
        {
            return enemyWave71To80Pooler;
        }

        if (currentWave > 81 && currentWave <= 90)// 81-90
        {
            return enemyWave81To90Pooler;
        }

        if (currentWave > 91 && currentWave <= 100)// 91-100
        {
            return enemyWave91To100Pooler;
        }

        return null;
    }

    
    

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        int currentWave = LevelManager.Instance.CurrentWave;
        enemyCount = currentWave+1;
        _enemiesRamaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }
    private void RecordEnemy(Enemy enemy)
    {
        _enemiesRamaining--;
        if (_enemiesRamaining <=0 )
        {
            
                OnWaveCompleted?.Invoke();
                StartCoroutine(NextWave());
        }
    }

    private void OnEnable() 
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;

    }

    private void OnDisable() 
    {
        Enemy.OnEndReached -= RecordEnemy;  
        EnemyHealth.OnEnemyKilled -= RecordEnemy;

    }
}
