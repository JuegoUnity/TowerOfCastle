using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint;
     private async void OnSceneGUI() 
     {
         //Elegimos el color para nuestro handle
         Handles.color = Color.red;

        for (int i = 0; i < Waypoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
        //Creamos los handles para nuestros Points
         Vector3 currentWaypointPoint = Waypoint.CurrentPostion + Waypoint.Points[i];   
         Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

         //Creamos los numeros que van referenciados a nuestros Points
         GUIStyle textStyle = new GUIStyle();
         textStyle.fontStyle = FontStyle.Bold;
         textStyle.fontSize = 16;
         textStyle.normal.textColor = Color.yellow;
         Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
         Handles.Label(Waypoint.CurrentPostion + Waypoint.Points[i] + textAlligment, $"{i + 1}", textStyle);

         EditorGUI.EndChangeCheck();
        //Gracias al EditorGUI y del if podemos mover nuestros Handles a donde queramos, sin necesidad de moverlos solo con las directrices XYZ.
         if(EditorGUI.EndChangeCheck()){

             Undo.RecordObject(target, "Free Move Handle");
             Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPostion;

         }

        }
    }
}
    
