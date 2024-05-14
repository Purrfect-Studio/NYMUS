using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomPassos : MonoBehaviour
{
    [SerializeField] private AudioSource audioJogador;
    [SerializeField] private AudioClip[] passosGramaMP3;

    public void Passos()
    {
        audioJogador.PlayOneShot(passosGramaMP3[Random.Range(0, passosGramaMP3.Length)]);
    }
}
