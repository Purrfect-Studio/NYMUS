using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InimigoDanoDeColisao : MonoBehaviour
{
    [Header("Collider 2D")]
    public Collider2D Collider2D;

    [Header("Jogador")]
    [SerializeField] private LayerMask layerJogador;
    public GameObject jogador;
    public float danoNoJogador;
    private VidaJogador vidaJogador;
    private VidaInimigo vidaInimigo;

    private void Start()
    {
        vidaInimigo = GetComponent<VidaInimigo>();
        jogador = GameObject.FindWithTag("Jogador");

        if(vidaInimigo.slime)
        {
            danoNoJogador = SeletorDeDificuldade.danoSlime;
        }else if(vidaInimigo.morcego)
        {
            danoNoJogador = SeletorDeDificuldade.danoMorcego;
        }else if(vidaInimigo.fantasma)
        {
            danoNoJogador = SeletorDeDificuldade.danoFantasma;
        }else if(vidaInimigo.inimigoProjetilParabola)
        {
            danoNoJogador = SeletorDeDificuldade.danoInimigoProjetilParabola;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colisaoJogador(collision))
        {
            CausarDanoNoJogador(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (colisaoJogador(collision))
        {
            CausarDanoNoJogador(collision);
        }
    }

    public void CausarDanoNoJogador(Collider2D collision)
    {
        vidaJogador = collision.GetComponent<VidaJogador>();
        if (vidaJogador != null && !VidaJogador.invulneravel)
        {
            if (transform.position.x <= jogador.transform.position.x)
            {
                VidaJogador.knockbackParaDireita = -1;
            }
            else
            {
                VidaJogador.knockbackParaDireita = 1;
            }
            vidaJogador.tomarDano(danoNoJogador);
        }
    }

    private bool colisaoJogador(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Jogador");
    }
}
