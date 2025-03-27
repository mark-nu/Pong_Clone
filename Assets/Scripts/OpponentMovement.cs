using UnityEngine;

public class OpponentMovement : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private float MoveSpeed = 5f;
    private Vector3 startingPosition;
    private new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;

        if (GameManager.Instance.selectedGame != null)
        {
            MoveSpeed = (float)GameManager.Instance.selectedGame.GameDifficulty;
        }

    }

    private void Update()
    {
        if (!GameManager.Instance.start)
        {
            rigidbody2D.velocity = Vector2.zero;
            transform.position = startingPosition;
        }
    }

    private void FixedUpdate()
    {
        Vector2 newPos = Vector2.MoveTowards(transform.position, ball.transform.position, MoveSpeed * Time.fixedDeltaTime);
        rigidbody2D.MovePosition(newPos);
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
