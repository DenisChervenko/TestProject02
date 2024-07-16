using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    private Enemy _enemy;

    private void Start() => _enemy = GetComponentInParent<Enemy>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            _enemy.SetPlayer(other.transform, other.GetComponent<IDamagable>());
    }
}
