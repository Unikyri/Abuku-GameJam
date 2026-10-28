using System.Collections;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField] private float enterSpeed = 5f;
    [SerializeField] private float stopX = 3.5f;
    [SerializeField] private float stopY = 0;
    [SerializeField] private float aimTime = 0.6f;
    [SerializeField] private float deathDelay = 0.6f;
    [SerializeField] private float waitBehindDoor = 1f;
    [SerializeField] private ProtagonistAnimation protagonist;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Door door;
    [SerializeField] private Sprite [] runKiller; 
    [SerializeField] private Sprite IdleKiller; 
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        muzzleFlash.SetActive(false);
    }

    public void Enter()
    {
        gameObject.SetActive(true);
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        if (door != null)
        {
            transform.position = new Vector2(door.transform.position.x, transform.position.y);
        }

        yield return new WaitForSeconds(waitBehindDoor);

        if (door != null)
        {
            door.Open();
        }

        while (transform.position.x > stopX)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(stopX, transform.position.y), enterSpeed * Time.deltaTime);
            
            spriteRenderer.sprite = runKiller[0];
            spriteRenderer.sprite = runKiller[1];
            
            yield return null;
        }

        yield return new WaitForSeconds(aimTime);

        //spriteRenderer.color = new Color(0.7f, 0.1f, 0.1f);
      
        spriteRenderer.sprite = IdleKiller;

        if (protagonist != null)
        {
            protagonist.Die();
        }

        yield return new WaitForSeconds(deathDelay);

        Time.timeScale = 0f;
    }
}
