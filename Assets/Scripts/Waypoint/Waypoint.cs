using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField]private Vector3[] points;

    public Vector3[] Points => points;
    public Vector3 CurrentPostion => _currenPosition;

    private Vector3 _currenPosition;
    private bool _gameStarted;
    
    private void Start()
    {
        _gameStarted = true;
        _currenPosition = transform.position;
    }

    public Vector3 GetWaypointPosition(int index)
    {
        return CurrentPostion + Points[index];
    }


    //Creamos nuestros Gizmos para darle el pathing a nuestro juego.
    //Gracias al if podemos hacer que las agrupemos en conjunto y asi poder mover nuestro emptyObject desde Unity.
    private async void  OnDrawGizmos() 
    {
        if (!_gameStarted && transform.hasChanged){
            _currenPosition = transform.position;
        }
        for (int i = 0; i < points.Length; i++){
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i] + _currenPosition, 0.5f);

            if(i < points.Length - 1){

                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] +_currenPosition, points[i + 1] + _currenPosition);
            }
        }
        
    }
}
