using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtagonistAnimation : MonoBehaviour
{
    [SerializeField] private Sprite idleRight;
    [SerializeField] private Sprite idleLeft;
    [SerializeField] private Sprite[] runFrames;
    [SerializeField] private Sprite death1;
    [SerializeField] private Sprite death2;
    [SerializeField] private float runFrameTime = 0.15f;
    [SerializeField] private float deathTime = 0.45f;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;
    private bool dead;
    private int runIndex;
    private float runTimer;
    private float lastX;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        lastX = transform.position.x;
        spriteRenderer.sprite = idleRight;
    }

    void Update()
    {
        if (dead)
        {
            return;
        }

        float x = transform.position.x;
        bool moving = movement != null && movement.IsMoving;

        if (moving && runFrames != null && runFrames.Length > 0)
        {
            if (x > lastX)
            {
                spriteRenderer.flipX = false;
            }
            else if (x < lastX)
            {
                spriteRenderer.flipX = true;
            }

            runTimer += Time.deltaTime;
            if (runTimer >= runFrameTime)
            {
                runTimer = 0f;
                runIndex++;
                if (runIndex >= runFrames.Length)
                {
                    runIndex = 0;
                }
            }

            spriteRenderer.sprite = runFrames[runIndex];
        }
        else
        {
            runIndex = 0;
            runTimer = 0f;
            spriteRenderer.flipX = false;

            if (Mouse.current != null && Camera.main != null)
            {
                float mouseX = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x;
                if (mouseX < x)
                {
                    spriteRenderer.sprite = idleLeft;
                }
                else
                {
                    spriteRenderer.sprite = idleRight;
                }
            }
            else
            {
                spriteRenderer.sprite = idleRight;
            }
        }

        lastX = x;
    }

    public void Die()
    {
        if (dead)
        {
            return;
        }

        dead = true;

        if (movement != null)
        {
            movement.Stop();
        }

        spriteRenderer.flipX = false;
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        spriteRenderer.sprite = death1;
        yield return new WaitForSeconds(deathTime);
        spriteRenderer.sprite = death2;
    }
}
