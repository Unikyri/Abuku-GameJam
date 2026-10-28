using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;
    public static int loopCount = 0;
    [SerializeField] private float timeUntilKiller = 10f;
    private bool escaped;
    public bool Escaped => escaped;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        Debug.Log("Ciclos " + loopCount);
        //Invoke(nameof(PlayerKilled), timeUntilKiller);
        Time.timeScale = 1f;
    }

    public void PlayerKilled()
    {
        Debug.Log("El asesino te atrapo");
        loopCount++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Escape()
    {
        CancelInvoke();
        escaped = true;
        Debug.Log("ganaste, n intentos"+ loopCount);
    }

    void OnGUI()
    {
        if (!escaped) return;
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 32 };
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "You survived!", style);
    }
    
}
