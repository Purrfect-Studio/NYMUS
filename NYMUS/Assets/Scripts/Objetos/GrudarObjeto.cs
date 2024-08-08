using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrudarObjeto : MonoBehaviour
{
    [Header("Jogador")]
    public Transform posicaoJogador;
    public GameObject jogador;
    public PlayerControlador playerControlador;

    [Header("Bool de apoio")]
    public static bool jogadorEstaGrudadoEmUmaCaixa; // Verifica se o jogador está grudado em alguma caixa
    public bool caixaEstaSendoEmpurrada; //Verifica se a caixa específica está sendo empurrada

    [Header("Caixa")]
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private float gravidade;
    private float posicaoX;
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        playerControlador = jogador.GetComponent<PlayerControlador>();
        posicaoJogador = jogador.GetComponent<Transform>();

        rigidbody2d = GetComponent<Rigidbody2D>();
        gravidade = rigidbody2d.gravityScale;
        boxCollider2d = GetComponent<BoxCollider2D>();

        jogadorEstaGrudadoEmUmaCaixa = false;
        caixaEstaSendoEmpurrada = false;
        // Calcula a diferença de posição inicial entre o jogador e a caixa
        //diferencaPosicao = transform.position - posicaoJogador.position;
    }

    private void Update()
    {
        if(caixaEstaSendoEmpurrada)
        {
            posicaoX = transform.position.x;
            transform.position = new Vector2(posicaoX, jogador.transform.position.y);
        }
    }

    public void Grudar()
    {
        //Debug.Log("Rodando");
        if (caixaEstaSendoEmpurrada == true) // jogador esta empurrando ESTA caixa == true
        {
            //Debug.Log("Desgrudou");
            Desgrudar(); //desgruda ESTA caixa
            return;
        }
        else
        {
            if (jogadorEstaGrudadoEmUmaCaixa == false)
            {
                Debug.Log("Grudou");
                caixaEstaSendoEmpurrada = true;  // Define que esta caixa está grudada no jogador
                jogadorEstaGrudadoEmUmaCaixa = true;
                playerControlador.rigidBody2D.gravityScale = 50;
                //rigidbody2d.gravityScale = 0;
                if((transform.position.x - posicaoJogador.position.x < 0) && PlayerControlador.olhandoDireita == true || (transform.position.x - posicaoJogador.position.x > 0) && PlayerControlador.olhandoDireita == false)
                {
                    PlayerControlador.olhandoDireita = !PlayerControlador.olhandoDireita;
                    posicaoJogador.transform.Rotate(0f, 180f, 0f);
                }
                transform.parent = jogador.transform;
            }
        }
       
    }

    public void Desgrudar()
    {
        // Define que a caixa não está mais grudada no jogador
        caixaEstaSendoEmpurrada = false;
        jogadorEstaGrudadoEmUmaCaixa = false; //jogador parou de empurrar
        transform.parent = null;
        playerControlador.rigidBody2D.gravityScale = playerControlador.gravidade;
        //rigidbody2d.gravityScale = gravidade;
    }
}
