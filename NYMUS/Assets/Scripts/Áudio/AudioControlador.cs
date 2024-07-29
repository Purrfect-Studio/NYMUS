using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControlador : MonoBehaviour
{
    public static AudioControlador Instancia { get; private set; }

    [Header("Players de áudio")]
    public AudioSource tocadorMusica;
    public AudioSource tocadorEfeitoAudio;

    [Header("Volumes")]
    public float volumeMusicaConfiguracao = 1;
    public float volumeEfeitosAudioConfiguracao = 1;

    [Header("Configurações do Slider")]
    public float valorMaximoSlider = 10f; // Valor máximo do slider, deve ser ajustado conforme a configuração do slider

    private void Awake()
    {
        GerenciarInstancia();
        EncontrarAudioSources();
    }

    private void GerenciarInstancia()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void EncontrarAudioSources()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        HashSet<AudioSource> audioSourceSet = new HashSet<AudioSource>(audioSources);

        if (audioSourceSet.Count >= 2)
        {
            AudioSource[] uniqueAudioSources = new AudioSource[2];
            audioSourceSet.CopyTo(uniqueAudioSources);
            tocadorMusica = uniqueAudioSources[0];
            tocadorEfeitoAudio = uniqueAudioSources[1];
        }
        else
        {
            Debug.LogWarning("Não há AudioSources suficientes no objeto para inicializar ambos os tocadores de música e efeito.");
        }
    }

    // Muda o volume da música
    public void MudarVolumeMusica(float valorSlider)
    {
        float volume = valorSlider / valorMaximoSlider; // Converte o valor do slider para o intervalo 0-1
        volumeMusicaConfiguracao = Mathf.Clamp(volume, 0, 1); // Garante que o valor esteja dentro do intervalo permitido
        tocadorMusica.volume = volumeMusicaConfiguracao;
    }

    // Muda o volume dos efeitos sonoros
    public void MudarVolumeEfeitos(float valorSlider)
    {
        float volume = valorSlider / valorMaximoSlider; // Converte o valor do slider para o intervalo 0-1
        volumeEfeitosAudioConfiguracao = Mathf.Clamp(volume, 0, 1); // Garante que o valor esteja dentro do intervalo permitido
        tocadorEfeitoAudio.volume = volumeEfeitosAudioConfiguracao;
    }

    // Configura o slider para mudar o volume da música
    public void ConfigurarSliderVolumeMusica(Slider slider)
    {
        slider.onValueChanged.AddListener(MudarVolumeMusica);
    }

    // Configura o slider para mudar o volume dos efeitos sonoros
    public void ConfigurarSliderVolumeEfeitos(Slider slider)
    {
        slider.onValueChanged.AddListener(MudarVolumeEfeitos);
    }
}
