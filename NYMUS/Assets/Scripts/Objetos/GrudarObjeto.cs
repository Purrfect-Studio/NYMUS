using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrudarObjeto : MonoBehaviour
{
    public Transform posicaoPlayer;
    public float distanciaMinima = 1f; // Distância mínima entre o jogador e a caixa

    private bool estaGrudado = false; // Verifica se a caixa está grudada no jogador

    private Vector3 diferencaPosicao; // Armazena a diferença de posição inicial entre o jogador e a caixa

    void Start()
    {
        // Calcula a diferença de posição inicial entre o jogador e a caixa
        diferencaPosicao = transform.position - posicaoPlayer.position;
    }

    void Update()
    {
        // Se a caixa estiver grudada, atualiza continuamente sua posição para seguir o jogador
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
        // Define que a caixa está grudada no jogador
        estaGrudado = true;

        // Calcula a diferença de posição inicial entre a caixa e o jogador
        diferencaPosicao = transform.position - posicaoPlayer.position;
    }

    public void Desgrudar()
    {
        // Define que a caixa não está mais grudada no jogador
        estaGrudado = false;
    }

    private void AtualizarPosicao()
    {
        // Calcula a nova posição da caixa com base na posição atual do jogador e na diferença de posição inicial
        Vector3 novaPosicao = posicaoPlayer.position + diferencaPosicao;

        // Mantém a distância mínima entre o jogador e a caixa
        if (Vector3.Distance(novaPosicao, posicaoPlayer.position) < distanciaMinima)
        {
            novaPosicao = posicaoPlayer.position + (novaPosicao - posicaoPlayer.position).normalized * distanciaMinima;
        }

        // Define a nova posição da caixa
        transform.position = novaPosicao;
    }
}
