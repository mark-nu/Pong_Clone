using UnityEngine;

public class OpponentMovement : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 velocity;
    private float inputAxis;
    private Vector3 startingPosition;
    private new Rigidbody2D rigidbody2D;
    private bool twoPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;

        if (GameManager.Instance.selectedGame != null)
        {
            if (GameManager.Instance.selectedGame.GameMode == Assets.Scripts.GameConfig.GameMode.ONE_PLAYER)
            {
                moveSpeed = (float)GameManager.Instance.selectedGame.GameDifficulty;
            }
            else
            {
                twoPlayer = true;
                moveSpeed = 20f;
            }
        }
    }

    private void Update()
    {
        if (twoPlayer)
        {
            inputAxis = Input.GetAxis("Vertical2");
            velocity.y = inputAxis * moveSpeed;
        }

        if (!GameManager.Instance.start)
        {
            rigidbody2D.velocity = Vector2.zero;
            transform.position = startingPosition;
        }
    }

    private void FixedUpdate()
    {
        if (!twoPlayer)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, ball.transform.position, moveSpeed * Time.fixedDeltaTime);
            rigidbody2D.MovePosition(newPos);
        }
        else
        {
            Vector2 position = transform.position;
            position += velocity * Time.fixedDeltaTime;
            rigidbody2D.MovePosition(position);
        }
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
