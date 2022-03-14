using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField]private Vector3[] points;

    private Vector3 _currenPosition;
    private bool _gameStarted;
    // Start is called before the first frame update
    private void Start()
    {
        _gameStarted = true;
        _currenPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    //Creamos nuestros Gizmos para darle el pathing a nuestro juego. Añadimos ademas que podamos mover esos gizmos si necesidad de las variables XYZ.
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
