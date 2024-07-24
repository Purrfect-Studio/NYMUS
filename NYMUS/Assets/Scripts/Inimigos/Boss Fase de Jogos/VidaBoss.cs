using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaBoss : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima;
    public float vidaAtual;
    public BarraDeVidaBoss barraDeVidaBoss;
    public static bool invulneravel = false;
    public static bool invulnerabilidade = true;

    [Header("GameObject do inimigo")]
    public GameObject inimigo;

    [Header("Animator")]
    private Animator animacao;

    [Header("jogador")]
    private GameObject jogador;

    [Header("Sprite")]
    private SpriteRenderer sprite;

    private Rigidbody2D rigidbody2d;

    private BossControlador bossControlador;

    public float tempoParaLevantar;


    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima; // define a vida atual como a vida maxima

        animacao = GetComponent<Animator>();
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        rigidbody2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        barraDeVidaBoss.definirVidaMaxima(vidaMaxima);
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerabilidade == true)
        {
            invulneravel = true;
        }
    }

    public void derrubarBoss()
    {
        invulnerabilidade = false;
        invulneravel = false;
        rigidbody2d.gravityScale = 10f;
        BossControlador.podeExecutarAcoes = false;
        MovimentacaoBoss.podeMover = false;
        MovimentacaoBoss.seguindoJogador = false;
        StartCoroutine("LevantarBoss");
    }

    IEnumerator LevantarBoss()
    {
        yield return new WaitForSeconds(tempoParaLevantar);
        invulnerabilidade = true;
        rigidbody2d.gravityScale = 0f;
        BossControlador.podeExecutarAcoes = true;
        MovimentacaoBoss.podeMover = true;
        MovimentacaoBoss.seguindoJogador = true;
    }

    public void tomarDano(float dano)
    {
        Debug.Log("Boss Tomei dano" + dano);
        vidaAtual -= dano;
        barraDeVidaBoss.ajustarBarraDeVida(vidaAtual);
        StartCoroutine("Piscar");
        if (vidaAtual <= 0)
        {
            StartCoroutine("morreu");
        }
    }

    IEnumerator Piscar()
    {
        invulneravel = true;
        for (float i = 0f; i < 0.3f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invulneravel = false;
    }

    IEnumerator morreu()
    {
        MovimentacaoBoss.podeMover = false;
        yield return new WaitForSeconds(1f);
        Destroy(inimigo);
    }
}
