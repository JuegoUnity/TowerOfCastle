using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPosition;
    private Enemy _enemy;

    private void Start() 
    {
        _enemy = GetComponent<Enemy>();
    }

/// <summary>
/// Cuando golpeamos a un enemigo hacemos que podamos ver el daño que le inflinjimos que hemos añadido a un spawn.
/// </summary>
    private void EnemyHit (Enemy enemy, float damage)
    {
        if (_enemy == enemy)
        {
            GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
            TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;
            damageText.text = damage.ToString();

            newInstance.transform.SetParent(textDamageSpawnPosition);
            newInstance.transform.position = textDamageSpawnPosition.position;
            newInstance.SetActive(true);
        }
    }
    private void OnEnable() 
    {
        Projectile.OnEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {
        Projectile.OnEnemyHit -= EnemyHit;
    }
}
