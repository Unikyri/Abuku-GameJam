using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;
    public static int loopCount = 0;
    [SerializeField] private float timeUntilKiller = 10f;
    [SerializeField] private TimeRewindAudio timeRewindAudio; //comunicación con controlador de audio
    private bool escaped;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        Debug.Log("Ciclos " + loopCount);
        Invoke(nameof(PlayerKilled), timeUntilKiller);
    }

    public void PlayerKilled()
    {
        Debug.Log("El asesino te atrapo");
        loopCount++;
       //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        timeRewindAudio.PlayRewind();
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
    void Update()
    {
       void Update()
{
    if (Time.frameCount % 60 == 0)
        Debug.Log("t=" + Time.timeSinceLevelLoad + "  timeScale=" + Time.timeScale + "  pendiente=" + IsInvoking(nameof(PlayerKilled)));
}
    }
}
