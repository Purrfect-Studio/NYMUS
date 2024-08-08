using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    [Header("Jogador")]
    [SerializeField] private LayerMask layerJogador; //Variavel de apoio para rechonhecer a layer do chao;
    public float danoNoJogador;
    private Rigidbody2D rigidbody2d;
    private CircleCollider2D circleCollider2d;
    private BoxCollider2D boxCollider2d;
    private Animator animator;
    public float velocidadeHorizontal;

    private void Start()
    {
        animator = GetComponent<Animator>();
        circleCollider2d = GetComponent<CircleCollider2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        boxCollider2d.enabled = false;
        animator.enabled = false;
        velocidadeHorizontal = AtirarEmVariosPontos.velocidadeTiroX;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            VidaJogador vidaJogador = collision.GetComponent<VidaJogador>();
            if (vidaJogador != null && !VidaJogador.invulneravel)
            {
                if (transform.position.x <= collision.transform.position.x)
                {
                    VidaJogador.knockbackParaDireita = -1;
                }
                else
                {
                    VidaJogador.knockbackParaDireita = 1;
                }
                vidaJogador.tomarDano(danoNoJogador);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Chao"))
        {
            rigidbody2d.gravityScale = 0;
            rigidbody2d.velocity = new Vector2(velocidadeHorizontal, 0);
            transformacao();
        }
    }

    public void transformacao()
    {
        float posicaox = transform.position.x;
        float posicaoy = transform.position.y;
        transform.position = new Vector2(posicaox, posicaoy+0.2f);
        circleCollider2d.enabled = false;
        boxCollider2d.enabled = true;
        animator.enabled = true;
    }

}
