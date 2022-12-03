using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioClip[] backgroundMusics;
    [SerializeField] int currentMusic;
    [SerializeField] int currentClipLengt;
    public Slider volumeSlider;
    public float currentVolume;
    private AudioSource audioSource;
    private float elapsedTime;
    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        audioSource.clip = backgroundMusics[0];
        Invoke("play", 3f);
    }

    void Update()
    {
        currentVolume = volumeSlider.value; audioSource.volume = volumeSlider.value; // Updates the volume;

        // This is for the time;

        if (audioSource.isPlaying == true)
        {
            elapsedTime = elapsedTime + Time.deltaTime;
        }
        if (elapsedTime >= backgroundMusics[currentMusic].length - 0.2f)
        {
            currentMusic = currentMusic +1;
            elapsedTime = 0;
            if (currentMusic == backgroundMusics.Length)
            {
                currentMusic = 0;
                Debug.Log(currentMusic + "    " + backgroundMusics.Length);
            }
            audioSource.clip = backgroundMusics[currentMusic];
            audioSource.Play();
        }
    }

    void play() //Do not use;
    {
        audioSource.Play();
    }

    public void PlayClipAtPoint(AudioClip clip, float volume, Vector3 position)
    {
        volume = Mathf.Clamp(volume, 0, 1);
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayClipAtMainCamera(AudioClip clip, float volume)
    {
        volume = Mathf.Clamp(volume, 0, 1);
        audioSource.PlayOneShot(clip, volume);
    }

    public void PauseIfTrue(bool pause)
    {
        if (pause == true)
        {
            audioSource.Pause();
        }
        else if (pause == false)
        {
            audioSource.Play();
        }
    }

    public void MuteIfTrue(bool mute)
    {
        audioSource.mute = mute;
    }

    public void setCurrentVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0, 1);
        currentVolume = volume;
    }
}
