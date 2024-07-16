using System.Collections;
using UnityEngine;

public class PunkEnemy : Enemy
{
    [SerializeField] private float _attackCooldown;
    public override void Attack()
    {
        _playerDamagable.TakeDamage(_damage);
        _canAttack = false;
        StartCoroutine(AttackAction());
    }

    private IEnumerator AttackAction()
    {
        yield return new WaitForSeconds(_attackCooldown);
        
        _canAttack = true;
        yield break;
    }
}
