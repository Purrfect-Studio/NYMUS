using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacoesL3H : MonoBehaviour
{
    [Header("Animator")]
    public Animator animacao;
    [Header("Acesso a outros Scripts do L3H")]
    private PlayerControlador playerControlador;
    private VidaJogador vidaJogador;
    private Animator animacaoPontoDeAtaque;

    private int contadorParaAnimacaoDeTomarDano;

    private void Start()
    {
        playerControlador = this.GetComponent<PlayerControlador>();
        animacaoPontoDeAtaque = playerControlador.pontoDeAtaque.GetComponent<Animator>();
        vidaJogador = this.GetComponent<VidaJogador>();
        contadorParaAnimacaoDeTomarDano = 0;
    }
    // Update is called once per frame
    void Update()
    {
        tomarDano();
        if (PlayerControlador.podeMover == false)
        {
            animacao.SetBool("estaAndando", false);
            animacao.SetBool("estaPulando", false);
        }
        else
        {
            andar();
            empurrarCaixa();
            morrer();
            if (GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == false)
            {
                atacar();
                pular();
            }
        }
    }

    void andar()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            animacao.SetBool("estaAndando", true);
        }
        else
        {
            animacao.SetBool("estaAndando", false);
        }
    }

    void atacar()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animacao.SetTrigger("estaAtacando");
            animacaoPontoDeAtaque.SetTrigger("estaAtacando");
        }
    }

    void pular()
    {
        if (PlayerControlador.estaPulando == true)
        {
            animacao.SetBool("estaPulando", true);
        }
        else
        {
            animacao.SetBool("estaPulando", false);
        }
    }

    void empurrarCaixa()
    {
        if (GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == true)
        {
            animacao.SetBool("EmpurrandoCaixa", true);
        }
        else
        {
            animacao.SetBool("EmpurrandoCaixa", false);
        }
    }

    void morrer()
    {
        if (vidaJogador.vidaAtual <= 0)
        {
            animacao.SetTrigger("estaMorto");
        }
    }

    void tomarDano()
    {
        if (vidaJogador.tomeiDano == true && contadorParaAnimacaoDeTomarDano == 1)
        {
            animacao.SetTrigger("tomarDano");
            contadorParaAnimacaoDeTomarDano = 0;
        }
        if(vidaJogador.tomeiDano == false && contadorParaAnimacaoDeTomarDano == 0)
        {
            contadorParaAnimacaoDeTomarDano = 1;
        }
    }
}
