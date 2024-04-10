using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlador : MonoBehaviour
{
    public AudioSource player;
    public AudioClip musicaDeFundo;
    // Start is called before the first frame update
    void Start()
    {
        player.clip = musicaDeFundo; // COLOCA A MÚSICA
        player.Play();// TOCA A MUSICA

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
