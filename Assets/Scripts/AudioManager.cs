using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneMusicMap
    {
        public string sceneName;
        public AudioClip musicClip;
        public AudioClip ambientClip; // Added for layering
    }

    private static AudioManager instance;
    
    // We now use two sources to play sounds at the same time
    private AudioSource musicSource;
    private AudioSource ambientSource;

    [Header("Custom Scene Mapping")]
    [SerializeField] private List<SceneMusicMap> customPlaylist = new List<SceneMusicMap>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Initialize the two separate tracks
        musicSource = gameObject.AddComponent<AudioSource>();
        ambientSource = gameObject.AddComponent<AudioSource>();

        SetupSource(musicSource);
        SetupSource(ambientSource);
    }

    private void SetupSource(AudioSource source)
    {
        source.loop = true;
        source.playOnAwake = false;
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var map in customPlaylist)
        {
            if (map.sceneName == scene.name)
            {
                // Play both tracks simultaneously
                UpdateTrack(musicSource, map.musicClip);
                UpdateTrack(ambientSource, map.ambientClip);
                return;
            }
        }
    }

    private void UpdateTrack(AudioSource source, AudioClip newClip)
    {
        if (newClip == null)
        {
            source.Stop();
            source.clip = null;
            return;
        }

        if (source.clip == newClip) return;

        source.clip = newClip;
        source.Play();
    }

    // Public method to manually trigger a sound from other scripts
    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        musicSource.PlayOneShot(clip, volume);
    }
}