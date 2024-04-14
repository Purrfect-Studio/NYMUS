using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    // Codigo para a movimentacao da camera
    public Transform jogador; // Posicao do player

    public float minX; // Movimentacao maxima para a esquerda
    public float maxX; // Movimentacao maxima para a direita
    public float delayTempo; //Delay da camera chegar no jogador
    
    private void FixedUpdate()
    {   Vector3 novaPosicao = jogador.position + new Vector3(0, 0, -10); // Afasta a camera do cenario
        novaPosicao = Vector3.Lerp(transform.position, novaPosicao, delayTempo); // Delay para a camera chegar no jogador
        transform.position = novaPosicao;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
        // Limita o posicionamento horizontal da camera
    }
}
