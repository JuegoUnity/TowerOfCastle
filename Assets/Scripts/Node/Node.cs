using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;
    public static Action OnTurretSold;

    [SerializeField] private GameObject attackRangeSprite;
    public Turret Turret { get; set; }

    private float _rangeSize;
    private Vector3 _rangeOriginalSize;

    private void Start() 
    {
        _rangeSize = attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        _rangeOriginalSize = attackRangeSprite.transform.localScale;
    }
/// <summary>
/// Creamos la funcion para settear las torretas
/// </summary>
    public void SetTurret(Turret turret)
    {
        Turret = turret;
    }
/// <summary>
/// Funcion que permite abrir el panel que muestra las torretas.
/// </summary>
    public bool IsEmpty()
    {
        return Turret == null;
    }
/// <summary>
/// Funcion que cuando cerramos el panel 
/// </summary>
    public void CloseAttackRangeSprite()
    {
        attackRangeSprite.SetActive(false);
    }
/// <summary>
/// Funcion que nos permite pinchar en las torretas y recibir su informacion.
/// </summary>
    public void SelectTurret()
    {
        OnNodeSelected?.Invoke(this);
        if (!IsEmpty())
        {
            ShowTurretInfo();
        }
    }
/// <summary>
/// Nos da la informacion para poder vender nuestra torreta y añada las monedas que cueste a nuestras monedas
/// </summary>
    public void SellTurret()
    {
        if (!IsEmpty())
        {
            CurrencySystem.Instance.AddCoins(Turret.TurretUpgrade.UpgradeCost);
            Destroy(Turret.gameObject);
            Turret = null;
            attackRangeSprite.SetActive(false);
            OnTurretSold?.Invoke();
        }
    }
/// <summary>
/// Nos muestra una informacion  que nos muestra el rango de ataque que puede tener la torreta.
/// </summary>
    private void ShowTurretInfo()
    {
        attackRangeSprite.SetActive(true);
        attackRangeSprite.transform.localScale = _rangeOriginalSize * Turret.AttackRange / (_rangeSize / 2);
    }
}
