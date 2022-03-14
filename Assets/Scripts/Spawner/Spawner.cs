using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SpawnModes{

    Fixed,
    Random
}
public class Spawner : MonoBehaviour
{
    //Creamos las variables para hacer los diferentes tipos random spawn de los enemigos.
    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private GameObject testGO;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;    
    
    private float _spawnTimer;
    private int _enemiesSpawned;

    // Hacemos que Spawneen enemigos desde el contador. Hasta 10 enemigos. Agregando los segundos que nosotros queramos y aleatoriamente saldran los enemigos.
    void Update()
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
        Instantiate(testGO, transform.position, Quaternion.identity);
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
    //Creamos la funcion para que nos devuelva un tipo de randomTimer
    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }
}
