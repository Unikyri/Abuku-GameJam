using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance;
    [SerializeField] private float speed = 3f;
    private Vector2 target;
    private bool moving;
    private SelectObject pendingObject;
    [SerializeField] private float maxY = -0.79f; 
    
    void Awake()
    {
        Instance = this;
        target = transform.position; 
    }
    
    void Update()
    {
        if(Mouse.current != null && Camera.main != null && Mouse.current.leftButton.wasPressedThisFrame && !ClickOnObject())
        {
            pendingObject = null;
            MoveTo(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }

        if(!moving)
        {
            return;
        }   
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if((Vector2)transform.position == target)
        {
            moving =false;
            if(pendingObject != null)
            {
                SequenceManager.Instance.ObjectClicked(pendingObject);
                pendingObject = null;
            }
        }
    }

    public void MoveTo(Vector2 pos)
    {
        float y = Mathf.Min(pos.y,maxY);
        target = new Vector2(pos.x, y);
        moving = true;
    }

    public void GoToObject(SelectObject obj)
    {
        pendingObject = obj;
        MoveTo(obj.transform.position);
    }
    
    bool ClickOnObject()
    {
        Vector2 m = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D hit = Physics2D.OverlapPoint(m);
        return hit != null && hit.GetComponent<SelectObject>() != null;
    }
}
