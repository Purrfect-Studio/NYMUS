using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoTrojan : MonoBehaviour
{
    public GameObject[] pontosMovimentacao;
    public int indexPontoMovimento;
    public static bool podeMover;

    private Animator animacao;
    private bool podeAtivarAnimacaoSumir;

    public float cooldownDefinirNovaPosicao;
    public float cooldownRestanteDefinirNovaPosicao;

    private Vector3 posicaoAlvo;
    // Start is called before the first frame update
    void Start()
    {
        animacao = GetComponent<Animator>();
        cooldownRestanteDefinirNovaPosicao = cooldownDefinirNovaPosicao;
        definirPosicaoAlvo();
        podeMover = true;
        podeAtivarAnimacaoSumir = true;
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
}
