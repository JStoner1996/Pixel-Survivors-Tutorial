using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject destroyEffect;

    private Vector3 direction;

    // enemy stats
    [SerializeField] private int experienceWorth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float health;


    // push back helpers
    [SerializeField] private float pushTime;
    private float pushCounter;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerController.Instance.gameObject.activeSelf)
        {

            // Face the player
            if (PlayerController.Instance.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            // push back
            if (pushCounter > 0)
            {
                pushCounter -= Time.deltaTime;
                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed;
                }

                if (pushCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed);
                }
            }

            // Move towards player
            direction = (PlayerController.Instance.transform.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageNumberController.Instance.CreateNumber(damage, transform.position);
        pushCounter = pushTime;

        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
        Instantiate(destroyEffect, transform.position, transform.rotation);
        PlayerController.Instance.GetExperience(experienceWorth);
    }
}
