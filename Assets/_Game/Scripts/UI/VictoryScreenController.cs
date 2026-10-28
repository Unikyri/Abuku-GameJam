
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class VictoryScreenController : MonoBehaviour
{
    private VisualElement root;
    private Button restartButton;
    private Button mainMenuButton;
    [SerializeField] private AudioSource buttonAudio;
    void OnEnable()
    {
        // Obtenemos la interfaz de victoria.
        root = GetComponent<UIDocument>().rootVisualElement;

        // Buscamos los botones por sus nombres en el UXML.
        restartButton = root.Q<Button>("restart-button");
        mainMenuButton = root.Q<Button>("main-menu-button");

        // Conectamos las acciones de los botones.
        restartButton.clicked += RestartGame;
        mainMenuButton.clicked += ReturnToMenu;

        // Ocultamos la pantalla al comenzar.
        root.style.display = DisplayStyle.None;
    }

    void OnDisable()
    {
        // Evitamos dejar eventos registrados al desactivar el objeto.
        if (restartButton != null)
            restartButton.clicked -= RestartGame;

        if (mainMenuButton != null)
            mainMenuButton.clicked -= ReturnToMenu;
    }

    public void ShowVictory()
    {       
        root.style.display = DisplayStyle.Flex;       
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        // Reiniciamos el contador de bucles.
        LoopManager.loopCount = 0;

        // Reproduce el sonido y después reinicia Library.
        StartCoroutine(ChangeSceneWithSound("Library"));
    }

    private void ReturnToMenu()
    {
        // Reiniciamos el contador de bucles.
        LoopManager.loopCount = 0;

        // Reproduce el sonido y después vuelve al menú.
        StartCoroutine(ChangeSceneWithSound("UI_Test"));
    }

  
 
    private System.Collections.IEnumerator ChangeSceneWithSound(string sceneName)
    {
        if (buttonAudio != null)
        {
            buttonAudio.Play();
        }

        yield return new WaitForSecondsRealtime(0.2f);

        // Restauramos el tiempo antes de cargar otra escena.
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneName);
    }
}
