using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{
    private Animator animator;
    public float forcaPulo;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jogador"))
        {
            animator.SetTrigger("AtivarTrampolim");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
        }
    }
}
