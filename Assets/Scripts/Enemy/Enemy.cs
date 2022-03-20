using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
     public static Action<Enemy> OnEndReached;
     [SerializeField] private float moveSpeed = 3f;
     public float MoveSpeed { get; set; }

     public Waypoint Waypoint { get; set; }

     public EnemyHealth EnemyHealth { get; set; }
     

     public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

     private int _currentWaypointIndex;
     private Vector3 _lastPointPosition;
     private EnemyHealth _enemyHealth;
     private SpriteRenderer _spriteRenderer;

/// <summary>
/// Iniciamos nuestros objetos y les añadimos sus componentes
/// </summary>
     private void Start() 
     {
         _enemyHealth = GetComponent<EnemyHealth>();
         _spriteRenderer = GetComponent<SpriteRenderer>();
         EnemyHealth = GetComponent<EnemyHealth>();

         _currentWaypointIndex = 0;
         MoveSpeed = moveSpeed;
        _lastPointPosition = transform.position;
     }

/// <summary>
/// Actualizamos el movimiento y rotacion de los enemigos. Asi pudiendo hacer que se muevan por nuestros puntos de referencias del camino.
/// </summary>
     private void Update() 
     {
         Move();
         Rotate();

         if (CurrentPointPositionReached())
         {
             UpdateCurrentPointIndex();
         }
     }
/// <summary>
/// Creo la funcion para mover al enemigo
/// </summary>
     private void Move()
     {
        
        transform.position = Vector3.MoveTowards (transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
     }
/// <summary>
/// Funcion para que el enemigo se pare
/// </summary>
     public void StopMovement()
     {
        MoveSpeed = 0f;
     }
/// <summary>
/// Funcion para reanudar el movimiento del enemigo
/// </summary>
     public void ResumeMovement()
     {
         MoveSpeed = moveSpeed;
     }
/// <summary>
/// Funcion que se usa para poder hacer que los enemigos roten(solo a donde miran)
/// </summary>
     private void Rotate()
     {
         if (CurrentPointPosition.x > _lastPointPosition.x)
         {
             _spriteRenderer.flipX = false;
         }
         else
         {
             _spriteRenderer.flipX = true;
         }
     }

/// <summary>
/// Funcion para actualizar la posicion que llega el enemigo
/// </summary>
     private bool CurrentPointPositionReached()
     {
         float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
         if (distanceToNextPointPosition < 0.1f)
         {
             _lastPointPosition = transform.position;
             return true;
         }

         return false;
     }
/// <summary>
/// Actualizamos el punto que llega el enemigo
/// </summary>
     private void UpdateCurrentPointIndex()
     {
         int lastWaypointIndex = Waypoint.Points.Length - 1;
         if (_currentWaypointIndex < lastWaypointIndex)
         {
             _currentWaypointIndex++;
         }
         else
         {
            EndPointReached();
         }
     }
/// <summary>
/// Cuando el enemigo llega el punto final hacemos que vuelva a su objectpooler y retorne su vida para poder ser usado de nuevo
/// </summary>
      private void EndPointReached()
         {
             OnEndReached?.Invoke(this);
             _enemyHealth.ResetHealth();
             ObjectPooler.ReturnToPool(gameObject);
         }

/// <summary>
/// Volvemos a cargar al enemigo en el punto principal del mapa
/// </summary>
    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }


}
