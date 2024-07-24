using System.Collections;
using UnityEngine;

public class AudioControlador : MonoBehaviour
{
    public static AudioControlador Instancia { get; private set; }
    [Header("Players de áudio")]
    public static AudioSource tocadorMusica;
    public static AudioSource tocadorEfeitoAudio;

    [Header("Clips de áudio")]
    public static AudioClip musicaDeFundo;
    public static AudioClip efeitoAudio;

    [Header("Volumes")]
    public static float volumeMusicaConfiguracao = 1;
    public static float volumeEfeitosAudioConfiguracao;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else
        {
            Destroy(gameObject); // Destroi duplicatas
        }
        Start();
    }

    private void Start()
    {
        tocadorMusica = GetComponent<AudioSource>();
    }

    public static IEnumerator FadeVolume(float delay, float volumeInicial, float volumeAlvo) // Parametros opcionais
    {
        if (tocadorMusica == null) yield break;

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

    public static void mudarVolumeMusica(float volume)
    {
        volumeMusicaConfiguracao = volume;
        tocadorMusica.volume = volumeMusicaConfiguracao;
    }

    public static void mudarVolumeEfeitos(float volume)
    {
        volumeEfeitosAudioConfiguracao = volume;
        tocadorEfeitoAudio.volume = volumeEfeitosAudioConfiguracao;
    }

    public static void colocarMusica(AudioClip musica)
    {
        musicaDeFundo = musica;
        tocadorMusica.clip = musicaDeFundo;
        tocadorMusica.loop = true;
        tocadorMusica.Play();
    }

    public static void colocarEfeitoSonoro(AudioClip efeito)
    {
        efeitoAudio = efeito;
        tocadorEfeitoAudio.clip = efeito;
        tocadorEfeitoAudio.Play();
    }
}