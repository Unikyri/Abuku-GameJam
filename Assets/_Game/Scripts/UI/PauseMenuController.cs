using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class PauseMenuController : MonoBehaviour
{

    private bool isPaused = false;
    private VisualElement pauseRoot;
    private UIDocument uiDocument;
    private Button resumeButton;
    private Button mainMenuButton;
    private VisualElement pauseContent;
    private VisualElement pauseOptionsContent;
    private Button optionsButton;
    private Button backButton;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource buttonAudioSource;

    private Slider musicSlider;
    private Slider sfxSlider;




    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

        VisualElement root = uiDocument.rootVisualElement;

        pauseRoot = root.Q<VisualElement>("pause-root");
        pauseRoot.style.display = DisplayStyle.None;

        resumeButton = root.Q<Button>("resume-button");
        resumeButton.clicked += ResumeGame;

        mainMenuButton = root.Q<Button>("main-menu-button");
        mainMenuButton.clicked += ReturnToMainMenu;

        pauseContent = root.Q<VisualElement>("pause-content");
        pauseOptionsContent = root.Q<VisualElement>("pause-options-content");

        optionsButton = root.Q<Button>("pause-options-button");
        backButton = root.Q<Button>("back-button");

        optionsButton.clicked += OpenOptions;
        backButton.clicked += BackToPause;

        musicSlider = root.Q<Slider>("music-slider");
        sfxSlider = root.Q<Slider>("sfx-slider");

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 50f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 50f);

        musicSlider.SetValueWithoutNotify(musicVolume);
        sfxSlider.SetValueWithoutNotify(sfxVolume);
        audioMixer.SetFloat("MusicVolume", ConvertToDecibels(musicVolume));
        audioMixer.SetFloat("SFXVolume", ConvertToDecibels(sfxVolume));



        musicSlider.RegisterValueChangedCallback(evt =>
        {
            float volume = evt.newValue;

            audioMixer.SetFloat("MusicVolume", ConvertToDecibels(volume));
            PlayerPrefs.SetFloat("MusicVolume", volume);
        });

        sfxSlider.RegisterValueChangedCallback(evt =>
        {
            float volume = evt.newValue;

            audioMixer.SetFloat("SFXVolume", ConvertToDecibels(volume));
            PlayerPrefs.SetFloat("SFXVolume", volume);
        });
    }


   
    
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseRoot.style.display = DisplayStyle.Flex;
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        pauseContent.style.display = DisplayStyle.Flex;
        buttonAudioSource.Play();
        pauseOptionsContent.style.display = DisplayStyle.None;

        pauseRoot.style.display = DisplayStyle.None;
    }

    private void ReturnToMainMenu()
    {
        StartCoroutine(PlayAndReturnToMenu());
    }

    private void OpenOptions()
    {
        buttonAudioSource.Play();

        pauseContent.style.display = DisplayStyle.None;
        pauseOptionsContent.style.display = DisplayStyle.Flex;
    }

    private void BackToPause()
    {
        buttonAudioSource.Play();

        pauseOptionsContent.style.display = DisplayStyle.None;
        pauseContent.style.display = DisplayStyle.Flex;
    }

    private float ConvertToDecibels(float volume)
    {
        if (volume <= 0f)
            return -80f;

        float normalizedVolume = volume / 100f;
        return 20f * Mathf.Log10(normalizedVolume);
    }


    private IEnumerator PlayAndReturnToMenu()
    {
        buttonAudioSource.Play();

        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1f;
        SceneManager.LoadScene("UI_Test");
    }

}


