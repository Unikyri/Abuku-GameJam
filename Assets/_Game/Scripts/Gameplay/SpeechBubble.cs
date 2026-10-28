using System.Collections;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private TextMesh bubbleText;
    [SerializeField] private Killer killer;
    [SerializeField] private string message = "¿Qué? ¿Dónde estoy?\n¿Qué pasa?";
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float letterTime = 0.05f;
    [SerializeField] private float timeVisible = 2f;

    void Start()
    {
        bubbleText.text = "";
        bubble.SetActive(false);
        Invoke(nameof(ShowBubble), waitTime);
    }

    void ShowBubble()
    {
        bubble.SetActive(true);
        StartCoroutine(WriteText());
    }

    IEnumerator WriteText()
    {
        bubbleText.text = "";

        for (int i = 0; i < message.Length; i++)
        {
            bubbleText.text += message[i];
            yield return new WaitForSeconds(letterTime);
        }

        Invoke(nameof(HideBubble), timeVisible);
    }

    void HideBubble()
    {
        bubble.SetActive(false);

        if (killer != null)
        {
            killer.Enter();
        }
    }
}
