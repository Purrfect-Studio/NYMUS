using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
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
            }
        }
    }
}
