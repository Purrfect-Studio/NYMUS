using UnityEngine;
using UnityEngine.UI;

public class EfeitosSonorosScript : MonoBehaviour
{
    [Header("Efeitos Sonoros")]
    public AudioClip[] efeitosSonoros;

    [Header("Configuracoes Opcionais")]
    public Slider alteradorVolumeEfeitos;

    private AudioControlador audioControlador;
    private AudioSource tocadorEfeitoAudio;

    private void Start()
    {
        audioControlador = AudioControlador.Instancia;
        tocadorEfeitoAudio = audioControlador.tocadorEfeitoAudio;

        // Configura o slider para alterar o volume dos efeitos sonoros
        if (alteradorVolumeEfeitos != null)
        {
            audioControlador.ConfigurarSliderVolumeEfeitos(alteradorVolumeEfeitos);
        }
    }

    public void TocarEfeitoSonoro(int indiceEfeito)
    {
        if (indiceEfeito >= 0 && indiceEfeito < efeitosSonoros.Length)
        {
            tocadorEfeitoAudio.PlayOneShot(efeitosSonoros[indiceEfeito], audioControlador.volumeEfeitosAudioConfiguracao);
        }
    }
}
