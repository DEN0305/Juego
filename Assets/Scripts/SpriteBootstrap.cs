using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteBootstrap : MonoBehaviour
{
    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = RuntimeSpriteFactory.GetDefaultSprite();
        }
    }
}
