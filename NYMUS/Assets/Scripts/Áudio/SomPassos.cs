using UnityEngine;

public class SomPassos : MonoBehaviour
{
    [SerializeField] private AudioSource audioJogador;
    [SerializeField] private AudioClip[] passosGramaMP3;

    // Método chamado para tocar um som de passo
    public void Passos()
    {
        int indiceAleatorio = Random.Range(0, passosGramaMP3.Length);
        audioJogador.PlayOneShot(passosGramaMP3[indiceAleatorio]);
    }
}
