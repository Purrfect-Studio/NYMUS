using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Trampolim : MonoBehaviour
{
    private Animator animator;
    public float forcaPuloCima;
    public float forcaPuloLado;

    [Header("Direcao")]
    public bool cima;
    public bool direita;
    public bool esquerda;
    public bool diagonalDireita;
    public bool diagonalEsquerda;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            Rigidbody2D rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            animator.SetTrigger("AtivarTrampolim");
            if (cima)
            {
                rigidbody2D.AddForce(new Vector2(0f, forcaPuloCima), ForceMode2D.Impulse);
            }
            if (direita)
            {
                rigidbody2D.AddForce(new Vector2(1 * forcaPuloLado, 0f), ForceMode2D.Impulse);
            }
            if (esquerda)
            {
                rigidbody2D.AddForce(new Vector2(-1 * forcaPuloLado, 0f), ForceMode2D.Impulse);
            }
            if (diagonalDireita)
            {
                rigidbody2D.AddForce(new Vector2(1 * forcaPuloLado, forcaPuloCima), ForceMode2D.Impulse);
            }
            if (diagonalEsquerda)
            {
                rigidbody2D.AddForce(new Vector2(-1 * forcaPuloLado, forcaPuloCima), ForceMode2D.Impulse);
            }
        }
    }
}
