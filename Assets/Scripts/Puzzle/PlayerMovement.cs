using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving;

    [SerializeField] private float moveSpeed = 5f;

    private Animator animator;

    private void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
       // animator.SetFloat("MoveX", 1);
        //animator.SetFloat("MoveY", 0);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePosition.z = 0;

            targetPosition = mousePosition;

            isMoving = true;
        }

        if (isMoving)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;

            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
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