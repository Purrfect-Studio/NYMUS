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
    [Header("Layer do Chao")]
    [SerializeField] private LayerMask layerChao;
    [Header("Layer da Plataforma")]
    [SerializeField] private LayerMask layerPlataforma;
    [Header("BoxCollider")]
    private BoxCollider2D boxCollider2D; 

    private bool contadorParaAnimacaoDeTomarDano;
    //private bool pulei;
    //private bool estavaNaPlataforma;

    private void Start()
    {
        playerControlador = this.GetComponent<PlayerControlador>();
        animacaoPontoDeAtaque = playerControlador.pontoDeAtaque.GetComponent<Animator>();
        vidaJogador = this.GetComponent<VidaJogador>();
        boxCollider2D = this.GetComponent<BoxCollider2D>();
        contadorParaAnimacaoDeTomarDano = false;
        //pulei = false;
        //estavaNaPlataforma = false;
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
            //pulei = true;
        }
        else
        {
            animacao.SetBool("estaPulando", false);
            //if (pulei == true)
            //{
                if (estaChao())
                {
                    animacao.SetBool("estaChao", true);
                    //pulei = false;
                    //estavaNaPlataforma = false;
                }
                /*if (estaPlataforma() && estavaNaPlataforma == true)
                {
                    animacao.SetBool("estaPlataforma", true);
                    pulei = false;
                    estavaNaPlataforma = false;
                }else if(estaPlataforma() && estavaNaPlataforma == false)
                {
                    StartCoroutine(DesativarPuloCaindoNaPlataforma());
                }*/
            //}
        }
        /*if (estaPlataforma() && estavaNaPlataforma == false && animacao.GetBool("estaPlataforma") == true)
        {
            estavaNaPlataforma = true;
        }*/
    }

    /*IEnumerator DesativarPuloCaindoNaPlataforma()
    {
        yield return new WaitForSeconds(0.3f);
        if (estaChao())
        {
            animacao.SetBool("estaChao", true);
        }
        if (estaPlataforma())
        {
            animacao.SetBool("estaPlataforma", true);
        }
        pulei = false;
        estavaNaPlataforma = false;
    }*/

    private bool estaChao()
    {
        RaycastHit2D chao = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.3f, layerChao); // Cria um segundo box collider para reconhecer o chao
        return chao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }

    /*private bool estaPlataforma()
    {
        RaycastHit2D plataforma = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.3f, layerPlataforma); // Cria um segundo box collider para reconhecer o chao
        return plataforma.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no chao
    }*/

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
        if (vidaJogador.vidaAtual <= 0)
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
