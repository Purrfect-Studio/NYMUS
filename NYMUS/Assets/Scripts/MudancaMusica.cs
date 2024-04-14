using UnityEngine;

public class MudancaMusica : MonoBehaviour
{
    
    public AudioControlador audioControlador;

   

    public void FadeOutVolume(float duracao)
    {
                
        StartCoroutine(audioControlador.FadeVolume(duracao, 1, 0.5f)); // Inicia o fade out 
    }

    public void FadeInVolume(float duracao)
    {
        StartCoroutine(audioControlador.FadeVolume(duracao, 0.5f, 1)); // Inicia o fade in 
    }
}
