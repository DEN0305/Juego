using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float fallbackSpeed = 2.5f;
    [SerializeField] private float destroyY = -8f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            col.usedByEffector = true;
            PlatformEffector2D effector = GetComponent<PlatformEffector2D>();
            if (effector == null)
            {
                effector = gameObject.AddComponent<PlatformEffector2D>();
            }
            effector.useOneWay = true;
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = RuntimeSpriteFactory.GetDefaultSprite();
        }
    }

    private void FixedUpdate()
    {
        float speed = fallbackSpeed;
        if (GameManager.Instance != null)
        {
            speed = GameManager.Instance.GetCurrentPlatformSpeed();
        }

        Vector2 nextPosition = rb.position + Vector2.down * speed * Time.fixedDeltaTime;
        rb.MovePosition(nextPosition);

        if (rb.position.y <= destroyY)
        {
            Destroy(gameObject);
        }
    }
}
