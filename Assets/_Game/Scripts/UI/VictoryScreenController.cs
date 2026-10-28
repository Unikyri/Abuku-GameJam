using UnityEngine;
using UnityEngine.UIElements;

public class VictoryScreenController : MonoBehaviour
{
    private VisualElement root;

    void OnEnable()
    {
        // Obtenemos la interfaz del UI Document.
        root = GetComponent<UIDocument>().rootVisualElement;

        // La ocultamos cuando empieza el juego.
        root.style.display = DisplayStyle.None;
    }

    // LoopManager llamará a este método cuando el jugador escape.
    public void ShowVictory()
    {
        // Mostramos la pantalla de victoria.
        root.style.display = DisplayStyle.Flex;

        // Pausamos el juego para que el protagonista
        // y el asesino dejen de moverse.
        Time.timeScale = 0f;
    }

    void Update()
    {
        // PRUEBA TEMPORAL:
        // Detecta F8 utilizando el nuevo Input System.
        if (UnityEngine.InputSystem.Keyboard.current != null &&
            UnityEngine.InputSystem.Keyboard.current.f8Key.wasPressedThisFrame)
        {
            // Muestra la pantalla de victoria.
            ShowVictory();
        }
    }
}
