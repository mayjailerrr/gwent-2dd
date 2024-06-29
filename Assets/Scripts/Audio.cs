using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip firstAudio;
    public AudioClip secondAudio;

    private AudioSource audioSource;
    private bool firstAudioReproduced = false;
    public GameObject audio;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ReproduceFirstAudio();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            if(firstAudioReproduced)
            {
                ReproduceSecondAudio();
            }
            else
            {
                ReproduceFirstAudio();
            }
        }
    }

    void ReproduceFirstAudio()
    {
        audioSource.clip = firstAudio;
        audioSource.Play();
        firstAudioReproduced = true;
    }

    void ReproduceSecondAudio()
    {
        audioSource.clip = secondAudio;
        audioSource.Play();
        firstAudioReproduced = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu")
        {
            audio.SetActive(false);
        }
        else
        {
            audio.SetActive(true);
        }
    }
}
