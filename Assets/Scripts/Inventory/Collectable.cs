using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Inventory playerInventory;
    public string type;
    private readonly float riseSpeed = 3f;
    public float fadeSpeed = 1f;
    public float duration = 0.5f;
    private float timer;
    private bool isPickedUp = false;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    public Sprite icon;


    void Start()
    {
        icon = GetComponent<SpriteRenderer>().sprite;
        playerInventory = FindObjectOfType<Player>().inventory;

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        PickupAnimation();
    }

    void OnMouseOver()
    {

        PickupLogic();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPickedUp)
        {
            playerTransform = collision.transform;
            PickupLogic();
        }
    }


    private void PickupLogic()
    {
        if (playerInventory != null && isPickedUp == false)
        {

            isPickedUp = playerInventory.Add(this);
        }
    }

    void PickupAnimation()
    {
        if (isPickedUp)
        {

            // Increment the timer
            timer += Time.deltaTime;

            // Move the object up
            if (!playerTransform)
            {

                transform.Translate(riseSpeed * Time.deltaTime * Vector3.up);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, (riseSpeed + 2f) * Time.deltaTime);

            }

            // Fade out the sprite
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(1f, 0f, timer / duration);
                spriteRenderer.color = color;
            }

            // Destroy the object after the duration
            if (timer >= duration)
            {
                Destroy(gameObject);
            }
        }
    }
}


