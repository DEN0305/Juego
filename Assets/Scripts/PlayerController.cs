using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Visuals (Opcional)")]
    [SerializeField] private Sprite playerSprite;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float jumpForce = 24f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector3 startPosition;
    private bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Forzar los valores (para ignorar los valores viejos trancados en la escena de Unity)
        moveSpeed = 8f; 
        jumpForce = 15f;

        startPosition = new Vector3(0, -6.5f, 0);
        transform.position = startPosition;
        transform.localScale = new Vector3(2f, 2f, 1f);
        rb.gravityScale = 2.2f;
        rb.freezeRotation = true;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = playerSprite != null ? playerSprite : RuntimeSpriteFactory.GetDefaultSprite();
            spriteRenderer.color = playerSprite != null ? Color.white : new Color(0.1f, 0.6f, 1f); 
        }
        
                // Limpiamos colliders viejos para evitar m�rgenes
        foreach (var c in GetComponents<Collider2D>()) Destroy(c);
        BoxCollider2D boxCol = gameObject.AddComponent<BoxCollider2D>();
        
        // Ajustamos la caja para ignorar espacios transparentes
        if (spriteRenderer != null && spriteRenderer.sprite != null) {
             boxCol.size = new Vector2(boxCol.size.x * 0.6f, boxCol.size.y * 0.8f);
             boxCol.offset = new Vector2(0f, boxCol.size.y * -0.1f); // Bajarlo un poco visualmente
        }
    }

    private void Update()
    {
        if (isDead) return;
        HandleJumpInput();
        CheckDeathBounds();
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        HandleHorizontalMove();
        UpdateGroundedState();
        FollowCameraY();
    }

    private void FollowCameraY()
    {
        float targetY = Mathf.Max(0f, transform.position.y + 2f);
        if (targetY > Camera.main.transform.position.y)
        {
            Vector3 camPos = Camera.main.transform.position;
            camPos.y = Mathf.Lerp(camPos.y, targetY, 0.1f);
            Camera.main.transform.position = camPos;
        }
    }

    private void HandleHorizontalMove()
    {
        float inputX = 0f;
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) inputX -= 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) inputX += 1f;
        }

        Gamepad gamepad = Gamepad.current;
        if (gamepad != null) inputX += gamepad.leftStick.x.ReadValue();

        rb.linearVelocity = new Vector2(inputX * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJumpInput()
    {
        Keyboard keyboard = Keyboard.current;
        bool jumpPressed = keyboard != null && keyboard.spaceKey.wasPressedThisFrame;

        Gamepad gamepad = Gamepad.current;
        if (!jumpPressed && gamepad != null) jumpPressed = gamepad.buttonSouth.wasPressedThisFrame;

        if (!jumpPressed || !isGrounded) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void UpdateGroundedState()
    {
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        if (bc == null) return;

        // Calculamos el fondo del collider actual de forma dinámica
        Vector2 bottomCenter = (Vector2)bc.bounds.center + Vector2.down * bc.bounds.extents.y;
        
        // Hacemos el check ligeramente por debajo del collider usando la capa de suelo (las plataformas)
        isGrounded = Physics2D.OverlapCircle(bottomCenter, 0.2f, groundLayer);
    }

    private void CheckDeathBounds()
    {
        // Se reinicia si cae abajo de la pantalla, o si se sale de la zona jugable
        if (transform.position.y < Camera.main.transform.position.y - 8f || Mathf.Abs(transform.position.x) > 10f)
        {
            // Reiniciar toda la escena completamente para evitar problemas de bugs al caer
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Campana")
        {
            isDead = true;
            rb.linearVelocity = Vector2.zero;
            
            AudioSource au = other.GetComponent<AudioSource>();
            if (au != null && au.clip != null) au.Play();

            if (GameManager.Instance != null) GameManager.Instance.LevelCompleted();
        }
    }
}

