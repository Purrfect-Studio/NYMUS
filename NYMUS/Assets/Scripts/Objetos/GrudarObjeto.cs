using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrudarObjeto : MonoBehaviour
{
    public Transform posicaoPlayer;
    public float distanciaMinima = 1f; // Distância mínima entre o jogador e a caixa

    static public bool estaEmpurrando; // Verifica se o jogador está grudado em alguma caixa
    public bool estaGrudado; //Verifica se a caixa específica está sendo empurrada

    private Vector3 diferencaPosicao; // Armazena a diferença de posição inicial entre o jogador e a caixa

    void Start()
    {
        // Calcula a diferença de posição inicial entre o jogador e a caixa
        estaEmpurrando = false;
        estaGrudado = false;
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
        Debug.Log("Rodando");

        if (estaGrudado == true) // jogador esta empurrando ESTA caixa == true
        {
            Debug.Log("Desgrudou");
            Desgrudar(); //desgruda ESTA caixa
            return;
        }
        else
        {
            if (estaEmpurrando == false)
            {
                Debug.Log("Grudou");
                estaGrudado = true;  // Define que esta caixa está grudada no jogador
                estaEmpurrando = true; //
                diferencaPosicao = transform.position - posicaoPlayer.position;
            }
        }
       
    }

    public void Desgrudar()
    {
        // Define que a caixa não está mais grudada no jogador
        estaGrudado = false;
        estaEmpurrando = false; //jogador parou de empurrar
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
