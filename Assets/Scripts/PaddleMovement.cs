using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Vector2 velocity;
    private float inputAxis;
    [SerializeField] private float moveSpeed = 20f;
    private Vector3 startingPosition;
    private new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        inputAxis = Input.GetAxis("Vertical");
        velocity.y = inputAxis * moveSpeed;

        if (!GameManager.Instance.start)
        {
            velocity = Vector2.zero;
            transform.position = startingPosition;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position += velocity * Time.fixedDeltaTime;

        rigidbody2D.MovePosition(position);
    }

    private void LateUpdate()
    {
        Vector3 viewpointCoord = Camera.main.WorldToViewportPoint(transform.position);

        if (viewpointCoord.y < 0.1f)
        {
            viewpointCoord.y = 0.1f;
            transform.position = Camera.main.ViewportToWorldPoint(viewpointCoord);
        }
        else if (viewpointCoord.y > 0.9f)
        {
            viewpointCoord.y = 0.9f;
            transform.position = Camera.main.ViewportToWorldPoint(viewpointCoord);
        }
    }
}
