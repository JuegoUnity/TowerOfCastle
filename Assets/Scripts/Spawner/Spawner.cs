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

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRamaining;

    private ObjectPooler _pooler;
    private Waypoint _waypoint;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
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
        GameObject newInstance = _pooler.GetInstanceFromPool();
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

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
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
