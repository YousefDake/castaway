using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float dashTime = 0.2f;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;
    private float dashTimeLeft = 0f;
    private float lastImageXpos;
    private float lastImageYpos;
    public GameObject dustEffectPrefab; // Assign this in the Inspector

    public float distanceBetweenImages = 0.5f;
    public float initialDashCd = 2f;
    public float dashDistance = 1f;
    public float dashSpeed = 10f;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer sp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.magnitude != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if (movement.x < 0)
        {
            sp.flipX = true;
        }
        else if (movement.x > 0)
        {
            sp.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && Time.time >= lastDashTime + initialDashCd)
        {
            StartCoroutine(Dash());
        }
        CheckDash();
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator Dash()
    {
        dustEffectPrefab.transform.position = transform.position;
        StartCoroutine(PlayDustEffectAndDestroy(dustEffectPrefab));
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDashTime = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
        lastImageYpos = transform.position.y;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the necessary impulse force for the desired distance and time
        float requiredSpeed = dashDistance / dashTime; // distance / duration
        Vector2 impulse = direction * requiredSpeed * rb.mass; // Impulse = change in momentum (mass * change in velocity)

        // Apply the impulse force
        rb.AddForce(impulse, ForceMode2D.Impulse);

        // Wait for the duration of the dash
        yield return new WaitForSeconds(dashTime);

        // Optional: Stop the dash abruptly or let it slow down naturally
        rb.velocity = Vector2.zero; // Stop immediately for an abrupt stop

        isDashing = false;
    }


    //checkDash function

    private void CheckDash()
    {

        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                dashTimeLeft -= Time.deltaTime;
                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                    lastImageYpos = transform.position.y; // Store the Y position as well

                }
            }
        }
    }
    IEnumerator PlayDustEffectAndDestroy(GameObject dustEffect)
    {
        dustEffectPrefab.SetActive(true);

        Animator dustAnimator = dustEffect.GetComponent<Animator>();
        if (dustAnimator != null)
        {
            // Wait for the length of the animation clip
            yield return new WaitForSeconds(dustAnimator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            Debug.Log("HI");
        }

        dustEffectPrefab.SetActive(false);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing)
        {
            rb.velocity = Vector2.zero;
            isDashing = false;
        }
    }
}
