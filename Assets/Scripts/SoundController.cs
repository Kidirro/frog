using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _deathSound;
    [SerializeField] private static AudioSource _deathSoundStat;
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private static AudioSource _jumpSoundStat;

    private void Start()
    {
        _deathSoundStat = _deathSound;
        _jumpSoundStat = _jumpSound;
    }

    public static void PlayDeathSound()
    {
        _deathSoundStat.Play();
    }

    public static void PlayJumpSound()
    {

        _jumpSoundStat.pitch = 1 + Random.Range(-0.1f, 0.1f);
        _jumpSoundStat.Play();
    }
}
