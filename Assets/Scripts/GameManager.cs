using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool levelComplete = false;
    private GUIStyle style;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public void LevelCompleted()
    {
        levelComplete = true;
    }

    private void OnGUI()
    {
        if (levelComplete)
        {
            if (style == null)
            {
                style = new GUIStyle(GUI.skin.label);
                style.fontSize = 80;
                style.alignment = TextAnchor.MiddleCenter;
                style.normal.textColor = Color.yellow;
            }
            
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "¡Nivel Completado!", style);
        }
    }

    
    public bool IsGameOver => levelComplete;
    public float GetCurrentPlatformSpeed() => 0f;
    public void PlayerDied() {}
}
