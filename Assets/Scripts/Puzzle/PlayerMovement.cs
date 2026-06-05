using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [Header("Movement Bounds")]
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    [Header("Collision")]
    [SerializeField] private LayerMask obstacleLayer;

    private Vector2 targetPosition;
    private bool isMoving;

    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        targetPosition = rb.position;
    }

    private void Update()
    {
        HandleClick();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleClick()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 clampedPosition = new(
            Mathf.Clamp(mousePosition.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(mousePosition.y, minBounds.y, maxBounds.y)
        );

        RaycastHit2D obstacle = Physics2D.Linecast(
            rb.position,
            clampedPosition,
            obstacleLayer
        );

        if (obstacle.collider != null)
        {
            return;
        }

        targetPosition = clampedPosition;
        isMoving = true;
    }

    private void MovePlayer()
    {
        if (!isMoving)
        {
            return;
        }

        Vector2 newPosition = Vector2.MoveTowards(
            rb.position,
            targetPosition,
            moveSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, targetPosition) < 0.05f)
        {
            rb.MovePosition(targetPosition);
            isMoving = false;
        }
    }

    private void UpdateAnimation()
    {
        animator.SetBool("IsMoving", isMoving);

        if (!isMoving)
        {
            return;
        }

        Vector2 direction = (targetPosition - rb.position).normalized;

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }
}