using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private new Rigidbody2D rigidbody2D;
    private CircleCollider2D circleCollider2D;
    [SerializeField] private float initialSpeed = -5f;
    [SerializeField] private float bounceForce = 1.1f;
    private float verticalExtent; // half the height of the collider in world units
    private bool canBounceOffWall = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        verticalExtent = transform.localScale.y * circleCollider2D.radius / 2f;
    }

    private void Update()
    {
        if (!GameManager.Instance.start && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.velocity = new Vector2(initialSpeed, 0f);
            GameManager.Instance.start = true;
        }
    }

    private void LateUpdate()
    {
        Vector3 center = transform.position;
        Vector3 leftEdge = center - new Vector3(verticalExtent, 0f, 0f);
        Vector3 rightEdge = center + new Vector3(verticalExtent, 0f, 0f);

        float leftVP = Camera.main.WorldToViewportPoint(leftEdge).x;
        float rightVP = Camera.main.WorldToViewportPoint(rightEdge).x;

        if (leftVP <= 0f)
        {
            GameManager.Instance.ScorePointOpponent();
            GameManager.Instance.Reset();
            rigidbody2D.velocity = Vector2.zero;
            transform.position = Vector3.zero;
        }
        else if (rightVP >= 1f)
        {
            GameManager.Instance.ScorePointPlayer();
            GameManager.Instance.Reset();
            rigidbody2D.velocity = Vector2.zero;
            transform.position = Vector3.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collidedObject = other.gameObject;
        if (collidedObject.CompareTag("Player") || collidedObject.CompareTag("Opponent"))
        {
            ContactPoint2D contactPoint2D = other.GetContact(0);
            Vector2 normal = contactPoint2D.normal;

            if (normal != Vector2.down || normal != Vector2.up)
            {
                // Calculate hit factor (-1 at bottom, 1 at top)
                float hitFactor = (transform.position.y - collidedObject.transform.position.y) /
                          (collidedObject.transform.localScale.y / 2f);
                hitFactor = Mathf.Clamp(hitFactor, -1f, 1f);

                Vector2 paddleHitDirection = new(normal.x, -hitFactor);

                Vector2 newDirection = paddleHitDirection.normalized;

                float impulseStrength = rigidbody2D.velocity.magnitude;

                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.AddForce(bounceForce * impulseStrength * newDirection, ForceMode2D.Impulse);
            }
        }
        else if (collidedObject.CompareTag("Wall") && canBounceOffWall)
        {
            ContactPoint2D contact = other.GetContact(0);
            Vector2 normal = contact.normal;

            Vector2 reflectedVelocity = Vector2.Reflect(rigidbody2D.velocity, normal).normalized;
            float impulseStrength = rigidbody2D.velocity.magnitude;

            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(reflectedVelocity * impulseStrength, ForceMode2D.Impulse);

            StartCoroutine(WallBounceCooldown());
        }
    }

    private IEnumerator WallBounceCooldown()
    {
        canBounceOffWall = false;
        yield return new WaitForSeconds(0.1f); // adjust the time as needed
        canBounceOffWall = true;
    }
}
