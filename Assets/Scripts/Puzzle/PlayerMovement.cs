using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving;

    [SerializeField] private float moveSpeed = 5f;

    private Animator animator;

    private Rigidbody2D rb; //cambie esto
    [Header("Movement Bounds")]
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    [Header("Collision")]
    [SerializeField] private LayerMask obstacleLayer;

    private void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); //cambie esto
    }

    private void Update()
    {
       // animator.SetFloat("MoveX", 1);
        //animator.SetFloat("MoveY", 0);

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
            Vector3 direction = (targetPosition - transform.position).normalized;

            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);

            /*transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );*/
            rb.MovePosition(
            Vector2.MoveTowards( //cambie esto
            rb.position,
            targetPosition,
            moveSpeed * Time.deltaTime
             )
            );

           if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
            isMoving = false;

            //animator.SetFloat("MoveX", 0);
           // animator.SetFloat("MoveY", 0);
           //si las descomento el personaje cuando dejo de caminar mira para el frente
            }
        }
    }
}