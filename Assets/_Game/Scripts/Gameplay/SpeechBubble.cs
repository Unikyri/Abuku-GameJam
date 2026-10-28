using System.Collections;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private TextMesh bubbleText;
    [SerializeField] private Killer killer;
    [SerializeField] private string message = "¿Qué? ¿Dónde estoy?\n¿Qué pasa?";
    [SerializeField] private string messageLoop = "Otra vez aquí...\nYa viví esto.";
    [SerializeField] private string messageLoop2 = "¿Qué está pasando....?";
    [SerializeField] private int lastLoopWithBubble = 2;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float letterTime = 0.05f;
    [SerializeField] private float timeVisible = 2f;

    void Start()
    {
        bubbleText.text = "";
        bubble.SetActive(false);

        if(LoopManager.loopCount > lastLoopWithBubble)
        {
             Invoke(nameof(HideBubble), waitTime);
        }else
        {
            Invoke(nameof(ShowBubble), waitTime);
        }

    }

    void ShowBubble()
    {
        if(LoopManager.loopCount == 1)
        {
            message = messageLoop;
        }else if(LoopManager.loopCount >= 2)
        {
            message = messageLoop2;
        }

        
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
