using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] bgMusic;
    [SerializeField] float[] volumes;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        BGMusic[] bGMusics = FindObjectsOfType<BGMusic>();
        if (bGMusics.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("New BG Music");
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        //audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            return;
        }

        if (sceneName.StartsWith("Level "))
        {
            int currentLevel = int.Parse(sceneName.Substring(sceneName.Length - 1));
            audioSource.clip = bgMusic[currentLevel];
            audioSource.volume = volumes[currentLevel];
            audioSource.Stop();
        }
        else if (sceneName != "Loading")
        {
            if (audioSource.clip != bgMusic[0])
            {
                audioSource.volume = volumes[0];
                audioSource.clip = bgMusic[0];
            }
        }
        
        if (audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void Play()
    {
        audioSource.Play();
    }
}
