using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance;
    public static int loopCount = 0;
    [SerializeField] private float timeUntilKiller = 30f;
    
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Escape()
    {
        CancelInvoke();
        Debug.Log("ganaste, n intentos"+ loopCount);
        SceneManager.LoadScene("Victoria");
    }
}
