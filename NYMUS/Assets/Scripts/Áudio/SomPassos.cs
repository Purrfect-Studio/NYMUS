using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomPassos : MonoBehaviour
{
    [SerializeField] private AudioSource playerAudio;

    [SerializeField] private AudioClip[] passosGramamp3;

    private void Passos()
    {
        playerAudio.PlayOneShot(passosGramamp3[Random.Range(0, passosGramamp3.Length)]);
    }
}
