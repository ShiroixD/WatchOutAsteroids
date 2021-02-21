using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private static Dictionary<string, AudioClip> _sounds;
	private static AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _sounds = new Dictionary<string, AudioClip>()
        {
            {"ShieldToMeteor", Resources.Load<AudioClip>("Sounds/ShieldToMeteor")},
            {"MeteorToMeteor", Resources.Load<AudioClip>("Sounds/MeteorToMeteor")},
            {"HammerToShield", Resources.Load<AudioClip>("Sounds/HammerToShield")}
        };
    }

    public void PlaySound(string soundKey )
    {
        _audioSource.PlayOneShot( _sounds[soundKey] );
    }
}