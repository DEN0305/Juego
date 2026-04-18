using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Sprites (Opcional - borra si usas los default)")]
    [SerializeField] private Sprite platformSprite;
    [SerializeField] private Sprite bellSprite;
    [SerializeField] private Sprite backgroundSprite;

    private void Start()
    {
        GameObject startPlat = GameObject.Find("StartPlatform");
        if (startPlat != null) Destroy(startPlat);

        GenerateParcial1Level();
    }

    private void GenerateParcial1Level()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10f);
        Camera.main.orthographicSize = 8f; 

        
        GameObject bg = new GameObject("CieloFondo");
        SpriteRenderer bgSr = bg.AddComponent<SpriteRenderer>();
        bgSr.sprite = backgroundSprite != null ? backgroundSprite : RuntimeSpriteFactory.GetDefaultSprite();
        bgSr.color = backgroundSprite != null ? Color.white : new Color(0.6f, 0.8f, 1f); 
        bgSr.sortingOrder = -100;
                if (backgroundSprite != null) {
            Vector2 spriteSize = bgSr.sprite.bounds.size;
            bg.transform.localScale = new Vector3(40f / spriteSize.x, 90f / spriteSize.y, 1f);
        } else {
            bg.transform.localScale = new Vector3(40f, 90f, 1f);
        } 
        bg.transform.position = new Vector3(0, 30f, 0); 

        
        CreatePlatform(new Vector2(0, -7.0f), 16f, false);

        float startY = -4.0f;
        float spacingY = 5f;
        float leftX = -3.5f;
        float rightX = 3.5f;

        float currentY = startY;
        float lastPlatformX = 0f;

        for (int i = 0; i < 8; i++)
        {
            float x = (i % 2 == 0) ? rightX : leftX;
            CreatePlatform(new Vector2(x, currentY), 3.5f, true);
            lastPlatformX = x;
            currentY += spacingY;
        }

        
        CreateBell(new Vector2(lastPlatformX, currentY - spacingY + 2f));
    }

    private void CreatePlatform(Vector2 pos, float width, bool useOneWay)
    {
        GameObject plat = new GameObject("Plataforma");
        plat.transform.position = pos;
        plat.transform.localScale = new Vector3(2.11f, 6.5f, 1f);
        plat.layer = 8; 

        SpriteRenderer sr = plat.AddComponent<SpriteRenderer>();
        sr.sprite = platformSprite != null ? platformSprite : RuntimeSpriteFactory.GetDefaultSprite();
        sr.color = platformSprite != null ? Color.white : new Color(0.2f, 0.7f, 0.2f);
        if (platformSprite == null) sr.drawMode = SpriteDrawMode.Simple; 

        BoxCollider2D bc = plat.AddComponent<BoxCollider2D>();
        bc.isTrigger = false;

    }

    private void CreateBell(Vector2 pos)
    {
        GameObject bell = new GameObject("Campana");
        bell.transform.position = pos;
        bell.transform.localScale = new Vector3(2.5f, 2.5f, 1f);

        SpriteRenderer sr = bell.AddComponent<SpriteRenderer>();
        sr.sprite = bellSprite != null ? bellSprite : RuntimeSpriteFactory.GetDefaultSprite();
        sr.color = bellSprite != null ? Color.white : new Color(1f, 0.9f, 0.1f);

        CircleCollider2D cc = bell.AddComponent<CircleCollider2D>();
        cc.isTrigger = true;

        bell.AddComponent<AudioSource>();
    }
}

