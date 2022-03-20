using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineProjectile : Projectile
{
    
    public Vector2 Direction { get; set; }
    
    protected override void Update()
    {
        MoveProjectile();
    }
/// <summary>
/// Añadimos velocidad al poryectil de nuestras torretas
/// </summary>
    protected override void MoveProjectile()
    {
        Vector2 movement = Direction.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
/// <summary>
/// Creamos una funcion que cuando el projectil entre en contacto con el collider del enemigo reciba su daño reste vida y se devuelva al nuestro object pool
/// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.EnemyHealth.CurrentHealth > 0f)
            {
                OnEnemyHit?.Invoke(enemy, Damage);
                enemy.EnemyHealth.DealDamage(Damage);
            }
            
            ObjectPooler.ReturnToPool(gameObject);
        }
    }

    private void OnEnable() 
    {
        StartCoroutine(ObjectPooler.ReturnToPoolWithDelay(gameObject, 5f));
    }

}

