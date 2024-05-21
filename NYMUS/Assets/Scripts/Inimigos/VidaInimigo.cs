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
    [SerializeField] public static bool invulneravel;

    [Header("GameObject do inimigo")]
    public GameObject inimigo;

    [Header("RigidBody")]
    public Rigidbody2D rb;   // rb = rigidbody

    [Header("Knockback")]
    public float forcaKnockbackX;
    public float forcaKnockbackY;
    public static int direcaoDoKnockback; // 1 eh pra direita e -1 pra esquerda

    [Header("Animator")]
    public Animator animacao;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite;

    [Header("jogador")]
    public Transform jogador;

    // Start is called before the first frame update
    void Start()
    {
        invulneravel = false;
        vidaAtual = vidaMaxima; // define a vida atual como a vida maxima
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void tomarDano(float dano)
    {
        //Debug.Log("Tomei dano" + dano);
        vidaAtual -= dano;
        StartCoroutine("Piscar");
        if (vidaAtual <= 0)
        {
            Knockback();
            StartCoroutine("morreu");
            StartCoroutine("Invulnerabilidade");
        }
        else
        {
            Knockback();
            StartCoroutine("Invulnerabilidade");
        }   
    }

    void Knockback()
    {
        if (transform.position.x <= jogador.transform.position.x)
        {
            direcaoDoKnockback = 1;
        }
        else
        {
            direcaoDoKnockback = -1;
        }
        rb.AddForce(new Vector2(forcaKnockbackX * -direcaoDoKnockback, forcaKnockbackY), ForceMode2D.Impulse);
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

    IEnumerator Invulnerabilidade()
    {
        invulneravel = true;
        yield return new WaitForSeconds(1f);
        invulneravel = false;
    }

    IEnumerator morreu()
    {
        yield return new WaitForSeconds(1f);
        Destroy(inimigo);
    }
}
