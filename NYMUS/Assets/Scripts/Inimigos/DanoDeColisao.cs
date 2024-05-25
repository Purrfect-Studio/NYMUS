using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DanoDeColisao : MonoBehaviour
{
    [Header("Collider 2D")]
    public Collider2D Collider2D;

    [Header("Jogador")]
    [SerializeField] private LayerMask layerJogador; //Variavel de apoio para rechonhecer a layer do chao;
    public GameObject jogador;
    public float danoNoJogador;

    [Header("Inimigo")]
    [SerializeField] private LayerMask layerInimigo;
    public float danoNoInimigo;

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
    }
    // Update is called once per frame
    void Update()
    {
        if (colisaoJogador() == true)
        {
            Collider2D colisaoJogador = Physics2D.OverlapBox(Collider2D.bounds.center, Collider2D.bounds.size, 0, layerJogador);
            VidaJogador vidaJogador = colisaoJogador.GetComponent<VidaJogador>();
            if(vidaJogador != null && VidaJogador.invulneravel == false)
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

        if(colisaoInimigo() == true)
        {
            Collider2D colisaoInimigo = Physics2D.OverlapBox(Collider2D.bounds.center, Collider2D.bounds.size, 0, layerInimigo);
            if (colisaoInimigo != null)
            {
                VidaInimigo inimigo = colisaoInimigo.GetComponent<VidaInimigo>();
                if (inimigo != null && VidaInimigo.invulneravel == false)
                {
                    //Debug.Log("Dano no Inimigo:" + colisaoInimigo.name);
                    inimigo.tomarDano(danoNoInimigo);
                }
            }
        }
    }

    private bool colisaoJogador()
    {
        RaycastHit2D colisao = Physics2D.BoxCast(Collider2D.bounds.center, Collider2D.bounds.size, 0, Vector2.down, 0.05f, layerJogador); // Cria um segundo box collider para reconhecer o jogador
        return colisao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no jogador
    }

    private bool colisaoInimigo()
    {
        RaycastHit2D colisao = Physics2D.BoxCast(Collider2D.bounds.center, Collider2D.bounds.size, 0, Vector2.down, 0.05f, layerInimigo); // Cria um segundo box collider para reconhecer o jogador
        return colisao.collider != null; //Retorna um valor verdadeiro, dizendo que encostou no jogador
    }
}
