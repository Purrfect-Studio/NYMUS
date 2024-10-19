using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InimigoDanoDeColisao : MonoBehaviour
{
    [Header("Jogador")]
    [SerializeField] private LayerMask layerJogador;
    public GameObject jogador;
    public float danoNoJogador;
    private VidaJogador vidaJogador;
    public VidaInimigo vidaInimigo;

    [Header("Cavaleiro")]
    public bool cavaleiro;

    private void Start()
    {
        if(vidaInimigo == null)
        {
            vidaInimigo = GetComponent<VidaInimigo>();
        }
        jogador = GameObject.FindWithTag("Jogador");
        if(vidaInimigo != null)
        {
            if (vidaInimigo.slime)
            {
                danoNoJogador = SeletorDeDificuldade.danoSlime;
            }
            else if (vidaInimigo.morcego)
            {
                danoNoJogador = SeletorDeDificuldade.danoMorcego;
            }
            else if (vidaInimigo.fantasma)
            {
                danoNoJogador = SeletorDeDificuldade.danoFantasma;
            }
            else if (vidaInimigo.inimigoProjetilParabola)
            {
                danoNoJogador = SeletorDeDificuldade.danoInimigoProjetilParabola;
            }else if (vidaInimigo.touro)
            {
                danoNoJogador = SeletorDeDificuldade.danoTouro;
            }
        }
        if (cavaleiro)
        {
            danoNoJogador = SeletorDeDificuldade.danoCavaleiro;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colisaoJogador(collision))
        {
            CausarDanoNoJogador(collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (colisaoJogador2(collision))
        {
            CausarDanoNoJogador2(collision);
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (colisaoJogador(collision))
        {
            CausarDanoNoJogador(collision);
        }
    }*/

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
            if(danoNoJogador != 0)
            {
                vidaJogador.tomarDano(danoNoJogador);
            }
        }
    }

    public void CausarDanoNoJogador2(Collision2D collision)
    {
        vidaJogador = collision.gameObject.GetComponent<VidaJogador>();
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

    private bool colisaoJogador2(Collision2D collision)
    {
        return collision.gameObject.CompareTag("Jogador");
    }
}
