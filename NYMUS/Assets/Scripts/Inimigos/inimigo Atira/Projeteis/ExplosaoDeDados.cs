using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosaoDeDados : MonoBehaviour
{
    public Animator animacao;
    public CircleCollider2D circleCollider2D;
    [SerializeField] private LayerMask layerJogador;

    public int dano;

    // Start is called before the first frame update
    void Start()
    {
        animacao = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            Collider2D colisaoJogador = Physics2D.OverlapBox(circleCollider2D.bounds.center, circleCollider2D.bounds.size, 0, layerJogador);
            if(colisaoJogador != null )
            {
                VidaJogador vidaJogador = colisaoJogador.GetComponent<VidaJogador>();
                if (vidaJogador != null && VidaJogador.invulneravel == false)
                {
                    vidaJogador.tomarDano(dano);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            Collider2D colisaoJogador = Physics2D.OverlapBox(circleCollider2D.bounds.center, circleCollider2D.bounds.size, 0, layerJogador);
            if (colisaoJogador != null)
            {
                VidaJogador vidaJogador = colisaoJogador.GetComponent<VidaJogador>();
                if (vidaJogador != null && VidaJogador.invulneravel == false)
                {
                    vidaJogador.tomarDano(dano);
                }
            }
        }
    }

    public void ativarCollider()
    {
        circleCollider2D.enabled = true;
    }

    public void desativarCollider()
    {
        circleCollider2D.enabled = false;
    }
}
