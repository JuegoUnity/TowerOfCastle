using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class db : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void saveWave()
    {
        string con = "URI=file:" + Application.dataPath + "/DataBase/bd.db";
        IDbConnection conn = new SqliteConnection(con);
        IDbCommand comando = conn.CreateCommand();
        comando.CommandText = "insert into Historial values(10)";
        comando.ExecuteNonQuery();
        comando.Dispose();
        conn = null;


        conn.Dispose();
        conn = null;
    }
}
