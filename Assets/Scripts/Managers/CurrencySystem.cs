using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : Singleton<CurrencySystem>
{
    [SerializeField] private int coinTest;
    private string CURRENCY_SAVE_KEY = "MYGAME_CURRENCY";
    public int TotalCoins { get; set; }

    private void Start() 
    {
        PlayerPrefs.DeleteKey(CURRENCY_SAVE_KEY);
        LoadCoins();
    }
/// <summary>
/// Cargamos las monedas a nuestro sistema de oro
/// </summary>
    private void LoadCoins()
    {
        TotalCoins = PlayerPrefs.GetInt(CURRENCY_SAVE_KEY, coinTest);
    }
/// <summary>
/// Añadimos monedas a nuestro total de monedas
/// </summary>
    public void AddCoins(int amount)
    {
        TotalCoins += amount;
        PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
        PlayerPrefs.Save();
    }
/// <summary>
/// eliminamos monedas de nuestro total
/// </summary>
    public void RemoveCoins(int amount)
    {
        if (TotalCoins >= amount)
        {
            TotalCoins -= amount;
            PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
            PlayerPrefs.Save();

        }
    }
/// <summary>
/// Añadimos una nueva moneda por cada enemigo que matamos
/// </summary>
    private void AddCoins(Enemy enemy)
    {
        AddCoins(1);
    }
    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += AddCoins;
    }

    private void OnDisable() 
    {
        EnemyHealth.OnEnemyKilled -= AddCoins;
    }
}
