using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SFXSource;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip touchMove;
    [SerializeField] private AudioClip touchButton;
    [SerializeField] private AudioClip touchDrop;
    [SerializeField] private AudioClip touchLight;
    [SerializeField] private AudioClip touchWindow;

    private void Start()
    {
        // Tìm tiviController trong scene
        tiviController tivi = FindObjectOfType<tiviController>();
        if (tivi != null && tivi.isTiviOn == true) return;

        MusicSource.clip = backgroundMusic;
        MusicSource.Play();
        MusicSource.loop = true;
    }
    // public void Update()
    // {
    //     tiviController tivi = FindObjectOfType<tiviController>();
    //     if (tivi != null && tivi.isTiviOn == true)
    //     {
    //         MusicSource.Stop();
    //     }
        

    // }
    public void Update()
{
    tiviController tivi = FindObjectOfType<tiviController>();
    bool isTiviOn = (tivi != null && tivi.isTiviOn);

    if (isTiviOn)
    {
        if (MusicSource.isPlaying)
            MusicSource.Stop();
    }
    else
    {
        if (!MusicSource.isPlaying)
        {
            MusicSource.clip = backgroundMusic;
            MusicSource.loop = true;
            MusicSource.Play();
        }
    }
}

    public void PlayOnMouseDown()
    {
        if (SFXSource != null && touchMove != null)
            SFXSource.PlayOneShot(touchMove);
    }

    public void PlayTouchButton()
    {
        if (SFXSource != null && touchButton != null)
            SFXSource.PlayOneShot(touchButton);
    }

    public void PlayOnMouseUp()
    {
        if (SFXSource != null && touchDrop != null)
            SFXSource.PlayOneShot(touchDrop);
    }
    public void PlayLight()
    {
        if (SFXSource != null && touchLight != null)
            SFXSource.PlayOneShot(touchLight);
    }
    public void PlayWindow()
    {
        if (SFXSource != null && touchWindow != null)
            SFXSource.PlayOneShot(touchWindow);
    }
}