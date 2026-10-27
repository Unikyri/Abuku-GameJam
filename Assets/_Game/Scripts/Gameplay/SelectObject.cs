using UnityEngine;
using UnityEngine.InputSystem;
public class SelectObject : MonoBehaviour
{
    [SerializeField] private Color hoverColor = new Color(1f, 0.9f, 0.5f, 1f);
    private bool isHovering; 
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        if(spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        IsClickPressed();
    }

    bool IsMouseOverCharacter()
    {
        if(Mouse.current == null || Camera.main == null)
        {
            return false; 
        }
        
        Collider2D collider = GetComponent<Collider2D>();

        if(collider == null)
        {
            return false;
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );

        return collider.OverlapPoint(mousePosition);
    }

    void IsClickPressed()
    {
        if(Mouse.current == null )
        {
            return;
        }

        bool over = IsMouseOverCharacter();

        if(over != isHovering)
        {
            isHovering = over;
            spriteRenderer.color = over ? hoverColor : originalColor;
        }

        if(over && Mouse.current.leftButton.wasPressedThisFrame)
        {
            SequenceManager.Instance.ObjectClicked(this);
        }
    }
}
