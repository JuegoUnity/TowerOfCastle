using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{

    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    
    private TurretProjectile _turretProjectile;
    void Start()
    {
        _turretProjectile = GetComponent<TurretProjectile>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpgradeTurret();
        }
    }

    private void UpgradeTurret()
    {
        _turretProjectile.Damage += damageIncremental;
        _turretProjectile.DelayPerShot -= delayReduce;
    }

}
