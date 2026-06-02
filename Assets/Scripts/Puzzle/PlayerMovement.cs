using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving;

    [SerializeField] private float moveSpeed = 5f;

    [Header("Movement Bounds")]
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    [Header("Collision")]
    [SerializeField] private LayerMask obstacleLayer;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(
                Input.mousePosition
            );

            mousePosition.z = 0;

            Vector3 clampedPosition = new(
                Mathf.Clamp(mousePosition.x, minBounds.x, maxBounds.x),
                Mathf.Clamp(mousePosition.y, minBounds.y, maxBounds.y),
                0
            );

            RaycastHit2D obstacle = Physics2D.Linecast(
                transform.position,
                clampedPosition,
                obstacleLayer
            );

            if (obstacle.collider == null)
            {
                targetPosition = clampedPosition;
                isMoving = true;
            }
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                isMoving = false;
            }
        }
    }
}