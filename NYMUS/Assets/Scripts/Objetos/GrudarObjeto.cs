using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrudarObjeto : MonoBehaviour
{
    [Header("Jogador")]
    public Transform posicaoJogador;

    [Header("Distancia Minima")]
    public float distanciaMinimaJogadorCaixa = 1f; // Dist�ncia m�nima entre o jogador e a caixa

    [Header("Bool de apoio")]
    public static bool estaEmpurrando; // Verifica se o jogador est� grudado em alguma caixa
    public bool estaGrudado; //Verifica se a caixa espec�fica est� sendo empurrada

    private Vector3 diferencaPosicao; // Armazena a diferen�a de posi��o inicial entre o jogador e a caixa

    void Start()
    {
        // Calcula a diferen�a de posi��o inicial entre o jogador e a caixa
        estaEmpurrando = false;
        estaGrudado = false;
        diferencaPosicao = transform.position - posicaoJogador.position;
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
                estaGrudado = true;  // Define que esta caixa est� grudada no jogador
                estaEmpurrando = true;
                diferencaPosicao = transform.position - posicaoJogador.position;
                if((transform.position.x - posicaoJogador.position.x < 0) && PlayerControlador.olhandoDireita == true || (transform.position.x - posicaoJogador.position.x > 0) && PlayerControlador.olhandoDireita == false)
                {
                    PlayerControlador.olhandoDireita = !PlayerControlador.olhandoDireita;
                    posicaoJogador.transform.Rotate(0f, 180f, 0f);
                }
            }
        }
       
    }

    public void Desgrudar()
    {
        // Define que a caixa n�o est� mais grudada no jogador
        estaGrudado = false;
        estaEmpurrando = false; //jogador parou de empurrar
    }

    private void AtualizarPosicao()
    {
        // Calcula a nova posi��o da caixa com base na posi��o atual do jogador e na diferen�a de posi��o inicial
        Vector3 novaPosicao = posicaoJogador.position + diferencaPosicao;

        // Mant�m a dist�ncia m�nima entre o jogador e a caixa
        if (Vector3.Distance(novaPosicao, posicaoJogador.position) < distanciaMinimaJogadorCaixa)
        {
            novaPosicao = posicaoJogador.position + (novaPosicao - posicaoJogador.position).normalized * distanciaMinimaJogadorCaixa;
        }

        // Define a nova posi��o da caixa
        transform.position = novaPosicao;
    }
}
