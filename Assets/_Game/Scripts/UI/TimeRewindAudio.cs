using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeRewindAudio : MonoBehaviour
{
    [SerializeField] private AudioSource rewindAudio;

    public void PlayRewind()
    {
        StartCoroutine(RewindAndRestart());
    }

    private IEnumerator RewindAndRestart()
    {
        rewindAudio.Play();

        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}