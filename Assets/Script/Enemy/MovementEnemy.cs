using System.Collections;
using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    [Header("PatrolOption")]
    [Space()]
    [SerializeField] private float _patrolRange;
    [SerializeField] private float _timeBetweenPatrol;

    [Header("Chase")]
    [Space()]
    [SerializeField] private float _stopDistance;

    private bool _isNegativeMoveDirection = false;
    private float _speed;

    private Vector2 _basePosition;
    private Rigidbody2D _rb;
    private AnimationEnemy _animationEnemy;
    private Enemy _enemy;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>();
        _animationEnemy = _enemy.GetAnimationEnemy();
        _basePosition = transform.position;
        
        _speed = _enemy.Speed;
    }

    public void Patrol()
    {
        _animationEnemy.OnAnimationChange("Walk", _isNegativeMoveDirection);
        Vector2 direction = _isNegativeMoveDirection ? Vector2.right : Vector2.left;
        Vector2 newPosition = (Vector2)transform.position + direction * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);

        float distance =  ((Vector2)transform.position - _basePosition).sqrMagnitude;
        if(distance >= _patrolRange)
            Flip();
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = !_isNegativeMoveDirection ? -1 : 1;
        transform.localScale = scale;

        _isNegativeMoveDirection = !_isNegativeMoveDirection;
        _basePosition = transform.position;
        
        _enemy.PatrolPosibilityState(false);
        _animationEnemy.OnAnimationChange("Idle", _isNegativeMoveDirection);
        StartCoroutine(PatrolCooldown());
    }

    private IEnumerator PatrolCooldown()
    {
        yield return new WaitForSeconds(_timeBetweenPatrol);
       
        _enemy.PatrolPosibilityState(true);
        yield break;
    }

    public void ChasePlayer()
    {
        Transform playerTransform = _enemy.GetPlayerTrasnform();

        Vector2 chaseTarget = Vector2.MoveTowards(transform.position, playerTransform.position, _speed * 2 * Time.fixedDeltaTime);
        float distance = ((Vector2)transform.position - (Vector2)playerTransform.position).sqrMagnitude;

        Vector2 direction = chaseTarget - (Vector2)transform.position;
        transform.localScale = new Vector2(direction.x > 0 ? -1 : 1, 1);
        _isNegativeMoveDirection = direction.x > 0 ? true : false;
        
        if(distance < _stopDistance)
        {
            _animationEnemy.OnAnimationChange(_enemy.CanAttack ? "Punch" : "Idle", _isNegativeMoveDirection);
            _enemy.ChasePosibilityState(false);
        }
        else _enemy.ChasePosibilityState(true);

        if(_enemy.GetChaseState())
        {
            _animationEnemy.OnAnimationChange("Walk", _isNegativeMoveDirection);
            _rb.MovePosition(chaseTarget);
        }
    }
}
