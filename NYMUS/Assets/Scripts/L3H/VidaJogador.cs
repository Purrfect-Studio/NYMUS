using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima;
    [SerializeField] private float vidaAtual;

    [Header("Invulnerabilidade")]
    public static bool invulneravel;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Knockback")]
    public Rigidbody2D rb;   // rb = rigidbody
    public float forcaKnockbak;
    public static int knockbackParaDireita;
    public Animator animacao;


    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima; //Coloca o L3h na vida maxima quando comeca a fase
        invulneravel = false;   
    }

    void Update()
    {
        if (vidaAtual <= 0)
        {
            morrer();
        }
    }

    void morrer()
    {
        PlayerControlador.podeMover = false;
        animacao.SetBool("estaMorto", true);
        StartCoroutine("morreu");
    }

    IEnumerator morreu()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Lobby");
    }

    public void curar(float cura)
    {
        if (vidaAtual < vidaMaxima)
        {
            vidaAtual += cura;
            if(vidaAtual > vidaMaxima)
            {
                vidaAtual = vidaMaxima;
            }
        }
    }

    public void tomarDano(float danoTomado)
    {
        vidaAtual -= danoTomado;
        invulneravel = true;
        Knockback();
        StartCoroutine("invulnerabilidade");
    }


    void Knockback()
    {
        rb.AddForce(new Vector2(10 * -knockbackParaDireita, 10), ForceMode2D.Impulse);
        StartCoroutine("Parar");
    }

    IEnumerator Parar()
    {
        // Retira o controle do personagem
        PlayerControlador.podeMover = false;

        // comeca a animacao de tomar dano
        animacao.SetBool("tomarDano", true);

        yield return new WaitForSeconds(.5f);

        // Devolve o controle do personagem
        PlayerControlador.podeMover = true;

        // termina a animacao de tomar dano
        animacao.SetBool("tomarDano", false);
    }

    IEnumerator invulnerabilidade()
    {
        //animacao de piscar
        for(float i = 0f; i < 1f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invulneravel = false;
    }
}
