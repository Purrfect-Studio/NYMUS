using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptBala : MonoBehaviour
{
    [Header("Jogador")]
    [SerializeField] private LayerMask layerJogador; //Variavel de apoio para rechonhecer a layer do chao;
    public float danoNoJogador;
    [Header("Collider 2D")]
    public Collider2D Collider2D;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            GameObject jogador = GameObject.FindWithTag("Jogador");
            Collider2D colisaoJogador = Physics2D.OverlapBox(Collider2D.bounds.center, Collider2D.bounds.size, 0, layerJogador);
            VidaJogador vidaJogador = colisaoJogador.GetComponent<VidaJogador>();
            if (vidaJogador != null && VidaJogador.invulneravel == false)
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
        if (collision != null)
        {
            Destroy(gameObject);
        }
    }
}
