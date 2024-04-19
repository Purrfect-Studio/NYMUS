using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrudarObjeto : MonoBehaviour
{
    public Transform posicaoPlayer;
    public float distanciaMinima = 1f; // Dist�ncia m�nima entre o jogador e a caixa

    private bool estaGrudado = false; // Verifica se a caixa est� grudada no jogador

    private Vector3 diferencaPosicao; // Armazena a diferen�a de posi��o inicial entre o jogador e a caixa

    void Start()
    {
        // Calcula a diferen�a de posi��o inicial entre o jogador e a caixa
        diferencaPosicao = transform.position - posicaoPlayer.position;
    }

    void Update()
    {
        // Se a caixa estiver grudada, atualiza continuamente sua posi��o para seguir o jogador
        if (estaGrudado)
        {
            AtualizarPosicao();
        }
    }

    public void Grudar()
    {
        if (estaGrudado==true)
        {
            Desgrudar();
            return; 
        }
        // Define que a caixa est� grudada no jogador
        estaGrudado = true;

        // Calcula a diferen�a de posi��o inicial entre a caixa e o jogador
        diferencaPosicao = transform.position - posicaoPlayer.position;
    }

    public void Desgrudar()
    {
        // Define que a caixa n�o est� mais grudada no jogador
        estaGrudado = false;
    }

    private void AtualizarPosicao()
    {
        // Calcula a nova posi��o da caixa com base na posi��o atual do jogador e na diferen�a de posi��o inicial
        Vector3 novaPosicao = posicaoPlayer.position + diferencaPosicao;

        // Mant�m a dist�ncia m�nima entre o jogador e a caixa
        if (Vector3.Distance(novaPosicao, posicaoPlayer.position) < distanciaMinima)
        {
            novaPosicao = posicaoPlayer.position + (novaPosicao - posicaoPlayer.position).normalized * distanciaMinima;
        }

        // Define a nova posi��o da caixa
        transform.position = novaPosicao;
    }
}
