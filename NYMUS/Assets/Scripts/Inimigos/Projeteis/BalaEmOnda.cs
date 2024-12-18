using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalaEmOnda : MonoBehaviour
{
    public Rigidbody2D rb;
    private float velocidadeX;
    [Header("Jogador")]
    [SerializeField] private LayerMask layerJogador; //Variavel de apoio para rechonhecer a layer do chao;
    public float danoNoJogador;
    [Header("Collider 2D")]
    public Collider2D Collider2D;

    private void Start()
    {   
        if(GameObject.FindWithTag("Boss") == true)
        {
            velocidadeX = BossControlador.velocidadeFirewallX;
        }
        else
        {
            velocidadeX = AtirarEmVariosPontos.velocidadeTiroX;
        }
        
    }
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
    void Update()
    {
        rb.velocity = new Vector2(velocidadeX, Mathf.Cos(transform.position.x) * 10);
    }
}
