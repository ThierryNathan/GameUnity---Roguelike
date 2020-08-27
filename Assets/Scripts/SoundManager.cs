using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRang = 1.05f;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip) 
    {
        efxSource.clip = clip;
        efxSource.Play(); 
    }

    public void RandomSfx(params AudioClip[] clips) 
    {
        int randomIndex = Random.Range(0, clips.Length);
        float ramdomPitch = Random.Range(lowPitchRange, highPitchRang);

        efxSource.pitch = ramdomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }   
}
