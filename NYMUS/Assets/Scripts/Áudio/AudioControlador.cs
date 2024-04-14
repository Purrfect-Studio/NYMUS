using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlador : MonoBehaviour
{
    public AudioSource player;
    public AudioClip musicaDeFundo;
    public float volumeInicial = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        player.clip = musicaDeFundo; // Define a m�sica

        player.volume = volumeInicial; // Define o volume inicial


        StartCoroutine(FadeVolume(3, volumeInicial, 1.0f)); // Inicia o fade in de volume em 3 segundos
        //obs: para fazer Fades, use o script MudancaMusica.cs. O comando acima � uma excess�o.

        player.Play(); // Toca a m�sica
        player.loop = true; // Define para a m�sica repetir em loop
    }

       
    public IEnumerator FadeVolume(float delay, float volumeInicial, float volumeAlvo) // Parametros opcionais
    {
        float tempoAtual = 0;
        while (tempoAtual <= delay)
        {
            float t = tempoAtual / delay;
            player.volume = Mathf.Lerp(volumeInicial, volumeAlvo, t);
            tempoAtual += Time.deltaTime;
            yield return null;
        }
        player.volume = volumeAlvo; // Garante que o volume atingir� exatamente o valor final
    }

    
    

 
}
