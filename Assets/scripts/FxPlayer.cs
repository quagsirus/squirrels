using UnityEngine;

public class FxPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip hit, die, enemyDie;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("fxVolume", 0.6f);
    }
    public void UpdateVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("fxVolume", 0.6f);
    }
    public void PlayHit()
    {
        audioSource.clip = hit;
        audioSource.Play();
    }
    public void PlayDie()
    {
        audioSource.clip = die;
        audioSource.Play();
    }
    public void PlayEnemyDie()
    {
        audioSource.clip = enemyDie;
        audioSource.Play();
    }
}
