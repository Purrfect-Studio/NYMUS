using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DanoDeColisao : MonoBehaviour
{
    [Header("Collider 2D")]
    public Collider2D Collider2D;

    [Header("Jogador")]
    [SerializeField] private LayerMask layerJogador;
    public GameObject jogador;
    public float danoNoJogador;
    private VidaJogador vidaJogador;

    [Header("Inimigo")]
    public bool causarDanoNoInimigo;
    [SerializeField] private LayerMask layerInimigo;
    public float danoNoInimigo;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colisaoJogador(collision))
        {
            CausarDanoNoJogador(collision);
        }

        if (colisaoInimigo(collision) && causarDanoNoInimigo)
        {
            CausarDanoNoInimigo(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (colisaoJogador(collision))
        {
            CausarDanoNoJogador(collision);
        }

        if (colisaoInimigo(collision) && causarDanoNoInimigo)
        {
            CausarDanoNoInimigo(collision);
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

    public void CausarDanoNoInimigo(Collider2D collision)
    {
        VidaInimigo inimigo = collision.GetComponent<VidaInimigo>();
        if (inimigo != null && !VidaInimigo.invulneravel)
        {
            inimigo.tomarDano(danoNoInimigo);
        }
    }

    private bool colisaoJogador(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Jogador");
    }

    private bool colisaoInimigo(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Inimigo");
    }
}
