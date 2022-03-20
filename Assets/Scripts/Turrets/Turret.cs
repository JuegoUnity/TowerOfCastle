using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
   [SerializeField] private float attackRange = 3f;

   private bool _gameStarted;
   private List<Enemy> _enemies;
   public Enemy CurrentEnemyTarget { get; set; }
   public TurretUpgrade TurretUpgrade { get; set; }
   public float AttackRange => attackRange;

   private void Start() 
   {
       _gameStarted = true;
       _enemies = new List<Enemy>();

       TurretUpgrade = GetComponent<TurretUpgrade>();
   }

   private void Update() 
   {
       GetCurrentEnemytarget();
       RotateTowardsTarget();
   }
/// <summary>
/// Obtiene el target del enemigo actual
/// </summary>
   private void GetCurrentEnemytarget()
   {
       if (_enemies.Count <= 0)
       {
           CurrentEnemyTarget = null;
           return;
       }

       CurrentEnemyTarget = _enemies[0];
   }
/// <summary>
/// Hacemos que cuando la torreta obtenga el target del enemigo rote con el en su mismo movimiento y angulo
/// </summary>
    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }

        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }
/// <summary>
/// Hacemos que cuando el collider del enemigo y la torreta entre en colision podamos empezar a dispararles
/// </summary>
    private void OnTriggerEnter2D(Collider2D other) 
    {
       if (other.CompareTag("Enemy"))
       {
            Enemy newEnemy = other.GetComponent<Enemy>();
            _enemies.Add(newEnemy);      
       }
   }
/// <summary>
/// Detecta cuando el enemigo sale del collider de nuestra torreta
/// </summary>
   private void OnTriggerExit2D(Collider2D other) 
   {
       if (other.CompareTag("Enemy"))
       {
           Enemy enemy = other.GetComponent<Enemy>();
           if (_enemies.Contains(enemy))
           {
               _enemies.Remove(enemy);
           }
       }
   }
/// <summary>
/// Dibujamos el trazo del area de ataque de nuestra torreta
/// </summary>
   private void OnDrawGizmos() 
   {

       if (!_gameStarted)
       {
           GetComponent<CircleCollider2D>().radius = attackRange;
       }
       Gizmos.DrawWireSphere(transform.position, attackRange);
   }

}
