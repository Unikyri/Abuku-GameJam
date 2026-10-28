using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;
    public static int loopCount = 0;
    [SerializeField] private float timeUntilKiller = 10f;
    [SerializeField] private TimeRewindAudio timeRewindAudio; //comunicación con controlador de audio
    [SerializeField] private VictoryScreenController victoryScreen;
    private bool escaped;
    public bool Escaped => escaped;
    [SerializeField] private Killer killer;

    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        Debug.Log("Ciclos " + loopCount);
        Invoke(nameof(PlayerKilled), timeUntilKiller);
        Time.timeScale = 1f;
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
       if (killer != null)
      {
         Destroy(killer.gameObject);
      }

      
        Debug.Log("ganaste, n intentos"+ loopCount);

        victoryScreen.ShowVictory();
    }

    void OnGUI()
    {
        if (!escaped) return;
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 32 };
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "You survived!", style);
    }
    
}
