using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;

    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }

    private Image _healthBar;
    private Enemy _enemy;

/// <summary>
/// Inicializamos en la funcion Start nuestras funciones.
/// </summary>
    void Start()
    {
        CreateHealBar();
        CurrentHealth = initialHealth;
        _enemy = GetComponent<Enemy>();
    }

/// <summary>
/// Actualizamos la cantidad de vida que les queda a los enemigos del daño respectivo que les hace nuestras torretas.
/// </summary>
    private void Update() 
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }
/// <summary>
/// Creamos una funcion para poner una barra de vida en los enemigos
/// </summary>
    private void CreateHealBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }
/// <summary>
/// Funcion que nos devuelve el daño mientras el enemigo tenga mas vida que 0. En el momento que el enemigo llega a 0 invocamos a la funcion Die.
/// </summary>
    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            OnEnemyHit?.Invoke(_enemy);
        }
    }
/// <summary>
/// Restablecemos la vida de los enemigos
/// </summary>
    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        _healthBar.fillAmount = 1f;
    }
/// <summary>
/// Usamos la funcion Die para ademas de eliminar a los enemigos, cuando llegamos al numero x de kills poder recoger un logro.
/// </summary>
    private void Die()
    {
        AchievementManager.Instance.AddProgress("Kill20", 1);
        AchievementManager.Instance.AddProgress("Kill50", 1);
        AchievementManager.Instance.AddProgress("Kill100", 1);
        OnEnemyKilled?.Invoke(_enemy);
        
    }

}
