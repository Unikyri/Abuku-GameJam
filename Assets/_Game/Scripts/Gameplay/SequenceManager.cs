using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance;
    [SerializeField, HideInInspector] private SelectObject[] sequence;

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

        Debug.Log("Interacción: " + obj.name);
    }
}
