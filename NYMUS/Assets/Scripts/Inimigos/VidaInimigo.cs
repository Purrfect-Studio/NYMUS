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
    public float forcaKnockbak;
    public static int knockbackParaDireita;

    [Header("Animator")]
    public Animator animacao;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite;

    [Header("jogador")]
    public Transform jogador;

    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima; // define a vida atual como a vida maxima
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void tomarDano(float dano)
    {
        if(invulneravel == false)
        {
            Debug.Log("Tomei dano" + dano);
            vidaAtual -= dano;
            if (vidaAtual <= 0)
            {
                morrer();
                StartCoroutine("Invulnerabilidade");
            }
            else
            {
                Knockback();
                StartCoroutine("Invulnerabilidade");
            } 
        }   
    }

    void Knockback()
    {
        if (transform.position.x <= jogador.transform.position.x)
        {
            knockbackParaDireita = 1;
        }
        else
        {
            knockbackParaDireita = -1;
        }
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

    IEnumerator Invulnerabilidade()
    {
        invulneravel = true;
        yield return new WaitForSeconds(1f);
        invulneravel = false;
    }

    void morrer()
    {
        animacao.SetBool("estaMorto", true);
        StartCoroutine("morreu");
    }

    IEnumerator morreu()
    {
        yield return new WaitForSeconds(1f);
        Destroy(inimigo);
    }
}
