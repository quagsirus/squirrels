using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip hit;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("fxVolume", 0.6f);
    }
    public void PlayHit()
    {
        audioSource.clip = hit;
        audioSource.Play();
    }
}
