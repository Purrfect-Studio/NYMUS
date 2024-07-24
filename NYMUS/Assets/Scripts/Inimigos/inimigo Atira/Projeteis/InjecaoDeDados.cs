using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjecaoDeDados : MonoBehaviour
{
    public Animator animacao;
    public BoxCollider2D boxCollider2D;
    public VidaJogador vidaJogador;
    [SerializeField] private LayerMask layerJogador;

    public float dano;
    public int veneno;

    void Start()
    {
        animacao = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        boxCollider2D.enabled = false;
        animacao.SetTrigger("RaioDeVeneno");
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
            // Obtém o componente VidaJogador diretamente do objeto que colidiu
            VidaJogador vidaJogador = collision.GetComponent<VidaJogador>();
            if (vidaJogador != null && !VidaJogador.invulneravel)
            {
                vidaJogador.tomarDano(dano);
                vidaJogador.envenenar(veneno);
            }
        }
    }
}
