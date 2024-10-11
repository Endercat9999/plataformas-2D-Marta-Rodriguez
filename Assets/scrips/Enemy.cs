using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private int healPoints = 5;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void TakeDamage()
    {
        healPoints--;
        SoundManager.instance.PlaySFX(audioSource, SoundManager.instance.sonidoEnemigo);
        
        if(healPoints <= 0)
        {
            Destroy(gameObject);
        }
       
        


    }

}
