using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaInimigo : MonoBehaviour
{
    public float vidaMaxima;
    public float vidaAtual;
    public GameObject gameObject;

    [Header("Knockback")]
    public Rigidbody2D rb;   // rb = rigidbody
    public float forcaKnockbak;
    public static int knockbackParaDireita;
    public Animator animacao;
    [SerializeField] private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima;
        animacao.SetBool("estaMorto", false);
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
