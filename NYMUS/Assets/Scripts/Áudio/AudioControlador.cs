using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControlador : MonoBehaviour
{
    public static AudioControlador Instancia { get; private set; }

    [Header("Mixers de áudio")]
    public AudioMixerGroup grupoPrincipal;
    public AudioMixerGroup grupoMusicas; // Grupo do mixer para músicas
    public AudioMixerGroup grupoSFX; // Grupo do mixer para efeitos sonoros

    [Header("Volumes")]
    public float volumeGeralConfiguracao = 1;
    public float volumeMusicaConfiguracao = 1;
    public float volumeEfeitosAudioConfiguracao = 1;

    [Header("Configurações do Slider")]
    public float valorMaximoSlider = 10f; // Valor máximo do slider, deve ser ajustado conforme a configuração do slider

    private void Awake()
    {
        GerenciarInstancia();
    }

    private void GerenciarInstancia()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instancia != this)
        {
            Destroy(Instancia.gameObject); // Destrói a instância antiga em vez de destruir a nova
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Muda o volume da música através do AudioMixerGroup
    public void MudarVolumeGeral(float valorSlider)
    {
        float volume = Mathf.Lerp(-80f, 0f, valorSlider / valorMaximoSlider); // Converte o valor do slider para o intervalo -80dB a 0dB
        grupoPrincipal.audioMixer.SetFloat("VolumeGlobal", volume);
        volumeGeralConfiguracao = valorSlider / valorMaximoSlider;
    }

    public void MudarVolumeMusica(float valorSlider)
    {
        float volume = Mathf.Lerp(-80f, 0f, valorSlider / valorMaximoSlider); // Converte o valor do slider para o intervalo -80dB a 0dB
        grupoMusicas.audioMixer.SetFloat("VolumeMusica", volume);
        volumeMusicaConfiguracao = valorSlider / valorMaximoSlider;
    }

    // Muda o volume dos efeitos sonoros através do AudioMixerGroup
    public void MudarVolumeEfeitos(float valorSlider)
    {
        float volume = Mathf.Lerp(-80f, 0f, valorSlider / valorMaximoSlider); // Converte o valor do slider para o intervalo -80dB a 0dB
        grupoSFX.audioMixer.SetFloat("VolumeEfeitos", volume);
        volumeEfeitosAudioConfiguracao = valorSlider / valorMaximoSlider;
    }

    // Configura o slider para mudar o volume da música
    public void ConfigurarSliderVolumeGeral(Slider slider)
    {
        slider.onValueChanged.AddListener(MudarVolumeGeral);
        slider.value = volumeGeralConfiguracao * valorMaximoSlider;
    }

    public void ConfigurarSliderVolumeMusica(Slider slider)
    {
        slider.onValueChanged.AddListener(MudarVolumeMusica);
        slider.value = volumeMusicaConfiguracao * valorMaximoSlider;
    }

    // Configura o slider para mudar o volume dos efeitos sonoros
    public void ConfigurarSliderVolumeEfeitos(Slider slider)
    {
        slider.onValueChanged.AddListener(MudarVolumeEfeitos);
        slider.value = volumeEfeitosAudioConfiguracao * valorMaximoSlider;
    }

}
