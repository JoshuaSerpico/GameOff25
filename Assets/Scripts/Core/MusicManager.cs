using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] Music;
    public int currentIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = Music[0];
        musicSource.Play();
    }
    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = Music[(currentIndex + 1) % Music.Length];
            musicSource.Play();
        }
    }
}
