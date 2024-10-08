using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{
    [Header("Email Bom ou Ruim")]
    public bool fishing;
    public bool email;

    private float velocidade;

    private bool olhandoParaEsquerda;
    private irAteJogador irAteJogador;
    private VidaInimigo vidaInimigo;
    private GameObject jogador;
    private MoverEntrePontos moverEntrePontos;
    private ProcurarJogador procurarJogador;
    private InimigoDanoDeColisao danoDeColisao;
    private CuraCoracao curaCoracao;
    private Animator animacao;
    // Start is called before the first frame update
    void Start()
    {
        irAteJogador = GetComponent<irAteJogador>();
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        vidaInimigo = GetComponent<VidaInimigo>();
        procurarJogador = GetComponent<ProcurarJogador>();
        danoDeColisao = GetComponent<InimigoDanoDeColisao>();
        curaCoracao = GetComponent<CuraCoracao>();
        animacao = GetComponent<Animator>();

        velocidade = irAteJogador.velocidade;

        if(GetComponent<MoverEntrePontos>() != null)
        {
            moverEntrePontos = GetComponent<MoverEntrePontos>();
        }
        if(fishing)
        {
            danoDeColisao.enabled = true;
            curaCoracao.cura = 0;
            curaCoracao.enabled = false;

        }
        if (email)
        {
            danoDeColisao.danoNoJogador = 0;
            danoDeColisao.enabled = false;
            curaCoracao.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControlador.podeMover == false)
        {
            irAteJogador.velocidade = 0;
        }
        else
        {
            irAteJogador.velocidade = velocidade;
        }

        if (vidaInimigo.fantasma == true)
        {
            if ((transform.position.x - jogador.transform.position.x < 0 && PlayerControlador.olhandoDireita || transform.position.x - jogador.transform.position.x > 0 && !PlayerControlador.olhandoDireita) && fishing || (transform.position.x - jogador.transform.position.x > 0 && PlayerControlador.olhandoDireita || transform.position.x - jogador.transform.position.x < 0 && !PlayerControlador.olhandoDireita) && email)
            {
                irAteJogador.enabled = true;
                if(fishing)
                {
                    animacao.SetBool("AbrirEmail", false);
                }
                if(email)
                {
                    animacao.SetBool("AbrirEmail", true);
                }
            }
            else
            {
                irAteJogador.enabled = false;
                if (fishing)
                {
                    animacao.SetBool("AbrirEmail", true);
                }
                if (email)
                {
                    animacao.SetBool("AbrirEmail", false);
                }
            }

            if(moverEntrePontos != null)
            {
                if (procurarJogador.procurarJogador())
                {                   
                    moverEntrePontos.enabled = false;
                }
                else
                {                    
                    moverEntrePontos.enabled = true;                    
                }
            }
            
        }
        if (!olhandoParaEsquerda && transform.position.x - jogador.transform.position.x < 0 || olhandoParaEsquerda && transform.position.x - jogador.transform.position.x > 0)
        {
            flipSprite();
        }
    }

    public void flipSprite()
    {
        olhandoParaEsquerda = !olhandoParaEsquerda;
        transform.Rotate(0f, 180f, 0f);
    }

    public void desativarDepoisDeUmTempo()
    {
        StartCoroutine("Desativar");
    }

    IEnumerator Desativar()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
