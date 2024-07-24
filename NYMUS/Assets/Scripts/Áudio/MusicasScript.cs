using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MusicasScript : MonoBehaviour
{
    [Header("Musicas")]
    public AudioClip[] trilhasSonoras;

    [Header("Configuracoeusing System.Collections;\r\nusing UnityEngine;\r\nusing UnityEngine.UI;\r\n\r\npublic class MusicasScript : MonoBehaviour\r\n{\r\n    [Header(\"Musicas\")]\r\n    public AudioClip[] trilhasSonoras;\r\n\r\n    [Header(\"Configuracoes Opcionais \")]\r\n    public Slider alteradorVolume;\r\n    public bool comecarAutomaticamente = true;\r\n    public float duracaoFade = 3;\r\n\r\n    // Variaveis locais\r\n    private float duracaoFadeOut;\r\n    private float duracaoFadeIn;\r\n\r\n    private void Start()\r\n    {\r\n        if (comecarAutomaticamente)\r\n        {\r\n            if (trilhasSonoras.Length > 0 && trilhasSonoras[0] != null)\r\n            {\r\n                AudioControlador.Instancia.colocarMusica(trilhasSonoras[0]);\r\n                FadeInVolume();\r\n            }\r\n            else\r\n            {\r\n                AudioControlador.Instancia.pararMusica();\r\n            }\r\n        }\r\n    }\r\n\r\n    public void trocarMusica(int indiceArrayMusicas)\r\n    {\r\n        FadeOutVolume(true); // Fade out rápido\r\n        StartCoroutine(TrocarMusicaAtrasada(indiceArrayMusicas));\r\n    }\r\n\r\n    public void FadeOutVolume(bool fadeRapido = false)\r\n    {\r\n        duracaoFadeOut = fadeRapido ? duracaoFade / 2 : duracaoFade;\r\n        StartCoroutine(AudioControlador.FadeVolume(duracaoFadeOut, AudioControlador.Instancia.volumeMusicaConfiguracao, AudioControlador.Instancia.volumeMusicaConfiguracao / 2)); // Inicia o fade out \r\n    }\r\n\r\n    public void FadeInVolume(bool fadeRapido = false)\r\n    {\r\n        duracaoFadeIn = fadeRapido ? duracaoFade / 2 : duracaoFade;\r\n        StartCoroutine(AudioControlador.FadeVolume(duracaoFadeIn, AudioControlador.Instancia.volumeMusicaConfiguracao / 2, AudioControlador.Instancia.volumeMusicaConfiguracao)); // Inicia o fade in \r\n    }\r\n\r\n    private IEnumerator TrocarMusicaAtrasada(int indiceArrayMusicas)\r\n    {\r\n        // Espera tantos segundos:\r\n        yield return new WaitForSeconds(duracaoFadeOut);\r\n\r\n        // Executa os comandos após a espera\r\n        AudioControlador.Instancia.colocarMusica(trilhasSonoras[indiceArrayMusicas]);\r\n        FadeInVolume(true); // Fade in rápido\r\n    }\r\n\r\n    public void slideConfiguracaoVolume() // Método chamado ao mudar o volume nas configs\r\n    {\r\n        AudioControlador.Instancia.mudarVolumeMusica(alteradorVolume.value);\r\n    }\r\n}\r\ns Opcionais ")]
    public Slider alteradorVolume;
    public bool comecarAutomaticamente = true;
    public float duracaoFade = 3;

    // Variaveis locais
    private float duracaoFadeOut;
    private float duracaoFadeIn;

    private void Start()
    {
        if (comecarAutomaticamente)
        {
            AudioControlador.Instancia.colocarMusica(trilhasSonoras[0]);
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
        StartCoroutine(AudioControlador.FadeVolume(duracaoFadeOut, AudioControlador.Instancia.volumeMusicaConfiguracao, AudioControlador.Instancia.volumeMusicaConfiguracao / 2)); // Inicia o fade out 
    }

    public void FadeInVolume(bool fadeRapido = false)
    {
        duracaoFadeIn = fadeRapido ? duracaoFade / 2 : duracaoFade;
        StartCoroutine(AudioControlador.FadeVolume(duracaoFadeIn, AudioControlador.Instancia.volumeMusicaConfiguracao / 2, AudioControlador.Instancia.volumeMusicaConfiguracao)); // Inicia o fade in 
    }

    private IEnumerator TrocarMusicaAtrasada(int indiceArrayMusicas)
    {
        // Espera tantos segundos:
        yield return new WaitForSeconds(duracaoFadeOut);

        // Executa os comandos após a espera
        AudioControlador.Instancia.colocarMusica(trilhasSonoras[indiceArrayMusicas]);
        FadeInVolume(true); // Fade in rápido
    }

    public void slideConfiguracaoVolume() // Método chamado ao mudar o volume nas configs
    {
        AudioControlador.Instancia.mudarVolumeMusica(alteradorVolume.value);
    }
}
