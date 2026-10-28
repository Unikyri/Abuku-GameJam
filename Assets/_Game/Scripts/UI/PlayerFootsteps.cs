using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AudioSource footstepsAudio;
    


    void Start()
    {
        
    }


    void Update()
    {
        if (playerMovement.IsMoving && Time.timeScale > 0f)
        {
            if (!footstepsAudio.isPlaying)
            {
                footstepsAudio.Play();
            }
        }
        else
        {
            if (footstepsAudio.isPlaying)
            {
                footstepsAudio.Stop();
            }
        }
    }
}
