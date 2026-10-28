using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance;
    [SerializeField] private SelectObject[] sequence;
    private int index = 0; 

    void Awake()
    {
        Instance = this;
    }

    public void ObjectClicked(SelectObject obj)
    {
        if (obj == null)
        {
            return;
        }   

        if(obj == sequence[index])
        {
              Debug.Log("Correcto: " + obj.name + " (" + (index + 1) + "/" + sequence.Length + ")");
              index++;
              if(index >= sequence.Length)
              {
                LoopManager.Instance.Escape();
              }
        }else
        {
            Debug.Log("Incorrecto: " + obj.name + ", empieza de nuevo");
            index = 0;
        }
        Debug.Log("Interacción: " + obj.name);
    }
}
