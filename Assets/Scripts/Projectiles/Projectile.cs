using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private Enemy _enemyTarget;

    private void Update() {
        if (_enemyTarget != null)
        {
            MoveProjectile();
            RotatePorjectile();
        }
    }
    private void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, moveSpeed * Time.deltaTime);
    }

    public void RotatePorjectile()
    {
        Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemyTarget = enemy;
    }
}
