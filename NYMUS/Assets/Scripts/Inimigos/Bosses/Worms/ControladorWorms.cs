using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorWorms : MonoBehaviour
{
    [Header("Dano")]
    public float dano;

    private Vector2 posicaoIncial;
    // Start is called before the first frame update
    void Start()
    {
        posicaoIncial = transform.position;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (VidaJogador.estaMorto)
        {
            transform.position = posicaoIncial;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador")) 
        {
            VidaJogador vidaJogador = collision.gameObject.GetComponent<VidaJogador>();
            if(vidaJogador != null && !VidaJogador.invulneravel)
            {
                vidaJogador.tomarDano(dano);
            }
        }

        if (collision.gameObject.CompareTag("Inimigo"))
        {
            VidaInimigo vidaInimigo = collision.gameObject.GetComponent<VidaInimigo>();
            if (vidaInimigo != null)
            {
                vidaInimigo.tomarDano(dano);
            }
        }
    }
}
