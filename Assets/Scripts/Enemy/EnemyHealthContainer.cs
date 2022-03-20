using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthContainer : MonoBehaviour
{
/// <summary>
/// Contenedores para la imagen de la vida de los enemigos
/// </summary>
    [SerializeField] private Image fillAmountImage;
    public Image FillAmountImage => fillAmountImage;
}
