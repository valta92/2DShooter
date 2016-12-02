using UnityEngine;
using System.Collections;

public class SoundManager : PersistentSingleton<SoundManager> {

    public AudioSource efxSource;
    public AudioSource musicSource;           
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    public void Start()
    {
        efxSource = GetComponent<AudioSource>();
    }
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.PlayOneShot(clip);
    }


    public void RandomizeSfx (params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
}
