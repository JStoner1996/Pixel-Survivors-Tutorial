using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    public float moveSpeed;
    public Vector3 _moveDirection;

    public InputActionReference move;

    private void Update()
    {

        _moveDirection = move.action.ReadValue<Vector2>().normalized;
        animator.SetFloat("moveX", _moveDirection.x);
        animator.SetFloat("moveY", _moveDirection.y);

        if (_moveDirection == Vector3.zero)
        {
            animator.SetBool("moving", false);
        }
        else
        {
            animator.SetBool("moving", true);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
    }

}