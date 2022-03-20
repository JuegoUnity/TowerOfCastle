using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;
    private EnemyHealth _enemyHealth;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }
/// <summary>
/// Hacemos que el enemigo cuando le hacemos un hit con un golpe tenga su animacion que con anterioridad le hemos creado.
/// </summary>
    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hurt");
    }
/// <summary>
/// Hacemos que el enemigo cuando lo matamos tenga su animacion de muerte.
/// </summary>
    private void PlayDieAnimation()
    {
        _animator.SetTrigger("Die");
    }
/// <summary>
/// 
/// </summary>
    private float GetCurrentAnimationLenght()
    {
        float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLenght;
    }

/// <summary>
/// Funcion que permite que el enemigo se pare un breve periodo de tiempo cuando le inflingimos daño
/// </summary>
    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
        _enemy.ResumeMovement();
    }
/// <summary>
/// Funcion que permite que el tenga su animacion de un breve periodo de tiempo cuando muere
/// </summary>
    private IEnumerator PlayDead()
    {
        _enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
        _enemy.ResumeMovement();
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(_enemy.gameObject);
        
    }
/// <summary>
/// Funcion que reanuda la animacion de herir en el siguiente frame
/// </summary>
    private void EnemyHit(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }
/// <summary>
/// Funcion que permite que el enemigo muera
/// </summary>

    private void EnemyDead(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayDead());
        }
    }
    private void OnEnable() 
    {
        EnemyHealth.OnEnemyHit += EnemyHit;
        EnemyHealth.OnEnemyKilled += EnemyDead;
    }

    private void OnDisable() 
    {
        EnemyHealth.OnEnemyHit -= EnemyHit;
        EnemyHealth.OnEnemyKilled -= EnemyDead;
    }
}
