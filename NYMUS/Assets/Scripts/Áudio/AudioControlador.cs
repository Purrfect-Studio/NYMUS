using System.Collections;
using UnityEngine;

public class AudioControlador : MonoBehaviour
{
    public static AudioControlador Instancia { get; private set; }

    [Header("Players de �udio")]
    public AudioSource tocadorMusica;
    public AudioSource tocadorEfeitoAudio;

    [Header("Clips de �udio")]
    public AudioClip musicaDeFundo;
    public AudioClip efeitoAudio;

    [Header("Volumes")]
    public float volumeMusicaConfiguracao = 1;
    public float volumeEfeitosAudioConfiguracao;

    private void Awake()
    {
        // Verifica se j� existe uma inst�ncia do AudioControlador
        if (Instancia != null && Instancia != this)
        {
            // Se existir, destrua a inst�ncia rec�m-criada (a duplicata)
            Destroy(gameObject);
        }
        else
        {
            // Se n�o existir, defina a nova inst�ncia e fa�a com que persista entre as cenas
            Instancia = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
    }

    private void Start()
    {
        if (tocadorMusica == null)
        {
            tocadorMusica = GetComponent<AudioSource>();
        }
    }

    public static IEnumerator FadeVolume(float delay, float volumeInicial, float volumeAlvo)
    {
        if (Instancia.tocadorMusica == null) yield break;

        float tempoAtual = 0;
        while (tempoAtual <= delay)
        {
            float t = tempoAtual / delay;
            Instancia.tocadorMusica.volume = Mathf.Lerp(volumeInicial, volumeAlvo, t);
            tempoAtual += Time.deltaTime;
            yield return null;
        }
        Instancia.tocadorMusica.volume = volumeAlvo; // Garante que o volume atingir� exatamente o valor final
    }

    public void mudarVolumeMusica(float volume)
    {
        volumeMusicaConfiguracao = volume;
        tocadorMusica.volume = volumeMusicaConfiguracao;
    }

    public void mudarVolumeEfeitos(float volume)
    {
        volumeEfeitosAudioConfiguracao = volume;
        tocadorEfeitoAudio.volume = volumeEfeitosAudioConfiguracao;
    }

    public void colocarMusica(AudioClip musica)
    {
        musicaDeFundo = musica;
        tocadorMusica.clip = musicaDeFundo;
        tocadorMusica.loop = true;
        tocadorMusica.Play();
    }

    public void colocarEfeitoSonoro(AudioClip efeito)
    {
        efeitoAudio = efeito;
        tocadorEfeitoAudio.clip = efeito;
        tocadorEfeitoAudio.Play();
    }

    public void pararMusica()
    {
        if (tocadorMusica.isPlaying)
        {
            tocadorMusica.Stop();
        }
    }
}
