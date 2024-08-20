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
    public PlayerData playerData;
    [Header("Layer do Chao")]
    [SerializeField] private LayerMask layerChao;
    [Header("Layer da Plataforma")]
    [SerializeField] private LayerMask layerPlataforma;
    [Header("BoxCollider")]
    private BoxCollider2D boxCollider2D; 

    private bool contadorParaAnimacaoDeTomarDano;

    private void Start()
    {
        playerControlador = this.GetComponent<PlayerControlador>();
        animacaoPontoDeAtaque = playerControlador.pontoDeAtaque.GetComponent<Animator>();
        vidaJogador = this.GetComponent<VidaJogador>();
        boxCollider2D = this.GetComponent<BoxCollider2D>();
        contadorParaAnimacaoDeTomarDano = false;
    }
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) /*Input.GetAxisRaw("Horizontal") != 0*/)
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            animacao.SetTrigger("estaAtacando");
            animacaoPontoDeAtaque.SetTrigger("estaAtacando");
        }
    }

    void pular()
    {
        if (PlayerControlador.estaPulando == true)
        {
            animacao.SetBool("estaChao", false);
            animacao.SetBool("estaPlataforma", false);
            animacao.SetBool("estaPulando", true);
        }
        else
        {
            animacao.SetBool("estaPulando", false);
            if (estaChao())
            {
                animacao.SetBool("estaChao", true);
            }
        }
    }

    private bool estaChao()
    {
        RaycastHit2D chao = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.3f, layerChao); // Cria um segundo box collider para reconhecer o chao
        return chao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }

    void empurrarCaixa()
    {
        if (GrudarObjeto.jogadorEstaGrudadoEmUmaCaixa == true)
        {
            animacao.SetBool("empurrandoCaixa", true);
        }
        else
        {
            animacao.SetBool("empurrandoCaixa", false);
        }
    }

    void morrer()
    {
        if (playerData.vidaAtual <= 0)
        {
            animacao.SetTrigger("estaMorto");
        }
    }

    void tomarDano()
    {
        if (vidaJogador.tomeiDano == true && contadorParaAnimacaoDeTomarDano)
        {
            animacao.SetTrigger("tomarDano");
            contadorParaAnimacaoDeTomarDano = false;
        }
        if(vidaJogador.tomeiDano == false && !contadorParaAnimacaoDeTomarDano)
        {
            contadorParaAnimacaoDeTomarDano = true;
        }
    }
}
