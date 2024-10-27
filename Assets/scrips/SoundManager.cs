using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static SoundManager instance;

   public AudioSource _audioSource;

   public AudioClip coinAudio;

   public AudioClip jumpAudio;

   public AudioClip dieAudio;

   public AudioClip attackAudio;

   public AudioClip hurtAudio;

   public AudioClip pauseAudio;

   public AudioClip runAudio;

   public AudioClip sonidoEnemigo;
   public AudioClip starAudio;
   
   
   void Awake()
   {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
   }

   /*public void CoinSFX()
   {
        _audioSource.PlayOneShot(coinAudio);
   }*/

    public void PlaySFX(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    






}
