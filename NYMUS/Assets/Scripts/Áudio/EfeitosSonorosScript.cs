using UnityEngine;
using UnityEngine.UI;

public class EfeitosSonorosScript : MonoBehaviour
{
    [Header("Efeitos Sonoros")]
    public AudioClip[] efeitosSonoros;

    [Header("Configuracoes Opcionais")]
    private AudioControlador audioControlador;
    private AudioSource tocadorEfeitoAudio;

    private void Start()
    {
        audioControlador = AudioControlador.Instancia;
        tocadorEfeitoAudio = audioControlador.tocadorEfeitoAudio;
    }

    public void TocarEfeitoSonoro(int indiceEfeito)
    {
        if (indiceEfeito >= 0 && indiceEfeito < efeitosSonoros.Length)
        {
            tocadorEfeitoAudio.PlayOneShot(efeitosSonoros[indiceEfeito], audioControlador.volumeEfeitosAudioConfiguracao);
        }
    }
}
