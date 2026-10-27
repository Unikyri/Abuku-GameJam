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
        if (sequence == null || sequence.Length == 0 || index >= sequence.Length)
        {
            return;
        }
        if(obj == sequence[index])
        {
            Debug.Log("Correcto: " + obj.name);
            index ++;
            if(index >= sequence.Length)
            {
                LoopManager.Instance.Escape();
            }
        }else
        {
            Debug.Log("Incorrecto, Empieza de nuevo");
            index = 0;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
