using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaInimigo : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima;
    public float vidaAtual;

    [Header("GameObject do inimigo")]
    public GameObject gameObject;

    [Header("RigidBody")]
    public Rigidbody2D rb;   // rb = rigidbody

    [Header("Knockback")]
    public float forcaKnockbak;
    public static int knockbackParaDireita;

    [Header("Animator")]
    public Animator animacao;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima; // define a vida atual como a vida maxima
        animacao.SetBool("estaMorto", false); // desativa a animacao de morte
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaAtual <= 0)
        {
            morrer();
        }
    }

    public void tomarDano(float dano)
    {
        vidaAtual -= dano;
        Knockback();
    }

    void Knockback()
    {
        rb.AddForce(new Vector2(10 * -knockbackParaDireita, 10), ForceMode2D.Impulse);
        StartCoroutine("Piscar");
    }

    IEnumerator Piscar()
    {
        for (float i = 0f; i < 0.5f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void morrer()
    {
        animacao.SetBool("estaMorto", true);
        StartCoroutine("morreu");
    }

    IEnumerator morreu()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
