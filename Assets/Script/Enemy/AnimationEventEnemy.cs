using UnityEngine;
using Zenject;

public class AnimationEventEnemy : MonoBehaviour
{
    private Enemy _enemy;
    private void Start() => _enemy = GetComponentInParent<Enemy>();
    public void AttackEnemy() => _enemy.Attack();
}
