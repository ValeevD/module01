using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SoundPlay))]
public class GameEndSounds : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioMixer audioMixer;

    public AudioClip winSound;
    public AudioClip loseSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayGameEndSound(AudioClip soundToPlay)
    {
        backgroundMusic.Stop();
        audioSource.clip = soundToPlay;
        audioSource.Play();
    }

    public void PlayWinSound()
    {
        PlayGameEndSound(winSound);
    }

    public void PlayLoseSound()
    {
        PlayGameEndSound(loseSound);
    }

}
