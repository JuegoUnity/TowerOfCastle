using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;
    public Spawner spawner;

    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }

    private int Contador = 0;

    private void Start() 
    {
        TotalLives = lives;
        CurrentWave = 1;
    }

    private void ReducesLives(Enemy enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }

    private void GameOver()
    {
        Contador = Contador == 0?1:2;//Este contador es un seguro para que esta parte del codigo solo se ejecute una vez.
        if (Contador == 1)
        {
        string con = "URI=file:" + Application.dataPath + "/DataBase/bd.db";
        IDbConnection conn = new SqliteConnection(con);
        conn.Open();
        IDbCommand comando = conn.CreateCommand();
        comando.CommandText = "insert into Historial values("+ CurrentWave+")";
        comando.ExecuteNonQuery();
        comando.Dispose();
        comando = null;

        conn.Dispose();
        conn = null;

        //spawner.generaEnemigos = false;
        }
        UIManager.Instance.ShowGameOverPanel();
        
        
    }



    private void WaveCompleted()
    {
        
        CurrentWave++;
        AchievementManager.Instance.AddProgress("Waves10", 1);
        AchievementManager.Instance.AddProgress("Waves20", 1);
        AchievementManager.Instance.AddProgress("Waves50", 1);
        AchievementManager.Instance.AddProgress("Waves100", 1);
    }

     private void OnEnable() 
     {
        Enemy.OnEndReached += ReducesLives;
        Spawner.OnWaveCompleted += WaveCompleted;
    }

    
    private void OnDisable() 
    {
        Debug.Log("Entro aqui");
        Enemy.OnEndReached -= ReducesLives;
        Spawner.OnWaveCompleted -= WaveCompleted;
    }
}
