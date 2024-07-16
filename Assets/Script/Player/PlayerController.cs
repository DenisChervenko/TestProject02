using UnityEngine;

public class PlayerController : MonoBehaviour, IUpdateType
{
    [Header("Movement")]
    [SerializeField] private float _movementSpeed;
    private Rigidbody2D _rb;

    private void Start() => _rb = GetComponent<Rigidbody2D>();

    public void UpdateMethod()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(moveX, moveY);
        if(direction.magnitude > 1)
            direction.Normalize();

        _rb.velocity = direction * _movementSpeed * Time.fixedDeltaTime;
    }
}
