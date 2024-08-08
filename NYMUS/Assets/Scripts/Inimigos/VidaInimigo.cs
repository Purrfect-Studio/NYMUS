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
    [SerializeField] public static bool podeMover;

    //[Header("RigidBody")]
    //public Rigidbody2D rb;   // rb = rigidbody

    [Header("Knockback")]
    private KnockBack knockBack;
    public float forcaKnockbackX = 1f;
    public float forcaKnockbackY = 1f;
    public static int direcaoDoKnockback; // 1 eh pra direita e -1 pra esquerda

    [Header("Animator")]
    public Animator animacao;

    [Header("jogador")]
    public GameObject jogador;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite;

    [Header("Tipo de Inimigo")]
    public bool slime;
    public bool morcego;
    public bool fantasma;

    

    // Start is called before the first frame update
    void Start()
    {
        if(slime)
        {
            vidaMaxima = SeletorDeDificuldade.vidaSlime;
        }else if(morcego)
        {
            vidaMaxima = SeletorDeDificuldade.vidaMorcego;
        }else if(fantasma)
        {
            vidaMaxima = SeletorDeDificuldade.vidaFantasma;
        }

        podeMover = true;
        invulneravel = false;
        vidaAtual = vidaMaxima; // define a vida atual como a vida maxima

        jogador = GameObject.FindGameObjectWithTag("Jogador");
        sprite = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();

        if(animacao != null)
        {
            animacao = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void tomarDano(float dano)
    {
        Debug.Log("Tomei dano" + dano);
        vidaAtual -= dano;
        StartCoroutine("Piscar");
        if (vidaAtual <= 0)
        {
            StartCoroutine("morreu");
            knockBack.PlayKnockback();
        }
        else
        {
            //Knockback();
            knockBack.PlayKnockback();
            StartCoroutine("Invulnerabilidade");
        }   
    }

    /*void Knockback()
    {
        if (transform.position.x <= jogador.transform.position.x)
        {
            direcaoDoKnockback = -1; // inimigo a esquerda do jogador
        }
        else
        {
            direcaoDoKnockback = 1; // inimigo a direita do jogador
        }
        rb.AddForce(new Vector2(forcaKnockbackX * direcaoDoKnockback, forcaKnockbackY), ForceMode2D.Impulse);
    }*/

    IEnumerator Piscar()
    {
        for (float i = 0f; i < 0.2f; i += 0.1f)
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
        yield return new WaitForSeconds(0.4f);
        invulneravel = false;
    }

    IEnumerator morreu()
    {
        podeMover = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
