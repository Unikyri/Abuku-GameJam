using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement optionsPanel;
    private VisualElement menuPanel;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource buttonAudioSource;
    private bool isLoading = false;


    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

        VisualElement root = uiDocument.rootVisualElement;
        optionsPanel = root.Q<VisualElement>("options-panel");
        menuPanel = root.Q<VisualElement>("menu-panel");


        Button playButton = root.Q<Button>("play-button");
        playButton.clicked += OnPlayClicked;

        Button optionsButton = root.Q<Button>("options-button");
        optionsButton.clicked += OnOptionsClicked;

        Button backButton = root.Q<Button>("back-button");
        backButton.clicked += OnBackClicked;

        Button exitButton = root.Q<Button>("exit-button");
        exitButton.clicked += OnExitClicked;

        Slider musicSlider = root.Q<Slider>("music-slider");
        musicSlider.RegisterValueChangedCallback(OnMusicChanged);

        Slider sfxSlider = root.Q<Slider>("sfx-slider");
        sfxSlider.RegisterValueChangedCallback(OnSFXChanged);

        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 50f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 50f);

        musicSlider.SetValueWithoutNotify(savedMusicVolume);
        sfxSlider.SetValueWithoutNotify(savedSFXVolume);

        audioMixer.SetFloat("MusicVolume", ConvertToDecibels(savedMusicVolume));
        audioMixer.SetFloat("SFXVolume", ConvertToDecibels(savedSFXVolume));

    }



    private void OnPlayClicked()
    {
        Debug.Log("¡Presionaste Jugar!");
        StartCoroutine(PlayAndLoadGame());
    }

    private void OnOptionsClicked()
    {
        buttonAudioSource.Play();
        menuPanel.style.display = DisplayStyle.None;
        optionsPanel.style.display = DisplayStyle.Flex;
        Debug.Log("¡Presionaste Opciones!");
    }

    private void OnExitClicked()
    {
        Debug.Log("¡Presionaste Salir!");
        buttonAudioSource.Play();
        Application.Quit();
    }

    private void OnBackClicked()
    {
        buttonAudioSource.Play();
        optionsPanel.style.display= DisplayStyle.None;
        menuPanel.style.display = DisplayStyle.Flex;
    }

   private void OnMusicChanged(ChangeEvent<float> evt)
{
    float volume = evt.newValue;
    float volumenDB = ConvertToDecibels(volume);

    audioMixer.SetFloat("MusicVolume", volumenDB);
    PlayerPrefs.SetFloat("MusicVolume", volume);
}

    private void OnSFXChanged(ChangeEvent<float> evt)
    {
        float volume = evt.newValue;
        float volumenDB = ConvertToDecibels(volume);

        audioMixer.SetFloat("SFXVolume", volumenDB);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }


    private float ConvertToDecibels(float volume)
    {
        if (volume <= 0f)
        {
            return -80f;
        }

        float normalizedVolume = volume / 100f;
        return 20f * Mathf.Log10(normalizedVolume);
    }

    private IEnumerator PlayAndLoadGame()
    {
        buttonAudioSource.Play();
        yield return new WaitForSeconds(0.2f);        
        SceneManager.LoadScene("Library");
    }

    private void PlayGame()
    {
        if (isLoading)
        {
            return;
        }


        isLoading = true;
        StartCoroutine(PlayAndLoadGame());
    }
}
