using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlador : MonoBehaviour
{
    public AudioSource player;
    public AudioClip musicaDeFundo;
    public float volumeInicial;

    // Start is called before the first frame update
    void Start()
    {
        player.clip = musicaDeFundo; // Define a música

        player.volume = 0.5f; // Define o volume inicial como 0.5 para o fade in

        StartCoroutine(FadeInVolume(volumeInicial, 1.0f, 3)); // Inicia o fade in de volume em 3 segundos

        player.Play(); // Toca a música
        player.loop = true; // Define para a música repetir em loop
    }

    // Método para aumentar gradualmente o volume (Fade In)
    IEnumerator FadeInVolume(float startVolume, float targetVolume, float fadeTimeSeconds)
    {
        float currentTime = 0;
        while (currentTime <= fadeTimeSeconds)
        {
            float t = currentTime / fadeTimeSeconds;
            player.volume = Mathf.Lerp(startVolume, targetVolume, t);
            currentTime += Time.deltaTime;
            yield return null;
        }
        player.volume = targetVolume; // Garante que o volume atingirá exatamente o valor final
    }

    // Método para diminuir gradualmente o volume (Fade Out)
    public void FadeOutVolume(float fadeTimeSeconds)
    {
        StartCoroutine(FadeOutCoroutine(player.volume, 0.2f, fadeTimeSeconds)); // Inicia o fade out de volume em 3 segundos
    }

    IEnumerator FadeOutCoroutine(float startVolume, float targetVolume, float fadeTimeSeconds)
    {
        float currentTime = 0;
        while (currentTime <= fadeTimeSeconds)
        {
            float t = currentTime / fadeTimeSeconds;
            player.volume = Mathf.Lerp(startVolume, targetVolume, t);
            currentTime += Time.deltaTime;
            yield return null;
        }
        player.volume = targetVolume; // Garante que o volume atingirá exatamente o valor final
    }
}
