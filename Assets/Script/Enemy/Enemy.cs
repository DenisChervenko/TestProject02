using UnityEngine;
using Zenject;

public abstract class Enemy : MonoBehaviour, IUpdateType, IDamagable
{
    [SerializeField] private EnemyInfo _enemyInfo;
    [SerializeField] private GameObject _dieEnemy;

    private bool _canPatrol = true;
    private bool _canChase = true;
    protected bool _canAttack = true;
    public bool CanAttack { get { return _canAttack; } }

    protected float _health;
    protected float _defend;
    protected float _damage;
    protected float _speed;

    public float Speed { get { return _speed; } }
    
    private Transform _playerTransform;
    
    protected IDamagable _playerDamagable;
    private AnimationEnemy _animationEnemy;
    private MovementEnemy _movementEnemy;

    [Inject] protected EventHandler _eventHandler;

    private void Awake() 
    {
        _animationEnemy = GetComponent<AnimationEnemy>();
        _movementEnemy = GetComponent<MovementEnemy>();

        _health = _enemyInfo.health;
        _defend = _enemyInfo.defend;
        _damage = _enemyInfo.damage;
        _speed = _enemyInfo.speed; 
    } 

    private void Start()
    {
        _eventHandler.onAddUpdateComponent?.Invoke(this);
        _eventHandler.onAddEnemy?.Invoke();
    }

    public void UpdateMethod()
    {
        if(_canPatrol)
            _movementEnemy.Patrol();
        
        if(_playerTransform != null)
            _movementEnemy.ChasePlayer();   
    }
    
    public void SetPlayer(Transform player, IDamagable damagable)
    {
        _canPatrol = false;
        _playerTransform = player;
        _playerDamagable = damagable;   
    }
    public void TakeDamage(float damage)
    {
        damage -= _defend;
        _health -= damage;
        
        if(_health <= 0)
        {
            Instantiate(_dieEnemy, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            _eventHandler.onEnemyDie?.Invoke();
        }
    }

    public abstract void Attack();

    public AnimationEnemy GetAnimationEnemy() => _animationEnemy;
    public Transform GetPlayerTrasnform() => _playerTransform;
    public bool GetChaseState() => _canChase;

    public void PatrolPosibilityState(bool state) => _canPatrol = state;
    public void ChasePosibilityState(bool state) => _canChase = state;

    private void OnEnable() => _eventHandler.onAddUpdateComponent?.Invoke(this);
    private void OnDisable() => _eventHandler.onRemoveUpdateComponent?.Invoke(this);
}
