using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // public AudioClip standardBGM, bossBGM;
    Squirrel squirrel1, squirrel2;

    // Start is called before the first frame update
    void Start()
    {
        squirrel1 = GameObject.Find("playerOne").GetComponent<Squirrel>();
        squirrel2 = GameObject.Find("playerTwo").GetComponent<Squirrel>();
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicVolume", 0.2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (squirrel1.isDead && (squirrel2.isDead | squirrel2.isDespawned))
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
    }
}
