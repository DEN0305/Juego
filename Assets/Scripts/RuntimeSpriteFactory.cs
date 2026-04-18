using UnityEngine;

public static class RuntimeSpriteFactory
{
    private static Sprite cachedSprite;

    public static Sprite GetDefaultSprite()
    {
        if (cachedSprite != null) return cachedSprite;

        Texture2D texture = new Texture2D(100, 100);
        Color[] colors = new Color[100 * 100];
        for (int i = 0; i < colors.Length; i++) colors[i] = Color.white;
        texture.SetPixels(colors);
        texture.Apply();
        texture.hideFlags = HideFlags.HideAndDontSave;

        cachedSprite = Sprite.Create(
            texture,
            new Rect(0f, 0f, 100, 100),
            new Vector2(0.5f, 0.5f),
            100f
        );

        cachedSprite.name = "RuntimeDefaultSprite";
        cachedSprite.hideFlags = HideFlags.HideAndDontSave;
        
        return cachedSprite;
    }
}
