using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilL3H : MonoBehaviour
{
    [Header("Collider 2D")]
    public CircleCollider2D Collider2D;
    [Header("Inimigo")]
    [SerializeField] private LayerMask layerInimigo;
    private float dano;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D = GetComponent<CircleCollider2D>();
        dano = PlayerControlador.danoRanged;
        Physics2D.IgnoreLayerCollision(8, 8, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {           
            Collider2D colisaoInimigo = Physics2D.OverlapBox(Collider2D.bounds.center, Collider2D.bounds.size, 0, layerInimigo);
            VidaInimigo vidaInimigo = colisaoInimigo.GetComponent<VidaInimigo>();
            if (vidaInimigo != null && VidaInimigo.invulneravel == false)
            {
                vidaInimigo.tomarDano(dano);
            }
            
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Collider2D colisaoInimigo = Physics2D.OverlapBox(Collider2D.bounds.center, Collider2D.bounds.size, 0, layerInimigo);
            VidaBoss vidaBoss = colisaoInimigo.GetComponent<VidaBoss>();
            if (vidaBoss != null && VidaBoss.invulneravel == false)
            {
                vidaBoss.tomarDano(dano);
            }
        }
        if(collision.gameObject.CompareTag("Chao") || collision.gameObject.CompareTag("Inimigo") || collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
