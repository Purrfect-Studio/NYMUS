using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoTrojan : MonoBehaviour
{
    public GameObject[] pontosMovimentacao;
    private int indexPontoMovimento;
    public static bool podeMover;
    public bool copia = false;
    public bool copia1 = false;
    public bool copia2 = false;
    public float cura;

    public ControladorTrojan controladorTrojan;
    private GameObject Trojan;
    private VidaBoss vidaBoss;
    private Rigidbody2D rigidbody2d;

    private Animator animacao;
    public static bool executarAnimacaoAtaqueCopia;
    private bool podeAtivarAnimacaoSumir;
    public float tempoParaLevantarCopia;

    public float cooldownDefinirNovaPosicao;
    public float cooldownDefinirNovaPosicaoFrenesi;
    private float cooldownRestanteDefinirNovaPosicao;

    private Vector3 posicaoAlvo;
    // Start is called before the first frame update
    void Start()
    {
        Trojan = GameObject.FindGameObjectWithTag("Boss");
        vidaBoss = Trojan.GetComponent<VidaBoss>();
        Physics2D.IgnoreLayerCollision(9, 13, true);
        animacao = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        if(!copia)
        {
            vidaBoss = GetComponent<VidaBoss>();
        }

        cooldownDefinirNovaPosicao = SeletorDeDificuldade.cooldownDefinirNovaPosicaoTrojan;
        cooldownDefinirNovaPosicaoFrenesi = SeletorDeDificuldade.cooldownDefinirNovaPosicaoFrenesiTrojan;

        cooldownRestanteDefinirNovaPosicao = cooldownDefinirNovaPosicao;
        definirPosicaoAlvo();

        podeAtivarAnimacaoSumir = true;
        Physics2D.IgnoreLayerCollision(9, 9, true);
        tempoParaLevantarCopia = vidaBoss.tempoParaLevantar;

        if (copia1)
        {
            cura = SeletorDeDificuldade.curaCloneRosaTrojan;
        }
        if(copia2)
        {
            cura = SeletorDeDificuldade.curaCloneAzulTrojan;
        }
    }

    void Update()
    {
        if(podeMover)
        {
            if (transform.position == pontosMovimentacao[indexPontoMovimento].transform.position)
            {
                cooldownRestanteDefinirNovaPosicao -= Time.deltaTime;
                if (cooldownRestanteDefinirNovaPosicao < 0)
                {
                    definirPosicaoAlvo();
                    cooldownRestanteDefinirNovaPosicao = cooldownDefinirNovaPosicao;
                    podeAtivarAnimacaoSumir = true;
                    animacao.SetTrigger("Aparecer");
                }
            }
            mover();
        }
        else
        {
            cooldownRestanteDefinirNovaPosicao = cooldownDefinirNovaPosicao;
            podeAtivarAnimacaoSumir = true;
        }
        if(cooldownRestanteDefinirNovaPosicao <= 0.5f && podeAtivarAnimacaoSumir == true)
        {
            podeAtivarAnimacaoSumir = false;
            animacao.SetTrigger("Sumir");
        }
        if (!copia)
        {
            if (vidaBoss.frenesi)
            {
                cooldownDefinirNovaPosicao = cooldownDefinirNovaPosicaoFrenesi;
            }
        }
        if (copia && executarAnimacaoAtaqueCopia)
        {
            executarAnimacaoAtaqueCopia = false;
            animacao.SetTrigger("Atacar");
        }
    }

    public void mover()
    {
        transform.position = posicaoAlvo;
        //transform.position = Vector3.MoveTowards(transform.position, posicaoAlvo, velocidade * Time.deltaTime);
    }

    public void definirPosicaoAlvo()
    {
        indexPontoMovimento = definirIndex();
        posicaoAlvo = pontosMovimentacao[indexPontoMovimento].transform.position;
    }

    public int definirIndex()
    {
        return Random.Range(0, pontosMovimentacao.Length-1);
    }

    public void derrubarCopiaTrojan()
    {
        vidaBoss.curarTrojan(cura);
        rigidbody2d.gravityScale = 8f;
        podeMover = false;
        StartCoroutine("LevantarCopiaTrojan");
    }

    IEnumerator LevantarCopiaTrojan()
    {
        yield return new WaitForSeconds(tempoParaLevantarCopia);
        rigidbody2d.gravityScale = 0f;
        podeMover = true;
    }

}
