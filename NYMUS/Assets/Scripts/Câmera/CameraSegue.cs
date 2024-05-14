using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [Header("Jogador")]
    public Transform jogador; // Posicao do player

    [Header("posicao Minima do X")]
    public float minX; // Movimentacao maxima para a esquerda
    [Header("posicao Maxima do X")]
    public float maxX; // Movimentacao maxima para a direita
    [Header("Delay para alcançar o Jogador")]
    public float delayTempo; //Delay da camera chegar no jogador
    
    private void FixedUpdate()
    {   Vector3 novaPosicao = jogador.position + new Vector3(0, 0, -10); // Afasta a camera do cenario
        novaPosicao = Vector3.Lerp(transform.position, novaPosicao, delayTempo); // Delay para a camera chegar no jogador
        transform.position = novaPosicao;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
        // Limita o posicionamento horizontal da camera
    }
}
