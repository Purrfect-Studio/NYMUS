using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePulando : MonoBehaviour
{
    [Header("Configuraçôes")]
    public float velocidade;
    public float forcaPulo;
    private float direcao;
    public float cooldownPulo;
    private float cooldownRestantePulo;
    public bool olhandoParaEsquerda;

    [Header("Elementos do slime")]
    private Rigidbody2D rigidbody2d;
    private ProcurarJogador procurarJogador;
    private Animator animacao;

    [Header("Jogador")]
    private GameObject jogador;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        procurarJogador = GetComponent<ProcurarJogador>();
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        animacao = GetComponent<Animator>();
        cooldownRestantePulo = cooldownPulo;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownRestantePulo -= Time.deltaTime;
        if (procurarJogador.procurarJogador() == true)
        {
            if (cooldownRestantePulo <= 0)
            {
                cooldownRestantePulo = cooldownPulo;
                pular();
            }
        }
        else
        {
            cooldownRestantePulo = cooldownPulo;
        }
        if (olhandoParaEsquerda && direcao == 1 || !olhandoParaEsquerda && direcao == -1)
        {
            flipSprite();
        }
    }

    public void flipSprite()
    {
        olhandoParaEsquerda = !olhandoParaEsquerda;
        transform.Rotate(0f, 180f, 0f);
    }

    public float direcaoDoPulo()
    {
        if(transform.position.x - jogador.transform.position.x < 0) // esta a esquerda do l3h
        {
            direcao = 1;
        }else if(transform.position.x - jogador.transform.position.x > 0) // esta a direita do l3h
        {
            direcao = -1;
        }
        return direcao;
    }

    public void pular()
    {
        animacao.SetTrigger("Pular");
        Vector2 movimentacao = new Vector2(velocidade * direcaoDoPulo(), forcaPulo);
        rigidbody2d.AddForce(movimentacao, ForceMode2D.Impulse);
    }
}
