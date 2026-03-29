using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    public float moveSpeed;
    public Vector3 _moveDirection;

    public InputActionReference move;

    private void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>().normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
    }

}