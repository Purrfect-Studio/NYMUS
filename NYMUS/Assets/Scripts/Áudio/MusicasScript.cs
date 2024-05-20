using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicasScript : MonoBehaviour
{
    [Header("Musicas")]
    public AudioClip[] trilhasSonoras;

    [Header("Configuracoes Opcionais ")]
    public Slider alteradorVolume;
    public bool comecarAutomaticamente = true;
    public float duracaoFade = 3;

    // variaveis locais
    private float duracaoFadeOut;
    private float duracaoFadeIn;

    private void Start()
    {
        if (comecarAutomaticamente)
        {
            AudioControlador.colocarMusica(trilhasSonoras[0]);
            FadeInVolume();
        }
    }

    public void trocarMusica(int indiceArrayMusicas)
    {
        FadeOutVolume(true); // Fade out rápido
        StartCoroutine(TrocarMusicaAtrasada(indiceArrayMusicas));
    }

    public void FadeOutVolume(bool fadeRapido = false)
    {
        duracaoFadeOut = fadeRapido ? duracaoFade / 2 : duracaoFade;
        StartCoroutine(AudioControlador.FadeVolume(duracaoFadeOut, AudioControlador.volumeMusicaConfiguracao, AudioControlador.volumeMusicaConfiguracao / 2)); // Inicia o fade out 
    }

    public void FadeInVolume(bool fadeRapido = false)
    {
        duracaoFadeIn = fadeRapido ? duracaoFade / 2 : duracaoFade;
        StartCoroutine(AudioControlador.FadeVolume(duracaoFadeIn, AudioControlador.volumeMusicaConfiguracao / 2, AudioControlador.volumeMusicaConfiguracao)); // Inicia o fade in 
    }

    private IEnumerator TrocarMusicaAtrasada(int indiceArrayMusicas)
    {
        // Espera tantos segundos:
        yield return new WaitForSeconds(duracaoFadeOut);

        // Executa os comandos após a espera
        AudioControlador.colocarMusica(trilhasSonoras[indiceArrayMusicas]);
        FadeInVolume(true); // Fade in rápido
    }

    public void slideConfiguracaoVolume() // Método chamado ao mudar o volume nas configs
    {
        AudioControlador.mudarVolumeMusica(alteradorVolume.value);
    }
}