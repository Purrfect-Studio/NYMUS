using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjecaoDeDados : MonoBehaviour
{
    public Animator animacao;
    public BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask layerJogador;

    public float dano;

    [Header("Boss")]
    public bool Virut;
    public bool Trojan;

    [Header("Apenas Trojan")]
    public bool laserVerde;
    public bool laserEspinho;

    void Start()
    {
        if (Virut)
        {
            dano = BossControlador.danoInjecaoDeDados;
        }else if (Trojan)
        {
            if (laserVerde)
            {
                dano = SeletorDeDificuldade.danoLaserTrojan;
            }
            if (laserEspinho)
            {
                dano = SeletorDeDificuldade.danoLaserEspinhoTrojan;
            }
        }
        animacao = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        boxCollider2D.enabled = false;
        if(animacao != null)
        {
            animacao.SetTrigger("InjecaoDeDados");
        }
    }

    public void ativarCollider()
    {
        boxCollider2D.enabled = true;
    }

    public void desativarCollider()
    {
        boxCollider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            // Obtem o componente VidaJogador diretamente do objeto que colidiu
            VidaJogador vidaJogador = collision.GetComponent<VidaJogador>();
            if (vidaJogador != null && !VidaJogador.invulneravel)
            {
                vidaJogador.tomarDano(dano);
            }
        }
    }
}
