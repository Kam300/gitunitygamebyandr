using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundeffector : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip chestSound, coinSound, winSound, loseSound;

    public void PlayChestSound()
    {
        audioSource.PlayOneShot(chestSound);
    }
    public void PlayCoinSound()
    {
        audioSource.PlayOneShot(coinSound);
    }
    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }
    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound);
    }
}