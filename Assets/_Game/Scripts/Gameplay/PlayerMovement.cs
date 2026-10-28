using UnityEngine;

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
        if (!moving)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if ((Vector2)transform.position == target)
        {
            moving = false;
            if (pendingObject != null && SequenceManager.Instance != null)
            {
                SequenceManager.Instance.ObjectClicked(pendingObject);
            }
            pendingObject = null;
        }
    }

    public void MoveTo(Vector2 pos)
    {
        float y = Mathf.Min(pos.y, maxY);
        target = new Vector2(pos.x, y);
        moving = true;
    }

    public void GoToObject(SelectObject obj)
    {
        if (moving || obj == null)
        {
            return;
        }

        pendingObject = obj;
        MoveTo(obj.transform.position);
    }
}
