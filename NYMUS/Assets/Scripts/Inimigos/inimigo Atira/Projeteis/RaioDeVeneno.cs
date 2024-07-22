using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioDeVeneno : MonoBehaviour
{
    public Animator animacao;
    public BoxCollider2D boxCollider2D;
    public VidaJogador vidaJogador;
    [SerializeField] private LayerMask layerJogador;

    public float dano;
    public int veneno;
    // Start is called before the first frame update
    void Start()
    {
        animacao = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        vidaJogador = GetComponent<VidaJogador>();

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
            Collider2D colisaoJogador = Physics2D.OverlapBox(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, layerJogador);
            VidaJogador vidaJogador = colisaoJogador.GetComponent<VidaJogador>();
            if (vidaJogador != null && VidaJogador.invulneravel == false)
            {
                vidaJogador.tomarDano(dano);
                vidaJogador.envenenar(veneno);
            }
        }
    }
}
