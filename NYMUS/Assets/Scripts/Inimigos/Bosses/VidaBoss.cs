using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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

    [Header("Sprite")]
    private Color corOriginal;
    private SpriteRenderer spriteRenderer;
    public bool frenesi = false;

    [Header("Evento")]
    public UnityEvent evento;

    private Rigidbody2D rigidbody2d;

    public float tempoParaLevantar;

    [Header("Nome Boss")]
    public bool Virut;
    public bool Trojan;


    // Start is called before the first frame update
    void Start()
    {
        vidaMaxima = SeletorDeDificuldade.vidaMaximaVirut;
        vidaAtual = vidaMaxima; // define a vida atual como a vida maxima
        animacao = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;
        barraDeVidaBoss.definirVidaMaxima(vidaMaxima);
        frenesi = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Virut)
        {
            updateVirut();
        }
    }

    void updateVirut()
    {
        if (invulnerabilidade == true)
        {
            invulneravel = true;
        }

        if (vidaAtual <= vidaMaxima / 2 && frenesi == false)
        {
            StartCoroutine(frenesiAtivado());
            frenesi = true;
        }
    }

    IEnumerator frenesiAtivado()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(FrenesiAtivado());
    }

    IEnumerator FrenesiAtivado()
    {
        spriteRenderer.color = corOriginal;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(frenesiAtivado());
    }

    public void derrubarBoss()
    {
        animacao.SetTrigger("Desativar");
        invulnerabilidade = false;
        invulneravel = false;
        rigidbody2d.gravityScale = 8f;
        MovimentacaoBoss.podeMover = false;
        BossControlador.podeExecutarAcoes = false;
        MovimentacaoBoss.seguindoJogador = false;
        StartCoroutine("LevantarBoss");
    }

    IEnumerator LevantarBoss()
    {
        yield return new WaitForSeconds(tempoParaLevantar);
        animacao.SetTrigger("Ativar");
        invulnerabilidade = true;
        rigidbody2d.gravityScale = 0f;
        BossControlador.podeExecutarAcoes = true;
        MovimentacaoBoss.podeMover = true;
        MovimentacaoBoss.seguindoJogador = true;
    }

    public void tomarDano(float dano)
    {
        Debug.Log("Boss Tomei dano" + dano);
        animacao.SetTrigger("TomarDano");
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
            //sprite.enabled = false;
            yield return new WaitForSeconds(0.2f);
            //sprite.enabled = true;
            //yield return new WaitForSeconds(0.1f);
        }
        invulneravel = false;
    }

    IEnumerator morreu()
    {
        animacao.SetTrigger("Morrer");
        MovimentacaoBoss.podeMover = false;
        BossControlador.podeExecutarAcoes = false;
        MovimentacaoBoss.seguindoJogador = false;
        yield return new WaitForSeconds(1f);
        evento.Invoke();
        Destroy(inimigo);  
    }
}
