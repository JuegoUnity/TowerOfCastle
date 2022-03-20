using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretProjectile : TurretProjectile
{
/// <summary>
/// Actualizamos para disparar los projectiles de nuestras torretas y añadiendo un pequeño delay a los disparos.
/// </summary>

    protected override void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0)
            {
                FireProjectile(_turret.CurrentEnemyTarget);
            }
            
            _nextAttackTime = Time.time + delayBtwAttacks;
        }
    }
    

    protected override void LoadProjectile()
    {
        
    }

/// <summary>
/// Hacemos que nuestra torreta pueda disparar cargando los disparos en el object pooler.
/// </summary>
    private void FireProjectile(Enemy enemy)
    {
        GameObject instance = _pooler.GetInstanceFromPool();
        instance.transform.position = projectileSpawnPosition.position;

        Projectile projectile = instance.GetComponent<Projectile>();
        _currentProjectileLoaded = projectile;
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.ResetProjectile();
        _currentProjectileLoaded.SetEnemy (enemy);
        _currentProjectileLoaded.Damage = Damage;
        instance.SetActive(true);
    }
}
