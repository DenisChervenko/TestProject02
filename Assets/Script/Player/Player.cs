using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Player : MonoBehaviour, IDamagable
{
    [Header("BaseStat")]
    [SerializeField] private float _maxHealth;
    private float _health;

    [Header("Combat")]
    [Space()]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _playerDamage;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _layerMask;

    [Space()]
    [SerializeField] private Image _healthBar;

    [Inject] private EventHandler _eventHandler;

    private void Start() => _health = _maxHealth;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        HealthBar();

        if(_health <= 0)
            _eventHandler.onShowDieScreen?.Invoke();

        _eventHandler.onPlayerGetDamage?.Invoke();
    }

    private void HealthBar() => _healthBar.fillAmount = _health / _maxHealth;

    private void Combat()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _layerMask);
        foreach(Collider2D enemy in hitEnemies)
        {
            IDamagable damagable = enemy.GetComponent<IDamagable>();
            damagable.TakeDamage(_playerDamage);
        }
    }

    private void DisablePlayer() => gameObject.SetActive(false);

    private void OnEnable() 
    {
        _eventHandler.onDisablePlayer += DisablePlayer;
        _eventHandler.onPlayerAttack += Combat;
    } 
    private void OnDisable()
    {
        _eventHandler.onDisablePlayer -= DisablePlayer;
        _eventHandler.onPlayerAttack -= Combat;
    } 
}
