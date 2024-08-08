using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosaoDeDados : MonoBehaviour
{
    public Animator animacao;
    public CircleCollider2D circleCollider2D;
    [SerializeField] private LayerMask layerJogador;

    public float dano;

    // Start is called before the first frame update
    void Start()
    {
        dano = BossControlador.danoExplosaoDeDados;
        animacao = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            VidaJogador vidaJogador = collision.GetComponent<VidaJogador>();
            if (vidaJogador != null && !VidaJogador.invulneravel)
            {
                vidaJogador.tomarDano(dano);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            VidaJogador vidaJogador = collision.GetComponent<VidaJogador>();
            if (vidaJogador != null && !VidaJogador.invulneravel)
            {
                vidaJogador.tomarDano(dano);
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
