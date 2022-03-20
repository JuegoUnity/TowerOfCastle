using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretUpgrade : MonoBehaviour
{

    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    [Header("Sell")]
    [Range(0,1)]
    [SerializeField] private float sellPert;

    public float SellPerc { get; set; }


    public int UpgradeCost { get; set; }
    public int Level { get; set; }

    
    private TurretProjectile _turretProjectile;
    void Start()
    {
        _turretProjectile = GetComponent<TurretProjectile>();
        UpgradeCost = upgradeInitialCost;
        SellPerc = sellPert;
        Level = 1;
        
    }


/// <summary>
/// Funcion que hace aumenta el daño y disminuye el delay de los disparos cuando mejoramos nuestras torretas
/// </summary>
    public void UpgradeTurret()
    {
        if (CurrencySystem.Instance.TotalCoins >= UpgradeCost)
        {
        _turretProjectile.Damage += damageIncremental;
        _turretProjectile.DelayPerShot -= delayReduce;
        UpdateUpgrade();
        }  
    }

/// <summary>
/// Funcion que nos devuelve el valor al que podemos vender la torreta
/// </summary>

    public int GetSellValue()
    {
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPerc);
        return sellValue;
    }

/// <summary>
/// funcion que nos permite upgradear la torreta por el coste de las monedas
/// </summary>

    private void UpdateUpgrade()
    {
        CurrencySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
        Level++;
    }

}
