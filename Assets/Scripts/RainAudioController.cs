using UnityEngine;

public class RainAudioController : MonoBehaviour
{
    private AudioSource rainAudio;

    void Start()
    {
        rainAudio = GetComponent<AudioSource>();
        if (rainAudio != null && !rainAudio.isPlaying)
            rainAudio.Play();
    }

    void OnEnable()
    {
        if (rainAudio == null) rainAudio = GetComponent<AudioSource>();
        if (rainAudio != null && !rainAudio.isPlaying)
            rainAudio.Play();
    }

    void OnDisable()
    {
        if (rainAudio == null) rainAudio = GetComponent<AudioSource>();
        if (rainAudio != null && rainAudio.isPlaying)
            rainAudio.Stop();
    }
}