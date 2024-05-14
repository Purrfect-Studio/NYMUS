using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlador : MonoBehaviour
{
    public AudioSource tocadorMusica;
    public AudioClip musicaDeFundo;
    public float volumeInicial = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        tocadorMusica.clip = musicaDeFundo; // Define a música

        tocadorMusica.volume = volumeInicial; // Define o volume inicial


        StartCoroutine(FadeVolume(3, volumeInicial, 1.0f)); // Inicia o fade in de volume em 3 segundos
        //obs: para fazer Fades, use o script MudancaMusica.cs. O comando acima é uma excessão.

        tocadorMusica.Play(); // Toca a música
        tocadorMusica.loop = true; // Define para a música repetir em loop
    }

       
    public IEnumerator FadeVolume(float delay, float volumeInicial, float volumeAlvo) // Parametros opcionais
    {
        float tempoAtual = 0;
        while (tempoAtual <= delay)
        {
            float t = tempoAtual / delay;
            tocadorMusica.volume = Mathf.Lerp(volumeInicial, volumeAlvo, t);
            tempoAtual += Time.deltaTime;
            yield return null;
        }
        tocadorMusica.volume = volumeAlvo; // Garante que o volume atingirá exatamente o valor final
    }

    
    

 
}
