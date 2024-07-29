using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicasScript : MonoBehaviour
{
    [Header("Musicas")]
    public AudioClip[] trilhasSonoras;

    [Header("Configuracoes Opcionais")]
    public Slider alteradorVolumeMusica;
    public bool comecarAutomaticamente = true;
    public float duracaoFade = 3;

    private AudioControlador audioControlador;
    private AudioSource tocadorMusica;

    private void Start()
    {
        audioControlador = AudioControlador.Instancia;
        tocadorMusica = audioControlador.tocadorMusica;

        if (comecarAutomaticamente && trilhasSonoras.Length > 0)
        {
            TocarMusica(trilhasSonoras[0]);
            FadeInVolume();
        }

        // Configura o slider para alterar o volume da música
        if (alteradorVolumeMusica != null)
        {
            audioControlador.ConfigurarSliderVolumeMusica(alteradorVolumeMusica);
        }
    }

    public void TrocarMusica(int indiceArrayMusicas)
    {
        FadeOutVolume(true);
        StartCoroutine(TrocarMusicaAtrasada(indiceArrayMusicas));
    }

    public void FadeOutVolume(bool fadeRapido = false)
    {
        float duracaoFadeOut = fadeRapido ? duracaoFade / 2 : duracaoFade;
        StartCoroutine(FadeVolume(tocadorMusica, duracaoFadeOut, tocadorMusica.volume, 0));
    }

    public void FadeInVolume(bool fadeRapido = false)
    {
        float duracaoFadeIn = fadeRapido ? duracaoFade / 2 : duracaoFade;
        StartCoroutine(FadeVolume(tocadorMusica, duracaoFadeIn, 0, audioControlador.volumeMusicaConfiguracao));
    }

    private IEnumerator TrocarMusicaAtrasada(int indiceArrayMusicas)
    {
        yield return new WaitForSeconds(duracaoFade / 2);
        TocarMusica(trilhasSonoras[indiceArrayMusicas]);
        FadeInVolume(true);
    }

    private void TocarMusica(AudioClip musica)
    {
        tocadorMusica.clip = musica;
        tocadorMusica.loop = true;
        tocadorMusica.Play();
    }

    private IEnumerator FadeVolume(AudioSource audioSource, float delay, float volumeInicial, float volumeAlvo)
    {
        float tempoAtual = 0;
        while (tempoAtual <= delay)
        {
            float t = tempoAtual / delay;
            audioSource.volume = Mathf.Lerp(volumeInicial, volumeAlvo, t);
            tempoAtual += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = volumeAlvo;
    }
}
