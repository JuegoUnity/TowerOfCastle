using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
     public static Action OnEndReached;
     [SerializeField] private float moveSpeed = 3f;

     public Waypoint Waypoint { get; set; }
     

     public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

     private int _currentWaypointIndex;
     private EnemyHealth _enemyHealth;

     private void Start() 
     {
         _currentWaypointIndex = 0;
         _enemyHealth = GetComponent<EnemyHealth>();
     }

     private void Update() 
     {
         Move();
         if (CurrentPointPositionReached())
         {
             UpdateCurrentPointIndex();
         }
     }

     private void Move()
     {
        
        transform.position = Vector3.MoveTowards (transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime);
     }

     private bool CurrentPointPositionReached()
     {
         float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
         if (distanceToNextPointPosition < 0.1f)
         {
             return true;
         }

         return false;
     }

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
      private void EndPointReached()
         {
             OnEndReached?.Invoke();
             _enemyHealth.ResetHealth();
             ObjectPooler.ReturnToPool(gameObject);
         }


    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }


}
